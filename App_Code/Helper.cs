using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DateTime PHTime()
    {
        var timeUtc = DateTime.UtcNow;
        var chinaTz = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
        var phTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, chinaTz);

        return phTime;
    }
    public static string GetCon()
    {
        return ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString;
    }

    public static string CreateSHAHash(string Phrase)
    {
        SHA512Managed HashTool = new SHA512Managed();
        Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Phrase));
        Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
        HashTool.Clear();
        return Convert.ToBase64String(EncryptedBytes);
    }

    public static void ValidateAdmin()
    {
        //user not logged in
        if (HttpContext.Current.Session["userid"] == null)
        {
            HttpContext.Current.Response.Redirect("~/Admin/Login.aspx");
        }
    }

    public static void ValidateUser()
    {
        //user not logged in
        if (HttpContext.Current.Session["userid"] == null)
        {
            HttpContext.Current.Response.Redirect("~/Users/Login.aspx");
        }
    }

    public static void Log(string logTitle, string logContent, string logType, string userID)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Logs VALUES (@LogTitle, @LogContent, @LogType, @UserID, @LogDate)";
            cmd.Parameters.AddWithValue("@LogTitle", logTitle);
            cmd.Parameters.AddWithValue("@LogContent", logContent);
            cmd.Parameters.AddWithValue("@LogType", logType);
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@LogDate", Helper.PHTime());
            cmd.ExecuteNonQuery();
        }
    }
}
