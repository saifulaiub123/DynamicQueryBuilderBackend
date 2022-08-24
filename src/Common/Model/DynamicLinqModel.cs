using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class DynamicLinqModel
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public List<JoinTable> JoinTables { get; set; }
        public List<WhereCondition> WhereConditions { get; set; }
    }
    public class JoinTable
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
        public string ParentTableName { get; set; }
        public string ParentColumnOn { get; set; }
        public string CurrentColumnOn { get; set; }
    }
    public class WhereCondition
    {
        public string Condition { get; set; }//=,>,<
        public string ConditionTable { get; set; }
        public string ConditionColumn { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }//int,string
    }
}
