using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSources
{
    public interface IDataSourceService
    {

        Task<IEnumerable<DataSourceResponse>> GetAllDataSources();
        Task<IEnumerable<ClassificationResponse>> GetAllClassifications();
        Task<DataSourceResponse> GetDataSource(int id);
        Task<DataSourceResponse> CreateDataSource(CreateDataSourceQuery query);
        Task<DataSourceResponse> DeleteDataSource(int id);
        Task<OrderByResponse> DeleteOrderBy(int id);
        Task<DataSourceDataModel> FindDataSourceById(int id);
        bool DataSourceExists(int id);
        bool OrderByExists(int id);
        Task<OrderByResponse> GetOrderBy(int id);
        Task<IEnumerable<OrderByResponse>> GetOrderBys(int idDataSource);
        Task<OrderByDataModel> FindOrderByById(int id);
        Task<OrderByResponse> CreateOrderBy(CreateOrderByQuery query);
        public List<T> DataReaderMapToList<T>(IDataReader dr);
        public IEnumerable<IDictionary<string, object>> GetDataRequest(string sqlCommand);
        Task UpdateDataSource(int id, CreateDataSourceQuery query);
        Task UpdateOrderBy(int id, CreateOrderByQuery query);

    }
}
