using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Inventory_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetProductType();
        }
        this.Form.DefaultButton = this.btnSubmit.UniqueID;
    }

    void GetProductType()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        using (SqlCommand cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT ProdTypeID, ProductType FROM ProductType";
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                ddlProdType.DataSource = dr;
                ddlProdType.DataTextField = "ProductType";
                ddlProdType.DataValueField = "ProdTypeID";
                ddlProdType.DataBind();
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
            cmd.CommandText = @"INSERT INTO Products
                            (ProductName, ProductDescription, PurchasePrice, SellingPrice,
                            ProdTypeID, Status, DateAdded) VALUES
                            (@prodname, @proddesc, @pprice, @sprice, @typeid, @status, @dadded)
                            SELECT TOP 1 ProductID FROM Products ORDER BY ProductID DESC";
            cmd.Parameters.AddWithValue("@prodname", txtProdName.Text);
            cmd.Parameters.AddWithValue("@proddesc", txtProdDesc.Text);
            cmd.Parameters.AddWithValue("@pprice", txtPurPrice.Text);
            cmd.Parameters.AddWithValue("@sprice", txtSellPrice.Text);
            cmd.Parameters.AddWithValue("@typeid", ddlProdType.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@dadded", Helper.PHTime());
            int prodID = (int)cmd.ExecuteScalar();

            cmd.CommandText = @"INSERT INTO ProductInventory
                                (ProductID, PIQty, DateModified)
                                VALUES (@pid, @piqty, @dmod)";
            cmd.Parameters.AddWithValue("@pid", prodID);
            cmd.Parameters.AddWithValue("@piqty", txtProdInvty.Text);
            cmd.Parameters.AddWithValue("@dmod", Helper.PHTime());
            cmd.ExecuteNonQuery();

            Helper.Log("Add Product",
                "Added new product " + txtProdName.Text, ddlProdType.SelectedItem.Text, Session["userid"].ToString());

            Response.Redirect("View.aspx");
        }
    }
}