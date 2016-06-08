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
}