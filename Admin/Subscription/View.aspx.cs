using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Subscription_View : System.Web.UI.Page
{
    public static DateTime dateNow;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            dateNow = Helper.PHTime();
            GetSubscriptions(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetSubscriptions(string txtSearchText)
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
                    cmd.CommandText = @"SELECT Subscriptions.SubID, SubSpan,
                            SubStart, SubEnd, FirstName, LastName,
                            SubType, ORNo, PaymentDate
                            FROM Subscriptions
                            INNER JOIN Users ON Subscriptions.UserID = Users.UserID
                            INNER JOIN Payments ON Subscriptions.SubID = Payments.SubID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            SubType = @keyword) 
                            AND SubEnd > @datenow ORDER BY Subscriptions.SubID DESC";
                }
                else
                {
                    cmd.CommandText = @"SELECT Subscriptions.SubID, SubSpan,
                            SubStart, SubEnd, FirstName, LastName,
                            SubType, ORNo, PaymentDate
                            FROM Subscriptions
                            INNER JOIN Users ON Subscriptions.UserID = Users.UserID
                            INNER JOIN Payments ON Subscriptions.SubID = Payments.SubID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            SubType = @keyword) 
                            AND SubEnd < @datenow ORDER BY Subscriptions.SubID DESC";
                }
            }
            else
            {
                if (ddlStatus.SelectedValue == "Active")
                {
                    cmd.CommandText = @"SELECT Subscriptions.SubID, SubSpan,
                            SubStart, SubEnd, FirstName, LastName,
                            SubType, ORNo, PaymentDate
                            FROM Subscriptions
                            INNER JOIN Users ON Subscriptions.UserID = Users.UserID
                            INNER JOIN Payments ON Subscriptions.SubID = Payments.SubID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            SubType = @keyword)
                            AND SubType = @type 
                            AND SubEnd > @datenow ORDER BY Subscriptions.SubID DESC";
                }
                else
                {
                    cmd.CommandText = @"SELECT Subscriptions.SubID, SubSpan,
                            SubStart, SubEnd, FirstName, LastName,
                            SubType, ORNo, PaymentDate
                            FROM Subscriptions
                            INNER JOIN Users ON Subscriptions.UserID = Users.UserID
                            INNER JOIN Payments ON Subscriptions.SubID = Payments.SubID
                            WHERE (FirstName LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            ORNo LIKE @keyword OR
                            SubType = @keyword)
                            AND SubType = @type 
                            AND SubEnd < @datenow ORDER BY Subscriptions.SubID DESC";
                }

            }
            cmd.Parameters.AddWithValue("@datenow", Helper.PHTime());
            cmd.Parameters.AddWithValue("@type", ddlRate.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Subscriptions");
            lvSubscriptions.DataSource = ds;
            lvSubscriptions.DataBind();
        }
    }

    protected void lvSubscriptions_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpSubscriptions.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetSubscriptions(txtSearch.Text);
    }

    protected void lvSubscriptions_OnDataBound(object sender, EventArgs e)
    {
        dpSubscriptions.Visible = dpSubscriptions.PageSize < dpSubscriptions.TotalRowCount;
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetSubscriptions(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetSubscriptions(txtSearch.Text);
    }

    protected void ddlRate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubscriptions(txtSearch.Text);
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSubscriptions(txtSearch.Text);
    }
}