using Infrastructure.Data.Models.Alerts;
using Involys.Poc.Api.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Involys.Repository.Api.Infrastructure.Data.Models.Alerts
{
    /// <summary>
    /// dataSource
    /// table des requetteurs
    /// </summary>
    [Table("data_source_dataSources")]

    public class DataSourceDataModel : BaseEntity
    {
        public string Designation { get; set; }
        public string SqlText { get; set; }
        public bool Distinct { get; set; }
        /// <summary>
        /// classification of data source
        /// </summary>
        public ClassificationDataModel Classification { get; set; }
        public IList<DataSourceFieldDataModel> DataSourceFields { get; set; }
        public ExpressionTermDataModel WhereCondition { get; set; }
        public ExpressionTermDataModel HavingCondition { get; set; }
    }
}
