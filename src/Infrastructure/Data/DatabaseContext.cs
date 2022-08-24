using Infrastructure.Data.Models.Alerts;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.EntityFrameworkCore;

namespace Involys.Poc.Api.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // deleting values from associative table DataSourceTable when deleting the DataSource
            modelBuilder.Entity<DataSourceTableDataModel>()
              .HasOne(d => d.DataSource)
              .WithMany()
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ExpressionTermDataModel>()
              .HasOne(d => d.CalculatedFieldResulting)
              .WithMany()
              .HasConstraintName("FK_calculated_FIELD");
            modelBuilder.Entity<TableFieldDataModel>()
             .HasOne(d => d.ReferenceField)
             .WithMany()
             .HasConstraintName("FK_referenced_FIELD");

            // association one to one ExpressionTerms
            modelBuilder.Entity<ExpressionTermDataModel>()
                .HasOne(e => e.FirstTerm)
                .WithOne()
                .HasConstraintName("FK_firstTerm")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ExpressionTermDataModel>()
                .HasOne(e => e.SecondTerm)
                .WithOne()
                .HasConstraintName("FK_secondTerm")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);


            //datasourcefield
            modelBuilder.Entity<DataSourceDataModel>()
                .HasMany(d => d.DataSourceFields)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpressionTermDataModel>()
               .HasOne(e => e.DataSourceTable)
               .WithMany()
               .OnDelete(DeleteBehavior.Cascade);
            //Orderby
            modelBuilder.Entity<OrderByDataModel>()
                .HasOne(d => d.DataSource)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ExpressionTermDataModel>()
                .HasOne(d => d.FieldsDataSource)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

        }

        #region QueryBuilders
        public DbSet<ArithmeticOperatorDataModel> ArithmeticOperators { get; set; }
        public DbSet<DataSourceDataModel> DataSources { get; set; }
        public DbSet<ClassificationDataModel> Classifications { get; set; }
        public DbSet<DataSourceFieldDataModel> DataSourceFields { get; set; }
        public DbSet<DataSourceTableDataModel> DataSourceTables { get; set; }
        public DbSet<ExpressionTermDataModel> ExpressionTerms { get; set; }
        public DbSet<JoinConditionDataModel> JoinCanditions { get; set; }
        public DbSet<JoinDataModel> Joins { get; set; }
        public DbSet<OperatorDataModel> Operators { get; set; }
        public DbSet<TableDataModel> Tables { get; set; }
        public DbSet<TableFieldDataModel> TableFields { get; set; }
        public DbSet<OrderByDataModel> OrdersBy { get; set; }
        public DbSet<FunctionDataModel> Functions { get; set; }
        #endregion


    }
}
