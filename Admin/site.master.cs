using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
        GetUserInfo();
    }

    private void GetUserInfo()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT FirstName, LastName,
                EmailAddress FROM Users WHERE UserID = @id";
            cmd.Parameters.AddWithValue("@id", Session["userid"].ToString());
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ltEmail.Text = dr["EmailAddress"].ToString();
                        ltFirstName.Text = dr["FirstName"].ToString();
                        ltFullName.Text = dr["FirstName"].ToString() + ' ' +
                                          dr["LastName"].ToString(); ;
                    }
                }
            }
        }
    }
}
