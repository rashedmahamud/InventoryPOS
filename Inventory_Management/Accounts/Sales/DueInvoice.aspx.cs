using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Accounts_DueInvoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["invoice"] != null)
            //{
            //lblInvoiceNo.Text = Session["invoice"].ToString();
            ItemsListDataBind();
            //InvoiceInfo();
            txtPaid.Focus();
        }

    }

    // ///////  Item list Databind
    public void ItemsListDataBind()
    {

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_OrderItemList]", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString());
            //cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;



            cmd.CommandText = " Select ID as Invoice,CustName,Totalpayable,PaidAmount,DueAmount,Ordedate,ShopId from tbl_SalesPayment	where  dueAmount > 0  order by ID asc ";
            cmd.Connection = cn;
            cn.Open();

            grdItemList.DataSource = cmd.ExecuteReader();
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            cn.Close();
            //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

    public void ItemsListDataBind2()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;



            cmd.CommandText = " Select ItemCode as Barcode,ItemName as Name,Qty,Price,DiscRate as Discout,total as Total  from tbl_sales 	where SP_ID= '" + TextBox1.Text.ToString() + "'";
            cmd.Connection = cn;
            cn.Open();

            GridView1.DataSource = cmd.ExecuteReader();
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataBind();
            cn.Close();
            //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
    public void DueDetails()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " Select ItemCode as Barcode,ItemName as Name,Qty,Price,DiscRate as Discout,total as Total  from tbl_sales 	where SP_ID= '" + TextBox1.Text.ToString() + "'";
            cmd.Connection = cn;
            cn.Open();



        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
    // ///////  Invoice Info
    public void InvoiceInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;



            cmd1.CommandText = "  Select  *from     tbl_SalesPayment 	where  ID= " + Convert.ToInt64(TextBox1.Text);
            cmd1.Connection = cn;
            cn.Open();


            SqlDataReader rd = cmd1.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                Label3.Text = rd["CustName"].ToString();
                Label4.Text = rd["CustContact"].ToString();
                lblvat.Text = rd["Vat"].ToString();
                lbldis.Text = rd["discount"].ToString();
                lbltotal.Text = rd["totalpayable"].ToString();
                lblDue.Text = rd["dueAmount"].ToString();
                Label1.Text = rd["paidamount"].ToString();
            }
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
    // list column width
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 10;
        }
    }
    protected void btnReceivedPayment_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDecimal(lblDue.Text) < Convert.ToDecimal(txtPaid.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Exceed Paid amount please pay due amount')", true);
                txtPaid.Text = lblDue.Text;
            }
            else
            {
                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_INV_Insert_ReceiveDuePayment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@spid", TextBox1.Text);
                cmd.Parameters.AddWithValue("@payType", DDLPaidBy.Text);
                cmd.Parameters.AddWithValue("@paidAmount", txtPaid.Text);
                cmd.Parameters.AddWithValue("@date", txtDate.Text);
                cmd.Parameters.AddWithValue("@trxtype", "sales_Due");
                cmd.Parameters.AddWithValue("@dueAmount", lblDue.Text);
                cmd.Parameters.AddWithValue("@ServedBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());

                cmd.ExecuteNonQuery();
                cn.Close();
                InvoiceInfo();
                //txtPaid.Text = string.Empty;
                //txtPaid.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Done ')", true);
                // btnReceivedPayment.Enabled = false;
                ItemsListDataBind2();

            }

        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        try
        {



            ItemsListDataBind2();
            InvoiceInfo();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    //PrintDueInvoice
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    grdItemList.Visible = false;
    //    try
    //    {
    //        SqlConnection cn = new SqlConnection(ConnectionString);
    //        SqlCommand cmd = new SqlCommand();
    //        cmd.CommandType = CommandType.Text;

    //        cmd.CommandText = " Select ID,CustName,totalpayable,paidAmount,dueAmount,ShopId,ordedate from tbl_SalesPayment	where  dueAmount > 0 order by ID asc";
    //        cmd.Connection = cn;
    //        cn.Open();


    //        SqlDataReader rd = cmd.ExecuteReader();

    //         var dt = new DataTable();
    //         dt.Load(rd);
    //         List<DataRow> dr = dt.AsEnumerable().ToList();

    //            List<DueInvoiceList> dueList = new List<DueInvoiceList>();
    //            for (int i = 0; i < dr.Count; i++)
    //            {
    //                DueInvoiceList list = new DueInvoiceList();

    //                list.InvoiceNo = dr[i].ItemArray[0].ToString();
    //                list.CustomerName = dr[i].ItemArray[1].ToString();
    //                list.Totalpayable = Convert.ToDouble(dr[i].ItemArray[2]);
    //                list.PaidAmount = Convert.ToDouble(dr[i].ItemArray[3]);
    //                list.DueAmount =Convert.ToDouble(dr[i].ItemArray[4]);
    //                list.ShopId = dr[i].ItemArray[5].ToString();
    //                list.Ordedate = dr[i].ItemArray[6].ToString();
    //                dueList.Add(list);
    //            }


    //        //var reportParameters = new ReportParameterCollection
    //        //   {
    //        //       new ReportParameter("CompanyName",CompanyName),
    //        //       new ReportParameter("ComapanyAddress",ComapanyAddress),
    //        //       new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
    //        //       new ReportParameter("CompanyWebsite",CompanyWebsite),
    //        //       new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),

    //        //       new ReportParameter("CustomerID",CustomerID),
    //        //       new ReportParameter("CustomerName",CustomerName),
    //        //       new ReportParameter("CustomerMobileNumber",customerMobileNumber),
    //        //       new ReportParameter("BillTO",BillTO),

    //        //       new ReportParameter("BankName",bankName),
    //        //       new ReportParameter("AccountName",accountName),
    //        //       new ReportParameter("AccountNumber",accountNumber),
    //        //       new ReportParameter("SubTotal",SubTotal),
    //        //       new ReportParameter ("VAT_Calculation_on_Item", VAT_Calculation_on_Item),
    //        //       new ReportParameter("VAT_Percent", VAT_Percent),
    //        //       new ReportParameter("Paid",Paid),
    //        //       new ReportParameter("Due",Due),
    //        //       new ReportParameter("TotalQty",TotalQty),
    //        //       new ReportParameter("Total_after_adding_vat",Total_after_adding_vat)

    //        //   };


    //        ReportViewer1.ProcessingMode = ProcessingMode.Local;
    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/DueInvoiceReport.rdlc");

    //        ReportDataSource datasource = new ReportDataSource("DueInvoice", dueList);
    //        ReportViewer1.LocalReport.DataSources.Clear();

    //        ReportViewer1.LocalReport.EnableExternalImages = true;
    //        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

    //        ReportViewer1.LocalReport.DataSources.Add(datasource);
    //        //ReportViewer1.LocalReport.SetParameters(reportParameters);
    //        cn.Close();
    //    }
    //    catch
    //    {
    //        //lbtotalRow.Text = "No Records Found";
    //    }
    //}
}