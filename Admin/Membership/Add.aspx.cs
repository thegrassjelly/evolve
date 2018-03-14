using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Admin_Membership_Add : System.Web.UI.Page
{
    public static decimal RegRate;
    public static decimal StudRate;
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetMembershipStartUp();
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

            CheckMembershipStatus();
            txtORNo.Text = "";
        }
    }

    private void CheckMembershipStatus()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MembershipID FROM Memberships 
                                WHERE UserID = @id AND
                                MembershipStart < @datenow AND MembershipEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", DateTime.Now);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    pnlMember.Visible = true;
                    pnlNonMember.Visible = false;
                    GetMembershipDetails();

                    Hides(true);
                }
                else
                {
                    pnlMember.Visible = false;
                    pnlNonMember.Visible = true;

                    Hides(false);
                }
            }
        }
    }

    private void GetMembershipDetails()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MembershipStart, MembershipEnd,
                                MembershipSpan, MembershipType, DateAdded FROM Memberships
                                WHERE UserID = @id AND
                                MembershipStart < @datenow AND MembershipEnd > @datenow";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    DateTime sDate = Convert.ToDateTime(dr["MembershipStart"].ToString());
                    DateTime eDate = Convert.ToDateTime(dr["MembershipEnd"].ToString());

                    txtStartDate.Text = sDate.ToString("MMMM dd yyyy");
                    txtEndDate.Text = eDate.ToString("MMMM dd yyyy");
                    txtMembershipLength.Text = dr["MembershipSpan"].ToString();
                    txtMembershipType2.Text = dr["MembershipType"].ToString();

                    if (sDate < DateTime.Now &&
                        eDate > DateTime.Now)
                    {
                        memInactive.Visible = false;
                        memActive.Visible = true;

                        txtMembershipStatus2.Text = "Active";
                    }
                    else
                    {
                        memActive.Visible = false;
                        memInactive.Visible = true;

                        txtMembershipStatus.Text = "Inactive";
                    }
                }
            }
        }
    }

    private void Hides(bool isMember)
    {
        if (isMember)
        {
            btnAdd.Visible = false;
            ddlRate.Attributes.Add("disabled", "true");
            txtORNo.Attributes.Add("disabled", "true");
        }
        else
        {
            btnAdd.Visible = true;
            ddlRate.Attributes.Remove("disabled");
            txtORNo.Attributes.Remove("disabled");
        }
    }

    protected void ddlRate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetMembershipStartUp();
    }

    private void GetMembershipStartUp()
    {
        DateTime memSDate = Convert.ToDateTime(Helper.PHTime());

        txtSDate.Text = memSDate.ToString("MMMM dd yyyy");
        txtEDate.Text = memSDate.AddYears(1).ToString("MMMM dd yyyy");

        GetPrices();
    }

    private void GetPrices()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MemReg, MemStud FROM Pricelist";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    RegRate = Convert.ToDecimal(dr["MemReg"]);
                    StudRate = Convert.ToDecimal(dr["MemStud"]);

                    if (ddlRate.SelectedValue == "Regular")
                    {
                        ltTotal.Text = RegRate.ToString("₱ #,###.00");
                    }
                    else
                    {
                        ltTotal.Text = StudRate.ToString("₱ #,###.00");
                    }
                }
            }
        }
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        if (txtORNo2.Text != "")
        {
            errorORNo2.Visible = false;

            using (var con = new SqlConnection(Helper.GetCon()))
            using (var cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"INSERT INTO Memberships
                                (UserID, MembershipStart, MembershipEnd,
                                MembershipSpan, MembershipType, DateAdded)
                                VALUES (@userid, @memstart, @memend, 
                                @memspan, @memtype, @dateadded);
                                SELECT TOP 1 MembershipID FROM Memberships 
                                ORDER BY MembershipID DESC";
                cmd.Parameters.AddWithValue("@userid", hfName.Value);
                cmd.Parameters.AddWithValue("@memstart", Helper.PHTime());
                cmd.Parameters.AddWithValue("@memend", Helper.PHTime().AddYears(1));
                cmd.Parameters.AddWithValue("@memspan", 1);
                cmd.Parameters.AddWithValue("@memtype", ddlRate.SelectedValue);
                cmd.Parameters.AddWithValue("@dateadded", Helper.PHTime());
                int membershipID = (int)cmd.ExecuteScalar();

                cmd.CommandText = @"INSERT INTO Payments
                                (PaymentDate, MembershipID, Amount, ORNo)
                                VALUES (@paydate, @memid, @amnt, @orno)";
                cmd.Parameters.AddWithValue("@paydate", Helper.PHTime());
                cmd.Parameters.AddWithValue("@memid", membershipID);

                cmd.Parameters.AddWithValue("@amnt",
                    ddlRate.SelectedValue == "Regular" ? RegRate : StudRate);

                cmd.Parameters.AddWithValue("@orno", txtORNo.Text);
                cmd.ExecuteNonQuery();
            }

            Helper.Log("Add Membership",
                "New membership for " + txtLN.Text + ", " + txtFN.Text, "", Session["userid"].ToString());

            CheckMembershipStatus();
        }
        else
        {
            errorORNo2.Visible = true;
        }
    }

    protected void btnAddCurrent_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void ddlRate2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}
