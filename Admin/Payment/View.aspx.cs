using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Payment_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetPayments(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetPayments(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlPaymentType.SelectedValue == "All Payments")
            {
                cmd.CommandText = @"SELECT PaymentID, PaymentDate, ISNULL(SubID, '1') AS PaymentType, 
                                    Amount, ORNo
                                    FROM Payments
                                    WHERE (PaymentID LIKE @keyword OR 
                                    PaymentDate LIKE @keyword OR 
                                    Amount LIKE @keyword OR
                                    ORNo LIKE @keyword)
                                    ORDER BY PaymentDate DESC";
            }
            else if (ddlPaymentType.SelectedValue == "Memberships")
            {
                cmd.CommandText = @"SELECT PaymentID, PaymentDate, ISNULL(SubID, '1') AS PaymentType, 
                                    Amount, ORNo
                                    FROM Payments
                                    WHERE (PaymentID LIKE @keyword OR 
                                    PaymentDate LIKE @keyword OR 
                                    Amount LIKE @keyword OR
                                    ORNo LIKE @keyword)
                                    AND SubID IS NULL ORDER BY PaymentDate DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT PaymentID, PaymentDate, ISNULL(MembershipID, '2') AS PaymentType, 
                                    Amount, ORNo
                                    FROM Payments
                                    WHERE (PaymentID LIKE @keyword OR 
                                    PaymentDate LIKE @keyword OR 
                                    Amount LIKE @keyword OR
                                    ORNo LIKE @keyword)
                                    AND MembershipID IS NULL ORDER BY PaymentDate DESC";
            }
            cmd.Parameters.AddWithValue("@title", ddlPaymentType.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Payments");
            lvPayments.DataSource = ds;
            lvPayments.DataBind();
        }
    }

    protected void ddlPaymentType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetPayments(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetPayments(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetPayments(txtSearch.Text);
    }

    protected void lvPayments_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpPayments.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetPayments(txtSearch.Text);
    }

    protected void lvPayments_OnDataBound(object sender, EventArgs e)
    {
        dpPayments.Visible = dpPayments.PageSize < dpPayments.TotalRowCount;
    }
}