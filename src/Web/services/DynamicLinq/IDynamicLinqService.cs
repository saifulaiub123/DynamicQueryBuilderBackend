using System.Threading.Tasks;
using Common.Model;

namespace Involys.Poc.Api.services.DynamicLinq
{
    public interface IDynamicLinqService
    {
        Task<dynamic> GetQuery(DynamicLinqModel model);
    }
}
