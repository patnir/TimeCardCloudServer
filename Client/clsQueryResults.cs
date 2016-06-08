using System;
using System.Data.SqlClient;

// This class works with clsDatabase to encapsulate the ADO.NET OleDb provider 
// to create a layer of abstraction between our application code and ADO.NET.  This
// allows us to change ADO.NET providers without affecting our application code.

public class clsQueryResults : IDisposable
{
	private bool mEOF;
	private SqlDataReader mDataReader = null;

    public void Dispose()
    {
        if (mDataReader != null)
        {
            mDataReader.Close();
        }
    }

	// Returns true when there are no more records/rows in the query results.
	public bool EOF
	{
		get
		{
			return mEOF;
		}
	}
	
	// Performs a query and sets the current record/row pointer to the first one.
	public void Open(clsDatabase db, string sqlQuery)
	{
		mDataReader = db.ExecuteQuery(sqlQuery);
		MoveNext();
	}

	// Moves to the next record/row and sets the EOF property.
	public void MoveNext()
	{
		if (mDataReader.Read() == true)
		{
			mEOF = false;
		}
		else
		{
			mEOF = true;
		}
	}

	// Closes the query results.
	public void Close()
	{
		mDataReader.Close();
	}

	// Returns a reference to the value of the column name passed.  If
	// the value is null, a reasonable non-null value is returned instead.
	public object GetColValue(string columnName)
	{
        int columnNumber = mDataReader.GetOrdinal(columnName);

        if (mDataReader.IsDBNull(columnNumber) == false)
        {
            return mDataReader.GetValue(columnNumber);
        }
        else
        {
            switch (mDataReader.GetDataTypeName(columnNumber))
            {
                case "DBTYPE_I4": return 0; // int
                case "DBTYPE_DATE": return DateTime.MaxValue;
                case "DBTYPE_R4": return 0.00f;
                case "DBTYPE_WLONGVARCHAR": return "";
                case "DBTYPE_WVARCHAR": return "";

                default:
                    throw new Exception ("Unexpected database type " 
                        + columnName + " = " + mDataReader.GetDataTypeName(columnNumber));
            }
        }
	}
}
