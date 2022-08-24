using Involys.Poc.Api.Controllers.Common;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Controllers.DataSourceTable.Model;
using Involys.Poc.Api.Controllers.TableField.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Involys.Poc.Api.Controllers.ExpressionTerm.Model
{
    public class CreateExpressionTermQuery : BaseEntity
    {
      
        public CreateDataSourceFieldQuery FieldsDataSource { get; set; }
        public CreateExpressionTermQuery FirstTerm { get; set; }
        public CreateExpressionTermQuery SecondTerm { get; set; }
        public string ExpressionType { get; set; }
        public CreateOperatorQuery Operator { get; set; }
        public CreateTableFieldQuery TableField { get; set; }

        public CreateDataSourceTableQuery DataSourceTable { get; set; }
        public CreateDataSourceFieldQuery CalculatedFieldResulting { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
    }
}
