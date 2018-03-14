using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Administration_Pricelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
        if (!IsPostBack)
        {
            GetPriceList();
        }
    }

    private void GetPriceList()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MemReg, MemStud,
                                SubsRegOneM, SubsRegThreeM,
                                SubsRegSixM, SubsRegOneY,
                                SubsStudOneM, SubsStudThreeM,
                                SubsStudSixM, SubsStudOneY
                                FROM Pricelist";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    txtRegRate.Text = dr["MemReg"].ToString();
                    txtOneMReg.Text = dr["SubsRegOneM"].ToString();
                    txtThreeMReg.Text = dr["SubsRegThreeM"].ToString();
                    txtSixMReg.Text = dr["SubsRegSixM"].ToString();
                    txtOneYReg.Text = dr["SubsRegOneY"].ToString();

                    txtStudRate.Text = dr["MemStud"].ToString();
                    txtOneMStud.Text = dr["SubsStudOneM"].ToString();
                    txtThreeMStud.Text = dr["SubsStudThreeM"].ToString();
                    txtSixMStud.Text = dr["SubsStudSixM"].ToString();
                    txtOneYStud.Text = dr["SubsStudOneY"].ToString();
                }
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
            cmd.CommandText = @"UPDATE Pricelist SET
                                MemReg = @mreg, MemStud = @memstud,
                                SubsRegOneM = @subsregonem, SubsRegThreeM = @subsregthreem,
                                SubsRegSixM = @subsregsixm, SubsRegOneY = @subsregoney,
                                SubsStudOneM = @subsstudonem, SubsStudThreeM = @subsstudthreem,
                                SubsStudSixM = @subsstudsixm, SubsStudOneY = @subsstudoney";
            cmd.Parameters.AddWithValue("@mreg", txtRegRate.Text);
            cmd.Parameters.AddWithValue("@memstud", txtStudRate.Text);
            cmd.Parameters.AddWithValue("@subsregonem", txtOneMReg.Text);
            cmd.Parameters.AddWithValue("@subsregthreem", txtThreeMReg.Text);
            cmd.Parameters.AddWithValue("@subsregsixm", txtSixMReg.Text);
            cmd.Parameters.AddWithValue("@subsregoney", txtOneYReg.Text);
            cmd.Parameters.AddWithValue("@subsstudonem", txtOneMStud.Text);
            cmd.Parameters.AddWithValue("@subsstudthreem", txtThreeMStud.Text);
            cmd.Parameters.AddWithValue("@subsstudsixm", txtSixMStud.Text);
            cmd.Parameters.AddWithValue("@subsstudoney", txtOneYStud.Text);
            cmd.ExecuteNonQuery();
        }

        GetPriceList();
    }
}