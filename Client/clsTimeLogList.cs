using System.Collections.Generic;

public class clsTimeLogList 
{
    public static string UserID;
    private string mConnectionString = "Database=TimeCard5000;Server=SUNITA\\SQLEXPRESS;Trusted_Connection=True;";


    public List<clsTimeLogEntry> GetAllEntries(TimeLogDate beginDate, TimeLogDate endDate)
    {
        List<clsTimeLogEntry> listBetweenDates = new List<clsTimeLogEntry>();
        string sql;

        using (clsDatabase db = new clsDatabase(mConnectionString))
        using (clsQueryResults results = new clsQueryResults())
        {
            db.Open();

            sql = "SELECT COUNT(*) AS NumRecords FROM TimeLogEntries WHERE DateWorked >= "
                + db.ToSql(beginDate)
                + " AND DateWorked <= " + db.ToSql(endDate);

            results.Open(db, sql);
            int count = (int)results.GetColValue("NumRecords");

            if (count > 500)
            {
                throw new clsTooManyRecordsException("More than 500 matching records found.");
            }

            results.Close();

            sql = "SELECT * FROM TimeLogEntries WHERE DateWorked >= "
            + db.ToSql(beginDate)
            + " AND DateWorked <=" + db.ToSql(endDate)
            + " ORDER BY DateWorked DESC, EmployeeID, HoursWorked";

            results.Open(db, sql);

            while (results.EOF == false)
            {
                clsTimeLogEntry entry = new clsTimeLogEntry();
                entry.RestoreStateFromQuery(results);
                listBetweenDates.Add(entry);
                results.MoveNext();
            }
        }

        return listBetweenDates;
    }

    public void Update(clsTimeLogEntry entry)
    {
        using(clsDatabase db = new clsDatabase(mConnectionString))
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                entry.Update(db, UserID);
                db.CommitTransaction();
            }
            catch
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                }
                throw;
            }
        }
    }

    public void Delete(clsTimeLogEntry entry)
    {
        using(clsDatabase db = new clsDatabase(mConnectionString))
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                entry.Delete(db, UserID);
                db.CommitTransaction();
            }
            catch
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                }
                throw;
            }
        }
    }

    public void Add(clsTimeLogEntry entry)
    {
        using (clsDatabase db = new clsDatabase(mConnectionString))
        {
            try
            {
                db.Open();
                db.BeginTransaction();
                entry.Add(db, UserID);
                db.CommitTransaction();
            }
            catch
            {
                if (db != null)
                {
                    db.RollbackTransaction();
                }
                throw;
            }
        }
    }
}