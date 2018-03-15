using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Users_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetUsers(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetUsers(string txtSearch)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlType.SelectedValue == "All Users")
            {
                cmd.CommandText = @"SELECT UserID, FirstName, LastName, MobileNo, Birthday,
                            Status, UserType, DateAdded, DateModified
                            FROM Users
                            INNER JOIN Types ON Types.TypeID = Users.TypeID
                            WHERE (UserID LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            FirstName LIKE @keyword) 
                            AND Status = @status ORDER BY DateAdded DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT UserID, FirstName, LastName, MobileNo, Birthday,
                            Status, UserType, DateAdded, DateModified
                            FROM Users
                            INNER JOIN Types ON Types.TypeID = Users.TypeID
                            WHERE (UserID LIKE @keyword OR 
                            LastName LIKE @keyword OR 
                            FirstName LIKE @keyword) 
                            AND Status = @status AND UserType = @type ORDER BY DateAdded DESC";
            }

            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@type", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearch + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Users");
            lvUsers.DataSource = ds;
            lvUsers.DataBind();
        }
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetUsers(txtSearch.Text);
    }

    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetUsers(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetUsers(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetUsers(txtSearch.Text);
    }

    protected void lvUsers_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpUsers.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetUsers(txtSearch.Text);
    }

    protected void lvUsers_OnDataBound(object sender, EventArgs e)
    {
        dpUsers.Visible = dpUsers.PageSize < dpUsers.TotalRowCount;
    }
}