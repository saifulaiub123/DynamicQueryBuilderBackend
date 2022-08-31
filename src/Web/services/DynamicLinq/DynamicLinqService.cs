using System;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Common.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Involys.Poc.Api.services.DynamicLinq
{
    public class DynamicLinqService : IDynamicLinqService
    {
        private readonly IConfiguration _configuration;

        public DynamicLinqService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<dynamic> GetQuery(DynamicLinqModel model)
        {
            try
            {
                



                var selectedColumn = GetSelectedColumList(model);
                StringBuilder sb = new StringBuilder($"select {selectedColumn} from [{model.TableName}] ");
                if (model.JoinTables.Count > 0)
                {
                    foreach (var jt in model.JoinTables)
                    {
                        sb.Append("join [" + jt.TableName + "] on [" + jt.ParentTableName + "]." + jt.ParentColumnOn + "= [" + jt.TableName + "]." + jt.CurrentColumnOn + " ");
                    }
                }
                if (model.WhereConditions.Count > 0)
                {
                    
                    int count = 0;
                    sb.Append("where ");
                    foreach (var wc in model.WhereConditions)
                    {
                        string fullyQualifiedName = $"Common.Model.{wc.ConditionTable}, Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

                        Type type = Type.GetType(fullyQualifiedName);

                        var prop = type.GetProperty(wc.ConditionColumn);

                        if (prop.PropertyType == typeof(int))
                        {
                            sb.Append("[" + wc.ConditionTable + "]." + wc.ConditionColumn + " " + wc.Condition + " " + Convert.ToInt32(wc.Value));   
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            sb.Append("[" + wc.ConditionTable + "]." + wc.ConditionColumn + " " + wc.Condition + " '" + wc.Value + "'");
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            sb.Append("[" + wc.ConditionTable + "]." + wc.ConditionColumn + " " + wc.Condition + " " + Convert.ToInt32(wc.Value));
                        }

                        count++;
                        if (count < model.WhereConditions.Count)
                        {
                            sb.Append(" and ");
                        }
                    }
                }

                var query = sb.ToString();

                using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                await con.OpenAsync();

                var result =  con.Query(query).AsList();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        private string GetSelectedColumList(DynamicLinqModel model)
        {
            var columns = string.Empty;

            if(model.Columns.Count > 0)
            {
                foreach(var col in model.Columns)
                {
                    columns += $"[{model.TableName}].[{col}],";
                }
            }

            foreach(var joinTable in model.JoinTables)
            {
                if(joinTable.Columns.Count > 0)
                {
                    foreach (var col in joinTable.Columns)
                    {
                        columns += $"[{joinTable.TableName}].[{col}],";
                    }
                }
            }

            if(string.IsNullOrEmpty(columns))
            {
                columns = "*";
            }
            else
            {
                columns = columns.Remove(columns.Length - 1);
            }

            return columns;
        }
    }
}
