using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Services;
using Involys.Poc.Api.Services.DataSourceFields;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSourceField
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DataSourceFieldsController : ControllerBase
    {
        private readonly IDataSourceFieldService _dataSourceFieldService;

        public DataSourceFieldsController(IDataSourceFieldService dataSourceFieldService)
        {
            _dataSourceFieldService = dataSourceFieldService ?? throw new ArgumentNullException(nameof(dataSourceFieldService));
        }

        // GET: api/<DataSouceFieldsController>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<DataSourceFieldResponse>>> GetAllDataSources()
        {
            return Ok(await _dataSourceFieldService.GetAllDataSourceFields());
        }

        // POST api/<DataSouceFieldsController>
        [HttpPost]
        public async Task<ActionResult<DataSourceFieldResponse>> PostDataSourceField(CreateDataSourceFieldQuery query)
        {
            try
            {
                var dataSourceField = await _dataSourceFieldService.CreateDataSourceField(query);
                return CreatedAtAction(nameof(GetDataSourceField), new { id = dataSourceField.Id }, dataSourceField);
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
        }

        // PUT api/<DataSouceFieldsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataSourceField(int id, [FromBody] CreateDataSourceFieldQuery query)
        {
            try
            {
                await _dataSourceFieldService.UpdateDataSourceField(id, query);
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
        // GET api/<DataSouceFieldsController>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<DataSourceFieldResponse>> GetDataSourceField(int id)
        {
            var dataSourceField = await _dataSourceFieldService.GetDataSourceField(id);
            if (dataSourceField == null)
            {
                return NotFound();
            }
            return dataSourceField;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataSourceFieldResponse>> DeleteDataSourceField(int id)
        {
            try
            {
                return await _dataSourceFieldService.DeleteDataSourceField(id);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpPost("Function")]
        public async Task<ActionResult<FunctionResponse>> PostFunction(CreateFunctionQuery query)
        {
            try
            {
                var function = await _dataSourceFieldService.CreateFunction(query);
                return CreatedAtAction(nameof(GetFunction), new { id = function.Id }, function);
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
        }

        // GET api/<TableDataController>/5
        [HttpGet("Function/{id}")]
        public virtual async Task<ActionResult<FunctionResponse>> GetFunction(int id)
        {
            var function = await _dataSourceFieldService.GetFunction(id);
            if (function == null)
            {
                return NotFound();
            }
            return function;
        }
        [HttpGet("Function")]
        public virtual async Task<ActionResult<IEnumerable<FunctionResponse>>> GetAllFunctions()
        {
            return Ok(await _dataSourceFieldService.GetAllFunctions());
        }

    }
}
