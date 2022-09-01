using AutoMapper;
using Involys.Poc.Api.Data;
using Involys.Poc.Api.Utils.Logger;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Involys.Poc.Api.Common.Security.Extensions;
using Involys.Poc.Api.Controllers.Table.Model;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.TableField.Model;

namespace Involys.Poc.Api.Services.Tables
{

    public class TableService : ITableService
    {

        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;
        private readonly IAppLogger<TableDataModel> _logger;

        public TableService(DatabaseContext context, IMapper mapper, IAppLogger<TableDataModel> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TableResponse> CreateTable(CreateTableQuery query)
        {
            if (await FindTableById(query.Id) != null)
            {
                throw new DuplicateCommandeException();
            }

            var table = _mapper.Map<TableDataModel>(query);

            _context.Tables.Add(table);

            await _context.SaveChangesAsync();

            return _mapper.Map<TableResponse>(table);
        }

        public async Task<TableResponse> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                throw new CommandeNotFoundException();
            }

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return _mapper.Map<TableResponse>(table);
        }


        public async Task<IEnumerable<TableResponse>> GetAllTables()
        {
            _logger.LogInformation("Get All tables service  call!");
            var tables = await _context.Tables.ToListAsync();

            return _mapper.Map<List<TableResponse>>(tables).HideSensitiveProperties();

        }

        public async Task<TableResponse> GetTable(int id)
        {

            var Table = await _context.Tables.FindAsync(id);
            if (Table == null)
            {
                return null;
            }
            return _mapper.Map<TableResponse>(Table);
        }



        public bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.Id == id);
        }

        public async Task UpdateTable(int id, UpdateTableQuery query)
        {
            var table = _context.Tables.FirstOrDefault(e => e.Id == id);
            if (table == null)
            {
                throw new CommandeNotFoundException();
            }

            var otherTable = await FindTableById(query.Id);
            if (otherTable != null && otherTable.Id != id)
            {
                throw new DuplicateCommandeException();
            }

            table.UnHideSensitivePropertiesForItem(query);//update query field to not copy *****
            _mapper.Map(query, table);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
                {
                    throw new CommandeNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }


        public async Task<TableDataModel> FindTableById(int id)
        {
            return await _context.Tables.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<TableFieldResponse>> GetTableFields(int idTable)
        {
            _logger.LogInformation("Get All table fields service  call!");
            TableDataModel table = new TableDataModel();
            table.Id = idTable;
            var tableFields = await _context.TableFields.Where(e => e.Table == table).Include(t => t.Table).ToListAsync();



            return _mapper.Map<List<TableFieldResponse>>(tableFields).HideSensitiveProperties();


        }
        public async Task<IEnumerable<TableFieldResponse>> GetTableFieldsByTableName(string name)
        {
            _logger.LogInformation("Get All table fields service  call!");
            TableDataModel table = new TableDataModel();
            List<TableFieldDataModel> tableFields = new List<TableFieldDataModel>();
            var tableObj = await _context.Tables.Where(x => x.Name == name).FirstOrDefaultAsync();
            if(tableObj != null)
            {
                table.Id = tableObj.Id;
                tableFields = await _context.TableFields.Where(e => e.Table == table).Include(t => t.Table).ToListAsync();
            }
            return _mapper.Map<List<TableFieldResponse>>(tableFields).HideSensitiveProperties();
        }

        public async Task<TableFieldResponse> GetTableField(int id)
        {
            var tbf = await _context.TableFields.Include(t => t.Table).FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<TableFieldResponse>(tbf);
        }

        public async Task<IEnumerable<TableFieldResponse>> GetTablePrimaryKeyFields(int idTable)
        {
            _logger.LogInformation("Get All table fields service  call!");
            TableDataModel table = new TableDataModel();
            table.Id = idTable;
            var tableFields = await _context.TableFields.Where(e => e.Table == table)
                .Where(e=>e.Designation=="P").Include(t => t.Table).ToListAsync();



            return _mapper.Map<List<TableFieldResponse>>(tableFields).HideSensitiveProperties();
        }

        public async Task<IEnumerable<TableFieldResponse>> GetTableForeignKeyFields(int idTable)
        {
            _logger.LogInformation("Get All table fields service  call!");
            TableDataModel table = new TableDataModel();
            table.Id = idTable;
            var tableFields = await _context.TableFields.Where(e => e.Table == table)
                .Where(e=>e.Designation=="F").Include(t => t.Table).ToListAsync();



            return _mapper.Map<List<TableFieldResponse>>(tableFields).HideSensitiveProperties();
        }
    }
}
