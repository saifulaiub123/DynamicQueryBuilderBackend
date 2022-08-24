using Involys.Poc.Api.Controllers.ExpressionTerm.Model;
using Involys.Poc.Api.Services;
using Involys.Poc.Api.Services.ExpressionTerms;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.ExpressionTerm
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExpressionTermsController : ControllerBase
    {
        private readonly IExpressionTermService _expressionTermService;

        public ExpressionTermsController(IExpressionTermService expressionTermService)
        {
            _expressionTermService = expressionTermService ?? throw new ArgumentNullException(nameof(expressionTermService));
        }

        // GET: api/<ExpressionTermsController>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<ExpressionTermResponse>>> GetAllExpressionTerms()
        {
            return Ok(await _expressionTermService.GetAllExpressionTerms());
        }



        // GET: api/<ExpressionTermsController>
        [HttpGet("JoinC/{id}")]
        public virtual async Task<ActionResult<ExpressionTermResponse>> GetExpressionTermJoi(int id)
        {
            return Ok(await _expressionTermService.GetExpressionTermJoin(id));
        }

        // GET: api/<ExpressionTermsController>
        [HttpGet("WhereHaving/{idDataSourceTable}")]
        public virtual async Task<ActionResult<IEnumerable<ExpressionTermResponse>>> GetExpressionTermWhereHaving(int idDataSourceTable)
        {
            return Ok(await _expressionTermService.GetExpressionTermHavingWhere(idDataSourceTable));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpressionTermResponse>> DeleteExpressionTerm(int id)
        {
            try
            {
                return await _expressionTermService.DeleteExpressionTerm(id);
            }
            catch (CommandeNotFoundException)
            {
                return NotFound();
            }
        }

        // GET api/<ExpressionTermsController>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<ExpressionTermResponse>> GetExpressionTerm(int id)
        {
            var expressionTerm = await _expressionTermService.GetExpressionTerm(id);
            if (expressionTerm == null)
            {
                return NotFound();
            }
            return expressionTerm;
        }

        // PUT api/<ExpressionTermsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpressionTerm(int id, [FromBody] CreateExpressionTermQuery query)
        {
            try
            {
                await _expressionTermService.UpdateExpressionTerm(id, query);
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

        // POST api/<ExpressionTermsController>
        [HttpPost]
        public async Task<ActionResult<ExpressionTermResponse>> PostExpressionTerm(CreateExpressionTermQuery query)
        {
            try
            {
                var expressionTerm = await _expressionTermService.CreateExpressionTerm(query);
                return CreatedAtAction(nameof(GetExpressionTerm), new { id = expressionTerm.Id }, expressionTerm);
            }
            catch (DuplicateCommandeException)
            {
                ModelState.AddModelError(nameof(query.Id), Constants.ValueMustBeUnique);
                return ValidationProblem();
            }
        }

        // GET: api/<ExpressionTermsController>
        [HttpGet("Operator")]
        public virtual async Task<ActionResult<IEnumerable<OperatorResponse>>> GetAllOperators()
        {
            return Ok(await _expressionTermService.GetAllOperators());
        }

    }
}
