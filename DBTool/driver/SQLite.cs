using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTool.driver
{
    public class SQLiteDriver : BaseDriver
    {
        //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.
        public static string connectionString = CreateConnectionString();

        public static SQLiteDriver _instance = new SQLiteDriver();


        public static SQLiteDriver Instance
        {
            get { return _instance; }
        }

        public SQLiteDriver()
        {
        }

        private static string CreateConnectionString()
        {

            string dbName = Directory.GetCurrentDirectory() + "\\data.db";
            if (!File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
            }
            string sqlLitePath = "data source=" + dbName + ";version=3;";
            return sqlLitePath;
        }

        #region 公用方法

        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            var obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString()!);
            }
        }

        override
        public bool Exists(string strSql)
        {
            var obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString()!);
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Exists(string strSql, params SQLiteParameter[] cmdParms)
        {
            var obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString()!);
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        override
        public int Execute(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public int Execute(string SQLString, params SQLiteParameter[] cmdParams)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddRange(cmdParams);
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString, cmdParams);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        override
        public void ExecuteTran(List<string> SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                int n = 0;
                try
                {
                    for (; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    if (ex is SQLiteException)
                    {
                        HandleSQLException((SQLiteException)ex, SQLStringList[n]);
                    }
                    else
                    {
                        HandleException(ex);
                    }
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int Execute(string SQLString, string content)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(SQLString, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@content", DbType.String);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SQLiteException ex)
                {
                    HandleSQLException(ex, SQLString, myParameter);
                    throw;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteInsertImg(string strSQL, byte[] fs)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
                SQLiteParameter myParameter = new SQLiteParameter("@fs", DbType.Binary);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (SQLiteException ex)
                {
                    HandleSQLException(ex, strSQL, myParameter);
                    throw;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        override
        public object? GetSingle(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string strSQL)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand(strSQL, connection);
            try
            {
                connection.Open();
                SQLiteDataReader myReader = cmd.ExecuteReader();
                return myReader;
            }
            catch (SQLiteException ex)
            {
                HandleSQLException(ex, strSQL);
                throw;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        override
        public DataTable Query(string SQLString)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SQLiteDataAdapter command = new SQLiteDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (SQLiteException ex)
                {
                    HandleSQLException(ex, SQLString);
                    throw;
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
                return ds.Tables[0];
            }
        }


        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString, cmdParms);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public void ExecuteSqlTran(Dictionary<string, SQLiteParameter[]> SQLStringList)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    string cmdText = string.Empty;
                    try
                    {
                        //循环
                        foreach (KeyValuePair<string, SQLiteParameter[]> myDE in SQLStringList)
                        {
                            cmdText = myDE.Key;
                            SQLiteParameter[] cmdParms = myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        if (ex is SQLiteException)
                        {
                            HandleSQLException((SQLiteException)ex, cmdText, SQLStringList[cmdText]);
                        }
                        else
                        {
                            HandleException(ex);
                        }
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object? GetSingle(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString, cmdParms);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string SQLString, params SQLiteParameter[] cmdParms)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand cmd = new SQLiteCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SQLiteDataReader myReader = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (SQLiteException ex)
            {
                HandleSQLException(ex, SQLString, cmdParms);
                throw;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataTable Query(string SQLString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (SQLiteException ex)
                    {
                        HandleSQLException(ex, SQLString, cmdParms);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return ds.Tables[0];
                }
            }
        }


        private void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction? trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion


        private void HandleSQLException(SQLiteException ex, string SQLString = "", params SQLiteParameter[] cmdParms)
        {
            if (!string.IsNullOrEmpty(SQLString))
            {
                WriteLog(SQLString, "SQL ERROR");
                WriteLog(string.Join("\r\n", cmdParms.Select(param => param.ParameterName + ":" + param.Value.ToString())), "SQL ERROR");
            }
            WriteLog(ex.Message, "SQL ERROR");
        }

        private void HandleException(Exception ex)
        {
            WriteLog(ex.Message);
        }
    }
}
