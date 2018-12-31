using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridview();
        }
    }

    protected void BindGridview()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("rowid", typeof(int));
        dt.Columns.Add("productname", typeof(string));
        dt.Columns.Add("price", typeof(string));
        dt.Columns.Add("Qty", typeof(string));
        dt.Columns.Add("Dis", typeof(string));
        dt.Columns.Add("Total", typeof(string));
        DataRow dr = dt.NewRow();
        dr["rowid"] = 1;
        dr["productname"] = string.Empty;
        dr["price"] = string.Empty;
        dr["Qty"] = string.Empty;
        dr["Dis"] = string.Empty;
        dr["Total"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["Curtbl"] = dt;
        gvDetails.DataSource = dt;
        gvDetails.DataBind();
    }
    private void AddNewRow()
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
                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");
                    TextBox Qty = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("Qty");
                    TextBox Dis = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("Dis");
                    TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("Total");
                    drCurrentRow = dt.NewRow();
                    drCurrentRow["rowid"] = i + 1;
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
    private void SetOldData()
    {
        int rowIndex = 0;
        if (ViewState["Curtbl"] != null)
        {
            DataTable dt = (DataTable)ViewState["Curtbl"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtname = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                    TextBox txtprice = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtPrice");
                    TextBox Qty = (TextBox)gvDetails.Rows[rowIndex].Cells[3].FindControl("Qty");
                    TextBox Dis = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("Dis");
                    TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("Total");
                    txtname.Text = dt.Rows[i]["productname"].ToString();
                    txtprice.Text = dt.Rows[i]["price"].ToString();
                    Qty.Text = dt.Rows[i]["Qty"].ToString();
                    Dis.Text = dt.Rows[i]["Dis"].ToString();
                    Total.Text = dt.Rows[i]["Total"].ToString();
                    rowIndex++;
                }
            }
        }
    }

   public void cac()
{
    double sum = 0;
    int rowIndex = 0;
    if (ViewState["Curtbl"] != null)
    {
        DataTable dt = (DataTable)ViewState["Curtbl"];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {


                TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("Total" + (rowIndex + 1));
                //TextBox Total = (TextBox)gvDetails.Rows[rowIndex].Cells[5].FindControl("Total");
               
                //Total.Text = dt.Rows[i]["Total"].ToString();
                sum = sum + Convert.ToDouble(Total.Text);
                rowIndex++;

            }
        }
    }
    Label1.Text = sum.ToString();
}
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }


    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
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


                double sum = 0;
                for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                {
                    gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);


                }
                SetOldData();
                cac();
                
            }
        }
    }


    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Total_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtPrice_TextChanged(object sender, EventArgs e)
    {
       TextBox txt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
        TextBox lblRs = (TextBox)gvRow.FindControl("Qty");
        TextBox dis = (TextBox)gvRow.FindControl("Dis");
         TextBox lblTotalRs = ( TextBox)gvRow.FindControl("Total");
        string s;
        try
        {
            s = Convert.ToString((Convert.ToInt32(txt.Text)) * (Convert.ToInt32(lblRs.Text)));

            lblTotalRs.Text = Convert.ToString(Convert.ToInt32(s) - Convert.ToInt32(dis.Text));

            decimal sum = 0;
            foreach (GridViewRow dr in gvDetails.Rows)
            {
                sum += Convert.ToDecimal(lblTotalRs.Text);

            }
            Label1.Text = sum.ToString();

        }

        catch { 
       
        }

    }

    public void dele(GridViewRow row)
    {

      
    }
    protected void Qty_TextChanged(object sender, EventArgs e)
    {
         TextBox txt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
        TextBox lblRs = (TextBox)gvRow.FindControl("txtPrice");
        TextBox dis = (TextBox)gvRow.FindControl("Dis");
         TextBox lblTotalRs = ( TextBox)gvRow.FindControl("Total");
        string s;
        try
        {
            s = Convert.ToString((Convert.ToInt32(txt.Text)) * (Convert.ToInt32(lblRs.Text)));

            lblTotalRs.Text = Convert.ToString(Convert.ToInt32(s) - Convert.ToInt32(dis.Text));
            decimal sum = 0;
            foreach (GridViewRow dr in gvDetails.Rows)
            {
                sum += Convert.ToDecimal(lblTotalRs.Text);

            }
            Label1.Text = sum.ToString();
          
        }
        catch { 
       
        }
    }
    protected void Dis_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
        TextBox lblRs = (TextBox)gvRow.FindControl("Dis");
        TextBox dis = (TextBox)gvRow.FindControl("Dis");
        TextBox lblTotalRs = (TextBox)gvRow.FindControl("Total");
        string s;
        try
        {
            s = Convert.ToString((Convert.ToInt32(txt.Text)) * (Convert.ToInt32(lblRs.Text)));

            lblTotalRs.Text = Convert.ToString(Convert.ToInt32(s) - Convert.ToInt32(dis.Text));
            decimal sum = 0;
            foreach (GridViewRow dr in gvDetails.Rows)
            {
                sum += Convert.ToDecimal(lblTotalRs.Text);

            }
            Label1.Text = sum.ToString();
         
        }
        catch
        {

        }
    }
    //public void  Calc()
    //{
     
    // TextBox t1 = (TextBox)GridV
        
    //}
   

}