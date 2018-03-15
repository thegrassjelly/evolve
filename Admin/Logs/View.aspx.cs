using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Logs_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetLogType();
            GetLogs(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetLogType()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT DISTINCT LogTitle FROM Logs";
            SqlDataReader dr = cmd.ExecuteReader();
            ddlLogType.DataSource = dr;
            ddlLogType.DataTextField = "LogTitle";
            ddlLogType.DataValueField = "LogTitle";
            ddlLogType.DataBind();

            ddlLogType.Items.Insert(0, "All Logs");
        }
    }

    private void GetLogs(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlLogType.SelectedValue == "All Logs")
            {
                cmd.CommandText = @"SELECT LogID, FirstName, LastName, 
                                    LogTitle, LogContent, LogType, LogDate
                                    FROM Logs
                                    INNER JOIN Users ON Users.UserID = Logs.UserID
                                    WHERE (LogID LIKE @keyword OR 
                                    LastName LIKE @keyword OR 
                                    FirstName LIKE @keyword OR
                                    LogTitle LIKE @keyword OR
                                    LogContent LIKE @keyword) ORDER BY LogDate DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT LogID, FirstName, LastName, 
                                    LogTitle, LogContent, LogType, LogDate
                                    FROM Logs
                                    INNER JOIN Users ON Users.UserID = Logs.UserID
                                    WHERE (LogID LIKE @keyword OR 
                                    LastName LIKE @keyword OR 
                                    FirstName LIKE @keyword OR
                                    LogTitle LIKE @keyword OR
                                    LogContent LIKE @keyword)
                                    AND LogTitle = @title ORDER BY LogDate DESC";
            }
            cmd.Parameters.AddWithValue("@title", ddlLogType.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Logs");
            lvLogs.DataSource = ds;
            lvLogs.DataBind();
        }
    }

    protected void ddlLogType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetLogs(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetLogs(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetLogs(txtSearch.Text);
    }

    protected void lvLogs_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpLogs.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetLogs(txtSearch.Text);
    }

    protected void lvLogs_OnDataBound(object sender, EventArgs e)
    {
        dpLogs.Visible = dpLogs.PageSize < dpLogs.TotalRowCount;
    }
}