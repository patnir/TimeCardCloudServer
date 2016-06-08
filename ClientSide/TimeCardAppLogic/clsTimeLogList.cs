using System.Collections.Generic;

public class clsTimeLogList 
{
    public static string mUserID;

    public string UserID
    {
        set
        {
            mUserID = value;
            clsServer.UserID = value;
        }
    }

    public List<clsTimeLogEntry> GetAllEntries(TimeLogDate beginDate, TimeLogDate endDate)
    {
        List<clsTimeLogEntry> entries = new List<clsTimeLogEntry>();
        entries = clsServer.GetAllEntries(beginDate, endDate);
        return entries;
    }

    public void Update(clsTimeLogEntry entry)
    {
        clsServer.Update(entry);
    }

    public void Delete(clsTimeLogEntry entry)
    {
        clsServer.Delete(entry);
    }

    public void Add(clsTimeLogEntry entry)
    {
        clsServer.Add(entry);
    }
}