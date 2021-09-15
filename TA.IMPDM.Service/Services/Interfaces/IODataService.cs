using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TA.IMPDM.Service.StreamObjects;

namespace TA.IMPDM.Service.Services
{
    public interface IODataService
    {
        Task<Result> SendAsync<T>(T streamObject, System.Threading.CancellationToken token) where T : IStreamObject;
    }
}
