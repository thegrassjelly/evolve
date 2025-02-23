﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CheckIn_CheckOut : System.Web.UI.Page
{
    private static decimal _TotalConsumable;
    public static bool _isCheckedOut;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ValidateAdmin();

        int opID = 0;
        bool validOperation = int.TryParse(Request.QueryString["ID"], out opID);

        if (validOperation)
        {
            if (!IsPostBack)
            {
                GetOperations(opID);
                GetOperationDetails(opID);
                GetProducts();
                GetMemberRate();
                ComputeTotalProds();
                GetTotalAmount();
                Hides();
            }
        }
    }

    private void Hides()
    {
        if (txtStatus.Text == "Checked-Out")
        {
            btnCheckOut.Visible = false;
            btnAdd.Visible = false;

            ddlProduct.Attributes.Add("disabled", "true");
            txtQty.ReadOnly = true;
            txtAmntPaid.ReadOnly = true;
            _isCheckedOut = false;

            GetAmountPaid();
            GetTotalAmount();
        }
        else
        {
            _isCheckedOut = true;
        }
    }

    private void GetAmountPaid()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT PaidAmount
                                FROM Operations 
                                WHERE OperationID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["ID"]);
            using (var dr = cmd.ExecuteReader())
            {
                if (!dr.HasRows) return;
                if (!dr.Read()) return;

                decimal paidAmnt = decimal.Parse(dr["PaidAmount"].ToString());
                txtAmntPaid.Text = (paidAmnt - _TotalConsumable).ToString();
            }
        }
    }

    bool isExist()
    {
        bool isExist = false;

        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT ODID FROM OperationDetails
                                WHERE ProductID = @id AND OperationsID = @opid";
            cmd.Parameters.AddWithValue("@opid", Request.QueryString["ID"]);
            cmd.Parameters.AddWithValue("@id", ddlProduct.SelectedValue);
            using (var dr = cmd.ExecuteReader())
            {
                isExist = dr.HasRows;
            }
        }

        return isExist;
    }

    private void GetOperations(int opId)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT LastName, FirstName,
                                CheckIn, CheckOut, MemStatus, SubStatus,
                                SubStart, SubEnd FROM Operations
                                INNER JOIN Users ON Operations.UserID = Users.UserID
                                WHERE OperationID = @id";
            cmd.Parameters.AddWithValue("@id", opId);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtCustName.Text = dr["LastName"] + ", " + dr["FirstName"];
                        txtStatus.Text = dr["CheckOut"].ToString() != "" ? "Checked-Out" : "Checked-In";
                        txtChkIn.Text = dr["CheckIn"].ToString();
                        txtChkOut.Text = dr["CheckOut"].ToString();
                        txtMemStatus.Text = dr["MemStatus"].ToString();

                        if (dr["SubStatus"].ToString() == "Inactive")
                        {
                            txtSubStatus.Text = "No Active Subscriptions";
                        }
                        else
                        {
                            txtSubStatus.Text = Convert.ToDateTime(dr["SubStart"]).ToString("MMMM d, yyyy")
                                                + " - " +
                                                Convert.ToDateTime(dr["SubEnd"]).ToString("MMMM d, yyyy");
                        }
                    }
                }
            }
        }
    }

    private void GetMemberRate()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT MembershipType 
                                FROM Memberships
                                WHERE UserID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["UserID"]);
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtRate.Text = dr["MembershipType"].ToString();
                    }
                }

            }
        }
    }

    private void GetProducts()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
                @"SELECT ProductID, ProductName FROM Products 
                WHERE ProdTypeID = '3' OR ProdTypeID = '1'";
            SqlDataReader dr = cmd.ExecuteReader();
            ddlProduct.DataSource = dr;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductID";
            ddlProduct.DataBind();
        }
    }

    private void GetOperationDetails(int opID)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT ODID, ProductName, OperationDetails.SellingPrice, Qty,
                                OperationDetails.SellingPrice * Qty AS TotalPrice, OperationDetails.DateAdded
                                FROM OperationDetails
                                INNER JOIN Products ON OperationDetails.ProductID = Products.ProductID
                                WHERE OperationsID = @id ORDER BY DateAdded DESC";
            cmd.Parameters.AddWithValue("@id", opID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet(); 
            con.Close();
            da.Fill(ds, "OperationDetails");
            lvCheckOut.DataSource = ds;
            lvCheckOut.DataBind();
        }
    }

    private void ComputeTotalProds()
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT SUM(TotalPrice) AS Tot
                                FROM OperationDetails 
                                WHERE OperationsID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["ID"]);
            using (var dr = cmd.ExecuteReader())
            {
                if (!dr.HasRows) return;
                if (!dr.Read()) return;

                if (dr["Tot"].ToString() != "")
                {
                    _TotalConsumable = decimal.Parse(dr["Tot"].ToString());
                    ltConsumeTotal.Text = _TotalConsumable.ToString("c");
                }
                else
                {
                    _TotalConsumable = 0;
                    ltConsumeTotal.Text = "0.00";
                }
            }
        }
    }

    private void GetTotalAmount()
    {
        if (txtAmntPaid.Text != "")
        {
            decimal paidAmnt;

            if (decimal.TryParse(txtAmntPaid.Text, out paidAmnt))
            {
                ltTotalAmnt.Text = (paidAmnt + _TotalConsumable).ToString("c");
                hfTotalAmnt.Value = (paidAmnt + _TotalConsumable).ToString();
            }
        }
        else
        {
            txtAmntPaid.Text = "0.00";
            hfTotalAmnt.Value = "0";
        }

    }

    protected void txtAmntPaid_OnTextChanged(object sender, EventArgs e)
    {
        GetTotalAmount();
    }

    protected void btnCheckOut_OnClick(object sender, EventArgs e)
    {
        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"SELECT ProductID, Qty
                                FROM OperationDetails
                                WHERE OperationsID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["ID"]);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            cmd.Parameters.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int prodID = int.Parse(dr["ProductID"].ToString());
                decimal qty = decimal.Parse(dr["Qty"].ToString());

                cmd.CommandText = @"UPDATE ProductInventory
                                  SET PIQty -= @qty, DateModified = @DateModified
                                  WHERE ProductID = @pid";
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@DateModified", Helper.PHTime());
                cmd.Parameters.AddWithValue("@pid", prodID);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

            cmd.CommandText = @"UPDATE Operations
                                SET CheckOut = @chkout,
                                PaidAmount = @amnt
                                WHERE OperationID = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["ID"]);
            cmd.Parameters.AddWithValue("@chkout", Helper.PHTime());
            cmd.Parameters.AddWithValue("@amnt", hfTotalAmnt.Value);
            cmd.ExecuteNonQuery();

        }

        Response.Redirect("~/Admin/Default.aspx");
    }

    protected void lvCheckOut_OnItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal ltODID = (Literal)e.Item.FindControl("ltODID");

        using (var con = new SqlConnection(Helper.GetCon()))
        using (var cmd = new SqlCommand())
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = @"DELETE FROM OperationDetails
                                WHERE ODID = @id";
            cmd.Parameters.AddWithValue("@id", ltODID.Text);
            cmd.ExecuteNonQuery();
        }

        GetOperationDetails(int.Parse(Request.QueryString["ID"]));
        ComputeTotalProds();
        GetTotalAmount();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        if (!isExist())
        {
            errorProd.Visible = false;

            using (var con = new SqlConnection(Helper.GetCon()))
            using (var cmd = new SqlCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = @"SELECT SellingPrice FROM Products
                                WHERE ProductID = @pid";
                cmd.Parameters.AddWithValue("@pid", ddlProduct.SelectedValue);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                decimal sellingPrice = Convert.ToDecimal(dr["SellingPrice"]);
                dr.Close();
                cmd.Parameters.Clear();

                cmd.CommandText = @"INSERT INTO OperationDetails
                                (OperationsID, ProductID, SellingPrice, Qty, TotalPrice, DateAdded)
                                VALUES (@opid, @pid, @sprc, @qty, @totprice, @dadded)";
                cmd.Parameters.AddWithValue("@opid", Request.QueryString["ID"]);
                cmd.Parameters.AddWithValue("@pid", ddlProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@sprc", sellingPrice);
                cmd.Parameters.AddWithValue("@qty", txtQty.Text);
                cmd.Parameters.AddWithValue("@totprice", sellingPrice * decimal.Parse(txtQty.Text));
                cmd.Parameters.AddWithValue("@dadded", Helper.PHTime());
                cmd.ExecuteNonQuery();
            }

            GetOperationDetails(int.Parse(Request.QueryString["ID"]));
        }
        else
        {
            errorProd.Visible = true;
        }

        ComputeTotalProds();
        GetTotalAmount();
    }

 
}