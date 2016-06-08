using System;

public struct TimeLogDate
{
    public int Year;
    public int Month;
    public int Day;

    public TimeLogDate(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public static TimeLogDate Parse(string dateString)
    {
        try
        {
            TimeLogDate timeLogDate = new TimeLogDate();
            DateTime date = DateTime.Parse(dateString);
            timeLogDate.Year = date.Year;
            timeLogDate.Month = date.Month;
            timeLogDate.Day = date.Day;
            return timeLogDate;
        }
        catch
        {
            throw new Exception("String is not a valid date.");
        }
    }

    public static bool TryParse(string dateString, out TimeLogDate timeLogDate)
    {
        timeLogDate = new TimeLogDate();
        try
        {
            TimeLogDate date = TimeLogDate.Parse(dateString);
            timeLogDate = date;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string ToShortDateString()
    {
        if (isEmpty() == true)
        {
            return "";
        }

        return Month.ToString() + "/" + Day.ToString() + "/" + Year.ToString();
    }

    public bool isEmpty()
    {
        if (Year == 0 && Month == 0 && Day == 0)
        {
            return true;
        }

        return false;
    }

    public string Serialize()
    {
        return Year.ToString() + '\t'
            + Month.ToString() + '\t'
            + Day.ToString();
    }

    public void Deserialize(string serializedEntry)
    {
        string[] values = serializedEntry.Split('\t');

        Year = int.Parse(values[0]);
        Month = int.Parse(values[1]);
        Day = int.Parse(values[2]);
    }
}