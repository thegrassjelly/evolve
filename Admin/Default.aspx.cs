using System;
using System.Data.SqlClient;
using System.Globalization;

public partial class Admin_Default : System.Web.UI.Page
{
    static string _dSDate;
    static string _dEDate;

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
        }
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
        ltSalesWork.Text = "0";
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

    protected void tmrDaily_OnTick(object sender, EventArgs e)
    {
        GetDailyUsers();
        GetDailyMembers();
        GetDailySubs();
        GetDailyLogs();

        GetSalesMem();
        GetSalesSubs();
        GetSalesWork();
        GetExp();
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
}
