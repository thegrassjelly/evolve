using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Admin_Inventory_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        if (!IsPostBack)
        {
            GetProducts(txtSearch.Text);
        }

        this.Form.DefaultButton = this.btnSearch.UniqueID;
    }

    private void GetProducts(string txtSearchText)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;

            if (ddlCategory.SelectedValue == "All Products")
            {
                cmd.CommandText = @"SELECT Products.ProductID, ProductName, ProductDescription, 
                            PurchasePrice, SellingPrice, PIQty,
                            Status, ProductType, DateAdded, Products.DateModified
                            FROM Products
                            INNER JOIN ProductType ON ProductType.ProdTypeID = Products.ProdTypeID
                            INNER JOIN ProductInventory ON Products.ProductID = ProductInventory.ProductID
                            WHERE (Products.ProductID LIKE @keyword OR 
                            ProductName LIKE @keyword OR 
                            ProductDescription LIKE @keyword OR
                            ProductType = @keyword)
                            AND Status = @status ORDER BY DateAdded DESC";
            }
            else
            {
                cmd.CommandText = @"SELECT Products.ProductID, ProductName, ProductDescription, 
                            PurchasePrice, SellingPrice, PIQty,
                            Status, ProductType, DateAdded, Products.DateModified
                            FROM Products
                            INNER JOIN ProductType ON ProductType.ProdTypeID = Products.ProdTypeID
                            INNER JOIN ProductInventory ON Products.ProductID = ProductInventory.ProductID
                            WHERE (Products.ProductID LIKE @keyword OR 
                            ProductName LIKE @keyword OR 
                            ProductDescription LIKE @keyword OR
                            ProductType = @keyword)
                            AND Status = @status AND ProductType = @prodtype ORDER BY DateAdded DESC";
            }

            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@prodtype", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@keyword", "%" + txtSearchText + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Close();
            da.Fill(ds, "Products");
            lvProducts.DataSource = ds;
            lvProducts.DataBind();
        }
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetProducts(txtSearch.Text);
    }

    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetProducts(txtSearch.Text);
    }

    protected void txtSearch_OnTextChanged(object sender, EventArgs e)
    {
        GetProducts(txtSearch.Text);
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        GetProducts(txtSearch.Text);
    }

    protected void lvProducts_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        dpProducts.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        GetProducts(txtSearch.Text);
    }

    protected void lvProducts_OnDataBound(object sender, EventArgs e)
    {
        dpProducts.Visible = dpProducts.PageSize < dpProducts.TotalRowCount;
    }
}