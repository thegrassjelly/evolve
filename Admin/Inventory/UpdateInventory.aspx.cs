using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

public partial class Admin_Inventory_UpdateInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();
    }

    [WebMethod]
    public static List<string> GetProducts(string prefixText)
    {
        List<string> name = new List<string>();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myCon"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT ProductName, ProductID
                                    FROM Products WHERE 
                                    ProductName LIKE @SearchText OR
                                    ProductID LIKE @SearchText
                                    AND Status = 'Active' ORDER BY ProductName ASC";
                cmd.Parameters.AddWithValue("@SearchText", "%" + prefixText + "%");
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string myString = dr["ProductName"] + "/vn/" + dr["ProductID"];
                        name.Add(myString);
                    }
                }
                conn.Close();
            }

        }
        return name;
    }

    protected void btnProduct_OnClick(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT ProductDescription, 
                                PurchasePrice, SellingPrice,
                                ProductType, Status, PIQty
                                FROM Products
                                INNER JOIN ProductType ON
                                Products.ProdTypeID = ProductType.ProdTypeID
                                INNER JOIN ProductInventory ON
                                Products.ProductID = ProductInventory.ProductID
                                WHERE Products.ProductID = @id";
            cmd.Parameters.AddWithValue("@id", hfProduct.Value);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtProdDesc.Text = dr["ProductDescription"].ToString();
                        txtPurPrice.Text = dr["PurchasePrice"].ToString();
                        txtSellPrice.Text = dr["SellingPrice"].ToString();
                        txtStatus.Text = dr["Status"].ToString();
                        txtProductType.Text = dr["ProductType"].ToString();
                        txtProdInvty.Text = dr["PIQty"].ToString();
                    }
                }
            }
        }
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
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
            cmd.Parameters.AddWithValue("@expname", txtProdName.Text);
            cmd.Parameters.AddWithValue("@exptype", "Restock");
            cmd.Parameters.AddWithValue("@expdesc", txtProdDesc.Text);
            cmd.Parameters.AddWithValue("@orno", txtORNo.Text);
            cmd.Parameters.AddWithValue("@cost", txtPurAmnt.Text);
            cmd.Parameters.AddWithValue("@dadded", txtDDate.Text);
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"UPDATE ProductInventory SET
                              PIQty += @qty
                              WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@qty", txtQty.Text);
            cmd.Parameters.AddWithValue("@id", hfProduct.Value);
            cmd.ExecuteNonQuery();
        }
        Helper.Log("Add Expense",
            "Product restock for " + txtProdName.Text + " quantity of " + txtQty.Text, txtProductType.Text, 
            Session["userid"].ToString());
        Response.Redirect("View.aspx");
    }
}