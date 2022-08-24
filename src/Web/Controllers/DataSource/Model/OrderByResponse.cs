using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.TableField.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSource.Model
{
    public class OrderByResponse : BaseEntity
    {
       
        public int OrderDirection { get; set; }
        public DataSourceResponse DataSource { get; set; }
        public TableFieldResponse TableField { get; set; }
    }
}
