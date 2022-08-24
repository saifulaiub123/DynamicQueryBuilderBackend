using Involys.Poc.Api.Controllers.DataSourceTable;
using Involys.Poc.Api.Controllers.DataSourceTable.Model;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSourceTables
{
    public interface IDataSourceTableService
    {
      
        Task<IEnumerable<DataSourceTableResponse>> GetAllDataSourceTables();
        Task<DataSourceTableResponse> GetDataSourceTable(int id);
        Task<DataSourceTableResponse> CreateDataSourceTable(CreateDataSourceTableQuery query);
        Task<DataSourceTableResponse> DeleteDataSourceTable(int id);
        Task<DataSourceTableDataModel> FindDataSourceTableById(int id);
        Task<IEnumerable<DataSourceTableResponse>> GetDataSourceTables(int idDataSource);
        Task<DataSourceTableResponse> GetDataSourceMainTable(int idDataSource);
        bool DataSourceTableExists(int id);
        //Join
        Task<JoinResponse> FindJoinById(int id);
        Task<IEnumerable<JoinResponse>> GetAllJoin();
        Task UpdateDataSourceTable(int id, CreateDataSourceTableQuery query);
    }
}
