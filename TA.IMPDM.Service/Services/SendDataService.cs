using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;
using TA.IMPDM.Service.Visitors;

namespace TA.IMPDM.Service.Services
{
    /// <summary>
    /// Обработка данных пакета
    /// </summary>
    public class SendDataService : ISendDataService
    {
        private readonly Func<CancellationToken, MapAndSendDataVisitor> sendPacketVisitorFactory;

        public SendDataService(Func<CancellationToken, MapAndSendDataVisitor> sendPacketVisitorFactory)
        {
            this.sendPacketVisitorFactory = sendPacketVisitorFactory ?? throw new ArgumentNullException(nameof(sendPacketVisitorFactory));
        }

        /// <summary>
        /// Функция отправляет данные в svcm
        /// </summary>
        /// <param name="visitable"></param>
        /// <returns>Результат отправки: true - успешно, false - ошибка</returns>
        public async Task<Result> SendPacketPartAsync(IVisitable visitable, CancellationToken token)
        {
            var visitor = sendPacketVisitorFactory(token);
            await visitable.Accept(visitor).ConfigureAwait(false);
            return visitor.Result;
        }

    }
}
