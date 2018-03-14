using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
        if (!IsPostBack)
        {
            GetUserType();
        }
        this.Form.DefaultButton = this.btnSubmit.UniqueID;
    }

    void GetUserType()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        using (SqlCommand cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TypeID, UserType FROM Types";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                ddlType.DataSource = dr;
                ddlType.DataTextField = "UserType";
                ddlType.DataValueField = "TypeID";
                ddlType.DataBind();
            }
        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"INSERT INTO Users
                            (FirstName, LastName, Birthday, EmailAddress,
                            MobileNo, Address, TypeID, Status, DateAdded) VALUES
                            (@fn, @ln, @bday, @email, @mobile, @addr, @type, @status, @dadded)";
            cmd.Parameters.AddWithValue("@fn", txtFN.Text);
            cmd.Parameters.AddWithValue("@ln", txtLN.Text);
            cmd.Parameters.AddWithValue("@bday", txtBday.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@mobile", txtMNo.Text);
            cmd.Parameters.AddWithValue("@addr", txtAddr.Text);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@type", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@dadded", Helper.PHTime());
            cmd.ExecuteNonQuery();

            Helper.Log("Add User",
                "Added user " + txtLN.Text + ", " + txtFN.Text, "", Session["userid"].ToString());
            
            Response.Redirect("View.aspx");
        }
    }
}