using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Training_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetTrainings(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetTrainings(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlGoal.SelectedValue == "All Goals")
            {
                cmd.CommandText = @"SELECT
                                  TrainingID
                                 ,LastName
                                 ,FirstName
                                 ,CoachName
                                 ,GoalSetting
                                 ,TrainingPackage
                                 ,TrainingFee
                                 ,Trainings.DateAdded
                                 ,Trainings.DateModified
                                FROM Trainings
                                INNER JOIN Users
                                ON Trainings.UserID = Users.UserID
                                WHERE (LastName LIKE @keyword
                                OR FirstName LIKE @keyword
                                OR CoachName LIKE @keyword
                                OR GoalSetting LIKE @keyword
                                OR TrainingPackage LIKE @keyword)
                                ORDER BY DateAdded DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT
                                  TrainingID
                                 ,LastName
                                 ,FirstName
                                 ,CoachName
                                 ,GoalSetting
                                 ,TrainingPackage
                                 ,TrainingFee
                                 ,Trainings.DateAdded
                                 ,Trainings.DateModified
                                FROM Trainings
                                INNER JOIN Users
                                ON Trainings.UserID = Users.UserID
                                WHERE (LastName LIKE @keyword
                                OR FirstName LIKE @keyword
                                OR CoachName LIKE @keyword
                                OR GoalSetting LIKE @keyword
                                OR TrainingPackage LIKE @keyword)
                                AND GoalSetting = @title
                                ORDER BY DateAdded DESC";
            }
            cmd.Parameters.AddWithValue("@title", ddlGoal.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Logs");
            lvTrainings.DataSource = ds;
            lvTrainings.DataBind();
        }
    }

    protected void ddlGoal_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetTrainings(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetTrainings(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetTrainings(txtSearch.Text);
    }

    protected void lvTrainings_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpTrainings.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetTrainings(txtSearch.Text);
    }

    protected void lvTrainings_OnDataBound(object sender, EventArgs e)
    {
        dpTrainings.Visible = dpTrainings.PageSize < dpTrainings.TotalRowCount;
    }
}