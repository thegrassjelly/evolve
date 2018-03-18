using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Training_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetCoach();
        }
    }

    private void GetCoach()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
                @"SELECT LastName + ', ' + FirstName AS CoachName, UserID FROM Users 
                WHERE TypeID = '3'";
            SqlDataReader dr = cmd.ExecuteReader();
            ddlCoach.DataSource = dr;
            ddlCoach.DataTextField = "CoachName";
            ddlCoach.DataValueField = "CoachName";
            ddlCoach.DataBind();
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
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            txtFN.Text = dr["FirstName"].ToString();
                            txtLN.Text = dr["LastName"].ToString();
                            DateTime bDay = DateTime.Parse(dr["Birthday"].ToString());
                            txtBday.Text = bDay.ToString("yyyy-MM-dd");
                            txtEmail.Text = dr["EmailAddress"].ToString();
                            txtAddr.Text = dr["Address"].ToString();
                            txtMNo.Text = dr["MobileNo"].ToString();
                            txtStatus.Text = dr["Status"].ToString();
                            txtUserType.Text = dr["UserType"].ToString();
                        }
                    }
                }
            }
        }

        GetCoachingHist();
    }

    private void GetCoachingHist()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT Trainings.TrainingID, GoalSetting, CoachName, TrainingPackage,
                                Weight, TrainingDetails.DateAdded
                                FROM Trainings
                                INNER JOIN TrainingDetails
                                ON Trainings.TrainingID = TrainingDetails.TrainingID
                                WHERE UserID = @id ORDER BY TrainingDetails.DateAdded DESC";
            cmd.Parameters.AddWithValue("@id", hfName.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Trainings");
            lvCoaching.DataSource = ds;
            lvCoaching.DataBind();
        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"INSERT INTO Trainings
                            (UserID, CoachName, GoalSetting, TrainingPackage, TrainingFee,
                            DateAdded) VALUES
                            (@userid, @coachname, @gsetting, @tpackage, @tfee, @dadded)
                            SELECT TOP 1 TrainingID FROM Trainings ORDER BY TrainingID DESC";
            cmd.Parameters.AddWithValue("@userid", hfName.Value);
            cmd.Parameters.AddWithValue("@coachname", ddlCoach.SelectedValue);
            cmd.Parameters.AddWithValue("@gsetting", ddlGoal.SelectedValue);
            cmd.Parameters.AddWithValue("@tpackage", ddlPackage.SelectedValue);
            cmd.Parameters.AddWithValue("@tfee", txtCoachFee.Text);
            cmd.Parameters.AddWithValue("@dadded", Helper.PHTime());
            int trainID = (int) cmd.ExecuteScalar();

            cmd.CommandText = @"INSERT INTO TrainingDetails
                              (TrainingID, Age, Height, Weight, Arms, Chest, Waist,
                               Hip, Thigh, Legs, Monday, Tuesday, Wednesday,
                               Thursday, Friday, Saturday, DateAdded) VALUES
                              (@tid, @age, @hght, @wght, @arms, @chst, @wst, @hip,
                               @thgh, @lgs, @mon, @tue, @wed, @thu, @fri, @sat, @dadd)";
            cmd.Parameters.AddWithValue("@tid", trainID);
            cmd.Parameters.AddWithValue("@age", txtAge.Text);
            cmd.Parameters.AddWithValue("@hght", txtHeight.Text);
            cmd.Parameters.AddWithValue("@wght", txtWeight.Text);
            cmd.Parameters.AddWithValue("@arms", txtArms.Text);
            cmd.Parameters.AddWithValue("@chst", txtChst.Text);
            cmd.Parameters.AddWithValue("@wst", txtWst.Text);
            cmd.Parameters.AddWithValue("@hip", txtHip.Text);
            cmd.Parameters.AddWithValue("@thgh", txtThgh.Text);
            cmd.Parameters.AddWithValue("@lgs", txtLgs.Text);

            cmd.Parameters.AddWithValue("@mon", chkMon.Checked);
            cmd.Parameters.AddWithValue("@tue", chkTues.Checked);
            cmd.Parameters.AddWithValue("@wed", chkWed.Checked);
            cmd.Parameters.AddWithValue("@thu", chkThurs.Checked);
            cmd.Parameters.AddWithValue("@fri", chkFri.Checked);
            cmd.Parameters.AddWithValue("@sat", chkSat.Checked);

            cmd.Parameters.AddWithValue("@dadd", txtCurrentDate.Text);
            cmd.ExecuteNonQuery();

            GetCoachingHist();

            Helper.Log("Add Coaching",
                "Added for coaching user " + txtLN.Text + ", " + txtFN.Text, "", Session["userid"].ToString());
        }
    }

    protected void lvCoaching_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpCoachHist.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetCoachingHist();
    }

    protected void lvCoaching_OnDataBound(object sender, EventArgs e)
    {
        dpCoachHist.Visible = dpCoachHist.PageSize < dpCoachHist.TotalRowCount;
    }

    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("View.aspx");
    }

    protected void lvCoaching_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltTrainID = (Literal)e.Item.FindControl("ltTrainID");

        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT CoachName, Age, Weight, Height, Arms,
                                Chest, Waist, Hip, Thigh, Legs, GoalSetting,
                                TrainingPackage, TrainingFee, TrainingDetails.DateAdded,        
                                Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
                                FROM Trainings
                                INNER JOIN TrainingDetails
                                ON Trainings.TrainingID = TrainingDetails.TrainingID
                                WHERE Trainings.TrainingID = @id";
            cmd.Parameters.AddWithValue("@id", ltTrainID.Text);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtCoachName.Text = dr["CoachName"].ToString();
                        txtAge2.Text = dr["Age"].ToString();
                        txtWght2.Text = dr["Weight"].ToString();
                        txtHght2.Text = dr["Height"].ToString();
                        txtArms2.Text = dr["Arms"].ToString();
                        txtChst2.Text = dr["Chest"].ToString();
                        txtWst2.Text = dr["Waist"].ToString();
                        txtHip2.Text = dr["Hip"].ToString();
                        txtThgh2.Text = dr["Thigh"].ToString();
                        txtLgs2.Text = dr["Legs"].ToString();
                        txtGoal.Text = dr["GoalSetting"].ToString();
                        txtPackage.Text = dr["TrainingPackage"].ToString();
                        txtCoachFee2.Text = decimal.Parse(dr["TrainingFee"].ToString()).ToString("c");

                        DateTime dAdded = DateTime.Parse(dr["DateAdded"].ToString());
                        txtTOR.Text = dAdded.ToString("MMMM dd, yyyy");

                        chkMon2.Checked = dr["Monday"].ToString() == "1" ? true : false;
                        chkTue2.Checked = dr["Tuesday"].ToString() == "1" ? true : false;
                        chkWed2.Checked = dr["Wednesday"].ToString() == "1" ? true : false;
                        chkThu2.Checked = dr["Thursday"].ToString() == "1" ? true : false;
                        chkFri2.Checked = dr["Friday"].ToString() == "1" ? true : false;
                        chkSat2.Checked = dr["Saturday"].ToString() == "1" ? true : false;
                    }
                }
            }
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "coachingDetails", "$('#coachingDetails').modal();", true);
    }
}