using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.Table.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSourceTable.Model
{
    public class CreateDataSourceTableQuery : BaseEntity
    {
       
        public int Order { get; set; }
        public JoinResponse Join { get; set; }
        /// <summary>
        /// the principal table
        /// </summary>
        public CreateTableQuery Table { get; set; }
        public CreateDataSourceQuery DataSource { get; set; }
        public string Alias { get; set; }
        /// <summary>
        /// Id of the main table for example:
        /// Dep_Marche
        /// </summary>
        public bool MainEntity { get; set; }
    }
}
