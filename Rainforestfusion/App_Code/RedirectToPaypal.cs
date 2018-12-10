using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Configuration;


/// <summary>
/// Summary description for RedirectToPaypal
/// </summary>
public class RedirectToPaypal
{
    
	public RedirectToPaypal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>   
    public static string getItemNameAndCost(string itemName, string itemCost, string customername, string adddress, string state)
    {
      //  Guid token = new Guid(); 
        

       string PayPalReceiverEmail   = WebConfigurationManager.AppSettings["PayPalReceiverEmail"];
       string PayPalCurrencycode    = WebConfigurationManager.AppSettings["PayPalCurrencycode"];
       string Successurl            = WebConfigurationManager.AppSettings["PayPalSuccessurl"];
       string cancelurl             = WebConfigurationManager.AppSettings["PayPalcancelurl"];
        
        //Converting String Money Value Into Decimal
       decimal price = Convert.ToDecimal(itemCost);
        //declaring empty String
       string returnURL = "";
       returnURL += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + PayPalReceiverEmail;
        //Live Url
       // returnURL += "https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=" + PayPalReceiverEmail; 

        //Passing Item Name as dynamic
       returnURL +="&item_name=" + itemName;
        //Assigning Name as Statically to Parameter
       string fname = customername; // "Raghunadh Babu"; // fname customer 
       returnURL += "&first_name" + fname;
       //Assigning City as Statically to Parameter
       string myCity = adddress;
       returnURL += "&city" + myCity;
       //Assigning State as Statically to Parameter
       string myState = state;
       returnURL += "&state" + myState;
       //Passing Amount as Dynamic
       returnURL += "&amount=" + price;
       //Passing Currency as your 
       returnURL += "&currency=" + PayPalCurrencycode;
       //retturn Url if Customer wants To return to Previous Page Success url     + "?address=" + myCity + "&states=" + myState + "&token=" + token;
       returnURL += "&return=" + Successurl;
       //retturn Url if Customer Wants To Cancel the Transaction
       returnURL += "&cancel_return=" + cancelurl;
      // Session["amount"] = "10";
       return returnURL;
    }

    public static string getShippingCart(string itemName, string itemCost, string customername, string adddress, string state)
    {
        //  Guid token = new Guid();        

        string PayPalReceiverEmail = WebConfigurationManager.AppSettings["PayPalReceiverEmail"];
        string PayPalCurrencycode = WebConfigurationManager.AppSettings["PayPalCurrencycode"];
        string Successurl = WebConfigurationManager.AppSettings["PayPalSuccessurl"];
        string cancelurl = WebConfigurationManager.AppSettings["PayPalcancelurl"];
        string invoiceno = DateTime.Now.ToString("yyyyMMddHHmmss");
        //Converting String Money Value Into Decimal
        decimal price = Convert.ToDecimal(itemCost);
        //declaring empty String
        string returnURL = "";
        returnURL += "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_cart&business=" + PayPalReceiverEmail;
        //Live Url
        // returnURL += "https://www.paypal.com/cgi-bin/webscr?cmd=_cart&business=" + PayPalReceiverEmail; 

        //Passing Item Name as dynamic
       // returnURL += "&item_name=" + itemName;
        //Assigning Name as Statically to Parameter
        string fname = customername; // "Raghunadh Babu"; // fname customer 
        returnURL += "&first_name" + fname;
        //Assigning City as Statically to Parameter
        string myCity = adddress;
        returnURL += "&city" + myCity;
        //Assigning State as Statically to Parameter
        string myState = state;
        returnURL += "&state" + myState;
        returnURL += "&item_name=" + itemName;
        returnURL += "&quantity=1";
        returnURL += "&item_number=1";
        returnURL += "&amount=" + price;
        //Passing Currency as your 
        returnURL += "&currency=" + PayPalCurrencycode;
        //retturn Url if Customer wants To return to Previous Page Success url     + "?address=" + myCity + "&states=" + myState + "&token=" + token;
        returnURL += "&return=" + Successurl;
        //retturn Url if Customer Wants To Cancel the Transaction
        returnURL += "&cancel_return=" + cancelurl;
        returnURL += "&add=1";
        returnURL += "&invoice=" + invoiceno;
        // Session["amount"] = "10";
        return returnURL;

        //Variable https://developer.paypal.com/webapps/developer/docs/classic/paypal-payments-standard/integration-guide/Appx_websitestandard_htmlvariables/

        ////   https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_cart&business=hjkhjkavbcd@gmail.com
        //&invoice=1949&currency_code=USD
        //&item_name=localhost&item_number=1&quantity=1&amount=100.00
        //&return=http://localhost:30637/Charge.aspx
        //&cancel_return=http://localhost:30637/Default.aspx&add=1
    }
}
