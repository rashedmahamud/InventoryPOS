﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ReturnProudct : System.Web.UI.Page
{



    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tablereturn = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label11.Text = Request.Cookies["InventMgtCookies"]["ShopID"].ToString();

            txtItemSearch.Focus();
            CategoryDDLDataBind();
          //  ItemsListDataBind(DDLCategory.Text);
            CustomerNameDDLDataBind();
            VatRate();
            BindData();

            //Add item from item list 
            
            tablereturn.Columns.Add("Code", typeof(string));
            tablereturn.Columns.Add("Item Name", typeof(string));
            tablereturn.Columns.Add("Qty", typeof(string));
            tablereturn.Columns.Add("Price", typeof(string));
            tablereturn.Columns.Add("Disc%", typeof(string));
            tablereturn.Columns.Add("Total", typeof(string));
            Session["valuereturn"] = tablereturn;           
        }    

    }


    // Vat Rate from Settings default VAT Rate is 5 % 
    public void VatRate()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select VatRate from tbl_settings where Location='" + Label11.Text.ToString() + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    lblVatRate.Text = (rd4["VatRate"].ToString());
                    //l1.Text = rd4["Phone"].ToString();


                }


                cn.Close();
            }

        }
        catch
        {
        }

    }
    
    //Datalits Item List   
    protected void BindData()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ID, ItemCode as [Code],ItemName, CONVERT(int, ItemQty) as [Qty], RetailPrice	 as Price,Discount as [Disc] , CAST((RetailPrice -((RetailPrice * Discount) / 100))	as numeric(18,2)) as Total , Photo , Tax from    tbl_Item where   Status = 1 AND ShopID='" + Label11.Text + "'";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds2);

            cmd.ExecuteNonQuery();

            DataList1.Dispose();
            DataList1.DataSource = ds2;
            DataList1.DataBind();

            cn.Close();


        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }

    }


    //Click add to cart menu
    protected void btn_Goclick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblId = (Label)item.FindControl("LblID");
        Label LblCode = (Label)item.FindControl("LblCode");
        Label LblItemName = (Label)item.FindControl("LblItemName");
        Label LblQty = (Label)item.FindControl("LblQty");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblDisc = (Label)item.FindControl("LblDisc");
        Label LblTotal = (Label)item.FindControl("LblTotal");
        TextBox txtqty = (TextBox)item.FindControl("txtqty");

        string ID = lblId.Text;
        string Code = LblCode.Text;
        string ItemName = LblItemName.Text;
        string Qty = txtqty.Text; // LblQty.Text;
        decimal QtyStock = Convert.ToDecimal(LblQty.Text);
        string Price = LblPrice.Text;
        string Disc = LblDisc.Text;
        // string Total = LblTotal.Text; 
        decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);


        //Code	ItemsName	Available_Qty	Price	Disc%	Total
        DataTable dt = (DataTable)Session["valuereturn"];
        dt.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();


        // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
        double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
        // lbldisc.Text =  pricetotal - 
        lblVat.Text = Math.Round(tex, 2).ToString();
        lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        
    }

    //Category Bind on dropdown list
    public void CategoryDDLDataBind()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ItemCategory from tbl_Category";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds1);

            cmd.ExecuteNonQuery();

            DataList2.Dispose();
            DataList2.DataSource = ds1;
            DataList2.DataBind();

            cn.Close();


        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    // Customer Name Data bind into dropdown list 
    public void CustomerNameDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers_name", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCustname.DataSource = dt;
            DDLCustname.DataTextField = "Name";
            DDLCustname.DataValueField = "Name";
            DDLCustname.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
        

    //Fix Row Width 
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 310;           
        }
    }

    //Fix Row Width  and Sum of total cost
   // double pricetotal = 0;
    double total = 0;
    double Disc = 0;
    double Qty = 0;
    protected void grdSelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
            total   += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            Disc    += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
            Qty     += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 210;
            e.Row.Cells[6].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lblsubTotal.Text = total.ToString();
            lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
        }
    }
    
   
    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["valuereturn"];
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();

        ////////Show Popup dialogbox /////////////////////// 
        //LinkButton LbDelete;
        //foreach (GridViewRow rowItem in grdSelectedItem.Rows)
        //{
        //    LbDelete = (LinkButton)(rowItem.Cells[0].FindControl("LbDelete"));
        //    LbDelete.OnClientClick = "return confirm('are you sure to delete')";            
        //}

        if (grdSelectedItem.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lblVat.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
        }
        else
        {
           // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
      
        
    }
     
    ////// Barcode scan search box  
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ReturnItemBarCodeSearch", cn);
            cmd.Parameters.AddWithValue("@ItemCode", txtItemSearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();


            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dtable = new DataTable();
            dtable.Load(sdr);
      
 
                string Code = dtable.Rows[0].ItemArray[0].ToString();
                string itemName = dtable.Rows[0].ItemArray[1].ToString();
                string disc = dtable.Rows[0].ItemArray[3].ToString();
                string Price = dtable.Rows[0].ItemArray[2].ToString();
                string Total = dtable.Rows[0].ItemArray[4].ToString();


                DataTable dt = (DataTable)Session["valuereturn"];
                dt.Rows.Add(Code, itemName, "1", Price, disc, Total);
                grdSelectedItem.DataSource = dt;
                grdSelectedItem.DataBind();
                cn.Close();

                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();

                txtItemSearch.Text = string.Empty;
                txtItemSearch.Focus();
              //  btnPayment.Focus();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }    
    }

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
        //Session.Remove("value");
        //DataTable dt = (DataTable)Session["value"];
        //dt.Rows.Remove("value");
        //grdSelectedItem.DataSource = dt;
        //grdSelectedItem.DataBind();

        grdSelectedItem.DataSource = null;        
        grdSelectedItem.DataBind();
        lblsubTotal.Text = "0";
        lblVat.Text = "0";
        lbltotal.Text = "0";
        lblTotalQty.Text = "0";
    }

    // Open Payment Popup window 
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        //LinkButton Linkdetails = sender as LinkButton;
        //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        if (grdSelectedItem.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add at least one item')", true);
          //  lbltotal.Text = "Record is empty";
        }
        else
        {
            txtReturn.Text = string.Empty;
            lblChange.Text = "0";
            lblDue.Text = "0";

            lbltotalReturnable.Text = lbltotal.Text;
           // txtReturn.Text = lbltotal.Text;           
            txtSaleInvoiceNo.Focus();
            this.ModalPopupPayment.Show();
            
        }
    }
    
    //// Paid Amount from customer   - Popup Panel
    protected void txtPaid_TextChanged(object sender, EventArgs e)
    {
        double changeAmt  = Convert.ToDouble(txtReturn.Text) - Convert.ToDouble(lbltotalReturnable.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotalReturnable.Text) < Convert.ToDouble(txtReturn.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtReturn.Text) - Convert.ToDouble(lbltotalReturnable.Text)),2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotalReturnable.Text) - Convert.ToDouble(txtReturn.Text)), 2).ToString();
        }

        this.ModalPopupPayment.Show();
        bntPay.Focus();
    }

    //Inset Multiple Row in single trXID  - Function method
    protected void SaveReturnItem()
    {
        try
        {             
             for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
             {
                GridViewRow row = grdSelectedItem.Rows[i];

                string Code     = grdSelectedItem.Rows[i].Cells[1].Text;
                string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
                string Qty      = grdSelectedItem.Rows[i].Cells[3].Text;  
                string Price    = grdSelectedItem.Rows[i].Cells[4].Text;  
                string Disc     = grdSelectedItem.Rows[i].Cells[5].Text;                 
                string Total    = grdSelectedItem.Rows[i].Cells[6].Text;  

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_ReturnItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code",        Code);
                cmd.Parameters.AddWithValue("@ItemName",    ItemName);
                cmd.Parameters.AddWithValue("@Qty",         Qty);
                cmd.Parameters.AddWithValue("@Price",       Price);
                cmd.Parameters.AddWithValue("@Disc",        Disc);
                cmd.Parameters.AddWithValue("@Total",       Total);

                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;             
                cmd.ExecuteNonQuery();
                cn.Close();

                Session["InvoiceNoOutPut"] = cmd.Parameters["@InvoiceNoOutPut"].Value.ToString();                
             } 
           
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    //Insert One Row Return payment info every one trXID
    protected void SaveReturnPaymentInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_ReturnPayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@ReturnQty",       lblTotalQty.Text);
            cmd.Parameters.AddWithValue("@Subtotal",        lblsubTotal.Text);
            cmd.Parameters.AddWithValue("@Vat",             lblVat.Text);
            cmd.Parameters.AddWithValue("@totalReturnable", lbltotalReturnable.Text);
            cmd.Parameters.AddWithValue("@payType",         DDLReturnBy.Text);
            cmd.Parameters.AddWithValue("@ReturnAmount",    txtReturn.Text);
            cmd.Parameters.AddWithValue("@changeAmount",    lblChange.Text);
            cmd.Parameters.AddWithValue("@dueAmount",       lblDue.Text);            
            cmd.Parameters.AddWithValue("@note",            txtNote.Text);
            cmd.Parameters.AddWithValue("@SalesInvoiceID",  txtSaleInvoiceNo.Text);

           
            cmd.Parameters.AddWithValue("@CustID",          lblCustID.Text);
            cmd.Parameters.AddWithValue("@CustName",        DDLCustname.Text);
            cmd.Parameters.AddWithValue("@CustContact",     lblCustContact.Text);
            cmd.Parameters.AddWithValue("@ServedBy",        Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            cmd.Parameters.AddWithValue("@ShopId",          Request.Cookies["InventMgtCookies"]["ShopID"].ToString());
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
    

    //END Point - Sales Completed and Move to ----------------- >>>>>>>>>>> Print Page
    protected void bntPay_click(object sender, EventArgs e)
    {
        SaveReturnItem();
        SaveReturnPaymentInfo();

        Session["totalPayable"] = lbltotalReturnable.Text;
        Session["vat"]          = lblVat.Text;
        Session["PaidBy"]       = DDLReturnBy.Text;
        Session["PaidAmt"]      = txtReturn.Text;
        Session["ChangeAmt"]    = lblChange.Text;
        Session["DueAmt"]       = lblDue.Text;
        Session["Note"]         = txtNote.Text;

        Session["CustName"]     = DDLCustname.Text;
        Session["CustID"]       = lblCustID.Text;
        Session["Contact"]      = lblCustContact.Text;
        Session["TotalQty"]     = lblTotalQty.Text;
        Session["InvoiceNo"]    = Session["InvoiceNoOutPut"].ToString();
        Session["servedby"]     = Request.Cookies["InventMgtCookies"]["UserID"].ToString();
        Session["ShopID"]       = Request.Cookies["InventMgtCookies"]["ShopID"].ToString();

        DataTable table = new DataTable();

        table.Columns.Add("Code", typeof(string));
        table.Columns.Add("Items", typeof(string));
        table.Columns.Add("Qty", typeof(string));
        table.Columns.Add("Price", typeof(string));
        table.Columns.Add("Disc%", typeof(string));
        table.Columns.Add("Total", typeof(string));

        // Select the all row  from the GridView control
        for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
        {
            string Code = grdSelectedItem.Rows[i].Cells[1].Text;
            string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
            string Qty = grdSelectedItem.Rows[i].Cells[3].Text;
            string Price = grdSelectedItem.Rows[i].Cells[4].Text;
            string Disc = grdSelectedItem.Rows[i].Cells[5].Text;
            string Total = grdSelectedItem.Rows[i].Cells[6].Text;

            table.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
        }       
        table.AcceptChanges();
        Session.Add("Stablereturn", table);

        Response.Redirect("~/Report/ReturnPrintPage.aspx");
    }


    //Get customer information from customer table 
    protected void DDLCustname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CustomersEvent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@CustName",DDLCustname.Text);

            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            lblCustContact.Text = dt.Rows[0].ItemArray[1].ToString();
            lblCustID.Text =   dt.Rows[0].ItemArray[0].ToString();
            cn.Close();

            this.ModalPopupPayment.Show();
            bntPay.Focus();
        }
        catch
        {
            lblCustContact.Text = "";
            lblCustID.Text = "";             
        }
    }
    protected void lnkbtnRelatedLinks_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ID, ItemCode as [Code],	ItemName, CONVERT(int, ItemQty) as [Qty], RetailPrice	 as Price,Discount as [Disc] , CAST((RetailPrice -((RetailPrice * Discount) / 100))	as numeric(18,2)) as Total ,  Photo	, Tax from    tbl_Item where ItemCategory ='" + btn.Text + "'  and Status = 1 AND ShopID='" + Label11.Text + "'";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds2);

            cmd.ExecuteNonQuery();

            DataList1.Dispose();
            DataList1.DataSource = ds2;
            DataList1.DataBind();

            cn.Close();


        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    //Items filter by category  | Categoy list from store items Table
    //protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   // ItemsListDataBind(DDLCategory.Text);
    //    BindData(DDLCategory.Text);
       
    //}


}