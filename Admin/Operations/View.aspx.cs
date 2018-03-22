using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Operations_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
        if (!IsPostBack)
        {
            GetCheckIns(txtSearchCust.Text);
        }
    }

    private void GetCheckIns(string text)
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
                                ORDER BY CheckIN DESC";

            cmd.Parameters.AddWithValue("@keyword", "%" + text + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Operations");
            lvOperations.DataSource = ds;
            lvOperations.DataBind();
        }
    }

    protected void txtSearchCust_OnTextChanged(object sender, EventArgs e)
    {
        GetCheckIns(txtSearchCust.Text);
    }

    protected void btnSearchCust_OnClick(object sender, EventArgs e)
    {
        GetCheckIns(txtSearchCust.Text);
    }

    protected void lvOperations_OnDataBound(object sender, EventArgs e)
    {
        dpOperations.Visible = dpOperations.PageSize < dpOperations.TotalRowCount;
    }

    protected void lvOperations_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpOperations.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetCheckIns(txtSearchCust.Text);
    }
}