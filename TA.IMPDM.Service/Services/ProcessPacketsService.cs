using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.Services;

namespace TA.IMPDM.Service
{
    /// <summary>
    /// Сервис обработки пакетов
    /// </summary>
    class ProcessPacketsService
    {
        private readonly IDbService dbService;
        private readonly IODataService oDataService;
        private readonly ILogger<ProcessPacketsService> logger;
        private readonly ISendDataService sendPacketService;

        public ProcessPacketsService(IDbService dbService, 
            IODataService oDataService,
            ILogger<ProcessPacketsService> logger,
            ISendDataService sendPacketService)
        {
            this.dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            this.oDataService = oDataService ?? throw new ArgumentNullException(nameof(oDataService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.sendPacketService = sendPacketService ?? throw new ArgumentNullException(nameof(sendPacketService));
        }

        public async Task ProcessNewPacketsAsync(CancellationToken token)
        {
            logger.LogInformation("Получаю список пакетов готовых к отправке");
            var packets = await dbService
                .GetPacketsWithFinalStatusAsync(token)
                .ConfigureAwait(false);
            if (packets.Any())
            {
                foreach (var packet in packets)
                {
                    await ProcessPacketAsync(packet, token).ConfigureAwait(false);
                }
                logger.LogInformation("Все пакеты отправлены");
            }
            else
            {
                logger.LogInformation("Нет новых пакетов для отправки");
            }
        }

        private async Task ProcessPacketAsync(Packet packet, CancellationToken token)
        {
            using (logger.BeginScope($"Обработка пакета с Id = {packet.Id}"))
            {
                // получаем коллекцию данных пакета для отправки
                var asyncEnumerator = dbService.GetPacketPartsAsync(packet).GetEnumerator();

                try
                {
                    await dbService
                        .ChangePacketStatusAsync(packet.Id, PacketStatus.ProcessRead, "Отправка в SVCM", token)
                        .ConfigureAwait(false);
                    logger.LogInformation("Отправка в SVCM");

                    // обрабатываем коллекцию
                    bool canMoveNext = await asyncEnumerator.MoveNext().ConfigureAwait(false);
                    bool isEmpty = canMoveNext == false;
                    bool result = canMoveNext;
                    string errorMessage = "nop";
                    while (result && canMoveNext)
                    {
                        var part = asyncEnumerator.Current;
                        var opResult = await sendPacketService
                            .SendPacketPartAsync(part, token)
                            .ConfigureAwait(false);
                        result = opResult.Success;
                        errorMessage = opResult.ErrorMessage;
                        canMoveNext = await asyncEnumerator.MoveNext(token).ConfigureAwait(false);
                    }

                    // пакет (и все данные) успешно отправлены, меняем статус
                    if (result)
                    {
                        await dbService
                            .ChangePacketStatusAsync(packet.Id, PacketStatus.FinalRead, "Успешно отправлен в SVCM", token)
                            .ConfigureAwait(false);
                        logger.LogInformation("Успешно отправлен в SVCM");
                    }
                    else if (isEmpty)
                    {
                        await dbService
                            .ChangePacketStatusAsync(packet.Id, PacketStatus.FinalRead, "Нет связанных таблиц для отправки в SVCM", token)
                            .ConfigureAwait(false);
                        logger.LogInformation("Нет связанных таблиц для отправки в SVCM");
                    }
                    else
                    {
                        await dbService
                            .ChangePacketStatusAsync(packet.Id, PacketStatus.AbortRead, $"Ошибка при отправке в SVCM: {errorMessage}", token)
                            .ConfigureAwait(false);
                        logger.LogWarning($"Ошибка при отправке в SVCM: {errorMessage}");
                    }
                }
                catch (OperationCanceledException)
                {
                    await dbService
                        .ChangePacketStatusAsync(packet.Id, PacketStatus.AbortRead, "Операция была отменена")
                        .ConfigureAwait(false);
                    throw;
                }
            }
        }
    }
}
