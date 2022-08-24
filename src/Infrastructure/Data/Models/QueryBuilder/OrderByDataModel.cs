using Involys.Poc.Api.Data.Models;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder
{
    [Table("data_source_orderBy")]
    public class OrderByDataModel : BaseEntity
    {
        public int OrderDirection { get; set; }
        public DataSourceDataModel DataSource { get; set; } 
        public TableFieldDataModel TableField { get; set; } 
    }
}
