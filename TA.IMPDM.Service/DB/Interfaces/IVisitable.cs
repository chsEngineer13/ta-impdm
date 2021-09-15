using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TA.IMPDM.Service.Visitors;

namespace TA.IMPDM.Service.DB.Interfaces
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
        Task Accept(IAsyncVisitor visitor);
    }
}
