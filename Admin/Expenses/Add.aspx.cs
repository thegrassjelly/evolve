using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Admin_Expenses_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            pnlSalary.Visible = ddlExpense.SelectedValue == "Salaries";
            pnlExpName.Visible = ddlExpense.SelectedValue != "Salaries";
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
                    if (!dr.HasRows) return;
                    if (!dr.Read()) return;

                    txtFN.Text = dr["FirstName"].ToString();
                    txtLN.Text = dr["LastName"].ToString();
                }
            }
        }
    }

    protected void ddlExpense_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSalary.Visible = ddlExpense.SelectedValue == "Salaries";
        pnlExpName.Visible = ddlExpense.SelectedValue != "Salaries";
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"INSERT INTO Expenses
                                (ExpenseName, ExpenseType, ExpenseDescription,
                                ORNo, Cost, DateAdded) VALUES
                                (@expname, @exptype, @expdesc, @orno, @cost, @dadded)";

            if (ddlExpense.SelectedValue == "Salaries")
            {
                cmd.Parameters.AddWithValue("@expname", txtLN.Text + ", " + txtFN.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@expname", txtExpName.Text);
            }

            cmd.Parameters.AddWithValue("@exptype", ddlExpense.SelectedValue);
            cmd.Parameters.AddWithValue("@expdesc", txtExpDesc.Text);
            cmd.Parameters.AddWithValue("@orno", txtORNo.Text);
            cmd.Parameters.AddWithValue("@cost", txtPurAmnt.Text);
            cmd.Parameters.AddWithValue("@dadded", Helper.PHTime());
            cmd.ExecuteNonQuery();
        }

        string expName;

        if (ddlExpense.SelectedValue == "Salaries")
        {
            expName = txtLN.Text + ", " + txtFN.Text;
        }
        else
        {
            expName = txtExpName.Text;
        }

        Helper.Log("Add Expense", 
            "Added " + ddlExpense.SelectedValue + " Expense", 
            expName,
            Session["userid"].ToString());

        Response.Redirect("View.aspx");
    }
}