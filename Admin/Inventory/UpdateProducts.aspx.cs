using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Inventory_UpdateProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        int prodID = 0;
        bool validProduct = int.TryParse(Request.QueryString["ID"], out prodID);

        if (validProduct)
        {
            if (!IsPostBack)
            {
                GetProductType();
                GetProduct(prodID);
            }
        }
    }

    private void GetProduct(int prodId)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT ProductName, ProductDescription, 
                                PurchasePrice, SellingPrice,
                                ProdTypeID, Status
                                FROM Products WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@id", prodId);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtProdName.Text = dr["ProductName"].ToString();
                        txtProdDesc.Text = dr["ProductDescription"].ToString();
                        txtPurPrice.Text = dr["PurchasePrice"].ToString();
                        txtSellPrice.Text = dr["SellingPrice"].ToString();
                        ddlStatus.SelectedValue = dr["Status"].ToString();
                        ddlProdType.SelectedValue = dr["ProdTypeID"].ToString();
                    }
                }
            }
        }
    }

    private void GetProductType()
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
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        using (SqlCommand cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE Products SET ProductName = @pname,
                        ProductDescription = @pdesc, PurchasePrice = @pprice, 
                        SellingPrice = @sprice,
                        ProdTypeID = @ptypeid, Status = @status,
                        DateModified = @dmod WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["ID"]);
            cmd.Parameters.AddWithValue("@pname", txtProdName.Text);
            cmd.Parameters.AddWithValue("@pdesc", txtProdDesc.Text);
            cmd.Parameters.AddWithValue("@pprice", txtPurPrice.Text);
            cmd.Parameters.AddWithValue("@sprice", txtSellPrice.Text);
            cmd.Parameters.AddWithValue("@ptypeid", ddlProdType.SelectedValue);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@dmod", Helper.PHTime());
            cmd.ExecuteNonQuery();
        }

        Response.Redirect("View.aspx");
    }

    protected void btnBack_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("View.aspx");
    }
}