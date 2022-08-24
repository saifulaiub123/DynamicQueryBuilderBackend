using AutoMapper;
using Involys.Poc.Api.Common.Security.Extensions;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Utils.Logger;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSourceFields
{
    public class DataSourceFieldService : IDataSourceFieldService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IAppLogger<DataSourceFieldDataModel> _logger;

        public DataSourceFieldService(DatabaseContext context, IMapper mapper, IAppLogger<DataSourceFieldDataModel> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DataSourceFieldResponse> CreateDataSourceField(CreateDataSourceFieldQuery query)
        {
            if (await FindDataSourceFieldById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }

            var dataSourceField = _mapper.Map<DataSourceFieldDataModel>(query);


            if (query.TableField != null)
            {
                var tableField = await _context.TableFields.FirstOrDefaultAsync(tableField => tableField.Id == query.TableField.Id);
                dataSourceField.TableField = tableField;
            }

            if (query.Function != null)
            {
                var function = await _context.Functions.FirstOrDefaultAsync(function => function.Id == query.Function.Id);
                dataSourceField.Function = function;

            }
            _context.DataSourceFields.Add(dataSourceField);

            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceFieldResponse>(dataSourceField);
        }

        public async Task<FunctionResponse> CreateFunction(CreateFunctionQuery query)
        {
            if (await FindFunctionById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }

            var function = _mapper.Map<FunctionDataModel>(query);

            _context.Functions.Add(function);

            await _context.SaveChangesAsync();

            return _mapper.Map<FunctionResponse>(function);
        }

        public bool DataSourceFieldExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSourceFieldResponse> DeleteDataSourceField(int id)
        {
            var dataSourceField = await _context.DataSourceFields.FindAsync(id);
            if (dataSourceField == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.DataSourceFields.Remove(dataSourceField);
            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceFieldResponse>(dataSourceField);
        }

        public async Task<DataSourceFieldDataModel> FindDataSourceFieldById(int id)
        {
            return await _context.DataSourceFields.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<FunctionDataModel> FindFunctionById(int id)
        {
            return await _context.Functions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<DataSourceFieldResponse>> GetAllDataSourceFields()
        {
            _logger.LogInformation("Get All dataSourceField service  call!");
            var dataSourceField = await _context.DataSourceFields.OrderBy(e=>e.Order).ToListAsync();

            return _mapper.Map<List<DataSourceFieldResponse>>(dataSourceField).HideSensitiveProperties();

        }

        public async Task<IEnumerable<FunctionResponse>> GetAllFunctions()
        {
            _logger.LogInformation("Get All function service call!");
            var functions = await _context.Functions.ToListAsync();

            return _mapper.Map<List<FunctionResponse>>(functions).HideSensitiveProperties();
        }

        public async Task<DataSourceFieldResponse> GetDataSourceField(int id)
        {
            var dataSourceField = await _context.DataSourceFields.FindAsync(id);
            if (dataSourceField == null)
            {
                return null;
            }
            return _mapper.Map<DataSourceFieldResponse>(dataSourceField);
        }

        public Task<IEnumerable<DataSourceFieldResponse>> GetDataSourceFields()
        {
            throw new NotImplementedException();
        }

        public async Task<FunctionResponse> GetFunction(int id)
        {
            var function = await _context.Functions.FindAsync(id);
            if (function == null)
            {
                return null;
            }
            return _mapper.Map<FunctionResponse>(function);
        }

        public async Task UpdateDataSourceField(int id, CreateDataSourceFieldQuery query)
        {
            var dataSourceField = _context.DataSourceFields.Include(c => c.TableField).FirstOrDefault(e => e.Id == id);

            if (query.TableField != null)
            {
                var table = await _context.TableFields.FirstOrDefaultAsync(table => table.Id == query.TableField.Id);
                dataSourceField.TableField = table;

            }
            if (query.Function != null)
            {
                var function = await _context.Functions.FirstOrDefaultAsync(function => function.Id
                == query.Function.Id);
                dataSourceField.Function = function;

            }
            if (dataSourceField == null)
            {
                throw new CommandeNotFoundException();
            }

            var otherDataSource = await FindDataSourceFieldById(query.Id);

            if (otherDataSource != null && otherDataSource.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            dataSourceField.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, dataSourceField);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSourceFieldExists(id))
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
