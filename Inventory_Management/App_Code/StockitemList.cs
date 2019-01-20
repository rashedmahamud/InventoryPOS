using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockitemList
/// </summary>
public class StockitemList
{
	 public string Code { get; set; }
     public string  Name { get; set; }
     public string Category { get; set; }
     public string Price { get; set; }
     public string Retail_Price { get; set; }
     public string Current_Stock { get; set; }
     public string Minimum_Qty { get; set; }
     public string Maximum_Qty { get; set; }
     public string Manufacture_date { get; set; }
     public string Expire_Date { get; set; }
     public string Description { get; set; }
     public string Status { get; set; }
     public string Supplier_Id { get; set; }
     public string ShopID { get; set; }
}