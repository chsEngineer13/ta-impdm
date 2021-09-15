using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TA.IMPDM.Service.DB;
using TA.IMPDM.Service.Services;

namespace TA.IMPDM.Service.Visitors
{
    /// <summary>
    /// Отправляет объект БД (предварительно подготовив) используя OData в svcm
    /// </summary>
    public class MapAndSendDataVisitor : IAsyncVisitor
    {
        private readonly IODataService oDataService;
        private readonly IMapperService mapperService;
        private readonly CancellationToken token;

        public Result Result { get; private set; }

        public MapAndSendDataVisitor(IODataService oDataService, IMapperService mapperService, CancellationToken token)
        {
            this.oDataService = oDataService ?? throw new ArgumentNullException(nameof(oDataService));
            this.mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
            this.token = token;
        }

        public async Task VisitAsync(Building building)
        {
            var dto = mapperService.Map(building);
            Result = await oDataService.SendAsync(dto, token);
        }

        public async Task VisitAsync(Constrpart constrpart)
        {
            var dto = mapperService.Map(constrpart);
            Result = await oDataService.SendAsync(dto, token);
        }

        public async Task VisitAsync(Construction construction)
        {
            var dto = mapperService.Map(construction);
            Result = await oDataService.SendAsync(dto, token);
        }

        public async Task VisitAsync(Contract contract)
        {
            var dto = mapperService.Map(contract);
            Result = await oDataService.SendAsync(dto, token);
        }

        public async Task VisitAsync(Docset docset)
        {
            var dto = mapperService.Map(docset);
            Result = await oDataService.SendAsync(dto, token);
        }

        public async Task VisitAsync(Document document)
        {
            var dto = mapperService.Map(document);
            Result = await oDataService.SendAsync(dto, token);
        }
    }
}
