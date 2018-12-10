using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tableSR = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtItemSearch.Focus();
        CategoryDDLDataBind();
        BindData(DDLCategory.Text);



    }


    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ItemsListDataBind(DDLCategory.Text);
        BindData(DDLCategory.Text);

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
                tableSR.Columns.Add("image", typeof(string));
                Session["valuesr"] = tableSR;
            }
            tableSR.Rows.Add(Code, ItemName, Qty, Price, Disc, Total, imgPhoto.ImageUrl);
            Session.Add("valuesr", tableSR);

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




    protected void BindData(string category)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_SR");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@category", category);
            con.Open();

            DataList1.DataSource = cmd.ExecuteReader();
            DataList1.DataBind();
            con.Close();
        }
        catch
        {
        }

    }

    public void CategoryDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CategoryDDL_SR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCategory.DataSource = dt;
            DDLCategory.DataTextField = "ItemCategory";
            DDLCategory.DataValueField = "ItemCategory";
            DDLCategory.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }









}