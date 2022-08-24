using Involys.Poc.Api.Controllers.DataSourceTable.Model;
using Involys.Poc.Api.Services;
using Involys.Poc.Api.Services.DataSourceTables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSourceTable
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataSourceTablesController : ControllerBase
    {
        private readonly IDataSourceTableService _dataSourceTableService;

        public DataSourceTablesController(IDataSourceTableService dataSourceTableService)
        {
            _dataSourceTableService = dataSourceTableService ?? throw new ArgumentNullException(nameof(dataSourceTableService));
        }

        // GET: api/<DataSourceTablesController>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<DataSourceTableResponse>>> GetAllDataSourceTables()
        {
            return Ok(await _dataSourceTableService.GetAllDataSourceTables());
        }
        // POST api/<DataSouceTablesController>
        [HttpPost]
        public async Task<ActionResult<DataSourceTableResponse>> PostDataSourceTable(CreateDataSourceTableQuery query)
        {
            try
            {
                var dataSourceField = await _dataSourceTableService.CreateDataSourceTable(query);
                return CreatedAtAction(nameof(GetDataSourceTable), new { id = dataSourceField.Id }, dataSourceField);
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
        }

        // GET api/<DataSourceTablesController>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<DataSourceTableResponse>> GetDataSourceTable(int id)
        {
            var dataSourceField = await _dataSourceTableService.GetDataSourceTable(id);
            if (dataSourceField == null)
            {
                return NotFound();
            }
            return dataSourceField;
        }

        // GET: api/<DataSourceTablesController>
        [HttpGet("DataSource/{id}")]
        public virtual async Task<ActionResult<IEnumerable<DataSourceTableResponse>>> GetDStables(int id)
        {
            return Ok(await _dataSourceTableService.GetDataSourceTables(id));
        }

        // GET: api/<DataSourceTablesController>
        [HttpGet("MainDataSourceTable/{id}")]
        public virtual async Task<ActionResult<DataSourceTableResponse>> GetMDStables(int id)
        {
            return Ok(await _dataSourceTableService.GetDataSourceMainTable(id));
        }

        // PUT api/<DataSourceTablesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSourceTable(int id, [FromBody] CreateDataSourceTableQuery query)
        {
            try
            {
                await _dataSourceTableService.UpdateDataSourceTable(id, query);
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
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataSourceTableResponse>> DeleteDataSourceTable(int id)
        {
            try
            {
                return await _dataSourceTableService.DeleteDataSourceTable(id);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/<ExpressionTermsController>
        [HttpGet("Join")]
        public virtual async Task<ActionResult<IEnumerable<JoinResponse>>> GetAllJoins()
        {
            return Ok(await _dataSourceTableService.GetAllJoin());
        }



    }
}
