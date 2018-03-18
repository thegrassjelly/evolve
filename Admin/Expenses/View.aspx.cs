using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Expenenses_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetExpenseType();
            GetExpenses(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetExpenses(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlExpenseType.SelectedValue == "All Expenses")
            {
                cmd.CommandText = @"SELECT ExpenseID, ExpenseName, ExpenseDescription, 
                                    ExpenseType, ORNo, Cost, DateAdded
                                    FROM Expenses
                                    WHERE (ExpenseID LIKE @keyword OR 
                                    ExpenseName LIKE @keyword OR 
                                    ExpenseDescription LIKE @keyword OR
                                    ExpenseType LIKE @keyword OR
                                    ORNo LIKE @keyword) ORDER BY DateAdded DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT ExpenseID, ExpenseName, ExpenseDescription, 
                                    ExpenseType, ORNo, Cost, DateAdded
                                    FROM Expenses
                                    WHERE (ExpenseID LIKE @keyword OR 
                                    ExpenseName LIKE @keyword OR 
                                    ExpenseDescription LIKE @keyword OR
                                    ExpenseType LIKE @keyword OR
                                    ORNo LIKE @keyword) AND ExpenseType = @title
                                    ORDER BY DateAdded DESC";
            }
            cmd.Parameters.AddWithValue("@title", ddlExpenseType.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Expenses");
            lvExpenses.DataSource = ds;
            lvExpenses.DataBind();
        }
    }

    private void GetExpenseType()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT DISTINCT ExpenseType FROM Expenses";
            SqlDataReader dr = cmd.ExecuteReader();
            ddlExpenseType.DataSource = dr;
            ddlExpenseType.DataTextField = "ExpenseType";
            ddlExpenseType.DataValueField = "ExpenseType";
            ddlExpenseType.DataBind();

            ddlExpenseType.Items.Insert(0, "All Expenses");
        }
    }

    protected void ddlExpenseType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetExpenses(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetExpenses(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetExpenses(txtSearch.Text);
    }

    protected void lvExpenses_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpExpenses.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetExpenses(txtSearch.Text);
    }

    protected void lvExpenses_OnDataBound(object sender, EventArgs e)
    {
        dpExpenses.Visible = dpExpenses.PageSize < dpExpenses.TotalRowCount;
    }
}