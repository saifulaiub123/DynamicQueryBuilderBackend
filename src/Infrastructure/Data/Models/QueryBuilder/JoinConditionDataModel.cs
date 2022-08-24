using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    ///  join_condition
    /// </summary>
    [Table("data_source_join_conditions")]
    public class JoinConditionDataModel : BaseEntity
    {
        public TableFieldDataModel TableFieldDataModelField { get; set; }
        public ArithmeticOperatorDataModel ArithmeticOperator { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public OperatorDataModel Operator { get; set; }
    }
}
