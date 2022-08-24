using Involys.Poc.Api.Data.Models;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{   
    /// <summary>
    /// dataSource_field
    /// </summary>
    [Table("data_source_dataSource_fields")]
    public class DataSourceFieldDataModel : BaseEntity
    {
        public string Designation { get; set; }
        public bool CalculatedField { get; set; }
        public string Alias { get; set; }
        public int Order { get; set; }
        public bool GroupBy { get; set; }
        public TableFieldDataModel TableField { get; set; }
        public FunctionDataModel Function { get; set; }

    }
}
