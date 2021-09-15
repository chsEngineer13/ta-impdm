using System;
using System.Collections.Generic;
using System.Text;
using TA.IMPDM.Service.StreamObjects;

namespace TA.IMPDM.Service.Services
{
    public interface IMapperService
    {

        StreamConstruction Map(DB.Construction construction);

        StreamConstrPart Map(DB.Constrpart constrpart);

        StreamBuilding Map(DB.Building building);

        StreamContract Map(DB.Contract contract);

        StreamDocset Map(DB.Docset docset);

        StreamDocument Map(DB.Document document);
    }
}
