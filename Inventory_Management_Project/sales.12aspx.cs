using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
public partial class sales : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tableSR = new DataTable();
    string ShopId = "1461";
    string Qty1;
   string  member;
    string compnayphone;
   string amount;
    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            //if (Request.Cookies["InventMgtCookies"]["ShopID"].ToString() != null)
            //{
                try
                {

                    Label11.Text =ShopId .ToString();
                    txtItemSearch.Focus();
                    CategoryDDLDataBind();
                    //  ItemsListDataBind(DDLCategory.Text);
                    //CustomerNameDDLDataBind();
                    VatRate();
                    BindData();
                    //if (Session["valuesr"] != null)
                    //{
                        //  table = Session["valuesr"] as DataTable;
                        tableSR = (DataTable)Session["valuesr"];
                        dtlistgrid.DataSource = tableSR;
                        dtlistgrid.DataBind();

                        decimal sum = 0; decimal qty2 = 0;
                        foreach (DataRow dr in tableSR.Rows)
                        {
                            sum += Convert.ToDecimal(dr["Total"]);
                            qty2 += Convert.ToDecimal(dr["Qty"]);
                        }
                        lblsubTotal.Text = sum.ToString();

                        ////   double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
                        double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
                        //// lbldisc.Text = pricetotal -
                        lblVat.Text = Math.Round(tex, 2).ToString();
                        lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
                        lblTotalQty.Text = qty2.ToString();
                    //}

                }
                catch
                {
                }
            }
            else
            {
                Response.Redirect("sales.aspx");
            }

        }




    // Vat Rate from Terminal table its vary terminal to terminal Example ON rate 13% QC Rate 14.975
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
                    l1.Text = rd4["Phone"].ToString();


                }


                cn.Close();
            }

        }
        catch
        {
        }
    }





    //protected string PreviewImage(string Photo)
    //{
    //    string imageUrl = "";
    //    if (File.Exists(Photo))
    //    {
    //        FileStream fs = new FileStream(Photo, FileMode.Open, FileAccess.Read);
    //        BinaryReader br = new BinaryReader(fs);
    //        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
    //        br.Close();
    //        fs.Close();
    //        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
    //        imageUrl = "data:image/png;base64," + base64String;
    //    }
    //    return imageUrl;
    //}

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

   protected void  ImageButton1_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblId = (Label)item.FindControl("LblID");
        Label LblCode = (Label)item.FindControl("LblCode");
        Label LblItemName = (Label)item.FindControl("LblItemName");
        Label LblQty = (Label)item.FindControl("LblQty");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblDisc = (Label)item.FindControl("LblDisc");
        Label LblTotal = (Label)item.FindControl("LblTotal");
        Image imgPhoto = (Image)item.FindControl("imgPhoto");
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


        //Check Item Quantity less than 1 
        if (Convert.ToDecimal(Qty) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Purchase the Item from supplier')", true);
        }
        if (Convert.ToDecimal(Qty) > Convert.ToDecimal(QtyStock))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your given quantity is Greater than Stock Quantity')", true);
        }
        else
        {
            //Code	ItemsName	Available_Qty	Price	Disc%	Total
            if (Session["valuesr"] != null)
            {
                tableSR = Session["valuesr"] as DataTable;
            }
            else
            {



                //Add item from item list 
                //  table = (DataTable)Session["valuesr"];
                tableSR.Columns.Add("Code", typeof(string));
                tableSR.Columns.Add("ItemName", typeof(string));
                tableSR.Columns.Add("Qty", typeof(string));
                tableSR.Columns.Add("Price", typeof(string));
                tableSR.Columns.Add("Disc", typeof(string));
                tableSR.Columns.Add("Total", typeof(string));
                //tableSR.Columns.Add("image", typeof(string));
                Session["valuesr"] = tableSR;
            }

            string str = Code;
            DataRow[] result = tableSR.Select("Code='" + str + "'");
            if (result.Length > 0)
            {

                Qty1 = (from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (string)dr1["Qty"]).FirstOrDefault();
                Qty = (Convert.ToInt16(Qty1) + Convert.ToInt16(Qty)).ToString();
                //decimal   Total1 = (from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (decimal)dr1["Total"]).FirstOrDefault();
                decimal Price1 = Convert.ToDecimal((from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (string)dr1["Price"]).FirstOrDefault());

                Total = Convert.ToDecimal(Qty) * Price1;
                foreach (DataRow dr1 in tableSR.Select())
                {
                    if (dr1["Code"].ToString() == str)
                    {
                        dr1.Delete();
                    }

                }
                tableSR.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);


            }
            else
            {

                tableSR.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);

            }




            Session.Add("valuesr", tableSR);
            Qty1 = "";
            dtlistgrid.DataSource = tableSR;
            dtlistgrid.DataBind();

            decimal sum = 0; decimal qty2 = 0;
            foreach (DataRow dr in tableSR.Rows)
            {
                sum += Convert.ToDecimal(dr["Total"]);
                qty2 += Convert.ToDecimal(dr["Qty"]);
            }
            lblsubTotal.Text = sum.ToString();

            ////   double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            //// lbldisc.Text = pricetotal -
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
            lblTotalQty.Text = qty2.ToString();
        }

    }

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
        Image imgPhoto = (Image)item.FindControl("imgPhoto");
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


        //Check Item Quantity less than 1 
        if (Convert.ToDecimal(Qty) <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Purchase the Item from supplier')", true);
        }
        if (Convert.ToDecimal(Qty) > Convert.ToDecimal(QtyStock))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your given quantity is Greater than Stock Quantity')", true);
        }
        else
        {
            //Code	ItemsName	Available_Qty	Price	Disc%	Total
            if (Session["valuesr"] != null)
            {
                tableSR = Session["valuesr"] as DataTable;
            }
            else
            {

             

                //Add item from item list 
                //  table = (DataTable)Session["valuesr"];
                tableSR.Columns.Add("Code", typeof(string));
                tableSR.Columns.Add("ItemName", typeof(string));
                tableSR.Columns.Add("Qty", typeof(string));
                tableSR.Columns.Add("Price", typeof(string));
                tableSR.Columns.Add("Disc", typeof(string));
                tableSR.Columns.Add("Total", typeof(string));
                //tableSR.Columns.Add("image", typeof(string));
                Session["valuesr"] = tableSR;
            }
        
            string str = Code;
            DataRow[] result = tableSR.Select("Code='" + str + "'");
            if (result.Length > 0)
            {
               
               Qty1 = (from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (string)dr1["Qty"]).FirstOrDefault();
               Qty = (Convert.ToInt16(Qty1) + Convert.ToInt16( Qty)).ToString();
            //decimal   Total1 = (from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (decimal)dr1["Total"]).FirstOrDefault();
                         decimal   Price1 = Convert.ToDecimal( (from DataRow dr1 in tableSR.Rows where (string)dr1["Code"] == Code select (string)dr1["Price"]).FirstOrDefault());

                         Total = Convert.ToDecimal( Qty) * Price1;
               foreach (DataRow dr1 in tableSR.Select())
    {
        if (dr1["Code"].ToString() == str) 
     {
      dr1.Delete(); 
     }
        
   }
                tableSR.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
            
                
            }
            else
            {
               
                tableSR.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
               
            }
        

           
 
            Session.Add("valuesr", tableSR);
            Qty1 = "";
            dtlistgrid.DataSource = tableSR;
            dtlistgrid.DataBind();

            decimal sum = 0; decimal qty2 = 0;
            foreach (DataRow dr in tableSR.Rows)
            {
                sum += Convert.ToDecimal(dr["Total"]);
                qty2 += Convert.ToDecimal(dr["Qty"]);
            }
            lblsubTotal.Text = sum.ToString();

            ////   double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            //// lbldisc.Text = pricetotal -
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
            lblTotalQty.Text = qty2.ToString();
        }
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
            cmd.CommandText = "Select ItemCategory from tbl_Category  where  ShopID='" + Label11.Text + "'";

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
        //try
        //{
        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers_name", cn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cn.Open();

        //    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);

        //    DDLCustname.DataSource = dt;
        //    DDLCustname.DataTextField = "Name";
        //    DDLCustname.DataValueField = "Name";
        //    DDLCustname.DataBind();
        //    cn.Close();
        //}
        //catch
        //{
        //    // lbtotalRow.Text = "No Records Found";
        //}
    }

    ////// Barcode scan search box  
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemBarCodeSearch_SR", cn);
            cmd.Parameters.AddWithValue("@ItemCode", txtItemSearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();


            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dtable = new DataTable();
            dtable.Load(sdr);
            // cmd.ExecuteNonQuery();

            //  dtlistgrid.DataSource = cmd.ExecuteReader();
            // dtlistgrid.DataBind();

            string Code = dtable.Rows[0].ItemArray[0].ToString();
            string ItemName = dtable.Rows[0].ItemArray[1].ToString();
            string Disc = dtable.Rows[0].ItemArray[3].ToString();
            string Price = dtable.Rows[0].ItemArray[2].ToString();
            string Total = dtable.Rows[0].ItemArray[4].ToString();
            string image = dtable.Rows[0].ItemArray[5].ToString();

            if (Session["valuesr"] != null)
            {
                tableSR = Session["valuesr"] as DataTable;
            }
            else
            {
                //Add item from item list            
                tableSR.Columns.Add("Code", typeof(string));
                tableSR.Columns.Add("ItemName", typeof(string));
                tableSR.Columns.Add("Qty", typeof(string));
                tableSR.Columns.Add("Price", typeof(string));
                tableSR.Columns.Add("Disc", typeof(string));
                tableSR.Columns.Add("Total", typeof(string));
                tableSR.Columns.Add("image", typeof(string));
                Session["valuesr"] = tableSR;
            }
            tableSR.Rows.Add(Code, ItemName, "1", Price, Disc, Total, image);
            Session.Add("valuesr", tableSR);

            dtlistgrid.DataSource = tableSR;
            dtlistgrid.DataBind();
            cn.Close();

            decimal sum = 0; decimal qty2 = 0;
            foreach (DataRow dr in tableSR.Rows)
            {
                sum += Convert.ToDecimal(dr["Total"]);
                qty2 += Convert.ToDecimal(dr["Qty"]);
            }
            lblsubTotal.Text = sum.ToString();

            ////   double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            //// lbldisc.Text = pricetotal -
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
            lblTotalQty.Text = qty2.ToString();

            txtItemSearch.Text = string.Empty;
            txtItemSearch.Focus();
        }
        catch
        {
            txtItemSearch.Text = string.Empty;
            txtItemSearch.Focus();
            //lbtotalRow.Text = "No Records Found";
        }
    }

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
        Session.Remove("valuesr");
        dtlistgrid.DataSource = null;
        dtlistgrid.DataBind();
        lblsubTotal.Text = "0";
        lblVat.Text = "0";
        lbltotal.Text = "0";
        lblTotalQty.Text = "0";
    }


   
    protected void btnDeleteitem_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        DataListItem item = (DataListItem)btn.NamingContainer;

        int itemIndex = item.ItemIndex;
        tableSR = (DataTable)Session["valuesr"];
        tableSR.Rows.RemoveAt(itemIndex);

        dtlistgrid.DataSource = tableSR;
        dtlistgrid.DataBind();

        decimal sum = 0; decimal qty2 = 0;
        foreach (DataRow dr in tableSR.Rows)
        {
            sum += Convert.ToDecimal(dr["Total"]);
            qty2 += Convert.ToDecimal(dr["Qty"]);
        }
        lblsubTotal.Text = sum.ToString();

        ////   double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
        double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
        //// lbldisc.Text = pricetotal -
        lblVat.Text = Math.Round(tex, 2).ToString();
        lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        lblTotalQty.Text = qty2.ToString();

        if (tableSR.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
            // lbldiscountamount.Text = "0";
            //  lbldistype.Text = "0";
        }
        else
        {
            // if (lbldistype.Text == "2") { lbldiscountamount.Text = "0"; }
            //  lbltotal.Text = (Convert.ToDecimal(lblsubTotal.Text) - Convert.ToDecimal(lbldiscountamount.Text)).ToString();
        }





    }

    protected void txtPaid_TextChanged(object sender, EventArgs e)
    {
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
        bntPay.Focus();
    }

    //Inset Multiple Row in single trXID  - Function method
    protected void SaveSaleItem()
    {
        try
        {
            tableSR = (DataTable)Session["valuesr"];
            for (int i = 0; i < tableSR.Rows.Count; i++)
            {
                string Code = tableSR.Rows[i].ItemArray[0].ToString();
                string ItemName = tableSR.Rows[i].ItemArray[1].ToString();
                string Qty = tableSR.Rows[i].ItemArray[2].ToString();
                string Price = tableSR.Rows[i].ItemArray[3].ToString();
                string Disc = tableSR.Rows[i].ItemArray[4].ToString();
                string Total = tableSR.Rows[i].ItemArray[5].ToString();

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_SalesItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@ItemName", ItemName);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Disc", Disc);
                cmd.Parameters.AddWithValue("@Total", Total);
            
                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@ShopId", Session["ShopID"].ToString());
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

    //Insert One Row sales payment info every one trXID
    protected void SaveSalePaymentInfo()
    {
      



          

            //SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("insert into   tbl_SalesPayment([SalesQty],[Subtotal],[Vat],[totalpayable] ,[payType] , [paidAmount] ,[changeAmount] ,[dueAmount],[note] ,[ShopId] ,[CustID] ,[CustName] , [CustContact] ,[ServedBy],trxtype) values 	(@SalesQty, @Subtotal ,@Vat ,@totalpayable ,@payType ,@paidAmount ,@changeAmount ,@dueAmount,@note, @ShopId ,@CustID ,@CustName ,@CustContact,@ServedBy , 'POS') ", cn);
            //cmd.CommandType = CommandType.Text;
            //cn.Open();

             SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_SRsalesPayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@SalesQty", lbltotal.Text);
            cmd.Parameters.AddWithValue("@Subtotal", lblsubTotal.Text);
            cmd.Parameters.AddWithValue("@Vat", lblVat.Text);
            cmd.Parameters.AddWithValue("@totalpayable", lbltotal.Text);
            cmd.Parameters.AddWithValue("@payType", DDLPaidBy.Text);
            cmd.Parameters.AddWithValue("@paidAmount", txtPaid.Text);
            cmd.Parameters.AddWithValue("@changeAmount", lblChange.Text);
            cmd.Parameters.AddWithValue("@dueAmount", lblDue.Text);
            cmd.Parameters.AddWithValue("@note", txtNote.Text);
            cmd.Parameters.AddWithValue("@ShopId", Label11.Text);
            cmd.Parameters.AddWithValue("@CustID", lblCustID.Text);
            cmd.Parameters.AddWithValue("@CustName", Button9.Text);
            cmd.Parameters.AddWithValue("@CustContact", TextBox2.Text);
            cmd.Parameters.AddWithValue("@ServedBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
           
            cmd.ExecuteNonQuery();
            cn.Close();
        //}
        //catch
        //{
        //    // lbtotalRow.Text = "No Records Found";
        //}
    }

    //END Point - Sales Completed and Move to ----------------- >>>>>>>>>>> Print Page
    protected void bntPay_click(object sender, EventArgs e)
    {
        SaveSalePaymentInfo();
        SaveSaleItem();
       

        Session["totalPayable"] = lbltotal.Text;
        Session["vat"] = lblVat.Text;
        Session["vatRate"] = lblVatRate.Text;
        Session["PaidBy"] = DDLPaidBy.Text;
        Session["PaidAmt"] = txtPaid.Text;
        Session["ChangeAmt"] = lblChange.Text;
        Session["DueAmt"] = lblDue.Text;
        Session["Note"] = txtNote.Text;

        Session["CustName"] = Button9.Text;
        Session["CustID"] = lblCustID.Text;
        Session["Contact"] = TextBox2.Text;
        Session["TotalQty"] = lbltotal.Text;
        Session["InvoiceNo"] = Session["InvoiceNoOutPut"].ToString();
        Session["servedby"] = Request.Cookies["InventMgtCookies"]["UserID"].ToString();
        Session["ShopID"] = ShopId.ToString();

        DataTable POSprinttable = new DataTable();

        POSprinttable.Columns.Add("Code", typeof(string));
        POSprinttable.Columns.Add("Items", typeof(string));
        POSprinttable.Columns.Add("Qty", typeof(string));
        POSprinttable.Columns.Add("Price", typeof(string));
        POSprinttable.Columns.Add("Disc%", typeof(string));
        POSprinttable.Columns.Add("Total", typeof(string));

        // Select the all row  from the GridView control
        for (int i = 0; i < tableSR.Rows.Count; i++)
        {
            string Code = tableSR.Rows[i].ItemArray[0].ToString();
            string ItemName = tableSR.Rows[i].ItemArray[1].ToString();
            string Qty = tableSR.Rows[i].ItemArray[2].ToString();
            string Price = tableSR.Rows[i].ItemArray[3].ToString();
            string Disc = tableSR.Rows[i].ItemArray[4].ToString();
            string Total = tableSR.Rows[i].ItemArray[5].ToString();

            POSprinttable.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
        }
        POSprinttable.AcceptChanges();
        Session.Add("Stable", POSprinttable);
        Session.Remove("valuesr");

        //string popupScript = "<script language=javascript> window.open('New.aspx') </script>";
        //ClientScript.RegisterStartupScript(this.GetType(), "callpopup", popupScript);
        Response.Redirect("~/POS_printPage.aspx");
       
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
            cmd.Parameters.AddWithValue("@CustName", Button9.Text);


            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            lblCustContact.Text = dt.Rows[0].ItemArray[1].ToString();
            lblCustID.Text = dt.Rows[0].ItemArray[0].ToString();
            cn.Close();

            //this.ModalPopupPayment.Show();
            bntPay.Focus();
        }
        catch
        {
            lblCustContact.Text = "";
            lblCustID.Text = "";
        }
    }

    //Items filter by category  | Categoy list from store items Table
    //protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    // ItemsListDataBind(DDLCategory.Text);
    //    BindData(DDLCategory.Text);

    //}

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


    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounts/Default.aspx");
    }
    
     protected void Button8_Click(object sender, EventArgs e)
    {
       
        this.ModalPopupPayment.Hide();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {

       this.ModalPopupPayment.Show();

      
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {


    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "1";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
    
    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "2";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
      
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "3";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
    
    }
    protected void Button19_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "4";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
        bntPay.Focus();
    }
    protected void Button20_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "5";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
     
    }
    protected void Button21_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "6";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
   
    }
    protected void Button22_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "7";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

     
    }
    protected void Button23_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "8";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
     
    }

    protected void Button24_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "9";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
  
    }
    protected void Button25_Click(object sender, EventArgs e)
    {
      if (txtPaid.Text !="")
      { 
        txtPaid.Text = txtPaid.Text + ".";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

      }  //this.ModalPopupPayment.Show();
    
    }
    protected void Button26_Click(object sender, EventArgs e)
    {
        txtPaid.Text = txtPaid.Text + "0";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();
 
    }
    protected void Button31_Click(object sender, EventArgs e)
    {
        txtPaid.Text = "";
        txtPaid.Text = txtPaid.Text + "100";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();

    }
    protected void Button32_Click(object sender, EventArgs e)
    {

        txtPaid.Text = "";
        txtPaid.Text = txtPaid.Text + "500";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();

    }

    protected void Button33_Click(object sender, EventArgs e)
    {

        txtPaid.Text = "";
        txtPaid.Text = txtPaid.Text + "1000";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();

    }
    protected void Button34_Click(object sender, EventArgs e)
    {
        txtPaid.Text = "";
        
        txtPaid.Text = txtPaid.Text + "2000";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();

    }
    protected void Button35_Click(object sender, EventArgs e)
    {

        txtPaid.Text = "";
        txtPaid.Text = txtPaid.Text + "5000";
        double changeAmt = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotal.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotal.Text)), 2).ToString();
            lblDue.Text = "0";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotal.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        //this.ModalPopupPayment.Show();

    }

    protected void Button30_Click(object sender, EventArgs e)
    {
        txtPaid.Text = "";
       
        //this.ModalPopupPayment.Show();

    }

    protected void Button39_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "1";

        this.ModalPopupPayment.Show();

    }
    protected void Button40_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "2";

        this.ModalPopupPayment.Show();

    }
    protected void Button41_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "3";

        this.ModalPopupPayment.Show();

    }

    protected void Button42_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "4";

        this.ModalPopupPayment.Show();

    }

    protected void Button43_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "5";

        this.ModalPopupPayment.Show();

    }

    protected void Button44_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "6";

        this.ModalPopupPayment.Show();

    }


    protected void Button45_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "7";

        this.ModalPopupPayment.Show();

    }

    protected void Button46_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "8";

        this.ModalPopupPayment.Show();

    }
    protected void Button47_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "9";

        this.ModalPopupPayment.Show();

    }
    protected void Button48_Click(object sender, EventArgs e)
    {
        TextBox2.Text = TextBox2.Text + "0";

        this.ModalPopupPayment.Show();

    }
    protected void Button53_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";

        this.ModalPopupPayment.Show();

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        SqlConnection cn = new SqlConnection(ConnectionString);


        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandType = CommandType.Text;
        cmd1.CommandText = " select *from tbl_Customer where CustPhone='" + TextBox2.Text.Trim() + "' ";
        cmd1.Connection = cn;
        cn.Open();
        SqlDataReader rd4 = cmd1.ExecuteReader();

        if (rd4.HasRows)
        {



            while (rd4.Read())
            {



                TextBox2.Text = (rd4["CustPhone"].ToString());
                Button9.Text = (rd4["CustName"].ToString());

                lblCustID.Text = rd4["CustID"].ToString();




            }
        }
        else
        {

            Button9.Text = "Guest";


        }
    }
}