using AutoMapper;
using Involys.Poc.Api.Common.Security.Extensions;
using Involys.Poc.Api.Controllers.ExpressionTerm.Model;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Utils.Logger;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.ExpressionTerms
{
    public class ExpressionTermService : IExpressionTermService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IAppLogger<ExpressionTermDataModel> _logger;

        public ExpressionTermService(DatabaseContext context, IMapper mapper, IAppLogger<ExpressionTermDataModel> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ExpressionTermResponse> CreateExpressionTerm(CreateExpressionTermQuery query)
        {
            if (await FindExpressionTermById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }

            var expressionTerm = _mapper.Map<ExpressionTermDataModel>(query);

            if (query.DataSourceTable != null)
            {
                var dataSourceTable = await _context.DataSourceTables.FirstOrDefaultAsync(table => table.Id == query.DataSourceTable.Id);
                expressionTerm.DataSourceTable = dataSourceTable;

            }

            if (query.FirstTerm != null)
            {
                var firstTerm = await _context.ExpressionTerms.FirstOrDefaultAsync(firstTerm => firstTerm.Id == query.FirstTerm.Id);
                expressionTerm.FirstTerm = firstTerm;

            }

            if (query.SecondTerm != null)
            {
                var secondTerm = await _context.ExpressionTerms.FirstOrDefaultAsync(secondTerm => secondTerm.Id == query.SecondTerm.Id);
                expressionTerm.SecondTerm = secondTerm;

            }

            if (query.Operator != null)
            {
                var operators = await _context.Operators.FirstOrDefaultAsync(operators => operators.Id == query.Operator.Id);
                expressionTerm.Operator = operators;

            }

            if (query.FieldsDataSource != null)
            {
                var fieldData = await _context.DataSourceFields.FirstOrDefaultAsync(fieldData => fieldData.Id == query.FieldsDataSource.Id);
                expressionTerm.FieldsDataSource = fieldData;

            }

            if (query.CalculatedFieldResulting != null)
            {
                var calculatedField = await _context.DataSourceFields.FirstOrDefaultAsync(calculatedField => calculatedField.Id == query.CalculatedFieldResulting.Id);
                expressionTerm.CalculatedFieldResulting = calculatedField;

            }

            if (query.TableField != null)
            {
                var tableField = await _context.TableFields.FirstOrDefaultAsync(tableField => tableField.Id == query.TableField.Id);
                expressionTerm.TableField = tableField;

            }



            _context.ExpressionTerms.Add(expressionTerm);

            await _context.SaveChangesAsync();

            return _mapper.Map<ExpressionTermResponse>(expressionTerm);
        }

        public Task<OperatorResponse> CreateOperator(CreateOperatorQuery query)
        {
            throw new NotImplementedException();
        }

        public async Task<ExpressionTermResponse> DeleteExpressionTerm(int id)
        {
            var expressionTerm = await _context.ExpressionTerms.Where(a => a.Id == id)
                .Include(e => e.FirstTerm)
                .ThenInclude(f => f.TableField)

                .Include(t => t.SecondTerm)
                .Include(t => t.DataSourceTable)
                .FirstOrDefaultAsync();
            if (expressionTerm == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.ExpressionTerms.Remove(expressionTerm);
            if (expressionTerm.FirstTerm != null)
            {
                _context.ExpressionTerms.Remove(expressionTerm.FirstTerm);
            }
            if (expressionTerm.SecondTerm != null)
            {
                _context.ExpressionTerms.Remove(expressionTerm.SecondTerm);
            }




            await _context.SaveChangesAsync();

            return _mapper.Map<ExpressionTermResponse>(expressionTerm);
        }

        public bool ExpressionTermExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ExpressionTermDataModel> FindExpressionTermById(int id)
        {
            return await _context.ExpressionTerms.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<OperatorResponse> FindOperatorById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExpressionTermResponse>> GetAllExpressionTerms()
        {
            _logger.LogInformation("Get All ExpressionTerms service  call!");
            var expressionTerms = await _context.ExpressionTerms.ToListAsync();

            return _mapper.Map<List<ExpressionTermResponse>>(expressionTerms).HideSensitiveProperties();
        }

        public async Task<IEnumerable<OperatorResponse>> GetAllOperators()
        {
            _logger.LogInformation("Get All Operator service  call!");
            var operators = await _context.Operators.ToListAsync();

            return _mapper.Map<List<OperatorResponse>>(operators).HideSensitiveProperties();
        }

        public async Task<ExpressionTermResponse> GetExpressionTerm(int idExpressionTerm)
        {

            var exp =
             await _context.ExpressionTerms.Where(e => e.Id == idExpressionTerm)
                .Include(c => c.FirstTerm)
                .Include(d => d.SecondTerm)
                .Include(w => w.TableField).ThenInclude(t => t.Table)
                .Include(h => h.Operator)

                .Include(h => h.FieldsDataSource).FirstAsync();

            return _mapper.Map<ExpressionTermResponse>(exp);
        }

        public async Task<IEnumerable<ExpressionTermResponse>> GetExpressionTermHavingWhere(int idDataSourcetable)
        {
            _logger.LogInformation("Get All orderBy service  call!");
            DataSourceTableDataModel dataSourceTable = new DataSourceTableDataModel();
            dataSourceTable.Id = idDataSourcetable;
            var expressionTerms = await _context.ExpressionTerms.Where(e => e.DataSourceTable == dataSourceTable)
                .Include(t => t.TableField).ThenInclude(t => t.Table)

                .Include(o => o.Operator)
                .Include(first => first.FirstTerm).ThenInclude(t => t.TableField).ThenInclude(t => t.Table)
                .Include(first => first.FirstTerm).ThenInclude(t => t.FieldsDataSource).ThenInclude(t => t.TableField).ThenInclude(t => t.Table)
                .Include(first => first.FirstTerm).ThenInclude(t => t.FieldsDataSource).ThenInclude(t => t.Function)
                .Include(second => second.SecondTerm).ThenInclude(t => t.TableField).ThenInclude(t => t.Table).OrderBy(e => e.Order)
                .ToListAsync();



            return _mapper.Map<List<ExpressionTermResponse>>(expressionTerms);

        }

        public async Task<ExpressionTermResponse> GetExpressionTermJoin(int idDataSourcetable)
        {
            _logger.LogInformation("Get All orderBy service  call!");
            DataSourceTableDataModel dataSourceTable = new DataSourceTableDataModel();
            dataSourceTable.Id = idDataSourcetable;
            var expressionTerms = await _context.ExpressionTerms.Where(e => e.DataSourceTable == dataSourceTable && e.ExpressionType == "Join")
                .Include(t => t.TableField).ThenInclude(t => t.Table)
                .Include(o => o.Operator)
                .Include(first => first.FirstTerm).ThenInclude(t => t.TableField).ThenInclude(t => t.Table)
                .Include(second => second.SecondTerm).ThenInclude(t => t.TableField).ThenInclude(t => t.Table)
                .FirstOrDefaultAsync();



            return _mapper.Map<ExpressionTermResponse>(expressionTerms);
        }

        public async Task UpdateExpressionTerm(int id, CreateExpressionTermQuery query)
        {
            var expressionTerm = _context.ExpressionTerms
                .Include(c => c.FieldsDataSource)
                .Include(f => f.TableField)
                .Include(t => t.DataSourceTable)
                .Include(o => o.Operator)
                .FirstOrDefault(e => e.Id == id);

            if (query.FieldsDataSource != null)
            {
                var fieldDataSource = await _context.DataSourceFields.
                    FirstOrDefaultAsync(field => field.Id == query.FieldsDataSource.Id);
                expressionTerm.FieldsDataSource = fieldDataSource;

            }

            if (query.Operator != null)
            {
                var operator1 = await _context.Operators.
                    FirstOrDefaultAsync(op => op.Id == query.Operator.Id);
                expressionTerm.Operator = operator1;

            }
            if (query.DataSourceTable != null)
            {
                var dataSourceTable = await _context.DataSourceTables.
                    FirstOrDefaultAsync(table => table.Id == query.DataSourceTable.Id);
                expressionTerm.DataSourceTable = dataSourceTable;

            }
            if (query.TableField != null)
            {
                var tableField = await _context.TableFields.
                    FirstOrDefaultAsync(table => table.Id == query.TableField.Id);
                expressionTerm.TableField = tableField;

            }

            if (expressionTerm == null)
            {
                throw new CommandeNotFoundException();
            }

            var otherExpressionTerm = await FindExpressionTermById(query.Id);

            if (otherExpressionTerm != null && otherExpressionTerm.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            expressionTerm.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, expressionTerm);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpressionTermExists(id))
                {
                    throw new CommandeNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
