using Involys.Poc.Api.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder
{
    [Table("data_source_function")]
    public class FunctionDataModel : BaseEntity
    {
        public string Designation { get; set; }
    }
}
