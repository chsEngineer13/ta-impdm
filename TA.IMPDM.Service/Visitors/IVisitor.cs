using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service.Visitors
{
    public interface IVisitor
    {
        void Visit(DB.Building building);
        void Visit(DB.Constrpart constrpart);
        void Visit(DB.Construction construction);
        void Visit(DB.Contract contract);
        void Visit(DB.Docset docset);
        void Visit(DB.Document document);
    }
}
