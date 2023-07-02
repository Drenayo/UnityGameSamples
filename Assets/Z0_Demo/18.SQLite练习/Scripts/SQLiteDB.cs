using UnityEngine;
using System;
using System.Collections;
using Mono.Data.Sqlite;

/// <summary>
/// SQLite数据库操作类
/// </summary>
public class SQLiteDB
{
	private SqliteConnection conn; // SQLite连接
	private SqliteCommand cmd; // SQLite命令
	private SqliteDataReader reader;

	public SQLiteDB (string connectionString)	
	{
		OpenDB (connectionString);
	}

	public SQLiteDB (){ }

	/// <summary>
    /// 打开数据库
    /// </summary>
    /// <param name="connectionString"></param>
	public void OpenDB (string connectionString)		
	{
		try
		{
			conn = new SqliteConnection (connectionString);			
			conn.Open ();			
			Debug.Log ("Connected to db,连接数据库成功！");
		}
		catch(Exception e)
		{
			string temp1 = e.ToString();
			Debug.Log(temp1);
		}		
	}

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
	public void CloseSqlConnection ()	
	{
		if (cmd != null) { cmd.Dispose (); cmd = null; }						
		if (reader != null) { reader.Dispose (); reader = null;}					
		if (conn != null) {	conn.Close (); conn = null;}			
		Debug.Log ("Disconnected from db.关闭数据库！");	
	}	

	/// <summary>
    /// 执行SQL语句
    /// </summary>
    /// <param name="sqlQuery"></param>
    /// <returns></returns>
	public SqliteDataReader ExecuteQuery ( string sqlQuery )		
	{
        Debug.Log( "ExecuteQuery:: " + sqlQuery );
        cmd = conn.CreateCommand ();
		cmd.CommandText = sqlQuery;
		reader = cmd.ExecuteReader ();
		return reader;
	}
	
	/// <summary>
	/// 查询表中全部数据 param tableName=表名 
	/// </summary>
	public SqliteDataReader ReadFullTable (string tableName)		
	{
		string query = "SELECT * FROM " + tableName;
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 插入数据 param tableName=表名 values=插入数据内容
	/// </summary>
	public SqliteDataReader InsertInto (string tableName, string[] values)		
	{
		string query = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for (int i = 1; i < values.Length; ++i) {	
			query += ", " + values[i];	
		}
		query += ")";
		return ExecuteQuery (query);
	}	

	/// <summary>
	/// 更新数据 param tableName=表名 cols=更新字段 colsvalues=更新内容 selectkey=查找字段（主键) selectvalue=查找内容
	/// </summary>
	public SqliteDataReader UpdateInto (string tableName, string []cols,string []colsvalues,string selectkey,string selectvalue)
	{
		string query = "UPDATE "+tableName+" SET "+cols[0]+" = "+colsvalues[0];
		for (int i = 1; i < colsvalues.Length; ++i) {
			query += ", " +cols[i]+" ="+ colsvalues[i];
		}		
		query += " WHERE "+selectkey+" = "+selectvalue+" ";		
		return ExecuteQuery (query);
	}
	
	/// <summary>
	/// 删除数据 param tableName=表名 cols=字段 colsvalues=内容
	/// </summary>
	public SqliteDataReader Delete(string tableName,string []cols,string []colsvalues)
	{
		string query = "DELETE FROM "+tableName + " WHERE " +cols[0] +" = " + colsvalues[0];		
		for (int i = 1; i < colsvalues.Length; ++i) {			
			query += " or " +cols[i]+" = "+ colsvalues[i];
		}
		return ExecuteQuery (query);
	}	

	/// <summary>
	/// 插入数据 param tableName=表名 cols=插入字段 value=插入内容
	/// </summary>
	public SqliteDataReader InsertIntoSpecific (string tableName, string[] cols, string[] values)		
	{
		if (cols.Length != values.Length) {	
			throw new SqliteException ("columns.Length != values.Length");
		}
		string query = "INSERT INTO " + tableName + "(" + cols[0];
		for (int i = 1; i < cols.Length; ++i) {	
			query += ", " + cols[i];	
		}
		query += ") VALUES (" + values[0];
		for (int i = 1; i < values.Length; ++i) {	
			query += ", " + values[i];	
		}		
		query += ")";		
		return ExecuteQuery (query);		
	}

	/// <summary>
	/// 删除表中全部数据
	/// </summary>
	public SqliteDataReader DeleteContents (string tableName)		
	{	
		string query = "DELETE FROM " + tableName;
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 创建表 param name=表名 col=字段名 colType=字段类型
	/// </summary>
	public SqliteDataReader CreateTable (string name, string[] col, string[] colType)
	{
		if (col.Length != colType.Length) {
			throw new SqliteException ("columns.Length != colType.Length");
		}
		string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];		
		for (int i = 1; i < col.Length; ++i) {	
			query += ", " + col[i] + " " + colType[i];	
		}
		query += ")";		
		return ExecuteQuery (query);		
	}

	/// <summary>
	/// 按条件查询数据 param tableName=表名 items=查询字段 col=查找字段 operation=运算符 values=内容
	/// </summary>
	public SqliteDataReader SelectWhere (string tableName, string[] items, string[] col, string[] operation, string[] values)	
	{	
		if (col.Length != operation.Length || operation.Length != values.Length) {	
			throw new SqliteException ("col.Length != operation.Length != values.Length");	
		}
		string query = "SELECT " + items[0];
		for (int i = 1; i < items.Length; ++i) {	
			query += ", " + items[i];
		}
		query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";
		for (int i = 1; i < col.Length; ++i) {
			query += " AND " + col[i] + operation[i] + "'" + values[i] + "' ";
		}
        return ExecuteQuery (query);
	}

	/// <summary>
	/// 查询表
	/// </summary>
	public SqliteDataReader Select(string tableName, string col, string values)
	{
		string query = "SELECT * FROM " + tableName  + " WHERE " + col + " = " + values;
		return ExecuteQuery (query);
	}
	public SqliteDataReader Select(string tableName, string col,string operation, string values)
	{
		string query = "SELECT * FROM " + tableName  + " WHERE " + col + operation + values;
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 升序查询
	/// </summary>
	public SqliteDataReader SelectOrderASC (string tableName,string col)
	{
		string query = "SELECT * FROM " + tableName  + " ORDER BY " + col + " ASC";
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 降序查询
	/// </summary>
	public SqliteDataReader SelectOrderDESC (string tableName,string col)
	{
		string query = "SELECT * FROM " + tableName  + " ORDER BY " + col + " DESC";
		return ExecuteQuery (query);
	}

	/// <summary>
	/// 查询表行数
	/// </summary>
	public SqliteDataReader SelectCount(string tableName)
	{
		string query = "SELECT COUNT(*) FROM " + tableName;
		return ExecuteQuery (query);
	}
}