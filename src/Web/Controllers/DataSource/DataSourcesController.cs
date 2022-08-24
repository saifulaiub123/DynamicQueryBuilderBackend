using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Services;
using Involys.Poc.Api.Services.DataSources;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSource
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataSourcesController : ControllerBase
    {
        private readonly IDataSourceService _dataSourceService;


        public DataSourcesController(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService ?? throw new ArgumentNullException(nameof(dataSourceService));

        }

        // GET: api/<DataSourcesController>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<DataSourceResponse>>> GetAllDataSources()
        {
            return Ok(await _dataSourceService.GetAllDataSources());
        }

        // GET: api/<DataSourcesController>
        [HttpGet("OrderBys/{id}")]
        public virtual async Task<ActionResult<IEnumerable<OrderByResponse>>> GetOrderBys(int id)
        {
            return Ok(await _dataSourceService.GetOrderBys(id));
        }


        [HttpPost("DataRequest")]
        public virtual IEnumerable<IDictionary<string, object>> GetDataRequest([FromBody] string sql)
        {

            return _dataSourceService.GetDataRequest(sql);

        }

        // GET: api/<DataSourcesController>
        [HttpGet("Classifications")]
        public virtual async Task<ActionResult<IEnumerable<ClassificationResponse>>> GetAllClassifications()
        {
            return Ok(await _dataSourceService.GetAllClassifications());
        }
        // POST api/<DataSourcesController>
        [HttpPost]
        public async Task<ActionResult<DataSourceResponse>> PostDataSource(CreateDataSourceQuery query)
        {
                var dataSource = await _dataSourceService.CreateDataSource(query);
                return CreatedAtAction(nameof(GetDataSource), new { id = dataSource.Id }, dataSource); 
        }

        // POST api/<DataSourcesController>
        [HttpPost("OrderBy")]
        public async Task<ActionResult<OrderByResponse>> PostOrderBy(CreateOrderByQuery query)
        {
                var orderBy = await _dataSourceService.CreateOrderBy(query);
                return CreatedAtAction(nameof(GetOrderBy), new { id = orderBy.Id }, orderBy);
        }

        // GET api/<DataSourcesController>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<DataSourceResponse>> GetDataSource(int id)
        {
            var dataSource = await _dataSourceService.GetDataSource(id);
            if (dataSource == null)
            {
                return NotFound();
            }
            return dataSource;
        }
        // GET api/<DataSourcesController>/5
        [HttpGet("OrderBy/{id}")]
        public virtual async Task<ActionResult<OrderByResponse>> GetOrderBy(int id)
        {
            var orderBy = await _dataSourceService.GetOrderBy(id);
            if (orderBy == null)
            {
                return NotFound();
            }
            return orderBy;
        }

        // PUT api/<DataSourcesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSource(int id, [FromBody] CreateDataSourceQuery query)
        {
            try
            {
                await _dataSourceService.UpdateDataSource(id, query);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
            return NoContent();

        }
        // PUT api/<DataSourcesController>/5
        [HttpPut("OrderBy/{id}")]
        public async Task<IActionResult> PutOrderBy(int id, [FromBody] CreateOrderByQuery query)
        {
            try
            {
                await _dataSourceService.UpdateOrderBy(id, query);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
            return NoContent();

        }
        [HttpDelete("OrderBy/{id}")]
        public async Task<ActionResult<OrderByResponse>> DeleteOrderBy(int id)
        {
            try
            {
                return await _dataSourceService.DeleteOrderBy(id);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataSourceResponse>> DeleteDataSource(int id)
        {    
                return await _dataSourceService.DeleteDataSource(id);
        }



    }
}
