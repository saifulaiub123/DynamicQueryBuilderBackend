using AutoMapper;
using Involys.Poc.Api.Common.Security.Extensions;
using Involys.Poc.Api.Controllers.DataSourceTable;
using Involys.Poc.Api.Controllers.DataSourceTable.Model;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Utils.Logger;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Services.DataSourceTables
{
    public class DataSourceTableService : IDataSourceTableService
    {
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IAppLogger<DataSourceTableDataModel> _logger;

        public DataSourceTableService(DatabaseContext context, IMapper mapper, IAppLogger<DataSourceTableDataModel> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<DataSourceTableResponse> CreateDataSourceTable(CreateDataSourceTableQuery query)
        {

            if (await FindDataSourceTableById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }


            var dataSourceTable = _mapper.Map<DataSourceTableDataModel>(query);

            if (query.Table != null)
            {
                var table = await _context.Tables.FirstOrDefaultAsync(table => table.Id == query.Table.Id);
                dataSourceTable.Table = table;
            }

            if (query.DataSource != null)
            {
                var dataSource = await _context.DataSources.FirstOrDefaultAsync(dataSource => dataSource.Id == query.DataSource.Id);
                dataSourceTable.DataSource = dataSource;

            }

            if (query.Join != null)
            {
                var join = await _context.Joins.FirstOrDefaultAsync(join => join.Id == query.Join.Id);
                dataSourceTable.Join = join;

            }
            _context.DataSourceTables.Add(dataSourceTable);

            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceTableResponse>(dataSourceTable);
        }

        public bool DataSourceTableExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSourceTableResponse> DeleteDataSourceTable(int id)
        {
            var dataSourceTable = await _context.DataSourceTables.FindAsync(id);
            if (dataSourceTable == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.DataSourceTables.Remove(dataSourceTable);
            await _context.SaveChangesAsync();

            return _mapper.Map<DataSourceTableResponse>(dataSourceTable);
        }

        public async Task<DataSourceTableDataModel> FindDataSourceTableById(int id)
        {
            return await _context.DataSourceTables.Include(c => c.DataSource).Include(t => t.Table).
                FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<JoinResponse> FindJoinById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DataSourceTableResponse>> GetAllDataSourceTables()
        {
            _logger.LogInformation("Get All dataSourceTable service  call!");
            var dataSourceTable = await _context.DataSourceTables.ToListAsync();

            return _mapper.Map<List<DataSourceTableResponse>>(dataSourceTable).HideSensitiveProperties();
        }

        public async Task<IEnumerable<JoinResponse>> GetAllJoin()
        {
            _logger.LogInformation("Get All join service  call!");
            var joins = await _context.Joins.ToListAsync();

            return _mapper.Map<List<JoinResponse>>(joins).HideSensitiveProperties();
        }

        public async Task<DataSourceTableResponse> GetDataSourceMainTable(int idDataSource)
        {
            _logger.LogInformation("Get main table  fields service  call!");
            DataSourceDataModel dataSource = new DataSourceDataModel();
            dataSource.Id = idDataSource;
            var dsTables = await _context.DataSourceTables.Where(e => e.DataSource == dataSource)
                .Where(e => e.MainEntity)
                .Include(t => t.Table).Include(j => j.Join).FirstOrDefaultAsync();



            return _mapper.Map<DataSourceTableResponse>(dsTables);
        }

        public async Task<DataSourceTableResponse> GetDataSourceTable(int id)
        {
            var DataSourceTable = await _context.DataSourceTables.FindAsync(id);
            if (DataSourceTable == null)
            {
                return null;
            }
            return _mapper.Map<DataSourceTableResponse>(DataSourceTable);
        }

        public async Task<IEnumerable<DataSourceTableResponse>> GetDataSourceTables(int idDataSource)
        {
            _logger.LogInformation("Get All table fields service  call!");
            DataSourceDataModel dataSource = new DataSourceDataModel();
            dataSource.Id = idDataSource;
            var dsTables = await _context.DataSourceTables.Where(e => e.DataSource == dataSource).OrderBy(e => e.Order)
                .Include(t => t.Table).Include(j => j.Join).ToListAsync();



            return _mapper.Map<List<DataSourceTableResponse>>(dsTables).HideSensitiveProperties();
        }

        public async Task UpdateDataSourceTable(int id, CreateDataSourceTableQuery query)
        {
            var dataSourceTable = _context.DataSourceTables.Include(c => c.Table).FirstOrDefault(e => e.Id == id);

            if (query.Table != null)
            {
                var table = await _context.Tables.FirstOrDefaultAsync(table => table.Id == query.Table.Id);
                dataSourceTable.Table = table;

            }
            if (dataSourceTable == null)
            {
                throw new CommandeNotFoundException();
            }

            var otherDataSource = await FindDataSourceTableById(query.Id);

            if (otherDataSource != null && otherDataSource.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            dataSourceTable.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, dataSourceTable);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataSourceTableExists(id))
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
