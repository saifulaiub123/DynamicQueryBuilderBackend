using AutoMapper;
using Involys.Poc.Api.Common.Security.Extensions;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Utils.Logger;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSources
{
    public class DataSourceService : IDataSourceService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IAppLogger<DataSourceDataModel> _logger;

        public DataSourceService(DatabaseContext context, IMapper mapper, IAppLogger<DataSourceDataModel> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DataSourceResponse> CreateDataSource(CreateDataSourceQuery query)
        {


            if (await FindDataSourceById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }
            var dataSource = _mapper.Map<DataSourceDataModel>(query);

            if (query.WhereCondition != null)
            {
                var whereCondition = await _context.ExpressionTerms.FirstOrDefaultAsync(whereCondition => whereCondition.Id == query.WhereCondition.Id);
                dataSource.WhereCondition = whereCondition;

            }

            if (query.HavingCondition != null)
            {
                var havingCondition = await _context.ExpressionTerms.FirstOrDefaultAsync(havingCondition => havingCondition.Id == query.HavingCondition.Id);
                dataSource.HavingCondition = havingCondition;
            }

            if (query.Classification != null)
            {
                var classification = await _context.Classifications.FirstOrDefaultAsync(classification => classification.Id == query.Classification.Id);
                dataSource.Classification = classification;

            }

            var dataSourceFieldResponse = new List<DataSourceFieldDataModel>();
            if (query.DataSourceFields != null)
            {



                var dataSourceFields = query.DataSourceFields.AsQueryable();



                if (query.DataSourceFields != null)

                {
                    foreach (var item in dataSourceFields)
                    {
                        var dataSourceField = _mapper.Map<DataSourceFieldDataModel>(item);

                        if (item.TableField != null)
                        {
                            var tableField = await _context.TableFields.FirstOrDefaultAsync(tableField => tableField.Id == item.TableField.Id);
                            dataSourceField.TableField = tableField;
                        }

                        if (item.Function != null)
                        {
                            var function = await _context.Functions.FirstOrDefaultAsync(function => function.Id == item.Function.Id);
                            dataSourceField.Function = function;

                        }

                        _context.DataSourceFields.Add(dataSourceField);
                        dataSourceFieldResponse.Add(dataSourceField);

                    }

                }
            }
            dataSource.DataSourceFields = dataSourceFieldResponse;

            _context.DataSources.Add(dataSource);

            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceResponse>(dataSource);
        }

        public async Task<OrderByResponse> CreateOrderBy(CreateOrderByQuery query)
        {
            if (await FindOrderByById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }

            var dataSource = await _context.DataSources.FirstOrDefaultAsync(dataSource => dataSource.Id == query.DataSource.Id);
            var tableField = await _context.TableFields.FirstOrDefaultAsync(tableField => tableField.Id == query.TableField.Id);

            var orderBy = _mapper.Map<OrderByDataModel>(query);

            orderBy.DataSource = dataSource;
            orderBy.TableField = tableField;

            _context.OrdersBy.Add(orderBy);

            await _context.SaveChangesAsync();

            return _mapper.Map<OrderByResponse>(orderBy);
        }

        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            throw new NotImplementedException();
        }

        public bool DataSourceExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSourceResponse> DeleteDataSource(int id)
        {
            var dataSource = await _context.DataSources.FindAsync(id);
            if (dataSource == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.DataSources.Remove(dataSource);
            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceResponse>(dataSource);
        }

        public async Task<OrderByResponse> DeleteOrderBy(int id)
        {
            var orderBy = await _context.OrdersBy.FindAsync(id);
            if (orderBy == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.OrdersBy.Remove(orderBy);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderByResponse>(orderBy);
        }

        public async Task<DataSourceDataModel> FindDataSourceById(int id)
        {
            return await _context.DataSources.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<OrderByDataModel> FindOrderByById(int id)
        {
            return await _context.OrdersBy.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<ClassificationResponse>> GetAllClassifications()
        {
            _logger.LogInformation("Get All classification service  call!");
            var classifications = await _context.Classifications.ToListAsync();

            return _mapper.Map<List<ClassificationResponse>>(classifications).HideSensitiveProperties();
        }

        public async Task<IEnumerable<DataSourceResponse>> GetAllDataSources()
        {
            _logger.LogInformation("Get All DataSource service  call!");
            var dataSources = await _context.DataSources
                .Include(c => c.Classification)
                .Include(d => d.DataSourceFields)
                .Include(w => w.WhereCondition)
                .Include(h => h.HavingCondition)
                .ToListAsync();
           

            return _mapper.Map<List<DataSourceResponse>>(dataSources).HideSensitiveProperties();

        }

        public IEnumerable<IDictionary<string, object>> GetDataRequest(string sqlCommand)
        {
            using var cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sqlCommand;
            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            using var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var row = new ExpandoObject() as IDictionary<string, object>;
                for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                {
                    row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                }
                yield return row;
            }
        }

        public async Task<DataSourceResponse> GetDataSource(int id)
        {

            var ds = await _context.DataSources
                .Include(c => c.Classification)
                .Include(d => d.DataSourceFields).ThenInclude(f => f.TableField).ThenInclude(t => t.Table)
                .Include(d => d.DataSourceFields).ThenInclude(f => f.Function)
                .Include(w => w.WhereCondition)
                .Include(h => h.HavingCondition)
                .FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<DataSourceResponse>(ds);

        }


        public async Task<OrderByResponse> GetOrderBy(int id)
        {
            var orderBy = await _context.OrdersBy.FindAsync(id);
            if (orderBy == null)
            {
                return null;
            }
            return _mapper.Map<OrderByResponse>(orderBy);
        }

        public async Task<IEnumerable<OrderByResponse>> GetOrderBys(int idDataSource)
        {
            _logger.LogInformation("Get All orderBy service  call!");
            DataSourceDataModel dataSource = new DataSourceDataModel();
            dataSource.Id = idDataSource;
            var orderBys = await _context.OrdersBy.Where(e => e.DataSource == dataSource)
                .Include(f => f.TableField).ThenInclude(t => t.Table).Include(d => d.DataSource).ToListAsync();



            return _mapper.Map<List<OrderByResponse>>(orderBys).HideSensitiveProperties();
        }

        public bool OrderByExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDataSource(int id, CreateDataSourceQuery query)
        {
            var dataSource = _context.DataSources
                .Include(c => c.Classification)
                .Include(f => f.DataSourceFields)
                .FirstOrDefault(e => e.Id == id);

            if (query.Classification != null)
            {
                var classification = await _context.Classifications.FirstOrDefaultAsync(classification => classification.Id == query.Classification.Id);
                dataSource.Classification = classification;

            }

            var dataSourceFieldResponse = new List<DataSourceFieldDataModel>();
            if (query.DataSourceFields != null)
            {



                var dataSourceFields = query.DataSourceFields.AsQueryable();



                if (query.DataSourceFields != null)

                {
                    foreach (var item in dataSourceFields)
                    {
                        var dataSourceField = _mapper.Map<DataSourceFieldDataModel>(item);

                        if (item.TableField != null)
                        {
                            var tableField = await _context.TableFields.FirstOrDefaultAsync(tableField => tableField.Id == item.TableField.Id);
                            dataSourceField.TableField = tableField;
                        }

                        if (item.Function != null)
                        {
                            var function = await _context.Functions.FirstOrDefaultAsync(function => function.Id == item.Function.Id);
                            dataSourceField.Function = function;

                        }

                        _context.DataSourceFields.Add(dataSourceField);
                        dataSourceFieldResponse.Add(dataSourceField);

                    }

                }
                dataSource.DataSourceFields = dataSourceFieldResponse;
            }

            if (dataSource == null)
            {
                throw new CommandeNotFoundException();
            }


            var otherDataSource = await FindDataSourceById(query.Id);

            if (otherDataSource != null && otherDataSource.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            dataSource.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, dataSource);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSourceExists(id))
                {
                    throw new CommandeNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task UpdateOrderBy(int id, CreateOrderByQuery query)
        {
            var orderBy = _context.OrdersBy.Include(c => c.TableField).FirstOrDefault(e => e.Id == id);

            if (query.TableField != null)
            {
                var tableField = await _context.TableFields.FirstOrDefaultAsync(tableF => tableF.Id == query.TableField.Id);
                orderBy.TableField = tableField;

            }
            if (orderBy == null)
            {
                throw new CommandeNotFoundException();
            }

            var otherOrderBy = await FindOrderByById(query.Id);

            if (otherOrderBy != null && otherOrderBy.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            orderBy.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, orderBy);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderByExists(id))
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
