using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Model;
using Involys.Poc.Api.Controllers.DynamicLinq.Model;
using Involys.Poc.Api.services.DynamicLinq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Involys.Poc.Api.Controllers.DynamicLinq
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamicLinqController : ControllerBase
    {
        private readonly IDynamicLinqService _dynamicLinqService;

        public DynamicLinqController(IDynamicLinqService dynamicLinqService)
        {
            _dynamicLinqService = dynamicLinqService;
        }

        [HttpPost("GetData")]
        public virtual async Task<ActionResult<DynamicQueryResponse>> GetData(DynamicLinqModel model)
        {
            var result =  await _dynamicLinqService.GetQuery(model);
            return Ok(new DynamicQueryResponse() { data = result});
        }
    }
}
