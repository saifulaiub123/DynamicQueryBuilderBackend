using Involys.Poc.Api.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    ///  table
    /// </summary>
    [Table("data_source_tables")]
    public class TableDataModel : BaseEntity
    {
        public string Designation { get; set; }
        public string Name { get; set; }
        public IList<TableFieldDataModel> TableFields { get; set; }
    }
}
