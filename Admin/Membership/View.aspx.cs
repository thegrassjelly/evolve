using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Membership_View : System.Web.UI.Page
{
    public static DateTime dateNow;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            dateNow = Helper.PHTime();
            GetMemberships(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetMemberships(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlRate.SelectedValue == "All Users")
            {
                if (ddlStatus.SelectedValue == "Active")
                {
                    cmd.CommandText = @"SELECT Memberships.MembershipID,
                            MembershipStart, MembershipEnd, FirstName, LastName,
                            MembershipType, ORNo, PaymentDate
                            FROM Memberships
                            INNER JOIN Users ON Memberships.UserID = Users.UserID
                            INNER JOIN Payments ON Memberships.MembershipID = Payments.MembershipID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            MembershipType = @keyword) 
                            AND MembershipEnd > @datenow ORDER BY Memberships.MembershipID DESC";
                }
                else
                {
                    cmd.CommandText = @"SELECT Memberships.MembershipID,
                            MembershipStart, MembershipEnd, FirstName, LastName,
                            MembershipType, ORNo, PaymentDate
                            FROM Memberships
                            INNER JOIN Users ON Memberships.UserID = Users.UserID
                            INNER JOIN Payments ON Memberships.MembershipID = Payments.MembershipID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            MembershipType = @keyword) 
                            AND MembershipEnd < @datenow ORDER BY Memberships.MembershipID DESC";
                }
            }
            else
            {
                if (ddlStatus.SelectedValue == "Active")
                {
                    cmd.CommandText = @"SELECT Memberships.MembershipID,
                            MembershipStart, MembershipEnd, FirstName, LastName,
                            MembershipType, ORNo, PaymentDate
                            FROM Memberships
                            INNER JOIN Users ON Memberships.UserID = Users.UserID
                            INNER JOIN Payments ON Memberships.MembershipID = Payments.MembershipID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            MembershipType = @keyword)
                            AND MembershipType = @type 
                            AND MembershipEnd > @datenow ORDER BY Memberships.MembershipID DESC";
                }
                else
                {
                    cmd.CommandText = @"SELECT Memberships.MembershipID,
                            MembershipStart, MembershipEnd, FirstName, LastName,
                            MembershipType, ORNo, PaymentDate
                            FROM Memberships
                            INNER JOIN Users ON Memberships.UserID = Users.UserID
                            INNER JOIN Payments ON Memberships.MembershipID = Payments.MembershipID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            MembershipType = @keyword)
                            AND MembershipType = @type 
                            AND MembershipEnd < @datenow ORDER BY Memberships.MembershipID DESC";
                }

            }
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            cmd.Parameters.AddWithValue("@type", ddlRate.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Memberships");
            lvMemberships.DataSource = ds;
            lvMemberships.DataBind();
        }
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetMemberships(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetMemberships(txtSearch.Text);
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetMemberships(txtSearch.Text);
    }

    protected void ddlRate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetMemberships(txtSearch.Text);
    }

    protected void lvMemberships_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpMemberships.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetMemberships(txtSearch.Text);
    }

    protected void lvMemberships_OnDataBound(object sender, EventArgs e)
    {
        dpMemberships.Visible = dpMemberships.PageSize < dpMemberships.TotalRowCount;
    }
}