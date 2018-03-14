using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Services;

public partial class Admin_Subscription_Add : System.Web.UI.Page
{
    static decimal _SubsRegOneM;
    static decimal _SubsRegThreeM;
    static decimal _SubsRegSixM;
    static decimal _SubsRegOneY;
    static decimal _SubsStudOneM;
    static decimal _SubsStudThreeM;
    static decimal _SubsStudSixM;
    static decimal _SubsStudOneY;

    static decimal _Total;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetPrices();
            GetSubscriptionStartup();
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

    protected void btnUser_OnClick(object sender, EventArgs e)
    {
        if (hfName.Value != "0")
        {
            using (var con = new SqlConnection(Helper.GetCon()))
            using (var cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT UserID, FirstName, LastName, Birthday,
                EmailAddress, MobileNo, Address,
                Status, UserType 
                FROM Users 
                INNER JOIN Types ON Users.TypeID = Types.TypeID
                WHERE UserID = @id";
                cmd.Parameters.AddWithValue("@id", hfName.Value);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            txtFN.Text = dr["FirstName"].ToString();
                            txtLN.Text = dr["LastName"].ToString();
                            DateTime bDay = DateTime.Parse(dr["Birthday"].ToString());
                            txtBday.Text = bDay.ToString("yyyy-MM-dd");
                            txtEmail.Text = dr["EmailAddress"].ToString();
                            txtAddr.Text = dr["Address"].ToString();
                            txtMNo.Text = dr["MobileNo"].ToString();
                            txtStatus.Text = dr["Status"].ToString();
                            txtUserType.Text = dr["UserType"].ToString();
                        }
                    }
                }
            }
        }
        CheckSubStatus();
        txtORNo.Text = "";
    }

    private void CheckSubStatus()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SubID FROM Subscriptions 
                                WHERE UserID = @id AND
                                SubEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    pnlCantSub.Visible = false;
                    pnlSub.Visible = true;
                    pnlNonSub.Visible = false;

                    GetSubDetails();

                    Hides(true);
                }
                else
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = @"SELECT MembershipID FROM Memberships 
                                WHERE UserID = @id AND
                                MembershipEnd > @datenow";
                    cmd.Parameters.AddWithValue("@id", hfName.Value);
                    cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
                    dr.Close();
                    using (var dr2 = cmd.ExecuteReader())
                    {
                        if (dr2.HasRows)
                        {
                            pnlCantSub.Visible = false;
                            pnlSub.Visible = false;
                            pnlNonSub.Visible = true;

                            Hides(false);
                        }
                        else
                        {
                            pnlCantSub.Visible = true;
                            pnlSub.Visible = false;
                            pnlNonSub.Visible = false;

                            Hides(true);
                        }
                    }
                }
            }
        }
    }

    private void GetSubDetails()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SubStart, SubEnd,
                                SubSpan, SubType, DateAdded FROM Subscriptions
                                WHERE UserID = @id AND
                                SubEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DateTime sDate = Convert.ToDateTime(dr["SubStart"].ToString());
                    DateTime eDate = Convert.ToDateTime(dr["SubEnd"].ToString());

                    txtStartDate.Text = sDate.ToString("MMMM dd yyyy");
                    txtEndDate.Text = eDate.ToString("MMMM dd yyyy");

                    if (dr["SubSpan"].ToString() == "12")
                    {
                        txtSubLength.Text = dr["SubSpan"] + " Year";
                    }
                    else if (dr["SubSpan"].ToString() == "1")
                    {
                        txtSubLength.Text = dr["SubSpan"] + " Month";
                    }
                    else
                    {
                        txtSubLength.Text = dr["SubSpan"] + " Months";
                    }

                    txtSubType2.Text = dr["SubType"].ToString();

                    if (eDate > Helper.PHTime())
                    {
                        subInactive.Visible = false;
                        SubActive.Visible = true;

                        txtSubStatus2.Text = "Active";
                    }
                    else
                    {
                        SubActive.Visible = false;
                        subInactive.Visible = true;

                        txtSubStatus.Text = "Inactive";
                    }
                }
            }
        }
    }

    private void Hides(bool isSub)
    {
        if (isSub)
        {
            btnAdd.Visible = false;
            ddlRate.Attributes.Add("disabled", "true");
            txtORNo.Attributes.Add("disabled", "true");
            ddlSubs.Attributes.Add("disabled", "true");
            txtSDate.Attributes.Add("disabled", "true");

            //btnAddCurrent.Visible = false;
            //ddlRate2.Attributes.Add("disabled", "true");
            //txtSDate2.Attributes.Add("disabled", "true");
            //txtORNo2.Attributes.Add("disabled", "true");
            //txtPayDate.Attributes.Add("disabled", "true");
        }
        else
        {
            btnAdd.Visible = true;
            ddlRate.Attributes.Remove("disabled");
            txtORNo.Attributes.Remove("disabled");
            ddlSubs.Attributes.Remove("disabled");
            txtSDate.Attributes.Remove("disabled");

            //btnAddCurrent.Visible = true;
            //ddlRate2.Attributes.Remove("disabled");
            //txtSDate2.Attributes.Remove("disabled");
            //txtORNo2.Attributes.Remove("disabled");
            //txtPayDate.Attributes.Remove("disabled");
        }
    }

    private void GetPrices()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SubsRegOneM, SubsRegThreeM,
                                SubsRegSixM, SubsRegOneY,
                                SubsStudOneM, SubsStudThreeM,
                                SubsStudSixM, SubsStudOneY
                                FROM Pricelist";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    _SubsRegOneM = Convert.ToDecimal(dr["SubsRegOneM"]);
                    _SubsRegThreeM = Convert.ToDecimal(dr["SubsRegThreeM"]);
                    _SubsRegSixM = Convert.ToDecimal(dr["SubsRegSixM"]);
                    _SubsRegOneY = Convert.ToDecimal(dr["SubsRegOneY"]);

                    _SubsStudOneM = Convert.ToDecimal(dr["SubsStudOneM"]);
                    _SubsStudThreeM = Convert.ToDecimal(dr["SubsStudThreeM"]);
                    _SubsStudSixM = Convert.ToDecimal(dr["SubsStudSixM"]);
                    _SubsStudOneY = Convert.ToDecimal(dr["SubsStudOneY"]);
                }
            }
        }
    }

    private void GetSubscriptionStartup()
    {
        if (txtSDate.Text != "")
        {
            txtEDate.Text = Convert.ToDateTime(txtSDate.Text).
                AddMonths(Convert.ToInt32(ddlSubs.SelectedValue)).ToString("MMMM dd yyyy");
        }

        var subFee = ddlSubs.SelectedValue;

        if (ddlRate.SelectedValue == "Regular")
        {
            switch (subFee)
            {
                case "1":
                    ltTotal.Text = _SubsRegOneM.ToString("₱ #,###.00");
                    _Total = _SubsStudOneM;
                    break;
                case "3":
                    ltTotal.Text = _SubsRegThreeM.ToString("₱ #,###.00");
                    _Total = _SubsRegThreeM;
                    break;
                case "6":
                    ltTotal.Text = _SubsRegSixM.ToString("₱ #,###.00");
                    _Total = _SubsRegSixM;
                    break;
                case "12":
                    ltTotal.Text = _SubsRegOneY.ToString("₱ #,###.00");
                    _Total = _SubsRegOneY;
                    break;
            }
        }
        else
        {
            switch (subFee)
            {
                case "1":
                    ltTotal.Text = _SubsStudOneM.ToString("₱ #,###.00");
                    _Total = _SubsStudOneM;
                    break;
                case "3":
                    ltTotal.Text = _SubsStudThreeM.ToString("₱ #,###.00");
                    _Total = _SubsStudThreeM;
                    break;
                case "6":
                    ltTotal.Text = _SubsStudSixM.ToString("₱ #,###.00");
                    _Total = _SubsStudSixM;
                    break;
                case "12":
                    ltTotal.Text = _SubsStudOneY.ToString("₱ #,###.00");
                    _Total = _SubsStudOneY;
                    break;
            }
        }
    }

    protected void ddlRate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubscriptionStartup();
    }

    protected void ddlSubs_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubscriptionStartup();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        if (txtORNo.Text != "")
        {
            errorORNo.Visible = false;

            using (var con = new SqlConnection(Helper.GetCon()))
            using (var cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO Subscriptions
                                (UserID, SubStart, SubEnd,
                                SubSpan, SubType, DateAdded)
                                VALUES (@userid, @subs, @sube, 
                                @subspn, @subtype, @dateadded);
                                SELECT TOP 1 SubID FROM Subscriptions 
                                ORDER BY SubID DESC";
                cmd.Parameters.AddWithValue("@userid", hfName.Value);
                cmd.Parameters.AddWithValue("@subs", txtSDate.Text);
                cmd.Parameters.AddWithValue("@sube", 
                    Convert.ToDateTime(txtSDate.Text).AddMonths(Convert.ToInt32(ddlSubs.SelectedValue)));
                cmd.Parameters.AddWithValue("@subspn", ddlSubs.SelectedValue);
                cmd.Parameters.AddWithValue("@subtype", ddlRate.SelectedValue);
                cmd.Parameters.AddWithValue("@dateadded", Helper.PHTime());
                int subID = (int) cmd.ExecuteScalar();

                cmd.CommandText = @"INSERT INTO Payments
                                (PaymentDate, SubID, Amount, ORNo)
                                VALUES (@paydate, @subid, @amnt, @orno)";
                cmd.Parameters.AddWithValue("@paydate", Helper.PHTime());
                cmd.Parameters.AddWithValue("@subid", subID);
                cmd.Parameters.AddWithValue("@amnt", _Total);
                cmd.Parameters.AddWithValue("@orno", txtORNo.Text);
                cmd.ExecuteNonQuery();
            }

            Helper.Log("Add Subscription",
                "New Subscription for " + txtLN.Text + ", " + txtFN.Text, "", Session["userid"].ToString());

            CheckSubStatus();
        }
        else
        {
            errorORNo.Visible = true;
        }
    }

    protected void txtSDate_OnTextChanged(object sender, EventArgs e)
    {
        GetSubscriptionStartup();
    }
}