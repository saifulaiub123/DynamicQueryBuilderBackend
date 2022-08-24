using Involys.Poc.Api.Controllers.Table.Model;
using Involys.Poc.Api.Controllers.TableField.Model;
using Involys.Poc.Api.Services;
using Involys.Poc.Api.Services.Tables;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.Table
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private  readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        }


        // GET: api/<TablesController>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TableResponse>>> GetAllTables()
        {
            return Ok(await _tableService.GetAllTables());
        }
        // GET: api/<TablesController>
        [HttpGet("TableField/{id}")]
        public virtual async Task<ActionResult<IEnumerable<TableFieldResponse>>> GetTableFields(int id)
        {
            return Ok(await _tableService.GetTableFields(id));
        }

        // GET: api/<TablesController>
        [HttpGet("TableField/Primary/{id}")]
        public virtual async Task<ActionResult<IEnumerable<TableFieldResponse>>> GetTablePrimaryFields(int id)
        {
            return Ok(await _tableService.GetTablePrimaryKeyFields(id));
        }

        // GET: api/<TablesController>
        [HttpGet("TableField/Foreign/{id}")]
        public virtual async Task<ActionResult<IEnumerable<TableFieldResponse>>> GetTableForeignFields(int id)
        {
            return Ok(await _tableService.GetTableForeignKeyFields(id));
        }


        // GET api/<TablesController>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TableResponse>> GetTable(int id)
        {
            var table = await _tableService.GetTable(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }
        // GET api/<TablesController>/5
        [HttpGet("TableF/{id}")]
        public virtual async Task<ActionResult<TableFieldResponse>> GetTableFiel(int id)
        {
            var tableF = await _tableService.GetTableField(id);
            if (tableF == null)
            {
                return NotFound();
            }
            return tableF;
        }

        // POST api/<TablesController>
        [HttpPost]
        public async Task<ActionResult<TableResponse>> PostTable(CreateTableQuery query)
        {
            try
            {
                var table = await _tableService.CreateTable(query);
                return CreatedAtAction(nameof(GetTable), new { id = table.Id }, table);
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
        }


        // PUT api/<TablesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, [FromBody] UpdateTableQuery query)
        {
            try
            {
                await _tableService.UpdateTable(id, query);
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

        // DELETE api/<TablesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TableResponse>> DeleteTable(int id)
        {
            try
            {
                return await _tableService.DeleteTable(id);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
        }
    
}

    
}
