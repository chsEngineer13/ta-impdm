using System.Threading.Tasks;
using TA.IMPDM.Service.DB.Interfaces;

namespace TA.IMPDM.Service.Services
{
    public interface ISendDataService
    {
        Task<Result> SendPacketPartAsync(IVisitable visitable, System.Threading.CancellationToken token);
    }
}