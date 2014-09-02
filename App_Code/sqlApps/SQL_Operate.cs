using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
///SQLSERVER数据处理基础类
/// </summary>
public class SQL_Operate
{
    SqlCommand scom;
    SqlConnection scon;
    SqlDataAdapter sda;
    SqlBulkCopy sbc;
    SqlDataReader sdr;
    SqlTransaction tran;

    /// <summary>
    /// 数据库操作集
    /// 北京鹰普世信息技术有限公司
    /// 编写:陶思宇
    /// </summary>
    public SQL_Operate()
    {
        scon = new SqlConnection(memberStatic.connectionString);
    }

    /// <summary>
    /// 初始化SqlCommand对象
    /// </summary>
    private void initSqlCommand()
    {
        scom = new SqlCommand();
        scom.Connection = scon;
    }

    /// <summary>
    /// 初始化SqlCommand对象
    /// </summary>
    /// <param name="sql">需要执行的SQL语句</param>
    private void initSqlCommand(string sql)
    {
        scom = new SqlCommand(sql, scon);
    }

    /// <summary>
    /// 初始化SqlDataAdapter对象
    /// </summary>
    /// <param name="sql">需要执行的SQL语句</param>
    private void initSqlDataAdapter(string sql)
    {
        sda = new SqlDataAdapter(sql, scon);
    }

    /// <summary>
    /// 打开数据库连接.失败时返回异常信息.
    /// </summary>
    private void openConnection()
    {
        try
        {
            if (scon.State != ConnectionState.Open)
                scon.Open();
        }
        catch (Exception ex)
        { throw ex; };
    }

    /// <summary>
    /// 关闭数据库连接.
    /// </summary>
    private void closeConnection()
    {
        if (scon.State == ConnectionState.Open)
            scon.Close();
    }

    public object sqlExecuteScalar(string inputString)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            return this.scom.ExecuteScalar();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 具有占位符的单对象查询
    /// </summary>
    /// <param name="inputString">SQL查询语句</param>
    /// <param name="parameters">占位符信息表[占位符],[数值],[数据类型]</param>
    /// <returns>返回OBJECT对象</returns>
    public object sqlExecuteScalar(string inputString, DataTable parameters)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            for (int i = 0; i < parameters.Rows.Count; i++)
            {
                switch (parameters.Rows[i][2].ToString())
                {
                    case "int":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Int).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "varchar":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.VarChar).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "datetime":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.DateTime).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "bit":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Bit).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "float":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Float).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "binary":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Binary).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                }
            }
            return this.scom.ExecuteScalar();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 以SqlDataAdapter形式返回结果集,适用于多列查询
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <returns>返回DataTable集合</returns>
    public DataTable sqlExcuteQueryTable(string inputString)
    {
        try
        {
            this.openConnection();
            this.initSqlDataAdapter(addUse(inputString));
            DataTable tempTable = new DataTable();
            this.sda.Fill(tempTable);
            return tempTable;
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 有占位符的SqlDataAdapter形式查询
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <param name="parameters">占位符信息表[占位符],[数值],[数据类型]</param>
    /// <returns>返回DataTable集合</returns>
    public DataTable sqlExcuteQueryTable(string inputString, DataTable parameters)
    {
        try
        {
            this.openConnection();
            this.initSqlDataAdapter(addUse(inputString));
            DataTable tempTable = new DataTable();
            for (int i = 0; i < parameters.Rows.Count; i++)
            {
                switch (parameters.Rows[i][2].ToString())
                {
                    case "int":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Int).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "varchar":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.VarChar).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "datetime":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.DateTime).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "bit":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Bit).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "float":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Float).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "binary":
                        this.sda.SelectCommand.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Binary).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                }
            }
            this.sda.Fill(tempTable);
            return tempTable;
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 以SqlDataAdapter存储过程形式返回结果集
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <param name="parTable">输入类参数占位符信息表[占位符],[数值],[数据类型]</param>
    /// <param name="outTable">输出类参数占位符信息表[占位符],[数值],[数据类型]</param>
    /// <returns>返回DataTable集合</returns>
    public DataSet sqlExcuteQueryDataSet(string procText, DataTable parTable, DataTable outTable)
    {
        try
        {
            this.openConnection();
            this.scon.ChangeDatabase(memberStatic.clientDataBase);
            this.initSqlCommand();
            this.scom.CommandText = procText;
            this.scom.CommandType = CommandType.StoredProcedure;
            this.sda = new SqlDataAdapter();
            this.sda.SelectCommand = this.scom;
            //设置输入参数
            for (int i = 0; i < parTable.Rows.Count; i++)
            {
                switch (parTable.Rows[i][2].ToString())
                {
                    case "int":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.Int).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                    case "varchar":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.VarChar).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                    case "datetime":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.DateTime).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                    case "bit":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.Bit).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                    case "float":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.Float).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                    case "binary":
                        this.scom.Parameters.Add(parTable.Rows[i][0].ToString(), SqlDbType.Binary).Value = parTable.Rows[i][1] == null ? System.DBNull.Value : parTable.Rows[i][1];
                        break;
                }
            }
            //设置输出参数
            for (int i = 0; i < outTable.Rows.Count; i++)
            {
                switch (outTable.Rows[i][1].ToString())
                {
                    case "int":
                        this.scom.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.Int).Direction = ParameterDirection.Output;
                        break;
                    case "varchar":
                        this.scom.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.VarChar).Direction = ParameterDirection.Output;
                        break;
                    case "datetime":
                        this.scom.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.DateTime).Direction = ParameterDirection.Output;
                        break;
                    case "bit":
                        this.scom.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.Bit).Direction = ParameterDirection.Output;
                        break;
                    case "float":
                        this.scom.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.Float).Direction = ParameterDirection.Output;
                        break;
                    case "binary":
                        this.sda.SelectCommand.Parameters.Add(outTable.Rows[i][0].ToString(), SqlDbType.Binary).Direction = ParameterDirection.Output;
                        break;
                }
            }
            DataSet rds = new DataSet();
            DataTable tempTable = new DataTable();
            this.sda.Fill(tempTable);
            rds.Tables.Add(tempTable);

            DataTable output = new DataTable();
            output.Columns.Add("valueCol", typeof(string));
            for (int i = 0; i < outTable.Rows.Count; i++)
            {
                DataRow dr = output.NewRow();
                dr[0] = this.scom.Parameters[outTable.Rows[i][0].ToString()].Value;
                output.Rows.Add(dr);
            }
            rds.Tables.Add(output);
            return rds;
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 以SQLDATAREADER形式返回结果集,适用于单列查询
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <returns>返回ArrayList集合</returns>
    public ArrayList sqlExcuteQueryList(string inputString)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            this.sdr = this.scom.ExecuteReader();
            ArrayList al = new ArrayList();
            if (this.sdr.Read())
            {
                do
                {
                    al.Add(this.sdr.GetValue(0));
                } while (this.sdr.Read());
            }
            this.sdr.Close();
            return al;
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 返回INT值的SQL语句执行
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <returns>返回所影响的行数</returns>
    public int sqlExcuteNonQueryInt(string inputString)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            int r = this.scom.ExecuteNonQuery();
            return r;
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// SQL语句执行,无返回值
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <param name="isProc">是否执行存储过程</param>
    public void sqlExcuteNonQuery(string inputString, bool isProc)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            if (isProc)
                this.scom.CommandType = CommandType.StoredProcedure;
            this.scom.ExecuteNonQuery();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }
    /// <summary>
    /// 新增加的就是为了创建新增视图用的
    /// </summary>
    /// <param name="inputStr"></param>
    public void sqlExcuteNonQuery(string inputStr)
    {
        try
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = memberStatic.connectionString.Replace("master", memberStatic.clientDataBase);
                SqlCommand cmd = new SqlCommand(inputStr, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }
    /// <summary>
    /// 返回数据库受影响的行数
    /// </summary>
    /// <param name="inputString"></param>
    /// <param name="isProc"></param>
    /// <returns></returns>
    public int sqlExcuteNonQueryInt(string inputString, bool isProc)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            if (isProc)
                this.scom.CommandType = CommandType.StoredProcedure;
            return this.scom.ExecuteNonQuery();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 带有占位符的SQL语句执行
    /// </summary>
    /// <param name="inputString">SQL语句</param>
    /// <param name="parameters">占位符信息表[占位符],[数值],[数据类型]</param>
    public void sqlExcuteNonQuery(string inputString, DataTable parameters)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(addUse(inputString));
            for (int i = 0; i < parameters.Rows.Count; i++)
            {
                switch (parameters.Rows[i][2].ToString())
                {
                    case "int":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Int).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "varchar":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.VarChar).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "datetime":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.DateTime).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "bit":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Bit).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "float":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Float).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                    case "binary":
                        this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Binary).Value = parameters.Rows[i][1] == null ? System.DBNull.Value : parameters.Rows[i][1];
                        break;
                }
            }
            this.scom.ExecuteNonQuery();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// SQL存储过程执行，无返回值
    /// </summary>
    /// <param name="procName">存储过程名称</param>
    /// <param name="parameters">存储过程参数信息表[参数名称],[参数值],[参数类型]</param>
    public void sqlExcuteNonQueryByProc(string procName, DataTable parameters)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand(procName);
            this.scom.CommandType = CommandType.StoredProcedure;
            if (parameters.Rows.Count > 0)
            {
                for (int i = 0; i < parameters.Rows.Count; i++)
                {
                    switch (parameters.Rows[i][2].ToString())
                    {
                        case "int":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Int);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                        case "varchar":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.VarChar);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                        case "datetime":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.DateTime);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                        case "bit":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Bit);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                        case "float":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Float);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                        case "binary":
                            this.scom.Parameters.Add(parameters.Rows[i][0].ToString(), SqlDbType.Binary);
                            this.scom.Parameters[parameters.Rows[i][0].ToString()].Value = parameters.Rows[i][1];
                            break;
                    }
                }

            }

            this.scom.ExecuteNonQuery();
        }
        catch (Exception ex)
        { throw ex; }
        finally { this.closeConnection(); }
    }


    /// <summary>
    /// 多SQL语句执行
    /// </summary>
    /// <param name="al">SQL语句集合</param>
    /// <param name="isTrans">是否启用SQL事物</param>
    public void sqlExcuteNonQuery(ArrayList al, bool isTrans)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand();
            if (isTrans)
            {
                this.tran = this.scon.BeginTransaction();
                for (int i = 0; i < al.Count; i++)
                {
                    this.scom.CommandText = addUse(al[i].ToString());
                    this.scom.Transaction = this.tran;
                    this.scom.ExecuteNonQuery();
                }
                this.tran.Commit();
            }
            else
            {
                for (int i = 0; i < al.Count; i++)
                {
                    this.scom.CommandText = addUse(al[i].ToString());
                    this.scom.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            if (isTrans)
                this.tran.Rollback();
            throw ex;
        }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 多SQL语句执行含带事务,存储过程
    /// </summary>
    /// <param name="dt">SQL语句表.第二列为语句是否按存储过程执行</param>
    /// <param name="isTrans">是否启用SQL事物</param>
    public void sqlExcuteNonQuery(DataTable dt, bool isTrans)
    {
        try
        {
            this.openConnection();
            this.initSqlCommand();
            if (isTrans)
            {
                this.tran = this.scon.BeginTransaction();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.scom.CommandText = addUse(dt.Rows[i][0].ToString());
                    this.scom.Transaction = this.tran;
                    if (dt.Rows[i][1].ToString() == "True")
                        this.scom.CommandType = CommandType.StoredProcedure;
                    this.scom.ExecuteNonQuery();
                }
                this.tran.Commit();
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.scom.CommandText = addUse(dt.Rows[i][0].ToString());
                    if (dt.Rows[i][1].ToString() == "True")
                        this.scom.CommandType = CommandType.StoredProcedure;
                    this.scom.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            if (isTrans)
                this.tran.Rollback();
            throw ex;
        }
        finally { this.closeConnection(); }
    }

    /// <summary>
    /// 整表导入数据库
    /// </summary>
    /// <param name="tableName">目标表名</param>
    /// <param name="innerTable">源数据集</param>
    public string sqlExcuteBulkCopy(string tableName, DataTable innerTable)
    {
        try
        {
            this.openConnection();
            sbc = new SqlBulkCopy(scon);
            sbc.DestinationTableName = memberStatic.clientDataBase + ".dbo." + tableName;
            sbc.BulkCopyTimeout = 3600;
            sbc.WriteToServer(innerTable);
            return "OK";
        }
        catch (Exception ex)
        { return ex.Message; }
        //finally { SQL_Static.closeConnection(); }
        finally { this.closeConnection(); }
    }
    /// <summary>
    /// 切换数据库至DM数据库
    /// </summary>
    /// <param name="ins">原SQL语句</param>
    /// <returns>增加USE语句后的结果</returns>
    private string addUse(string ins)
    {
        if (ins.IndexOf("use") == 0)//如果value为空才是返回0 匹配的原则是一个完整的单词，如果是一样的那就是完全匹配的
            return ins;
        else
        {
            if (ins.IndexOf("empoxCancel") >= 0)
            {
                ins = ins.Replace("empoxCancel", "");
                return ins;
            }
            else
                return "use " + memberStatic.clientDataBase + " " + ins;
        }
    }

    /// <summary>
    /// 返回占位符信息表
    /// </summary>
    /// <returns>占位符信息表</returns>
    public DataTable getParamTable()
    {
        DataTable paramTable = new DataTable();
        paramTable.Columns.Add("text", typeof(string));
        paramTable.Columns.Add("value", typeof(string));
        paramTable.Columns.Add("type", typeof(string));
        return paramTable;
    }
}
