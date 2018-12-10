function printDiv(divID) {
    //Get the HTML of div
    var divElements = document.getElementById(divID).innerHTML;
    //Get the HTML of whole page
    var oldPage = document.body.innerHTML;

    //Reset the page's HTML with div's HTML only
    document.body.innerHTML =
             "<html><head><title>Print Copy</title></head><body><center>" +
              divElements + "</center></body>";
            //printWindow.document.write('<html><head><title>Report Print</title>');
    //Print Page
    window.print();
    
    //Restore orignal HTML
    document.body.innerHTML = oldPage;
}
 