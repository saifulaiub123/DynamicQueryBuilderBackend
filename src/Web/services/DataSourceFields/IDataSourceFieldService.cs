using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSourceFields
{
    public interface IDataSourceFieldService
    {

        Task<IEnumerable<DataSourceFieldResponse>> GetAllDataSourceFields();
        Task<DataSourceFieldResponse> GetDataSourceField(int id);
        Task<DataSourceFieldResponse> CreateDataSourceField(CreateDataSourceFieldQuery query);
        Task<DataSourceFieldResponse> DeleteDataSourceField(int id);
        Task<DataSourceFieldDataModel> FindDataSourceFieldById(int id);
        bool DataSourceFieldExists(int id);
        Task<IEnumerable<FunctionResponse>> GetAllFunctions();
        Task<FunctionResponse> GetFunction(int id);
        Task<FunctionResponse> CreateFunction(CreateFunctionQuery query);
        Task<FunctionDataModel> FindFunctionById(int id);
        Task UpdateDataSourceField(int id, CreateDataSourceFieldQuery query);
    }
}
