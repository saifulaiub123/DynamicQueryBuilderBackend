using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    /// arithmetic_operator
    /// </summary>
    [Table("data_source_arithm_operators")]
    public class ArithmeticOperatorDataModel : BaseEntity
    {
        public string Designation { get; set; }
    }
}
