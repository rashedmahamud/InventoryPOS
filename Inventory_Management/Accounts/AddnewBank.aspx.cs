﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
public partial class Accounts_AddnewBank : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    string ShopId = "1461";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string ShopId = Session["ShopID1"].ToString();
           
            CategoryListDataBind();

            lblmsg.Visible = false;
            DropDownList3.Items.Add("Savings Account");

            DropDownList3.Items.Add(" Cheque Account");
            TextBox6.Text = ShopId.ToString();
        }
    }
    public void CategoryListDataBind()
    {
        //try
        //{
        //string ShopId = Session["ShopID1"].ToString();

        grdCategoryList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
        SqlConnection con = new SqlConnection(ConnectionString);

        SqlCommand cmd = new SqlCommand("Select *from Bank where  Branch_ID='" + ShopId.ToString() + "'", con);
        cmd.CommandType = CommandType.Text;
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        grdCategoryList.DataSource = ds;
        grdCategoryList.EmptyDataText = "No Records Found";
        grdCategoryList.DataBind();
        con.Close();
        //}
        //catch
        //{
        lbtotalRow.Text = "No Records Found";
        //}
    }

     protected void btnCategoryLink_Click(object sender, EventArgs e)
    {
        //  txtCategory.Text = string.Empty;
        lblmsg.Visible = false;
        TextBox1.Focus();
        this.MpeAddCategoryShow.Show();
    }

      protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
          try
          {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("Bank_Insert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bank_Name", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Accout_Type", DropDownList3.Text);
            cmd.Parameters.AddWithValue("@Account_Number", TextBox2.Text);
            cmd.Parameters.AddWithValue("@Account_Name", TextBox3.Text);
       
            cmd.Parameters.AddWithValue("@logby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", ShopId.ToString());
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Inserted";
            CategoryListDataBind();

            this.MpeAddCategoryShow.Show();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";

          }
          catch
          {
              lblmsg.Visible = true;
              lblmsg.Text = "Error";
          }
    }
      protected void LinkDelete_Click(object sender, EventArgs e)
      {

          try
          {
              //LinkButton Linkdetails = sender as LinkButton;
              //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

              //SqlConnection cn = new SqlConnection(ConnectionString);
              //SqlCommand cmd = new SqlCommand("Income_delete", cn);
              //cmd.CommandType = CommandType.StoredProcedure;

              //cmd.Parameters.AddWithValue("@ID", gvrow.Cells[1].Text);

              //cn.Open();
              //cmd.ExecuteNonQuery();
              //cn.Close();
              //CategoryListDataBind();
              //lbtotalRow.ForeColor = System.Drawing.Color.Red;
              //lbtotalRow.Text = "Deleted";

          }
          catch
          {

              //lbtotalRow.Text = "Error";
          }

      }

      protected void grdCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
          grdCategoryList.PageIndex = e.NewPageIndex;
          CategoryListDataBind();
      }

      protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
      {
          //txtsearch.Text = string.Empty;
          CategoryListDataBind();
      }
}

