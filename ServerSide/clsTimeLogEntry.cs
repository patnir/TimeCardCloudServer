using System;

public class clsTimeLogEntry
{
    private int mEntryID;
    public string EmployeeID;
    public double HoursWorked;
    public TimeLogDate DateWorked;
    public string Description;
    public bool Billable;
    public DateTime DateTimeLastMaint;

    public string Serialize()
    {
        return mEntryID.ToString() + "\t"
            + EmployeeID + "\t"
            + HoursWorked.ToString() + "\t"
            + DateWorked.ToShortDateString() + "\t"
            + Description + "\t"
            + Billable.ToString() + "\t"
            + DateTimeLastMaint.ToString();
    }

    public void Deserialize(string serializedString)
    {
        string[] states = serializedString.Split('\t');
        mEntryID = int.Parse(states[0]);
        EmployeeID = states[1];
        HoursWorked = double.Parse(states[2]);
        DateWorked = TimeLogDate.Parse(states[3]);
        Description = states[4];
        Billable = bool.Parse(states[5]);
        DateTimeLastMaint = DateTime.Parse(states[6]);
    }

    public void Update(clsDatabase db, string userID)
    {
        using (clsQueryResults results = new clsQueryResults())
        {
            string sql1 = "SELECT * FROM TimeLogEntries WHERE EntryID = " + db.ToSql(mEntryID);
            results.Open(db, sql1);
            DateTime lastMaint = (DateTime)results.GetColValue("DateTimeLastMaint");

            if (lastMaint != DateTimeLastMaint)
            {
                throw new Exception("Entry has been modified since retrieval from the database.");
            }
        }

        DateTimeLastMaint = DateTime.Now;

        string sql2 = "UPDATE TimeLogEntries SET "
            + "EmployeeID = " + db.ToSql(EmployeeID)
            + ", DateWorked = " + db.ToSql(DateWorked)
            + ", HoursWorked = " + db.ToSql(HoursWorked)
            + ", Billable = " + db.ToSql(Billable)
            + ", Description = " + db.ToSql(Description)
            + ", DateTimeLastMaint = " + db.ToSql(DateTimeLastMaint)
            + " WHERE EntryID = " + db.ToSql(mEntryID);
        db.ExecuteSQL(sql2);
        addTimeLogHistory(sql2, userID, db);
    }

    public void Add(clsDatabase db, string userID)
    {
        DateTimeLastMaint = DateTime.Now;
        string sql = "INSERT INTO TimeLogEntries ("
                + "EmployeeID, "
                + "DateWorked, "
                + "HoursWorked, "
                + "Billable, "
                + "Description, "
                + "DateTimeLastMaint"

                + ") VALUES ("

                + db.ToSql(EmployeeID) + ", "
                + db.ToSql(DateWorked) + ", "
                + db.ToSql(HoursWorked) + ", "
                + db.ToSql(Billable) + ", "
                + db.ToSql(Description) + ", "
                + db.ToSql(DateTimeLastMaint)

                + ")";

        db.ExecuteSQL(sql);
        addTimeLogHistory(sql, userID, db);

        mEntryID = getEntryID(db);
    }

    private int getEntryID(clsDatabase db)
    {
        using (clsQueryResults results = new clsQueryResults())
        {
            string sqlGetEntryID = "SELECT MAX(EntryID) AS entryID FROM TimeLogEntries WHERE "
                + "EmployeeID=" + db.ToSql(EmployeeID) + " AND "
                + "DateWorked=" + db.ToSql(DateWorked) + " AND "
                + "HoursWorked=" + db.ToSql(HoursWorked) + " AND "
                + "Billable=" + db.ToSql(Billable) + " AND "
                + "Description=" + db.ToSql(Description) + " AND "
                + "DateTimeLastMaint=" + db.ToSql(DateTimeLastMaint) + ";";

            results.Open(db, sqlGetEntryID);
            int entryID = (int)results.GetColValue("entryID");

            return entryID;
        }
    }

    public void Delete(clsDatabase db, string userID)
    {
        string sql = "DELETE FROM TimeLogEntries WHERE EntryID = " + db.ToSql(mEntryID);
        db.ExecuteSQL(sql);
        addTimeLogHistory(sql, userID, db);
    }

    public void RestoreStateFromQuery(clsQueryResults results)
    {
        mEntryID = (int)results.GetColValue("EntryID");
        EmployeeID = (string)results.GetColValue("EmployeeID");
        HoursWorked = (double)results.GetColValue("HoursWorked");
        DateTime date = (DateTime)results.GetColValue("DateWorked");
        DateWorked = new TimeLogDate(date.Year, date.Month, date.Day);
        Description = (string)results.GetColValue("Description");
        Billable = (bool)results.GetColValue("Billable");
        DateTimeLastMaint = (DateTime)results.GetColValue("DateTimeLastMaint");
    }

    public void addTimeLogHistory(string sqlCommand, string userID, clsDatabase db)
    {     
            string sql = "INSERT INTO TimeLogHistory ("
                + "HistoryDateTime, "
                + "UserID, "
                + "SqlCommand "

                + ") VALUES ("

                + db.ToSql(DateTime.Now.Date) + ", "
                + db.ToSql(userID) + ", "
                + db.ToSql(sqlCommand)

                + ")";

            db.ExecuteSQL(sql);
    }
}