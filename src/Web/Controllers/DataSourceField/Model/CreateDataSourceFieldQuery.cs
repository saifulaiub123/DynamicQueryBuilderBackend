using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.TableField.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.DataSourceField.Model
{
    public class CreateDataSourceFieldQuery : BaseEntity
    {
       
        public string Designation { get; set; }
        public bool CalculatedField { get; set; }
        public string Alias { get; set; }
        public int Order { get; set; }
        public bool GroupBy { get; set; }
        public CreateTableFieldQuery TableField { get; set; }
        public CreateFunctionQuery Function { get; set; }
    }
}
