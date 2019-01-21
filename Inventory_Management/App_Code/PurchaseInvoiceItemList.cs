using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PurchaseInvoiceItemList
/// </summary>
public class PurchaseInvoiceItemList
{
    public string  ItemCode { get; set; }
    public string Description { get; set; }
    public string Quantity { get; set; }

    public string UnitPrice { get; set; }

    public double Total { get; set; }
}