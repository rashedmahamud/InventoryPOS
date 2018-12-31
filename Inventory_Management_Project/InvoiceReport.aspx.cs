using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.ComponentModel;

using System.Drawing.Text;
using System.Drawing.Design;
using System.Drawing.Imaging;
public partial class InvoiceReport : System.Web.UI.Page
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
                DataTable table = Session["Stable"] as DataTable;

                grdItemList.EmptyDataText = "No Records Found";
                grdItemList.DataSource = table;
                grdItemList.DataBind();


                lblDatetime.Text = DateTime.Now.ToString("MMM dd, yyyy.  hh:mm:ss tt");
                lblvat.Text = Session["vat"].ToString();
                lblvatRate.Text = "VAT:" + Session["vatRate"].ToString() + "%";
                lbltotalpay.Text = Session["totalPayable"].ToString();
                lblpaidby.Text = Session["PaidBy"].ToString();
                lblPaidAmt.Text = Session["PaidAmt"].ToString();
                lblChange.Text = Session["ChangeAmt"].ToString();
                lblDue.Text = Session["DueAmt"].ToString();
                lblTotalQty.Text = Session["TotalQty"].ToString();

                //Customer Info on POS Print Page
                lblCustName.Text = Session["CustName"].ToString();
                lblCustID.Text = Session["CustID"].ToString();
                lblCustContactNo.Text = Session["Contact"].ToString();

                lblServedBy.Text = Session["servedby"].ToString();
                lblInvoice.Text = Session["InvoiceNo"].ToString();

                if (System.Web.HttpContext.Current.Session["ShopID"] == null)
                {
                    lblShopID.Text = "1461";
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

                    Label18.Text = (rd4["CompanyName"].ToString());
                    Label19.Text = rd4["CompanyAddress"].ToString();
                    Label20.Text = rd4["WebAddress"].ToString();
                    Label21.Text = rd4["Phone"].ToString();
                    lblFooterMessage.Text = rd4["Footermsg"].ToString();

                }


                cn.Close();
            }

        }
        catch
        {
        }
    }




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