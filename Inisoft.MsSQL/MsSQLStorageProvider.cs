using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Inisoft.Core.Interface;
using Inisoft.Core.Attribute;
using Inisoft.Core;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Data.Common;
using Inisoft.Core.Provider;
using Inisoft.Core.Object;
using Inisoft.Core.Extension;
using Inisoft.Core.Interface.Storage;

namespace Inisoft.MsSQL
{
    public class MsSQLStorageProvider : BaseStorageProvider, IStorageProvider
    {
        public MsSQLStorageProvider() : base()
        {
            _sqlConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["SystemStorageProvider"].ConnectionString);
            _sqlCommand = _sqlConnection.CreateCommand();
        }
        ~MsSQLStorageProvider()
        {
            if (_sqlCommand != null)
            {
                _sqlCommand.Dispose();
                _sqlCommand = null;
            }
            if (_sqlConnection != null)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
                _sqlConnection = null;
            }
        }

        #region Properties
        private SqlConnection _sqlConnection = null;
        private SqlCommand _sqlCommand = null;
        #endregion

        #region MsSQL Connection

        #endregion

        #region Schema management
        public override MethodResult TestConnection()
        {
            try
            {
                _sqlConnection.Open();
                _sqlConnection.Close();
                return MethodResult.TRUE;
            }
            catch (Exception except)
            {
                return new MethodResult() { Success = false, Exception = except };
            }
        }

        public override MethodResult CheckObjectStorageExists(ObjectName objectName)
        {
            //SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Customers'
            try
            {
                int resultCount = doExecuteScalar("SELECT COUNT(*) AS RESULT_COUNT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName", CommandType.Text, new SqlParameter("@TableName", objectName.SqlName()));
                if (resultCount == 1)
                {
                    return MethodResult.TRUE;
                }
            }
            catch (Exception except)
            {
                return new MethodResult() {Success = false, Exception = except };
            }
            return MethodResult.FALSE;
        }

        public override MethodResult CreateStorage(ObjectDefinition objectDefinition)
        {
            try
            {
                SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();
                doExecuteNonQuery(sqlQueryBuilder.GetCreateTableSQL(objectDefinition));
                return MethodResult.TRUE;
            }
            catch (Exception except)
            {
                return new MethodResult() { Success = false, Exception = except };
            }
        }

        public override MethodResult UpdateStorage(ObjectDefinition objectDefinition)
        {
            return MethodResult.FALSE;
        }

        public override MethodResult RemoveStorage(ObjectDefinition objectDefinition)
        {
            return MethodResult.FALSE;
        }

        public override MethodResult<ObjectDefinition> GetObjectDefinition(string objectName)
        {
            return new MethodResult<ObjectDefinition>() { Success = false};
        }
        #endregion

        #region Private methods
        private Object lockObject = new object();
        private int doExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            lock (lockObject)
            {
                _sqlConnection.Open();
                try
                {
                    using (SqlCommand command = _sqlConnection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = commandType;
                        command.Connection = _sqlConnection;
                        if (parameters != null && parameters.Length > 0)
                        {
                            foreach (SqlParameter loopSqlParameter in parameters)
                            {
                                command.Parameters.Add(loopSqlParameter);
                            }
                        }
                        object result = command.ExecuteScalar();
                        if (result != null && result != System.DBNull.Value)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                finally
                {
                    _sqlConnection.Close();
                }
                return 0;
            }
        }

        private void doExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            lock (lockObject)
            {
                _sqlConnection.Open();
                try
                {
                    using (SqlCommand command = _sqlConnection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = CommandType.Text;
                        command.Connection = _sqlConnection;
                        if (parameters != null && parameters.Length > 0)
                        {
                            foreach (SqlParameter loopSqlParameter in parameters)
                            {
                                command.Parameters.Add(loopSqlParameter);
                            }
                        }
                        command.ExecuteNonQuery();
                    }
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
        }

        internal IEnumerator<T> doExecuteQuery<T>(string commandText, CommandType commandType, IEnumerable<SqlParameter> parameters, Func<DbDataReader, T> binder)
        {
            List<T> result = new List<T>();
            lock (lockObject)
            {
                _sqlConnection.Open();
                try
                {
                    using (SqlCommand command = _sqlConnection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = commandType;
                        command.Connection = _sqlConnection;
                        if (parameters != null)
                        {
                            foreach (SqlParameter loopSqlParameter in parameters)
                            {
                                command.Parameters.Add(loopSqlParameter);
                            }
                        }
                        using (DbDataReader reader = command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    result.Add(binder(reader));
                                }
                            }

                            reader.Close();
                        }
                    }
                }
                finally
                {
                    _sqlConnection.Close();
                }
            }
            return result.GetEnumerator();
        }
        #endregion


        public override IStorageQueryable<TObject> Select<TObject>(ObjectDefinition objectDefinition)
        {
            return new StorageQueryable<TObject>(objectDefinition, new SqlQueryExecutor(this));
        }

        protected override MethodResult<TObject> DoSave<TObject>(TObject genericObject, ObjectDefinition objectDefinition)
        {
            SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();
            if (genericObject.IsNew)
            {
                // insert
                string insertIntoSql = sqlQueryBuilder.BuildInsertIntoSQL<TObject>(objectDefinition, genericObject);
                int newId = doExecuteScalar(insertIntoSql, CommandType.Text, sqlQueryBuilder.GetParameters().ToArray());
                genericObject.Id = newId;
            }
            else
            {
                // update
                string updayeSql = sqlQueryBuilder.BuildUpdateSQL<TObject>(objectDefinition, genericObject);
                doExecuteScalar(updayeSql, CommandType.Text, sqlQueryBuilder.GetParameters().ToArray());
            }
            return new MethodResult<TObject>() { Data = genericObject, Success = true };
        }
    }

    public interface ISqlQueryExecutor
    {
        TObject ExecOne<TObject>(string sqlQuery, CommandType commandType, IEnumerable<SqlParameter> parameters, Func<DbDataReader, TObject> binder);
        IEnumerator<TObject> ExecList<TObject>(string sqlQuery, CommandType commandType, IEnumerable<SqlParameter> parameters, Func<DbDataReader, TObject> binder);
    }

    internal class SqlQueryExecutor : ISqlQueryExecutor
    {
        MsSQLStorageProvider storageProvider = null;

        public SqlQueryExecutor(MsSQLStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public TObject ExecOne<TObject>(string sqlQuery, CommandType commandType, IEnumerable<SqlParameter> parameters, Func<DbDataReader, TObject> binder)
        {
            IEnumerator<TObject> list = ExecList<TObject>(sqlQuery, commandType, parameters, binder);
            if (list != null && list.MoveNext())
            {
                return list.Current;
            }
            return default(TObject);
        }

        public IEnumerator<TObject> ExecList<TObject>(string sqlQuery, CommandType commandType, IEnumerable<SqlParameter> parameters, Func<DbDataReader, TObject> binder)
        {
            return storageProvider.doExecuteQuery(sqlQuery, commandType, parameters, binder);
        }

    }

    internal abstract class BaseStorageQueryResult<TObject> : IStorageQueryResult<TObject>
         where TObject : GenericObject, new()
    {
        protected ISqlQueryExecutor sqlQueryExecutor = null;
        protected ObjectDefinition objectDefinition = null;

        public BaseStorageQueryResult(ObjectDefinition objectDefinition, ISqlQueryExecutor sqlQueryExecutor)
        {
            this.sqlQueryExecutor = sqlQueryExecutor;
            this.objectDefinition = objectDefinition;
        }

        protected TObject binderFunction(DbDataReader reader)
        {
            TObject obj = new TObject();
            foreach (PropertyDefinition loopPropertyDefinition in objectDefinition.GetSystemProperties())
            {
                obj.SetValue(loopPropertyDefinition.Name, reader.GetValue(reader.GetOrdinal(loopPropertyDefinition.Name)));
            }
            foreach (PropertyDefinition loopPropertyDefinition in objectDefinition.Properties)
            {
                obj.SetValue(loopPropertyDefinition.Name, reader.GetValue(reader.GetOrdinal(loopPropertyDefinition.Name)));
            }
            obj.MarkNotModified();
            return obj;
        }


        public abstract TObject FirstOrDefault();
        public abstract TObject FirstOrDefault(Expression<Func<TObject, bool>> predicate);
        public abstract IEnumerator<TObject> GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    internal class StorageQueryResult<TObject> :  BaseStorageQueryResult<TObject>
        where TObject: GenericObject, new()
    {
        private Core.Interface.Storage.IStorageQuery query = null;
        public StorageQueryResult(ObjectDefinition objectDefinition, ISqlQueryExecutor sqlQueryExecutor, Core.Interface.Storage.IStorageQuery query)
            : base(objectDefinition, sqlQueryExecutor)
        {
            this.query = query;
        }

        private string queryText = null;
        private IList<SqlParameter> parameters = new List<SqlParameter>();
        private CommandType commandType = CommandType.Text;
        private void prepareQuery()
        {
            if (queryText == null)
            {
                queryText = query.QueryText;
                switch (query.StorageQueryType)
                { 
                    case Core.Storage.StorageQueryTypeEnum.Table:
                    case Core.Storage.StorageQueryTypeEnum.View:
                        commandType = CommandType.TableDirect;
                        queryText = string.Format("select * from {0}", query.QueryText);
                        if (query.Parameters.Count > 0)
                        {
                            List<string> whereParts = new List<string>();
                            foreach (IStorageQueryParameter storageQueryParameter in query.Parameters)
                            {
                                if (storageQueryParameter.IsNull)
                                {
                                    whereParts.Add(string.Format("{0} = @{0}", storageQueryParameter.Name));
                                }
                                else
                                {
                                    whereParts.Add(string.Format("{0} is null", storageQueryParameter.Name));
                                }
                                parameters.Add(storageQueryParameter.ToSqlParameter());
                            }
                            queryText = string.Format("{0} where {1}", queryText, string.Join(" and ", whereParts));
                        }
                        break;
                    case Core.Storage.StorageQueryTypeEnum.Query:
                    case Core.Storage.StorageQueryTypeEnum.StoredProcedure:
                            commandType = CommandType.Text;
                            foreach (IStorageQueryParameter storageQueryParameter in query.Parameters)
                            {
                                parameters.Add(storageQueryParameter.ToSqlParameter());
                            }
                        break;
                }

                if (query.StorageQueryType == Core.Storage.StorageQueryTypeEnum.StoredProcedure)
                {
                    commandType = CommandType.StoredProcedure;
                }
            }
        }

        public override TObject FirstOrDefault()
        {
            prepareQuery();
            return sqlQueryExecutor.ExecOne(queryText, commandType, parameters, binderFunction);
        }

        public override TObject FirstOrDefault(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate)
        {
            return FirstOrDefault();
        }

        public override IEnumerator<TObject> GetEnumerator()
        {
            prepareQuery();
            return sqlQueryExecutor.ExecList(queryText, commandType, parameters, binderFunction);
        }
    }

    internal class StorageQueryable<TObject> : BaseStorageQueryResult<TObject>, IStorageQueryable<TObject>
        where TObject: GenericObject, new()
    {
        SqlExpressionVisitor expressionVisitor = new SqlExpressionVisitor();

        public StorageQueryable(ObjectDefinition objectDefinition, ISqlQueryExecutor sqlQueryExecutor)
            : base(objectDefinition, sqlQueryExecutor)
        {
        }

        public IStorageQueryable<TObject> Where(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate)
        {
            expressionVisitor.VisitExpression(predicate);
            return this;
        }

        public IStorageQueryable<TObject> OrderBy(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate)
        {
            expressionVisitor.VisitExpression(predicate);
            return this;
        }

        public override TObject FirstOrDefault()
        {
            return sqlQueryExecutor.ExecOne(expressionVisitor.SqlQueryBuilder.GetSelectQuery(objectDefinition), CommandType.Text, expressionVisitor.SqlQueryBuilder.GetParameters(), binderFunction);
        }

        public override TObject FirstOrDefault(System.Linq.Expressions.Expression<Func<TObject, bool>> predicate)
        {
            expressionVisitor.VisitExpression(predicate);
            return FirstOrDefault();
        }

        public override IEnumerator<TObject> GetEnumerator()
        {
            return sqlQueryExecutor.ExecList(expressionVisitor.SqlQueryBuilder.GetSelectQuery(objectDefinition), CommandType.Text, expressionVisitor.SqlQueryBuilder.GetParameters(), binderFunction);
        }

        public string GetQuery()
        {
            return expressionVisitor.SqlQueryBuilder.GetSelectQuery(objectDefinition);
        }

        public IStorageQueryResult<TObject> ByQuery(Core.Interface.Storage.IStorageQuery query)
        {
            return new StorageQueryResult<TObject>(objectDefinition, sqlQueryExecutor, query);
        }
    }

    internal static class ObjectNameExtension
    {
        public static string SqlName(this ObjectName objectName)
        {
            return string.Format("{0}_{1}", objectName.Namespace, objectName.Name);
        }
    }

    internal static class PropertyDefinitionExtension
    {
        public static string SqlTypeName(this PropertyDefinition propertyDefinition)
        {
            switch (propertyDefinition.PropertyType)
            {
                case PropertyTypeEnum.Boolean:
                    return "bit";
                case PropertyTypeEnum.DateTime:
                    return "DateTime";
                case PropertyTypeEnum.Decimal:
                    return "decimal";
                case PropertyTypeEnum.GUID:
                    return "GUID";
                case PropertyTypeEnum.LongText:
                    return "nvarchar";
                case PropertyTypeEnum.Number:
                    return "int";
                case PropertyTypeEnum.Password:
                    return "nvarchar";
                case PropertyTypeEnum.Text:
                    return "varchar";
            }
            return "nvarchar";
        }

        public static string SqlTypeLength(this PropertyDefinition propertyDefinition)
        {
            if (propertyDefinition.PropertyType == PropertyTypeEnum.Text ||
                propertyDefinition.PropertyType == PropertyTypeEnum.Password)
            {
                return string.Format("({0})", propertyDefinition.MaxLength > 0 ? propertyDefinition.MaxLength : 100);
            }
            if (propertyDefinition.PropertyType == PropertyTypeEnum.LongText)
            {
                return "(max)";
            }
            return string.Empty;
        }
    }

    internal static class StorageQueryParameterExtension
    {
        public static SqlParameter ToSqlParameter(this IStorageQueryParameter storageQueryParameter)
        {
            if (!storageQueryParameter.IsNull)
            {
                switch (Type.GetTypeCode(storageQueryParameter.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        return new SqlParameter(storageQueryParameter.Name, ((bool)storageQueryParameter.Value) ? 1 : 0) { DbType = DbType.Boolean };
                    case TypeCode.String:
                        return new SqlParameter(storageQueryParameter.Name, storageQueryParameter.Value) { DbType = DbType.String };
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                        return new SqlParameter(storageQueryParameter.Name, storageQueryParameter.Value) { DbType = DbType.Int32 };
                    case TypeCode.DateTime:
                        return new SqlParameter(storageQueryParameter.Name, storageQueryParameter.Value) { DbType = DbType.DateTime };
                    case TypeCode.Object:
                        break;
                    default:
                        return new SqlParameter(storageQueryParameter.Name, storageQueryParameter.Value.ToString()) { DbType = DbType.String };
                }
            }
            return new SqlParameter(storageQueryParameter.Name, System.DBNull.Value);
        }
    }

    class SqlExpressionVisitor : Inisoft.Core.ExpressionVisitor
    {
        SqlQueryBuilder sqlQueryBuilder = new SqlQueryBuilder();

        public SqlQueryBuilder SqlQueryBuilder
        {
            get { return sqlQueryBuilder; }
        }

        public void VisitExpression(Expression exp)
        {
            base.Visit(exp);
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            sqlQueryBuilder.AddWhereParameter(c.Value);
            return c;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            sqlQueryBuilder.AppendWhere("(");
            Expression left = this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                    sqlQueryBuilder.AppendWhere(" = ");
                    break;
                case ExpressionType.NotEqual:
                    sqlQueryBuilder.AppendWhere(" <> ");
                    break;
                case ExpressionType.GreaterThan:
                    sqlQueryBuilder.AppendWhere(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sqlQueryBuilder.AppendWhere(" >= ");
                    break;
                case ExpressionType.LessThan:
                    sqlQueryBuilder.AppendWhere(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sqlQueryBuilder.AppendWhere(" <= ");
                    break;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    sqlQueryBuilder.AppendWhere(" and ");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    sqlQueryBuilder.AppendWhere(" or ");
                    break;
                default:
                    sqlQueryBuilder.AppendWhere(string.Format(" {0} ", b.NodeType.ToString()));
                    break;
            }
            Expression right = this.Visit(b.Right);
            Expression conversion = this.Visit(b.Conversion);
            sqlQueryBuilder.AppendWhere(")");
            return b;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression.NodeType == ExpressionType.Parameter)
            {
                sqlQueryBuilder.AppendWhere(m.Member.Name);
            }
            else
            {
                object value = Expression.Lambda(m).Compile().DynamicInvoke();
                sqlQueryBuilder.AddWhereParameter(value);
            }
            return m;
        }

        //protected override Expression VisitMethodCall(MethodCallExpression m)
        //{
        //    Expression obj = this.Visit(m.Object);
        //    IEnumerable<Expression> args = this.VisitExpressionList(m.Arguments);
        //    if (obj != m.Object || args != m.Arguments)
        //    {
        //        return Expression.Call(obj, m.Method, args);
        //    }
        //    return m;
        //}
    }

    class SqlQueryBuilder
    {
        private string lastQuery = null;
        private StringBuilder whereSql = new StringBuilder();
        private IList<SqlParameter> parameters = new List<SqlParameter>();

        private string getCurrentParamName()
        {
            return string.Format("p_{0}", parameters.Count);
        }

        public void AppendWhere(string sqlPart)
        {
            lastQuery = null;
            whereSql.Append(sqlPart);
        }

        public SqlParameter GetNewParameter(object value)
        {
            if (value == null)
            {
                return new SqlParameter(getCurrentParamName(), System.DBNull.Value);
            }
            else
            {
                switch (Type.GetTypeCode(value.GetType()))
                {
                    case TypeCode.Boolean:
                        return new SqlParameter(getCurrentParamName(), ((bool)value) ? 1 : 0) { DbType = DbType.Boolean };
                    case TypeCode.String:
                        return new SqlParameter(getCurrentParamName(), value) { DbType = DbType.String };
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                        return new SqlParameter(getCurrentParamName(), value) { DbType = DbType.Int32 };
                    case TypeCode.DateTime:
                        return new SqlParameter(getCurrentParamName(), value) { DbType = DbType.DateTime };
                    case TypeCode.Object:
                        break;
                    default:
                        return new SqlParameter(getCurrentParamName(), value.ToString()) { DbType = DbType.String };
                }
            }
            return new SqlParameter(getCurrentParamName(), System.DBNull.Value);
        }

        public void AddWhereParameter(object value)
        {
            lastQuery = null;
            if (value == null)
            {
                whereSql.Append(" is null ");
            }
            else
            {
                SqlParameter sqlParameter = GetNewParameter(value);
                whereSql.AppendFormat(" @{0} ", sqlParameter.ParameterName);
                parameters.Add(sqlParameter);
            }
        }

        public IList<SqlParameter> GetParameters()
        {
            return parameters;
        }

        public string GetCreateTableSQL(ObjectDefinition objectDefinition)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("create table [{0}] (", objectDefinition.ObjectName.SqlName());
            foreach (PropertyDefinition loopPropertyDefinition in objectDefinition.GetSystemProperties())
            {
                sql.AppendFormat(" [{0}] [{1}]{2} {3} NULL, ", loopPropertyDefinition.Name, loopPropertyDefinition.SqlTypeName(), loopPropertyDefinition.SqlTypeLength(), loopPropertyDefinition.IsRequired ? loopPropertyDefinition.IsPK ? "IDENTITY(1,1) NOT" : "NOT" : string.Empty);
            }
            foreach (PropertyDefinition loopPropertyDefinition in objectDefinition.Properties)
            {
                sql.AppendFormat(" [{0}] [{1}]{2} {3} NULL, ", loopPropertyDefinition.Name, loopPropertyDefinition.SqlTypeName(), loopPropertyDefinition.SqlTypeLength(), loopPropertyDefinition.IsRequired ? loopPropertyDefinition.IsPK ? "IDENTITY(1,1) NOT" : "NOT" : string.Empty);
            }
            sql.AppendFormat(" CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ", objectDefinition.ObjectName.SqlName());
            sql.Append(" (");
            sql.Append(" [Id] ASC");
            sql.Append(" ) ");
            sql.Append(" ) ON [PRIMARY]");
            return sql.ToString();
        }

        public string GetSelectQuery(ObjectDefinition objectDefinition)
        {
            if (!string.IsNullOrEmpty(lastQuery))
            {
                return lastQuery;
            }
            bool firstAdded = false;
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            foreach (PropertyDefinition propertyDefinition in objectDefinition.GetSystemProperties())
            {
                if (firstAdded)
                {
                    sql.Append(", ");
                }
                sql.Append(propertyDefinition.Name);
                firstAdded = true;
            }
            foreach (PropertyDefinition propertyDefinition in objectDefinition.Properties)
            {
                sql.Append(", ");
                sql.Append(propertyDefinition.Name);
            }
            sql.AppendFormat(" from [{0}]", objectDefinition.ObjectName.SqlName());
            string where = whereSql.ToString();
            if (!string.IsNullOrEmpty(where))
            {
                sql.Append(" where ");
                sql.Append(where);
            }
#if DEBUG
            DebugUtils.TraceOut("SqlQueryBuilder", "GetSelectQuery", " SQL: {0}", sql.ToString());
#endif
            lastQuery = sql.ToString();
            return lastQuery;
        }

        public string BuildInsertIntoSQL<TObject>(ObjectDefinition objectDefinition, TObject genericObject)
            where TObject : GenericObject
        {
            parameters.Clear();
            whereSql.Clear();
            lastQuery = null;

            bool firstAdded = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("insert into [{0}] (", objectDefinition.ObjectName.SqlName());
            foreach (PropertyDefinition propertyDefinition in objectDefinition.GetSystemProperties())
            {
                if (propertyDefinition.IsPK)
                {
                    continue;
                }

                if (firstAdded)
                {
                    sql.Append(", ");
                }
                sql.Append(propertyDefinition.Name);
                firstAdded = true;
            }
            foreach (PropertyDefinition propertyDefinition in objectDefinition.Properties)
            {
                sql.Append(", ");
                sql.Append(propertyDefinition.Name);
            }
            sql.Append(") values (");
            firstAdded = false;
            SqlParameter sqlParameter;
            foreach (PropertyDefinition propertyDefinition in objectDefinition.GetSystemProperties())
            {
                if (propertyDefinition.IsPK)
                {
                    continue;
                }

                if (firstAdded)
                {
                    sql.Append(", ");
                }
                sqlParameter = GetNewParameter(genericObject.GetValue(propertyDefinition.Name));
                sql.AppendFormat("@{0}", sqlParameter.ParameterName);
                parameters.Add(sqlParameter);
                firstAdded = true;
            }
            foreach (PropertyDefinition propertyDefinition in objectDefinition.Properties)
            {
                sql.Append(", ");
                sqlParameter = GetNewParameter(genericObject.GetValue(propertyDefinition.Name));
                sql.AppendFormat("@{0}", sqlParameter.ParameterName);
                parameters.Add(sqlParameter);
            }
            sql.Append(")");
            sql.AppendLine(string.Empty);
            sql.Append("select SCOPE_IDENTITY();");
#if DEBUG
            DebugUtils.TraceOut("StorageQueryable`1", "BuildInsertIntoSQL", " SQL: {0}", sql.ToString());
#endif
            return sql.ToString();
        }

        public string BuildUpdateSQL<TObject>(ObjectDefinition objectDefinition, TObject genericObject)
            where TObject : GenericObject
        {
            parameters.Clear();
            whereSql.Clear();
            lastQuery = null;

            SqlParameter sqlParameter;
            bool firstAdded = false;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update [{0}] set ", objectDefinition.ObjectName.SqlName());
            foreach (PropertyDefinition propertyDefinition in objectDefinition.GetSystemProperties())
            {
                if (propertyDefinition.IsPK)
                {
                    if (whereSql.Length > 0)
                    {
                        whereSql.Append(" and ");
                    }
                    sqlParameter = GetNewParameter(genericObject.GetValue(propertyDefinition.Name));
                    whereSql.AppendFormat("{0} = @{1} ", propertyDefinition.Name, sqlParameter.ParameterName);
                    parameters.Add(sqlParameter);
                    continue;
                }

                if (firstAdded)
                {
                    sql.Append(", ");
                }
                sqlParameter = GetNewParameter(genericObject.GetValue(propertyDefinition.Name));
                sql.AppendFormat("{0} = @{1} ", propertyDefinition.Name, sqlParameter.ParameterName);
                parameters.Add(sqlParameter);
                firstAdded = true;
            }
            foreach (PropertyDefinition propertyDefinition in objectDefinition.Properties)
            {
                sql.Append(", ");
                sqlParameter = GetNewParameter(genericObject.GetValue(propertyDefinition.Name));
                sql.AppendFormat("{0} = @{1} ", propertyDefinition.Name, sqlParameter.ParameterName);
                parameters.Add(sqlParameter);
            }
            if (whereSql.Length > 0)
            {
                sql.Append(" where ");
                sql.Append(whereSql);
                
            }
#if DEBUG
            DebugUtils.TraceOut("StorageQueryable`1", "BuildUpdateSQL", " SQL: {0}", sql.ToString());
#endif
            return sql.ToString();
        }
        
    }

}