using Involys.Poc.Api.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.Table.Model
{
    public class UpdateTableQuery : BaseEntity
    {
       
        public string Designation { get; set; }
        public string Name { get; set; }
    }
}
