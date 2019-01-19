﻿using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounts_Sales_PrintProfitLossReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            PrintPofitLossReport();
        }

    }


    public void PrintPofitLossReport()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;


        string ss = (string)Session["ShopID"];
        string dateFrom = Session["DateFrom"].ToString();
        string dateTo =  Session["DateTo"] .ToString();
        string ShopID = Session["StoreID"].ToString();


        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;


        List<LossProfits> LossProfits = new List<LossProfits>();


        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "select   ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number 	from  tbl_sales	 where ShopId= '" + ShopID + "' and Logdate between '" + dateFrom + "'     and   '" + dateTo + "'   ";
            cmd.Connection = cn;



            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();

            for (int i = 0; i < dr.Count; i++)
            {
                LossProfits list = new LossProfits();

                list.Bar_Code = dr[i].ItemArray[0].ToString();
                list.Product_Name = dr[i].ItemArray[1].ToString();
                list.Quantity = dr[i].ItemArray[2].ToString();
                list.Retail_Price = dr[i].ItemArray[3].ToString();
                list.Discount = dr[i].ItemArray[4].ToString();
                list.Total = Convert.ToDouble(dr[i].ItemArray[5]);
                list.Profit = Convert.ToDouble(dr[i].ItemArray[6]);
                list.Recipt_Number = dr[i].ItemArray[7].ToString();
                list.Branch_Number = dr[i].ItemArray[8].ToString();


                LossProfits.Add(list);
            }
            cn.Close();

        }
        catch
        {
        }

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
                }
                cn.Close();
            }
        }
        catch { }





        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),
               new ReportParameter("DateFrom",dateFrom),
               new ReportParameter("DateTo", dateTo)
           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/LossProfitReport.rdlc");

        ReportDataSource datasource = new ReportDataSource("LossProfit", LossProfits);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);



    }


 }
