using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    static string _dSDate;
    static string _dEDate;
    static DateTime dateNow;
    static string _SubStatus;
    static DateTime _SubStart;
    static DateTime _SubEnd;
    static string _OperationID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
        if (!IsPostBack)
        {
            GetDaily();

            GetDailyUsers();
            GetDailyMembers();
            GetDailySubs();
            GetDailyLogs();

            GetSalesMem();
            GetSalesSubs();
            GetSalesWork();
            GetExp();

            GetCheckIns(txtSearchCust.Text);
            dateNow = Helper.PHTime();
        }
    }

    [WebMethod]
    public static List<string> GetName(string prefixText)
    {
        List<string> name = new List<string>();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myCon"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT FirstName, LastName, UserID FROM Users WHERE 
                    FirstName LIKE @SearchText OR
                    LastName LIKE @SearchText
                    AND Status = 'Active' ORDER BY LastName ASC";
                cmd.Parameters.AddWithValue("@SearchText", "%" + prefixText + "%");
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string myString = dr["LastName"] + ", " + dr["FirstName"] + "/vn/" + dr["UserID"];
                        name.Add(myString);
                    }
                }

                conn.Close();
            }

        }

        return name;
    }

    private void GetExp()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SUM(Cost) AS Cost FROM Expenses
                WHERE DateAdded BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltExp.Text = dr["Cost"] != DBNull.Value
                            ? Convert.ToDecimal(dr["Cost"]).ToString("##,###.00")
                            : "0";
                    }
                }
                else
                {
                    ltExp.Text = "0";
                }
            }
        }
    }

    private void GetSalesWork()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SUM(PaidAmount) AS Amnt FROM Operations
                WHERE CheckIn BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltSalesWork.Text = dr["Amnt"] != DBNull.Value
                            ? Convert.ToDecimal(dr["Amnt"]).ToString("##,###.00")
                            : "0";
                    }
                }
                else
                {
                    ltSalesWork.Text = "0";
                }
            }
        }
    }

    private void GetSalesSubs()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SUM(Amount) AS Total FROM Payments WHERE SubID IS NOT NULL
                                AND MembershipID IS NULL AND 
                                PaymentDate BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltSalesSub.Text = dr["Total"] != DBNull.Value
                            ? Convert.ToDecimal(dr["Total"]).ToString("##,###.00")
                            : "0";
                    }
                }
                else
                {
                    ltSalesSub.Text = "0";
                }
            }
        }
    }


    private void GetSalesMem()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SUM(Amount) AS Total FROM Payments WHERE MembershipID IS NOT NULL
                                AND SubID IS NULL AND 
                                PaymentDate BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltSalesMem.Text = dr["Total"] != DBNull.Value
                            ? Convert.ToDecimal(dr["Total"]).ToString("##,###.00")
                            : "0";
                    }
                }
                else
                {
                    ltSalesMem.Text = "0";
                }
            }
        }
    }

    private void GetDailyLogs()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT COUNT(LogID) AS LogCount FROM Logs
                WHERE LogDate BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltDailyLogs.Text = dr["LogCount"].ToString();
                    }
                }
                else
                {
                    ltDailyLogs.Text = "0";
                }
            }
        }
    }

    private void GetDailySubs()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT COUNT(SubID) AS SubCount FROM Subscriptions
                WHERE DateAdded BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltDailySubs.Text = dr["SubCount"].ToString();
                    }
                }
                else
                {
                    ltDailySubs.Text = "0";
                }
            }
        }
    }

    private void GetDailyMembers()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT COUNT(MembershipID) AS MemCount FROM Memberships
                WHERE DateAdded BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltDailyMem.Text = dr["MemCount"].ToString();
                    }
                }
                else
                {
                    ltDailyMem.Text = "0";
                }
            }
        }
    }

    private void GetDailyUsers()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT COUNT(UserID) AS UserCount FROM Users
                WHERE DateAdded BETWEEN @sdate AND @edate";
            cmd.Parameters.AddWithValue("@sdate", _dSDate);
            cmd.Parameters.AddWithValue("@edate", _dEDate);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ltDailyUsers.Text = dr["UserCount"].ToString();
                    }
                }
                else
                {
                    ltDailyUsers.Text = "0";
                }
            }
        }
    }

    private void GetDaily()
    {
        DateTime now = Helper.PHTime();
        var startDate = new DateTime(now.Year, now.Month, now.Day);
        var endDate = startDate.AddDays(1).AddMinutes(-1);

        _dSDate = startDate.ToString(CultureInfo.InvariantCulture);
        _dEDate = endDate.ToString(CultureInfo.InvariantCulture);

        ltToday.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltToday2.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltToday3.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltToday4.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);

        ltDailyS1.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltDailyS2.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltDailyS3.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
        ltDailyS4.Text = startDate.ToString("MMMM dd", CultureInfo.InvariantCulture);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        if (hfName.Value != "0")
        {
            if (!isExist())
            {
                errorCheckIn.Visible = false;
                using (var con = new SqlConnection(Helper.GetCon()))
                using (var cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = @"INSERT Operations
                                    (CheckIn, UserID, MemStatus, SubStatus, SubStart, SubEnd)
                                    VALUES
                                    (@chkin, @id, @memstatus, @substatus, @sstart, @send)";
                    cmd.Parameters.AddWithValue("@chkin", Helper.PHTime());
                    cmd.Parameters.AddWithValue("@id", hfName.Value);
                    cmd.Parameters.AddWithValue("@memstatus", GetMemStatus());
                    GetSubs();
                    cmd.Parameters.AddWithValue("@substatus", _SubStatus);
                    cmd.Parameters.AddWithValue("@sstart", _SubStart);
                    cmd.Parameters.AddWithValue("@send", _SubEnd);
                    cmd.ExecuteNonQuery();
                }

                GetCheckIns(txtSearchCust.Text);
            }
            else
            {
                errorCheckIn.Visible = true;
            }
        }
    }

    bool isExist()
    {
        bool isExist;

        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT OperationID FROM Operations
                                WHERE UserID = @id 
                                AND CheckIN > @dateone";
            cmd.Parameters.AddWithValue("@id", hfName.Value);

            DateTime now = Helper.PHTime();
            var startDate = new DateTime(now.Year, now.Month, now.Day);
            var endDate = startDate.AddDays(1).AddMinutes(-1);

            cmd.Parameters.AddWithValue("@dateone", startDate);
            cmd.Parameters.AddWithValue("@datetwo", endDate);
            using (var dr = cmd.ExecuteReader())
            {
                isExist = dr.HasRows;
            }
        }

        return isExist;
    }

    private void GetSubs()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SubID, SubStart, SubEnd FROM Subscriptions 
                                WHERE UserID = @id AND
                                SubEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (!dr.Read()) return;
                    _SubStatus = "Active";
                    _SubStart = Convert.ToDateTime(dr["SubStart"]);
                    _SubEnd = Convert.ToDateTime(dr["SubEnd"]);
                }
                else
                {
                    _SubStatus = "Inactive";
                    _SubStart = Helper.PHTime();
                    _SubEnd = Helper.PHTime();
                }
            }
        }
    }

    private string GetMemStatus()
    {
        string isActive;

        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MembershipID FROM Memberships 
                                WHERE UserID = @id AND
                                MembershipEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            using (var dr = cmd.ExecuteReader())
            {
                isActive = dr.HasRows ? "Active" : "Inactive";
            }
        }

        return isActive;
    }

    private void GetCheckIns(string searchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Operations.UserID, OperationID, CheckIn, CheckOut, FirstName, LastName,
                                MemStatus, SubStatus, SubStart, SubEnd
                                FROM Operations
                                INNER JOIN Users ON Operations.UserID = Users.UserID
                                WHERE 
                                (FirstName LIKE @keyword OR
                                LastName LIKE @keyword OR
                                MemStatus LIKE @keyword OR
                                SubStatus LIKE @keyword)
                                AND CheckIn BETWEEN @dateone AND @datetwo
                                ORDER BY CheckIN DESC";
            DateTime now = Helper.PHTime();
            var startDate = new DateTime(now.Year, now.Month, now.Day);
            var endDate = startDate.AddDays(1).AddMinutes(-1);

            cmd.Parameters.AddWithValue("@keyword", "%" + searchText + "%");
            cmd.Parameters.AddWithValue("@dateone", startDate);
            cmd.Parameters.AddWithValue("@datetwo", endDate);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Operations");
            lvOperations.DataSource = ds;
            lvOperations.DataBind();
        }
    }

    protected void lvOperations_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpOperations.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetCheckIns(txtSearchCust.Text);
    }

    protected void lvOperations_OnDataBound(object sender, EventArgs e)
    {
        dpOperations.Visible = dpOperations.PageSize < dpOperations.TotalRowCount;
    }

    protected void btnSearchCust_OnClick(object sender, EventArgs e)
    {
        GetCheckIns(txtSearchCust.Text);
    }

    protected void txtSearchCust_OnTextChanged(object sender, EventArgs e)
    {
        GetCheckIns(txtSearchCust.Text);
    }
}
