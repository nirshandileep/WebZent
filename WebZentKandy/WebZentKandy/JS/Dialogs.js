function ValidatePage(vgCheckAll2)
{
  if (Page_ClientValidate(vgCheckAll2))
  {  
   //Load Pop up
   return this.ConfirmSave('Once saved you will not be able to edit the information entered. Are you sure, you want to continue?');
  }
  else
  {
   return false;
  }
}

function AllowSave(validationGroup)
{
    if (Page_ClientValidate(validationGroup))
    {  
        return ConfirmSave('Confirm save, are you sure you want to save?');
    }
    else
    {
        return false;
    }
}

function ConfirmSave(message)
{
    if(confirm(message))
    {
        return true;
    }
    else
    {
        return false;
    }
}

//Invoice Payment type
//1-Cash
//2-Cheque
//3-Credit Card
function OnSelectedIndexChange()
{
    var dropDown = document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
    var chequeTable = document.getElementById("tbChequeDetails");
    var chequeAfter = document.getElementById("trChequeAr");
    var cardTypeDiv = document.getElementById("dvCardType");
    var cardTypeControlDiv = document.getElementById("dvCardTypeControl");
    if(dropDown.value=='2')
    {
        $(chequeTable).show('slow', '');
        $(chequeAfter).show('slow', '');
        $(cardTypeDiv).hide('slow', '');
        $(cardTypeControlDiv).hide('slow', '');
    }
    else if(dropDown.value=='3')
    {
        $(cardTypeDiv).show('slow', '');
        $(cardTypeControlDiv).show('slow', '');
        $(chequeTable).hide('slow', '');
        $(chequeAfter).hide('slow', '');
    }
    else
    {
        $(cardTypeDiv).hide('slow', '');
        $(cardTypeControlDiv).hide('slow', '');
        $(chequeTable).hide('slow', '');
        $(chequeAfter).hide('slow', '');
    }
    return true;
}

//Invoice Payment type
//-1-Please Select
//1-Customer
//2-GRN
function OnCreditOptionChange(CheditOptionControl)
{
    var dropDown = CheditOptionControl;//document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
    var trCustomer = document.getElementById("trCustomer");
    var trGRN = document.getElementById("trGRN");
    //var cardTypeDiv = document.getElementById("dvCardType");
    //var cardTypeControlDiv = document.getElementById("dvCardTypeControl");
    if(dropDown.value=='1')
    {
        $(trCustomer).show('slow', '');
        $(trGRN).hide('slow', '');
    }
    else if(dropDown.value=='2')
    {
        $(trCustomer).hide('slow', '');
        $(trGRN).show('slow', '');
    }
    else
    {
        $(trCustomer).hide('slow', '');
        $(trGRN).hide('slow', '');
    }
    return true;
}

function ValidateCardType(s,a)
{
    var ddPaymentType = document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
    var ddCardType = document.getElementById("ctl00_ContentPlaceHolder1_ddlCardType");
    if(ddPaymentType.value=='3' && ddCardType.value=="-1")
    {
        a.IsValid = false;
    }
    else
    {
        a.IsValid = true;
    }
}

function ValidateChequeNumber(s,a)
{
    var ddPaymentType = document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
    var txtChequeNo = document.getElementById("ctl00_ContentPlaceHolder1_txtChequeNumber");
    
    if(ddPaymentType.value=='2' && txtChequeNo.value=="")
    {  
        a.IsValid = false;
    }
    else
    {
        a.IsValid = true;
    }    
}

//function Confirm()
//{
//    if(confirm('Once saved you will not be able to edit the information entered. Are you sure, you want to continue?'))
//    {
// 
//        return true; 
//    }
//    else
//    {
//        return false;
//        
//    }
//}
      
//
// Print Invoice Quotation
// 
function LoadInvoiceQuotation(strQstring_A)
{

        //rVal1 = window.showModalDialog('HostFrameForInvoiceQuotation.aspx?src=PrintQuotation.aspx?'+strQstring_A, window, 'dialogWidth:700px; dialogHeight:600px; resizable: Yes; status: No; help: No; unadorned: Yes;');
        rVal1 = window.showModalDialog('HostFrameForInvoiceQuotation.aspx?src=ViewQuotation.aspx?'+strQstring_A, window, 'dialogWidth:700px; dialogHeight:600px; resizable: Yes; status: No; help: No; unadorned: Yes;');

}

// 
// Print GRN
//
function LoadGRNPrintPopup(strGRNId)
{
        rVal2 = window.showModalDialog('HostFrameForGRNPrint.aspx?src=PrintGRNPopUp.aspx&GRNId='+strGRNId, window, 'dialogWidth:700px; dialogHeight:600px; resizable: Yes; status: No; help: No; unadorned: Yes;');
}

function PrintSurvey()
{
        document.getElementById('TrButtonPrint').style.display ="none";
        window.print();
        document.getElementById('TrButtonPrint').style.display ="inline";
}