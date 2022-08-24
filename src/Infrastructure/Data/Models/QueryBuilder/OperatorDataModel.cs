using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    ///  month
    /// </summary>
    [Table("data_source_operators")]
    public class OperatorDataModel : BaseEntity
    {
        public string Designation { get; set; }
    }
}
