using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Models.Alerts
{
    /// <summary>
    /// Classification of data source
    /// </summary>
    [Table("data_source_classification")]
    public class ClassificationDataModel : BaseEntity
    {
        public string Designation { get; set; }
    }
}
