using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TA.IMPDM.Service.Visitors
{
    public interface IAsyncVisitor
    {
        Task VisitAsync(DB.Building building);
        Task VisitAsync(DB.Constrpart constrpart);
        Task VisitAsync(DB.Construction construction);
        Task VisitAsync(DB.Contract contract);
        Task VisitAsync(DB.Docset docset);
        Task VisitAsync(DB.Document document);
    }
}
