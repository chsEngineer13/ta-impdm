using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TA.IMPDM.Service.OData
{
    public interface IODataClient
    {
        Task<ODataClientResult> InsertAsync(Uri address, string content, System.Threading.CancellationToken token);
    }
}
