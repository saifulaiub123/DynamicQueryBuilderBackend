using Involys.Poc.Api.Controllers.ExpressionTerm.Model;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.ExpressionTerms
{
    public interface IExpressionTermService
    {
        Task<IEnumerable<ExpressionTermResponse>> GetAllExpressionTerms();
        Task<ExpressionTermResponse> GetExpressionTerm(int idExpressionTerm);
        Task<ExpressionTermResponse> CreateExpressionTerm(CreateExpressionTermQuery query);
        Task<ExpressionTermResponse> DeleteExpressionTerm(int id);
        Task<ExpressionTermDataModel> FindExpressionTermById(int id);
        bool ExpressionTermExists(int id);
        //operator
        Task<OperatorResponse> FindOperatorById(int id);
        Task<IEnumerable<OperatorResponse>> GetAllOperators();
        Task<OperatorResponse> CreateOperator(CreateOperatorQuery query);
        Task<ExpressionTermResponse> GetExpressionTermJoin(int idDataSourcetable);
        Task<IEnumerable<ExpressionTermResponse>> GetExpressionTermHavingWhere(int idDataSourcetable);
        Task UpdateExpressionTerm(int id, CreateExpressionTermQuery query);

    }
}
