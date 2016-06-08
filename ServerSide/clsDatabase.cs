using System;
using System.Data.SqlClient;

public class clsDatabase : IDisposable
{    
    private SqlConnection mDBConn = null;
    private SqlTransaction mDBTrans = null;

    public clsDatabase(string connectionString)
    {
        mDBConn = new SqlConnection();
        mDBConn.ConnectionString = connectionString;
    }

    public void Dispose()
    {
        if (mDBConn != null)
        {
            mDBConn.Close();
        }
    }

    // Opens the database connection.
    public void Open()
    {
        mDBConn.Open();
    }
    
    // Closes the database connection.
	public void Close()
    {
        mDBConn.Close();
    }
    
    // Executes an action SQL statement and returns the number of rows affected.
	public int ExecuteSQL(string sqlStatement)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = mDBConn;
        cmd.Transaction = mDBTrans;
        cmd.CommandText = sqlStatement;
        return cmd.ExecuteNonQuery();
    }
    
    // Executes a query and returns a reference to the data reader object.  Note
	// this method should only be called by clsQueryResults.
	public SqlDataReader ExecuteQuery(string sqlQuery)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = mDBConn;
        cmd.Transaction = mDBTrans;
        cmd.CommandText = sqlQuery;
        return cmd.ExecuteReader();
    }

    // These methods are necessary because not all database engines want values 
    // formatted in the SQL statements the same way.

    public string ToSql(bool boolValue)
    {
        if (boolValue == true)
        {
            return "1";
        }

        return "0";
    }

    public string ToSql(TimeLogDate dateValue)
    {
        return "'" + dateValue.Year.ToString() + "-" + dateValue.Month.ToString() + "-" + dateValue.Day.ToString() + "'";
    }

    public string ToSql(DateTime dateTimeValue)
    {
        return "'" + dateTimeValue.ToString("yyyy-MM-dd hh:mm:ss") + "'";
    }

    public string ToSql(int intValue)
    {
        return intValue.ToString();
    }

    public string ToSql(string stringValue)
    {
        return "'" + stringValue.Replace("'", "''") + "'";
    }

    public string ToSql(double doubleValue)
    {
        return doubleValue.ToString();
    }

    public void BeginTransaction()
    {
        mDBTrans = mDBConn.BeginTransaction();
    }

    public void CommitTransaction()
    {
        mDBTrans.Commit();
    }

    public void RollbackTransaction()
    {
        mDBTrans.Rollback();
    }
}