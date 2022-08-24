using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    /// dataSource_table
    /// </summary>
    [Table("data_source_dataSource_tables")]
    public class DataSourceTableDataModel : BaseEntity
    {
        /// <summary>
        /// the join order
        /// </summary>
        public int Order { get; set; }
        public JoinDataModel Join { get; set; }
        /// <summary>
        /// the principal table
        /// </summary>
        public TableDataModel Table { get; set; }
        public DataSourceDataModel DataSource { get; set; }
        public string Alias { get; set; }
        /// <summary>
        /// Id of the main table for example:
        /// Dep_Marche
        /// </summary>
        public bool MainEntity { get; set; }
    }
}
