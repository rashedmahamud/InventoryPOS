using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Accounts_CreateInvoice : System.Web.UI.Page
{

    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
  
    DataTable tableSR = new DataTable();
    string ShopId = "1461";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string ShopId = Response.Cookies["InventMgtCookies"]["ShopID"];
            TextBox15.Text = DateTime.Now.ToString("dd-mm-yyyy");
            BindGridview();
            Label10.Text = ShopId;
            VatRate();
            SystemInfo();        
            Bank();
            //listbox();
            //Session["InvoiceNo"] = Session["InvoiceNoOutPut"].ToString();
        }
    }


    public void SystemInfo()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select * from tbl_settings where Location='" + ShopId.ToString() + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    Label13.Text = (rd4["CompanyName"].ToString());
                    Label14.Text = rd4["CompanyAddress"].ToString();
                    Label15.Text = rd4["WebAddress"].ToString();
                    Label16.Text = rd4["Phone"].ToString();
                    Label17.Text = rd4["Footermsg"].ToString();

                }


                cn.Close();
            }

        }
        catch
        {
        }
    }
    public void VatRate()
    {


        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select VatRate from tbl_settings where Location='" + Label10.Text + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    Label1.Text = (rd4["VatRate"].ToString());
                    //l1.Text = rd4["Phone"].ToString();


                }


                cn.Close();
            }

        }
        catch
        {
        }
    }

    protected void BindGridview()
    {
       try
       { 
        
            DataTable dt = new DataTable();
            dt.Columns.Add("rowid", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("productname", typeof(string));
            dt.Columns.Add("price", typeof(string));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Dis", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            DataRow dr = dt.NewRow();
            dr["rowid"] = 1;
            dr["ItemCode"] = string.Empty;
            dr["productname"] = string.Empty;
            dr["price"] = "0.00";
            dr["Qty"] = "0.00";
            dr["Dis"] = "0.00";
            dr["Total"] = "0.00";
            dt.Rows.Add(dr);
            ViewState["Curtbl"] = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
       }
       catch
       {

       }
    }
    private void AddNewRow()
    {
        try
        {

            int rowIndex = 0;

            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                 
                        TextBox ItemCode = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("ItemCode");
                        TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtName");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtPrice");
                        TextBox Qty = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("Qty");
                        TextBox Dis = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("Dis");
                        TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[6].FindControl("Total");
                        drCurrentRow = dt.NewRow();
                        drCurrentRow["rowid"] = i + 1;
                        //txtprice.Text = "0.00";
                        //Qty.Text = "0.00";
                        //Dis.Text = "0.00";
                        //Total.Text = "0.00";
                        dt.Rows[i - 1]["ItemCode"] = ItemCode.Text;
                        dt.Rows[i - 1]["productname"] = txtname.Text;
                        dt.Rows[i - 1]["price"] = txtprice.Text;
                        dt.Rows[i - 1]["Qty"] = Qty.Text;
                        dt.Rows[i - 1]["Dis"] = Dis.Text;
                        dt.Rows[i - 1]["Total"] = Total.Text;

                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();

                }

            }
            else
            {
                Response.Write("ViewState Value is Null");
            }
            SetOldData();
        }
        catch
        {

        }
       
    }
    private void SetOldData()
    {
        try
        {

            int rowIndex = 0;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox ItemCode = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("ItemCode");

                        TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtName");
                        TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("txtPrice");
                        TextBox Qty = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("Qty");
                        TextBox Dis = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("Dis");
                        TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[6].FindControl("Total");
                        ItemCode.Text = dt.Rows[i]["ItemCode"].ToString();
                        txtname.Text = dt.Rows[i]["productname"].ToString();
                        txtprice.Text = dt.Rows[i]["price"].ToString();
                        Qty.Text = dt.Rows[i]["Qty"].ToString();
                        Dis.Text = dt.Rows[i]["Dis"].ToString();
                        Total.Text = dt.Rows[i]["Total"].ToString();
                        //txtprice.Text = "0.00";
                        //Qty.Text = "0.00";
                        //Dis.Text = "0.00";
                        //Total.Text = "0.00";
                        rowIndex++;
                    }
                }

            }
        }
        catch
        {

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
      try
      { 
        AddNewRow();
          }
        catch
      {

      }
    }
    public void cal()
    {
      try
      {
        
        double columnTotal = 0; double Qty1 = 0;
        foreach (GridViewRow gvr in gvDetails.Rows)
        {
          
            TextBox lbl = (TextBox)(gvr.FindControl("Total"));
            TextBox lbl1 = (TextBox)(gvr.FindControl("Qty"));
            if (lbl.Text !="")
            {
            double num = Convert.ToDouble(lbl.Text);
            columnTotal = columnTotal + num;
            Qty1 = Qty1 + Convert.ToDouble(lbl1.Text);
            }
        }
        String TotalString = columnTotal.ToString();
        Label4.Text = TotalString;
        Label11.Text = Qty1.ToString();

        double tex = ((Convert.ToDouble(Label4.Text) * Convert.ToDouble(Label1.Text)) / 100);
        //// lbldisc.Text = pricetotal -
        Label5.Text = Math.Round(tex, 2).ToString();
        Label5.Text = Math.Round(tex, 2).ToString();
        Label7.Text = (Convert.ToDouble(Label4.Text) + Convert.ToDouble(Label5.Text)).ToString();

        Label3.Text = Label7.Text;
        Label9.Text = (Convert.ToDouble(Label7.Text) - Convert.ToDouble(TextBox16.Text)).ToString();

           }
        catch
      {

      }
    }

    public void searchitem(GridViewRow row)
    {
       try
       { 
        
        TextBox ItemName = (TextBox)row.FindControl("txtName");
        TextBox ItemCode = (TextBox)row.FindControl("ItemCode");
        TextBox Price = (TextBox)row.FindControl("txtPrice");
        TextBox Qty = (TextBox)row.FindControl("Qty");
        TextBox Dis = (TextBox)row.FindControl("Dis");
        TextBox Total = (TextBox)row.FindControl("Total");
        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cn.Open();
        cmd.CommandText = "Select *from tbl_Item where ItemCode='" + ItemCode.Text + "' and ShopID='" + Label10.Text + "'";
        cmd.Connection = cn;
        SqlDataReader rd4 = cmd.ExecuteReader();

        if (rd4.HasRows)
        {

            while (rd4.Read())
            {

                Price.Text = (rd4["RetailPrice"].ToString());
                ItemName.Text = (rd4["ItemName"].ToString());
                Qty.Text = "0.00";
                Dis.Text = "0.00";
                Total.Text = "0.00";
                //l1.Text = rd4["Phone"].ToString();

            }


            cn.Close();
        }
       }
        catch
       {

       }
     
    }
  
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {



            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Curtbl"] = dt;
                    gvDetails.DataSource = dt;
                    gvDetails.DataBind();




                    SetOldData();
                    cal();
                }
            }
        }
        catch
        {

        }
    }

    //protected void ItemCode_TextChanged(object sender, EventArgs e)
    //{
       
    //}
     protected void Total_TextChanged(object sender, EventArgs e)
    {

        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            calculationA(row);
        }
         catch
        {

        }
    }
    protected void txtPrice_TextChanged(object sender, EventArgs e)
    {
        try
        { 
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        calculationA(row);
            }
        catch
        {

        }

    }
    protected void Qty_TextChanged(object sender, EventArgs e)
    {

        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
            calculationA(row);
        }
        catch
        {

        }
    }
    protected void Dis_TextChanged(object sender, EventArgs e)
    {
        try
        {


            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

            calculationA(row);
        }
        catch
        {

        }
     
    }
    public void customer()
    {

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select *from tbl_Customer where CustPhone='" + TextBox1.Text.Trim() + "' ";
            cmd1.Connection = cn;
            cn.Open();
            SqlDataReader rd4 = cmd1.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    TextBox1.Text = (rd4["CustID"].ToString());
                    TextBox2.Text = (rd4["CustName"].ToString());
                    TextBox3.Text = (rd4["CustPhone"].ToString());
                    TextBox4.Text = (rd4["CustAddress"].ToString());
                    TextBox13.Text = TextBox4.Text;
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
    }


    public void Bank()
    {

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select *from Bank where Account_Number='" + Label22.Text.Trim() + "' ";
            cmd1.Connection = cn;
            cn.Open();
            SqlDataReader rd4 = cmd1.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    Label12.Text = (rd4["Bank_Name"].ToString());
                    Label18.Text = (rd4["Account_Name"].ToString());
                    Label20.Text = (rd4["Account_Number"].ToString());
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
    }
    private void calculationA(GridViewRow row)
    {
       
        //try
        //{ 
        TextBox Price = (TextBox)row.FindControl("txtPrice");
        TextBox Qty = (TextBox)row.FindControl("Qty");
        TextBox Dis = (TextBox)row.FindControl("Dis");
        TextBox Total = (TextBox)row.FindControl("Total");
        string s ;
        if (Price.Text != "" && Qty.Text != "" && Dis.Text !="" && Total.Text !="" )
        {

        s=(Convert.ToDecimal(Price.Text.Trim()) * Convert.ToDecimal(Qty.Text.Trim())).ToString();
        Total.Text = Convert.ToString(Convert.ToDecimal(s) -  Convert.ToDecimal(Dis.Text.Trim().ToString())) ;

        double columnTotal = 0;
            double Qty1 = 0;
        foreach (GridViewRow gvr in gvDetails.Rows)
        {
            TextBox lbl = (TextBox)(gvr.FindControl("Total"));
            TextBox lbl1 = (TextBox)(gvr.FindControl("Qty"));
            double num = Convert.ToDouble(lbl.Text);
            columnTotal = columnTotal + num;
            Qty1 = Qty1 + Convert.ToDouble(lbl1.Text);
        }
        String TotalString = columnTotal.ToString();
        Label4.Text = TotalString;
        Label11.Text = Qty1.ToString();
       
        double tex = ((Convert.ToDouble(Label4.Text) * Convert.ToDouble(Label1.Text)) / 100);
        //// lbldisc.Text = pricetotal -
        Label5.Text = Math.Round(tex, 2).ToString();
        Label5.Text = Math.Round(tex, 2).ToString();
        Label7.Text = (Convert.ToDouble(Label4.Text) + Convert.ToDouble(Label5.Text)).ToString();
       
        Label3.Text = Label7.Text;
        }
        else
        {
            //SetOldData();
            Price.Text = "0.00";
            Qty.Text = "0.00";
            Dis.Text = "0.00";
            Total.Text = "0.00";

        }

        //    }
        //catch
        //{

        //}
    }



    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            customer();
        }
        catch
        {

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      
        try
        {
            btnAdd.Visible = false;
            LinkButton1.Visible = false;
            SaveSalePaymentInfo();
            SaveSaleItem();
            TextBox14.Text = Session["InvoiceNoOutPut"].ToString();
            Label2.Visible =false;
            Label3.Visible = false;
            
        //string myScriptValue = "function callMe() {alert('You pressed Me!'); }";
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScriptName", myScriptValue, true);

        //LinkButton1.Attributes.Add("onclick", "return javascript: printDiv('wrapper')");
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Script1", "javascript: printDiv('wrapper');", true);
       
        }
        catch
        {

        }
    }

    protected void SaveSaleItem()
    {
        try
        {

            for (int i = 0; i < gvDetails.Rows.Count; i++)
            {
                TextBox Code = (TextBox)gvDetails.Rows[i].Cells[1].FindControl("ItemCode");
                TextBox ItemName = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtName");
                TextBox txtprice = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtPrice");
                TextBox Qty = (TextBox)gvDetails.Rows[i].Cells[4].FindControl("Qty");
                TextBox Dis = (TextBox)gvDetails.Rows[i].Cells[5].FindControl("Dis");
                TextBox Total = (TextBox)gvDetails.Rows[i].Cells[6].FindControl("Total");

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_SalesItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code", Code.Text);
                cmd.Parameters.AddWithValue("@ItemName", ItemName.Text);
                cmd.Parameters.AddWithValue("@Qty", Convert.ToDouble (Qty.Text));
                cmd.Parameters.AddWithValue("@Price", Convert.ToDouble( txtprice.Text));
                cmd.Parameters.AddWithValue("@Disc", Convert.ToDouble(Dis.Text));
                cmd.Parameters.AddWithValue("@Total", Convert.ToDouble(Total.Text));

                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ShopId", ShopId.ToString());
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

    // Print Invoice 
    //************************************** Operational Area***********************************
    public void PrintInvoice()
    {



         
    
    }
    //  *******************************************end *******************************************
    //Insert One Row sales payment info every one trXID
    protected void SaveSalePaymentInfo()
    {

        try
        { 

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SP_POS_Insert_SRsalesPayment", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        cmd.Parameters.AddWithValue("@SalesQty",Label11.Text);
        cmd.Parameters.AddWithValue("@Subtotal", Label4.Text);
        cmd.Parameters.AddWithValue("@Vat", Label5.Text);
        cmd.Parameters.AddWithValue("@totalpayable", Label7.Text);
        cmd.Parameters.AddWithValue("@payType", "Cash");
        cmd.Parameters.AddWithValue("@paidAmount", TextBox16.Text);
        cmd.Parameters.AddWithValue("@changeAmount", "0.00");
        cmd.Parameters.AddWithValue("@dueAmount", Label9.Text);
        cmd.Parameters.AddWithValue("@note", "Invoice");
        cmd.Parameters.AddWithValue("@ShopId", Label10.Text);
        cmd.Parameters.AddWithValue("@CustID", TextBox1.Text);
        cmd.Parameters.AddWithValue("@CustName", TextBox2.Text);
        cmd.Parameters.AddWithValue("@CustContact", TextBox3.Text);
        cmd.Parameters.AddWithValue("@ServedBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());

        cmd.ExecuteNonQuery();
        cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    protected void ItemCode_TextChanged1(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        searchitem(row);
    }



    public void listbox()
    {

        try
        {
            ListBox t1 = (ListBox)gvDetails.FindControl("ListBox1");
            SqlConnection cn = new SqlConnection(ConnectionString);


            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " select *from tbl_Item where  ShopID='" + Label10.Text + "' ";
            cmd1.Connection = cn;
            cn.Open();
            SqlDataReader rd4 = cmd1.ExecuteReader();

            if (rd4.HasRows)
            {



                while (rd4.Read())
                {
                    //t1.Text = (rd4["Bank_Name"].ToString());
                    //Label18.Text = (rd4["Account_Name"].ToString());
                    //Label20.Text = (rd4["Account_Number"].ToString());
                    t1.Items.Add(rd4["ItemCode"].ToString());
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
    }

    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as ListBox).NamingContainer as GridViewRow;
        searchitem(row);
    }
    protected void TextBox16_TextChanged(object sender, EventArgs e)
    {
       if (Label7.Text !="" && TextBox16.Text !="")
       {
           double s;
           s = (Convert.ToDouble(Label7.Text) - Convert.ToDouble(TextBox16.Text));
           Label9.Text = Math.Round(s, 2).ToString();
       }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        List<CreateInvoiceItemList> InvoiceItemList = new List<CreateInvoiceItemList>();


        for (int i = 0; i < gvDetails.Rows.Count; i++)
        {
            CreateInvoiceItemList createInvoice = new CreateInvoiceItemList();
            TextBox Code = (TextBox)gvDetails.Rows[i].Cells[1].FindControl("ItemCode");
            TextBox ItemName = (TextBox)gvDetails.Rows[i].Cells[2].FindControl("txtName");
            TextBox txtprice = (TextBox)gvDetails.Rows[i].Cells[3].FindControl("txtPrice");
            TextBox Qty = (TextBox)gvDetails.Rows[i].Cells[4].FindControl("Qty");
            TextBox Dis = (TextBox)gvDetails.Rows[i].Cells[5].FindControl("Dis");
            TextBox Total = (TextBox)gvDetails.Rows[i].Cells[6].FindControl("Total");

            createInvoice.ItemCode = Code.Text;
            createInvoice.Name = ItemName.Text;
            createInvoice.Quantity = Qty.Text;
            createInvoice.Price = txtprice.Text;
            createInvoice.Discount = Dis.Text;
            createInvoice.Total = Total.Text;
            InvoiceItemList.Add(createInvoice);

        }

        var reportParameters = new ReportParameterCollection
            {
                new ReportParameter("CompanyLogo","Company Logo"),
                new ReportParameter("Name","Company Name"),
                new ReportParameter("Address","Address")

            };

        var viewer = new ReportViewer
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            ZoomMode = ZoomMode.FullPage,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100)
        };

        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/Invoice.rdlc");
        // Employeexsd dsCustomers = GetData("select * from Employee");
        ReportDataSource datasource = new ReportDataSource("CreateInvoice", InvoiceItemList);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        // ReportViewer1.LocalReport.SetParameters(reportParameters);
    }
}
