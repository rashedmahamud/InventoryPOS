using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Order_module_Default : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
             BindData("All");
            DataTable t = new DataTable();          
            t.Columns.Add(new DataColumn("Itemnames"));
            t.Columns.Add(new DataColumn("Price"));
            t.Columns.Add(new DataColumn("Qty"));
            t.Columns.Add(new DataColumn("Total"));
        //    t.Columns.Add(new DataColumn("Barcode"));
         //   t.Rows.Add("", "", "");

            ViewState["dt"] = t;
           // GridView1.DataSource = t;
         //   GridView1.DataBind();
        }
    }


    double total = 0;
  //  double Disc = 0;
   // double Qty = 0;
    protected void Setwidths(object sender, GridViewRowEventArgs e)
    {
        //TableCell a = e.Row.Cells[0];
        //a.Width = TextBox1.Width;
        //TableCell b = e.Row.Cells[1];
        //b.Width = TextBox2.Width;
        ////TableCell c = e.Row.Cells[2];
        ////c.Width = TextBox2.Width; 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
            total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
          //  Disc += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
        //    Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[1].Width = 210;
           // e.Row.Cells[6].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lblsubTotal.Text = total.ToString();
          //  lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label LblCode = (Label)item.FindControl("LblCode");
        Label LblItemName = (Label)item.FindControl("LblItemName");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblTotal = (Label)item.FindControl("LblTotal");

        string ItemName = LblItemName.Text;
        lblitenam.Text = ItemName;
        string Barcode = LblCode.Text;
        lblitemcode.Text = Barcode;
        double Total    = Convert.ToDouble(LblTotal.Text);  
        double Price = Convert.ToDouble(LblPrice.Text);        
        TextBox2.Text = Price.ToString();

        DataTable t = (DataTable)ViewState["dt"];
        long i = 1;
        int n = Finditem(ItemName);

        if (n == -1)
        {
            if (t.Rows.Count == 1 && t.Rows[0][0].ToString().Equals("") && t.Rows[0][0].ToString() == "")
            {                
                t.Rows[0][0] = lblitenam.Text;
                t.Rows[0][1] = TextBox2.Text;
                t.Rows[0][2] = i;
                t.Rows[0][3] = Total;
             //   t.Rows[0][4] = lblitemcode.Text;
                goto kk;
            }

            t.Rows.Add( lblitenam.Text, TextBox2.Text, i, Total );

        }
        else
        {
            t.Rows[n][0] = lblitenam.Text;
            t.Rows[n][1] = TextBox2.Text;

            double x = Convert.ToDouble(t.Rows[n][2].ToString());
            t.Rows[n][2] = x + 1;
            t.Rows[n][3] = Total * (x + 1);
          //  t.Rows[0][4] = lblitemcode.Text;

        }
        kk:
        ViewState["dt"] = t;
        grdSelectedItem.DataSource = t;
        grdSelectedItem.DataBind();


        // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
        double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
        // lbldisc.Text =  pricetotal - 
        lblVat.Text = Math.Round(tex, 2).ToString();
        lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        return;
    }

    public int Finditem(String itemfind)
    {
        DataTable t = (DataTable)ViewState["dt"];
        int k = -1;

        foreach (DataRow row in t.Rows)
        {
            if (row[0].ToString().Equals(itemfind))
            {

                k = t.Rows.IndexOf(row);
                break;
            }
        }
        return k;

    }
    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable t = (DataTable)ViewState["dt"];
        t.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = t;
        grdSelectedItem.DataBind();

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
}