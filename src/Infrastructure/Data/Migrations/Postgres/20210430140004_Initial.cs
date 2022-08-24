using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Involys.Poc.Api.Infrastructure.Data.Migrations.Postgres
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_source_arithm_operators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_arithm_operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_classification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_classification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_function",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_joins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_joins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_operators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_tables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_source_table_fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true),
                    IsList = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TableId = table.Column<int>(nullable: true),
                    ReferenceFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_table_fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_referenced_FIELD",
                        column: x => x.ReferenceFieldId,
                        principalTable: "data_source_table_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_table_fields_data_source_tables_TableId",
                        column: x => x.TableId,
                        principalTable: "data_source_tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_join_conditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableFieldDataModelFieldId = table.Column<int>(nullable: true),
                    ArithmeticOperatorId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    OperatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_join_conditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_data_source_join_conditions_data_source_arithm_operators_Ar~",
                        column: x => x.ArithmeticOperatorId,
                        principalTable: "data_source_arithm_operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_join_conditions_data_source_operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "data_source_operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_join_conditions_data_source_table_fields_TableF~",
                        column: x => x.TableFieldDataModelFieldId,
                        principalTable: "data_source_table_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_dataSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true),
                    SqlText = table.Column<string>(nullable: true),
                    Distinct = table.Column<bool>(nullable: false),
                    ClassificationId = table.Column<int>(nullable: true),
                    WhereConditionId = table.Column<int>(nullable: true),
                    HavingConditionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_dataSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_data_source_dataSources_data_source_classification_Classifi~",
                        column: x => x.ClassificationId,
                        principalTable: "data_source_classification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_dataSource_fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(nullable: true),
                    CalculatedField = table.Column<bool>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    GroupBy = table.Column<bool>(nullable: false),
                    TableFieldId = table.Column<int>(nullable: true),
                    FunctionId = table.Column<int>(nullable: true),
                    DataSourceDataModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_dataSource_fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_fields_data_source_dataSources_DataS~",
                        column: x => x.DataSourceDataModelId,
                        principalTable: "data_source_dataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_fields_data_source_function_Function~",
                        column: x => x.FunctionId,
                        principalTable: "data_source_function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_fields_data_source_table_fields_Tabl~",
                        column: x => x.TableFieldId,
                        principalTable: "data_source_table_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_dataSource_tables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Order = table.Column<int>(nullable: false),
                    JoinId = table.Column<int>(nullable: true),
                    TableId = table.Column<int>(nullable: true),
                    DataSourceId = table.Column<int>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    MainEntity = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_dataSource_tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_tables_data_source_dataSources_DataS~",
                        column: x => x.DataSourceId,
                        principalTable: "data_source_dataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_tables_data_source_joins_JoinId",
                        column: x => x.JoinId,
                        principalTable: "data_source_joins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_dataSource_tables_data_source_tables_TableId",
                        column: x => x.TableId,
                        principalTable: "data_source_tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_orderBy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDirection = table.Column<int>(nullable: false),
                    DataSourceId = table.Column<int>(nullable: true),
                    TableFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_orderBy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_data_source_orderBy_data_source_dataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "data_source_dataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_source_orderBy_data_source_table_fields_TableFieldId",
                        column: x => x.TableFieldId,
                        principalTable: "data_source_table_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "data_source_expression_terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FieldsDataSourceId = table.Column<int>(nullable: true),
                    FirstTermId = table.Column<int>(nullable: true),
                    SecondTermId = table.Column<int>(nullable: true),
                    ExpressionType = table.Column<string>(nullable: true),
                    OperatorId = table.Column<int>(nullable: true),
                    TableFieldId = table.Column<int>(nullable: true),
                    DataSourceTableId = table.Column<int>(nullable: true),
                    ClculatedField = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_source_expression_terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_calculated_FIELD",
                        column: x => x.ClculatedField,
                        principalTable: "data_source_dataSource_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_data_source_expression_terms_data_source_dataSource_tables_~",
                        column: x => x.DataSourceTableId,
                        principalTable: "data_source_dataSource_tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_source_expression_terms_data_source_dataSource_fields_~",
                        column: x => x.FieldsDataSourceId,
                        principalTable: "data_source_dataSource_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_firstTerm",
                        column: x => x.FirstTermId,
                        principalTable: "data_source_expression_terms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_data_source_expression_terms_data_source_operators_Operator~",
                        column: x => x.OperatorId,
                        principalTable: "data_source_operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_secondTerm",
                        column: x => x.SecondTermId,
                        principalTable: "data_source_expression_terms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_data_source_expression_terms_data_source_table_fields_Table~",
                        column: x => x.TableFieldId,
                        principalTable: "data_source_table_fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_fields_DataSourceDataModelId",
                table: "data_source_dataSource_fields",
                column: "DataSourceDataModelId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_fields_FunctionId",
                table: "data_source_dataSource_fields",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_fields_TableFieldId",
                table: "data_source_dataSource_fields",
                column: "TableFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_tables_DataSourceId",
                table: "data_source_dataSource_tables",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_tables_JoinId",
                table: "data_source_dataSource_tables",
                column: "JoinId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSource_tables_TableId",
                table: "data_source_dataSource_tables",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSources_ClassificationId",
                table: "data_source_dataSources",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSources_HavingConditionId",
                table: "data_source_dataSources",
                column: "HavingConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_dataSources_WhereConditionId",
                table: "data_source_dataSources",
                column: "WhereConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_ClculatedField",
                table: "data_source_expression_terms",
                column: "ClculatedField");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_DataSourceTableId",
                table: "data_source_expression_terms",
                column: "DataSourceTableId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_FieldsDataSourceId",
                table: "data_source_expression_terms",
                column: "FieldsDataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_FirstTermId",
                table: "data_source_expression_terms",
                column: "FirstTermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_OperatorId",
                table: "data_source_expression_terms",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_SecondTermId",
                table: "data_source_expression_terms",
                column: "SecondTermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_data_source_expression_terms_TableFieldId",
                table: "data_source_expression_terms",
                column: "TableFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_join_conditions_ArithmeticOperatorId",
                table: "data_source_join_conditions",
                column: "ArithmeticOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_join_conditions_OperatorId",
                table: "data_source_join_conditions",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_join_conditions_TableFieldDataModelFieldId",
                table: "data_source_join_conditions",
                column: "TableFieldDataModelFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_orderBy_DataSourceId",
                table: "data_source_orderBy",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_orderBy_TableFieldId",
                table: "data_source_orderBy",
                column: "TableFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_table_fields_ReferenceFieldId",
                table: "data_source_table_fields",
                column: "ReferenceFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_data_source_table_fields_TableId",
                table: "data_source_table_fields",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_data_source_dataSources_data_source_expression_terms_Having~",
                table: "data_source_dataSources",
                column: "HavingConditionId",
                principalTable: "data_source_expression_terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_data_source_dataSources_data_source_expression_terms_WhereC~",
                table: "data_source_dataSources",
                column: "WhereConditionId",
                principalTable: "data_source_expression_terms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_data_source_dataSource_fields_data_source_dataSources_DataS~",
                table: "data_source_dataSource_fields");

            migrationBuilder.DropForeignKey(
                name: "FK_data_source_dataSource_tables_data_source_dataSources_DataS~",
                table: "data_source_dataSource_tables");

            migrationBuilder.DropTable(
                name: "data_source_join_conditions");

            migrationBuilder.DropTable(
                name: "data_source_orderBy");

            migrationBuilder.DropTable(
                name: "data_source_arithm_operators");

            migrationBuilder.DropTable(
                name: "data_source_dataSources");

            migrationBuilder.DropTable(
                name: "data_source_classification");

            migrationBuilder.DropTable(
                name: "data_source_expression_terms");

            migrationBuilder.DropTable(
                name: "data_source_dataSource_fields");

            migrationBuilder.DropTable(
                name: "data_source_dataSource_tables");

            migrationBuilder.DropTable(
                name: "data_source_operators");

            migrationBuilder.DropTable(
                name: "data_source_function");

            migrationBuilder.DropTable(
                name: "data_source_table_fields");

            migrationBuilder.DropTable(
                name: "data_source_joins");

            migrationBuilder.DropTable(
                name: "data_source_tables");
        }
    }
}
