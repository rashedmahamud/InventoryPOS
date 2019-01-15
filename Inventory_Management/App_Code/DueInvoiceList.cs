using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DueInvoiceList
{
    public string InvoiceNo { get; set; }
    public string CustomerName { get; set; }
    public double  Totalpayable { get; set; }
    public double PaidAmount { get; set; }
    public double DueAmount { get; set; }
    public string  Ordedate { get; set; }
   public string  ShopId { get; set; }

}