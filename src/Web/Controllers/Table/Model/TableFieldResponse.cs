using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.Table.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.TableField.Model
{
    public class TableFieldResponse : BaseEntity
    {
       
        public string Designation { get; set; }
        public bool IsList { get; set; }
        public string Name { get; set; }

        public TableResponse Table { get; set; }
        public TableFieldResponse ReferenceField { get; set; }
    }
}
