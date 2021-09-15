using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using TA.IMPDM.Service.StreamObjects;

namespace TA.IMPDM.Service.Services
{
    public class MapperService : IMapperService
    {
        private readonly IAdapter adapter;

        public MapperService(IAdapter adapter)
        {
            this.adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
        }

        public StreamConstruction Map(DB.Construction construction)
        {
            return adapter.Adapt<StreamConstruction>(construction);
        }

        public StreamConstrPart Map(DB.Constrpart constrpart)
        {
            return adapter.Adapt<StreamConstrPart>(constrpart);
        }

        public StreamBuilding Map(DB.Building building)
        {
            return adapter.Adapt<StreamBuilding>(building);
        }

        public StreamContract Map(DB.Contract contract)
        {
            return adapter.Adapt<StreamContract>(contract);
        }

        public StreamDocset Map(DB.Docset docset)
        {
            return adapter.Adapt<StreamDocset>(docset);
        }

        public StreamDocument Map(DB.Document document)
        {
            return adapter.Adapt<StreamDocument>(document);
        }
    }
}
