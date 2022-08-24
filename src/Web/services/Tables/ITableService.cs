using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.Table.Model;
using Involys.Poc.Api.Controllers.TableField.Model;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.Tables
{
    public interface ITableService
    {

        Task<IEnumerable<TableResponse>> GetAllTables();
        Task<IEnumerable<TableFieldResponse>> GetTableFields(int idTable);
        Task<IEnumerable<TableFieldResponse>> GetTablePrimaryKeyFields(int idTable);
        Task<IEnumerable<TableFieldResponse>> GetTableForeignKeyFields(int idTable);
        Task<TableResponse> GetTable(int id);
        Task<TableFieldResponse> GetTableField(int id);
        Task UpdateTable(int id, UpdateTableQuery query);
        Task<TableResponse> CreateTable(CreateTableQuery query);
        Task<TableResponse> DeleteTable(int id);
        Task<TableDataModel> FindTableById(int id);
        bool TableExists(int id);

    }
}
