using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounts_Sales_PrintCreateInvoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            PrintCreateInvoice();
        }
    }

    public void PrintCreateInvoice()
    {

        string ss = (string)Session["ShopID"];
        string BankAccountNumber = (string)Session["BankAccountNumber"];

        // print rdlc Report
        string bankName = null;
        string accountName = null;
        string accountNumber = null;
        string CompanyName = null;
        string ComapanyAddress = null;
        string customerMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;
        string CustomerID = null;
        string CustomerName = null;
        string CompanyMobileNumber = null;
        string BillTO = null;
        string SubTotal = null;
        string VAT_Percent = null;
        string VAT_Calculation_on_Item = null;
        string Total_after_adding_vat = null;
        string Paid = null;
        string Due = null;
        string TotalQty = null;
        string Currency = null;
        string customerPhone = null;
        string CompanyLogo = null;
        if (Session["VAT_Percent"] != null)
        {
            VAT_Percent = Session["VAT_Percent"].ToString();
        }

        if (Session["VAT_Calculation_on_Item"] != null)
        {
            VAT_Calculation_on_Item = Session["VAT_Calculation_on_Item"].ToString();
        }

        if (Session["SubTotal"] != null)
        {
            SubTotal = Session["SubTotal"].ToString();
        }
        if (Session["CustomerPhone"] != "") {

            customerPhone = (string)Session["CustomerPhone"];
        }

        if (Session["Paid"] != "")
        {
            Paid = (string)Session["Paid"];
        }

        if (Session["Due"] != "")
        {
            Due = (string)Session["Due"];
        }

        if (Session["TotalQty"] != "")
        {
            TotalQty = (string)Session["TotalQty"];
        }


        Total_after_adding_vat = (Convert.ToDouble(SubTotal) + Convert.ToDouble(VAT_Calculation_on_Item)).ToString();



        // get Invocie Item List form session

        List<CreateInvoiceItemList> ItemList = new List<CreateInvoiceItemList>();
        ItemList = (List<CreateInvoiceItemList>)Session["CreateInvoiceItemList"];


        // Get bank Information
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select *from Bank where Account_Number='" + BankAccountNumber + "' AND Branch_ID = '" + ss + "' ";
            cmd1.Connection = cn;
            cn.Open();
            SqlDataReader rd4 = cmd1.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    bankName = (rd4["Bank_Name"].ToString());
                    accountName = (rd4["Account_Name"].ToString());
                    accountNumber = (rd4["Account_Number"].ToString());
                }
            }
            else
            {
                //Button9.Text = "Guest";
            }
        }
        catch
        {

        }


        // Get Customer Information

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select *from tbl_Customer where CustPhone='" + customerPhone + "' ";
            cmd1.Connection = cn;
            cn.Open();
            SqlDataReader rd4 = cmd1.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    CustomerID = (rd4["CustID"].ToString());
                    CustomerName = (rd4["CustName"].ToString());
                    customerMobileNumber = (rd4["CustPhone"].ToString());
                    BillTO = (rd4["CustAddress"].ToString());
                }
            }
            else
            {
                //Button9.Text = "Guest";
            }
        }
        catch { }

        // Company Info

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select * from tbl_settings where Location='" + ss + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    CompanyName = (rd4["CompanyName"].ToString());
                    ComapanyAddress = rd4["CompanyAddress"].ToString();
                    CompanyWebsite = rd4["WebAddress"].ToString();
                    CompanyMobileNumber = rd4["Phone"].ToString();
                    CompanyFooterMassage = rd4["Footermsg"].ToString();
                    Currency = rd4["Currency"].ToString();
                    CompanyLogo = rd4["CompanyLogo"].ToString();
                }
                cn.Close();
            }
        }
        catch { }

          string imagePath = new Uri(Server.MapPath(CompanyLogo)).AbsoluteUri;


        var reportParameters = new ReportParameterCollection
            {
                new ReportParameter("CompanyName",CompanyName),
                new ReportParameter("ComapanyAddress",ComapanyAddress),
                new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
                new ReportParameter("CompanyWebsite",CompanyWebsite),
                new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),

                new ReportParameter("CustomerID",CustomerID),
                new ReportParameter("CustomerName",CustomerName),
                new ReportParameter("CustomerMobileNumber",customerMobileNumber),
                new ReportParameter("BillTO",BillTO),

                new ReportParameter("BankName",bankName),
                new ReportParameter("AccountName",accountName),
                new ReportParameter("AccountNumber",accountNumber),
                new ReportParameter("SubTotal",SubTotal),
                new ReportParameter ("VAT_Calculation_on_Item", VAT_Calculation_on_Item),
                new ReportParameter("VAT_Percent", VAT_Percent),
                new ReportParameter("Paid",Paid),
                new ReportParameter("Due",Due),
                new ReportParameter("TotalQty",TotalQty),
                new ReportParameter("Total_after_adding_vat",Total_after_adding_vat),
                new ReportParameter("Currency",Currency),
                new ReportParameter("CompanyLogo",imagePath)

            };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/Invoice.rdlc");

        ReportDataSource datasource = new ReportDataSource("CreateInvoice", ItemList);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);


    }
}