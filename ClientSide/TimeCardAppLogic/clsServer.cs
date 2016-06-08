using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

public class clsServer
{
    public static string UrlRoot = "http://localhost:1334/";

    public static string UserID;

    public static int Timeout = 300000;

    public static bool Ping()
    {
        string url = UrlRoot + "Default.aspx?action=ping";

        string responseString = getResponseString(url);

        if (responseString == "ok")
        {
            return true;
        }

        return false;
    }

    public static List<clsTimeLogEntry> GetAllEntries(TimeLogDate beginDate, TimeLogDate endDate)
    {
        string action = "getAllEntries";
        string arguments = "&beginDate=" + beginDate.ToShortDateString() + "&endDate=" + endDate.ToShortDateString();
        string url = createUrl(action, arguments);
        string responseString = getResponseString(url);

        List<clsTimeLogEntry> entries = new List<clsTimeLogEntry>();

        string[] serializedResponses = responseString.Split('\n');

        foreach (string serializedResponse in serializedResponses)
        {
            if (serializedResponse != "")
            {
                clsTimeLogEntry entry = new clsTimeLogEntry();
                entry.Deserialize(serializedResponse);
                entries.Add(entry);
            }
        }

        return entries;
    }

    public static void Add(clsTimeLogEntry entry)
    {
        string action = "add";
        string arguments = "&serializedEntry=" + entry.Serialize();
        string url = createUrl(action, arguments);
        
        string responseString = getResponseString(url);

        entry.Deserialize(responseString);
    }

    public static void Update(clsTimeLogEntry entry)
    {
        string action = "update";
        string arguments = "&serializedEntry=" + entry.Serialize();
        string url = createUrl(action, arguments);

        string responseString = getResponseString(url);

        entry.Deserialize(responseString);
    }

    public static void Delete(clsTimeLogEntry entry)
    {
        string action = "delete";
        string arguments = "&serializedEntry=" + entry.Serialize();
        string url = createUrl(action, arguments);

        string responseString = getResponseString(url);

        if (responseString != "ok")
        {
            throw new Exception("Unable to delete entry");
        }
    }

    private static string createUrl(string action, string arguments)
    {
        return UrlRoot 
            + "Default.aspx?action=" + action 
            + "&userID=" + UserID
            + arguments;
    }

    private static string getResponseString(string url, string stringToSend)
    {
        byte[] responseByteArray;
        string responseString;

        System.Text.Encoding encoding = new System.Text.ASCIIEncoding();
        byte[] bytesToSend = encoding.GetBytes(stringToSend);

        sendRequest(url, bytesToSend, out responseByteArray, out responseString);

        return responseString;
    }

    private static string getResponseString(string url, byte[] bytesToSend = null)
    {
        byte[] responseByteArray;
        string responseString;

        sendRequest(url, bytesToSend, out responseByteArray, out responseString);

        return responseString;
    }

    private static byte[] getResponseByteArray(string url, byte[] bytesToSend = null)
    {
        byte[] responseByteArray;
        string responseString;

        sendRequest(url, bytesToSend, out responseByteArray, out responseString);

        return responseByteArray;
    }

    private static void sendRequest(string url, byte[] bytesToSend = null)
    {
        byte[] responseByteArray;
        string responseString;

        sendRequest(url, bytesToSend, out responseByteArray, out responseString);
    }

    private static void sendRequest(string url, byte[] bytesToSend, out byte[] responseByteArray, out string responseString)
    {
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

        httpRequest.Timeout = Timeout;
        httpRequest.Proxy = null;
        httpRequest.AllowWriteStreamBuffering = false;

        if (bytesToSend == null)
        {
            httpRequest.Method = WebRequestMethods.Http.Get;
        }
        else
        {
            httpRequest.Method = WebRequestMethods.Http.Post;
            httpRequest.ContentLength = bytesToSend.Length;
            httpRequest.ContentType = @"application/octet-stream";
            BinaryWriter bw = new BinaryWriter(httpRequest.GetRequestStream());
            bw.Write(bytesToSend);
        }

        HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

        BinaryReader br = new BinaryReader(httpResponse.GetResponseStream());

        responseByteArray = br.ReadBytes((int)httpResponse.ContentLength);
        responseString = Encoding.ASCII.GetString(responseByteArray);

        if (responseString.StartsWith("error\t") == true)
        {
            string[] fields = responseString.Split('\t');

            if (fields[1] == "System.ApplicationException")
            {
                throw new ApplicationException(fields[2]);
            }

            throw new Exception(string.Format("An unexpected error was returned from the server. {0}", fields[2]));
        }
    }

}
