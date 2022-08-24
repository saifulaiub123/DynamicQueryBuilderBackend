using System.ComponentModel.DataAnnotations;

namespace Involys.Poc.Api.Data.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
    }
}
