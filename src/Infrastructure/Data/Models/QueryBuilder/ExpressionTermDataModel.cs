using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    /// expression_term
    /// </summary>
    [Table("data_source_expression_terms")]
    public class ExpressionTermDataModel : BaseEntity
    {
        public DataSourceFieldDataModel FieldsDataSource { get; set; }
        public ExpressionTermDataModel FirstTerm { get; set; }
        public ExpressionTermDataModel SecondTerm { get; set; }
        public string ExpressionType { get; set; }
        public OperatorDataModel Operator { get; set; }
        public TableFieldDataModel TableField { get; set; }
        public DataSourceTableDataModel DataSourceTable { get; set; }
        [ForeignKey("ClculatedField")]
        public DataSourceFieldDataModel CalculatedFieldResulting { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }    
    }
}
