﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ReturnPrintPage : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["vat"] != null)  
            {
                this.Title = "POS_Invoice#" + Session["InvoiceNo"].ToString(); 
                SystemInfo();
                DataTable table = Session["Stablereturn"] as DataTable;

                grdItemList.EmptyDataText = "No Records Found";
                grdItemList.DataSource = table;
                grdItemList.DataBind();
                

                lblDatetime.Text = DateTime.Now.ToString("MMM dd, yyyy.  hh:mm:ss tt");
                lblvat.Text             = Session["vat"].ToString();
                lbltotalpay.Text        = Session["totalPayable"].ToString();
                lblpaidby.Text          = Session["PaidBy"].ToString();
                lblPaidAmt.Text         = Session["PaidAmt"].ToString();
                lblChange.Text          = Session["ChangeAmt"].ToString();
                lblDue.Text             = Session["DueAmt"].ToString();
                lblTotalQty.Text        = Session["TotalQty"].ToString();

                //Customer Info on POS Print Page
                lblCustName.Text        = Session["CustName"].ToString();
                lblCustID.Text          = Session["CustID"].ToString();
                lblCustContactNo.Text   = Session["Contact"].ToString();

                lblServedBy.Text        =   Session["servedby"].ToString();
                lblInvoice.Text         =   Session["InvoiceNo"].ToString();

                if (System.Web.HttpContext.Current.Session["ShopID"] == null)
                {
                    lblShopID.Text = Session["ShopID"].ToString();
                }
                else
                {
                    lblShopID.Text = Session["ShopID"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Sales.aspx");
            }        

        }
       
    }

    //header part  System information
    //public void SystemInfo()    
    //{
    //    try
    //    {
    //        string s = "1461";
    //        SqlConnection cn = new SqlConnection(ConnectionString);
    //        SqlCommand cmd = new SqlCommand("SP_POS_terminalInfo", cn);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        //cmd.Parameters.AddWithValue("@ShopID",  Session["ShopID"].ToString());
    //        cmd.Parameters.AddWithValue("@ShopID", Request.Cookies["InventMgtCookies"]["ShopID"].ToString());
    //        cn.Open();

    //        SqlDataReader sdr = cmd.ExecuteReader();
    //        DataTable dt = new DataTable();
    //        dt.Load(sdr);


    //        lblshopTitle.Text = dt.Rows[0].ItemArray[0].ToString();
    //        lblshopAddress.Text = dt.Rows[0].ItemArray[1].ToString();
    //        lblPhone.Text = dt.Rows[0].ItemArray[2].ToString();
    //        lblwebAddress.Text = dt.Rows[0].ItemArray[6].ToString();
    //        //lblVATRegiNo.Text = dt.Rows[0].ItemArray[6].ToString();
    //        lblFooterMessage.Text = dt.Rows[0].ItemArray[8].ToString() + "<br/>  Email: " + dt.Rows[0].ItemArray[3].ToString();  
    //       // lblFooterMessage.Text = dt.Rows[0].ItemArray[7].ToString() + "Email: " + dt.Rows[0].ItemArray[3].ToString();    
    //        lblVATRegiNo.Text = dt.Rows[0].ItemArray[10].ToString();
    //        cn.Close();
    //    }
    //    catch
    //    {
    //    }
    //}

    public void SystemInfo()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select * from tbl_settings where Location='" + Request.Cookies["InventMgtCookies"]["ShopID"].ToString() + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    //        lblshopTitle.Text = dt.Rows[0].ItemArray[0].ToString();

                    lblshopTitle.Text = (rd4["CompanyName"].ToString());
                    lblshopAddress.Text = rd4["CompanyAddress"].ToString();
                    lblwebAddress.Text = rd4["WebAddress"].ToString();
                    lblPhone.Text = rd4["Phone"].ToString();
                    lblVATRegiNo.Text = rd4["VATRegistration"].ToString();
                    lblFooterMessage.Text = rd4["Footermsg"].ToString();

                }


                cn.Close();
            }

        }
        catch
        {
        }
    }
 

    //Fix Row Width  and Sum of total cost


    double total = 0;
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
           // e.Row.Cells[0].Width = 70;
          //  e.Row.Cells[2].Width = 310;
           // e.Row.Cells[4].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Label lblAmount = (Label)e.Row.FindControl("amountLabe");
           lblsubTotal.Text = total.ToString();
        }
    }
}