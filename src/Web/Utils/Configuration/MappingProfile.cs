using AutoMapper;
using Infrastructure.Data.Models.Alerts;
using Involys.Poc.Api.Controllers.DataSource.Model;
using Involys.Poc.Api.Controllers.DataSourceField.Model;
using Involys.Poc.Api.Controllers.DataSourceTable;
using Involys.Poc.Api.Controllers.DataSourceTable.Model;
using Involys.Poc.Api.Controllers.ExpressionTerm.Model;
using Involys.Poc.Api.Controllers.Table.Model;
using Involys.Poc.Api.Controllers.TableField.Model;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;

namespace Involys.Poc.Api
{
    /// <summary>
    /// The mapping between models
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Join

            CreateMap<JoinDataModel, JoinResponse>();

            #endregion

            #region Operators

            CreateMap<OperatorDataModel, OperatorResponse>();

            #endregion

            #region ExpressionTerms

            CreateMap<ExpressionTermDataModel, ExpressionTermResponse>();
            CreateMap<CreateExpressionTermQuery, ExpressionTermDataModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DataSourceTable, opt => opt.Ignore())
            .ForMember(dest => dest.FirstTerm, opt => opt.Ignore())
            .ForMember(dest => dest.SecondTerm, opt => opt.Ignore())
            .ForMember(dest => dest.FieldsDataSource, opt => opt.Ignore())
            .ForMember(dest => dest.CalculatedFieldResulting, opt => opt.Ignore())
            .ForMember(dest => dest.Operator, opt => opt.Ignore())
            .ForMember(dest => dest.TableField, opt => opt.Ignore());


            #endregion

            #region OrderBys

            CreateMap<OrderByDataModel, OrderByResponse>();
            CreateMap<CreateOrderByQuery, OrderByDataModel>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.DataSource, opt => opt.Ignore())
             .ForMember(dest => dest.TableField, opt => opt.Ignore());

            #endregion

            #region TableFields

            CreateMap<TableFieldDataModel, TableFieldResponse>();
            CreateMap<CreateTableFieldQuery, TableFieldDataModel>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.Table, opt => opt.Ignore());



            #endregion

            #region DataSourceTables

            CreateMap<DataSourceTableDataModel, DataSourceTableResponse>();
            CreateMap<CreateDataSourceTableQuery, DataSourceTableDataModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Table, opt => opt.Ignore())
            .ForMember(dest => dest.DataSource, opt => opt.Ignore())
            .ForMember(dest => dest.Join, opt => opt.Ignore());

            #endregion

            #region DataSourceFields

            CreateMap<DataSourceFieldDataModel, DataSourceFieldResponse>();
            CreateMap<CreateDataSourceFieldQuery, DataSourceFieldDataModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Function, opt => opt.Ignore())
                .ForMember(dest => dest.TableField, opt => opt.Ignore());
                

            #endregion

            #region Classifications

            CreateMap<ClassificationDataModel, ClassificationResponse>();
            CreateMap<CreateClassificationQuery, ClassificationDataModel>();

            #endregion 

            #region DataSources

            CreateMap<DataSourceDataModel, DataSourceResponse>();
            CreateMap<CreateDataSourceQuery, DataSourceDataModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WhereCondition, opt => opt.Ignore())
                .ForMember(dest => dest.HavingCondition, opt => opt.Ignore())
                .ForMember(dest => dest.Classification, opt => opt.Ignore())
                .ForMember(dest => dest.DataSourceFields, opt => opt.Ignore());

               
                

            #endregion 

            #region Functions

            CreateMap<FunctionDataModel, FunctionResponse>();
            CreateMap<CreateFunctionQuery, FunctionDataModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion Functions

            #region Tables

            CreateMap<TableDataModel, TableResponse>();
            CreateMap<UpdateTableQuery, TableDataModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
           
            CreateMap<CreateTableQuery, TableDataModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion Tables

        }
    }
}