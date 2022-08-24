using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    ///  join
    /// </summary>
    [Table("data_source_joins")]
    public class JoinDataModel : BaseEntity
    {
        public string Designation { get; set; }
    }
}
