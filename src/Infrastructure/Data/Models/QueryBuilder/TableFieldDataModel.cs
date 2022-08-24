using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    ///  table_field
    /// </summary>
    [Table("data_source_table_fields")]
    public class TableFieldDataModel : BaseEntity
    {
        public string Designation { get; set; }
        public bool IsList { get; set; }
        public string Name { get; set; }

        public TableDataModel Table { get; set; }
        public TableFieldDataModel ReferenceField { get; set; }

    }
}