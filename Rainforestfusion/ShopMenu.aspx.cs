using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ShopMenu : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tableSR = new DataTable();
    string Qty1;
    string member;
    string compnayphone;
    string amount;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindData();
        CategoryDDLDataBind();
        VatRate();
    }


    protected void btncustomizeampunt_Click(object sender, EventArgs e)
    {
        if (lbltotal.Text == string.Empty)
        {
            Label4.ForeColor = System.Drawing.Color.Red;
            Label4.Text = "Please insert Charge amount";
        }
        else
        {
            string iName = "Paypal Textbox";
            string custname = "Mohammad Sumon Miah";
            string address = "8564, Rue New Ontorika";
            string state = "Ontario";

            string responseURL = RedirectToPaypal.getItemNameAndCost(iName, lbltotal.Text, custname, address, state);
            Session["Amount"] = lbltotal.Text;
            Session["custname"] = custname;
            Session["iName"] = iName;
            Session["address"] = address;
            Session["states"] = state;
            Response.Redirect(responseURL);
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

    public void VatRate()
    {


        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select VatRate from tbl_settings where Location='" + Label1.Text.ToString() + "'";
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
    protected void BindData()
    {



        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ID, ItemCode as [Code],ItemName, CONVERT(int, ItemQty) as [Qty], RetailPrice	 as Price,Discount as [Disc] , CAST((RetailPrice -((RetailPrice * Discount) / 100))	as numeric(18,2)) as Total , Photo , Tax,Description from    tbl_Item where   Status = 1 AND ShopID='" + Label1.Text + "'";

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
            cmd.CommandText = "Select ID, ItemCode as [Code],	ItemName, CONVERT(int, ItemQty) as [Qty], RetailPrice	 as Price,Discount as [Disc] , CAST((RetailPrice -((RetailPrice * Discount) / 100))	as numeric(18,2)) as Total ,  Photo	, Tax ,Description from    tbl_Item where ItemCategory ='" + btn.Text + "'  and Status = 1 AND ShopID='" + Label1.Text + "'";

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
    public void CategoryDDLDataBind()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ItemCategory from tbl_Category  where  ShopID='" + Label1.Text + "'";

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

}