using Involys.Poc.Api.Controllers.Common;

namespace Involys.Poc.Api.Controllers.DynamicLinq.Model
{
    public class DynamicQueryResponse : BaseEntity
    {
        public dynamic data { get; set; }
    }
}
