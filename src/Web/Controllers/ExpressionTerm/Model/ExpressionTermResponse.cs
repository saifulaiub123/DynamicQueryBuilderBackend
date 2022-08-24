using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Controllers.DataSourceTable;
using Involys.Poc.Api.Controllers.TableField.Model;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.ExpressionTerm.Model
{
    public class ExpressionTermResponse : BaseEntity
    {
      
        public DataSourceFieldResponse FieldsDataSource { get; set; }
        public ExpressionTermResponse FirstTerm { get; set; }
        public ExpressionTermResponse SecondTerm { get; set; }
        public string ExpressionType { get; set; }
        public OperatorResponse Operator { get; set; }
        public TableFieldResponse TableField { get; set; }
        
        public DataSourceTableResponse DataSourceTable { get; set; }
        public DataSourceFieldResponse CalculatedFieldResulting { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
    }
}
