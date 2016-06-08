using System;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;

public class Default : Page
{
    private clsTimeLogList mTimeLogList = new clsTimeLogList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string action = Request.QueryString["action"];

            switch (action)
            {
                case "add": add(); break;

                case "update": update(); break;

                case "delete": delete(); break;

                case "getAllEntries": getAllEntries(); break;

                default:
                    throw new Exception("Invalid request");
            }
        }
        catch (ApplicationException ex)
        {
            Response.Write("error\t" + ex.GetType() + "\t" + ex.Message);
        }
        catch (Exception ex)
        {
            Response.Write("error\t" + ex.GetType() + "\t" + ex.Message);
        }
    }

    private void add()
    {
        clsTimeLogList.UserID = Request.QueryString["userID"];

        string serializedEntry = Request.QueryString["serializedEntry"];
        clsTimeLogEntry entry = new clsTimeLogEntry();
        entry.Deserialize(serializedEntry);
        mTimeLogList.Add(entry);

        string responseString = entry.Serialize();
        Response.Write(responseString);
    }

    private void update()
    {
        clsTimeLogList.UserID = Request.QueryString["userID"];
 
        string serializedEntry = Request.QueryString["serializedEntry"];
        clsTimeLogEntry entry = new clsTimeLogEntry();
        entry.Deserialize(serializedEntry);
        mTimeLogList.Update(entry);

        string responseString = entry.Serialize();
        Response.Write(responseString);
    }

    private void delete()
    {
        clsTimeLogList.UserID = Request.QueryString["userID"];

        string serializedEntry = Request.QueryString["serializedEntry"];
        clsTimeLogEntry entry = new clsTimeLogEntry();
        entry.Deserialize(serializedEntry);
        mTimeLogList.Delete(entry);

        string responseString = "ok";
        Response.Write(responseString);
    }

    private void getAllEntries()
    {
        TimeLogDate beginDate = TimeLogDate.Parse(Request.QueryString["beginDate"]);
        TimeLogDate endDate = TimeLogDate.Parse(Request.QueryString["endDate"]);

        List<clsTimeLogEntry> entries = mTimeLogList.GetAllEntries(beginDate, endDate);

        string responseString = "";

        foreach (clsTimeLogEntry entry in entries)
        {
            responseString += entry.Serialize() + "\n";
        }

        Response.Write(responseString);
    }

    private void ping()
    {
        Response.Write("ok");
    }

    private string requestString
    {
        get
        {
            Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);

            byte[] requestContents = new byte[Request.ContentLength];
            Request.InputStream.Read(requestContents, 0, Request.ContentLength);
            Encoding encoding = new ASCIIEncoding();
            string requestString = encoding.GetString(requestContents);

            return requestString;
        }
    }
}
