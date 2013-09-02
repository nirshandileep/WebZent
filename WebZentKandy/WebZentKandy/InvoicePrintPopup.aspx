<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoicePrintPopup.aspx.cs" Inherits="InvoicePrintPopup" %>
<%@ Register Assembly="DevExpress.XtraReports.v11.1.Web, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Invoice</title>
</head>
<body>

    <form id="form1" runat="server">
    <script language="Javascript" type="text/javascript">
function UpdateDuplicate()
{
    try{
        var asd = document.getElementById('hdnInvoiceId');
        InvoicePrintPopup.UpdateInvoice(asd.value);
    }
    catch(e)
    {
        alert(e.message);
    }
}
</script>
    <div>
    <table class="form" align="center">
        <tr>
            <td style="background-image: none; background-color: #f8f8ff">
                <dxxr:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewer="<%# ReportViewer1 %>">
                    <Items>
                        <dxxr:ReportToolbarButton ItemKind="Search" ToolTip="Display the search window" />
                        <dxxr:ReportToolbarSeparator />
                        <dxxr:ReportToolbarButton ItemKind="PrintReport" ToolTip="Print the report" />
                        <dxxr:ReportToolbarButton ItemKind="PrintPage" ToolTip="Print the current page"/>
                        <dxxr:ReportToolbarSeparator />
                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="First Page" />
                        <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Previous Page" />
                        <dxxr:ReportToolbarLabel Text="Page" />
                        <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                        </dxxr:ReportToolbarComboBox>
                        <dxxr:ReportToolbarLabel Text="of" />
                        <dxxr:ReportToolbarTextBox ItemKind="PageCount" />
                        <dxxr:ReportToolbarButton ItemKind="NextPage" ToolTip="Next Page" />
                        <dxxr:ReportToolbarButton ItemKind="LastPage" ToolTip="Last Page" />
                        <dxxr:ReportToolbarSeparator />
                        <dxxr:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Export a report and save it to the disk" />
                        <dxxr:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Export a report and show it in a new window" />
                        <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                            <Elements>
                                <dxxr:ListElement Text="Pdf" Value="pdf" />
                                <dxxr:ListElement Text="Xls" Value="xls" />
                                <dxxr:ListElement Text="Rtf" Value="rtf" />
                                <dxxr:ListElement Text="Mht" Value="mht" />
                                <dxxr:ListElement Text="Text" Value="txt" />
                                <dxxr:ListElement Text="Image" Value="png" />
                            </Elements>
                        </dxxr:ReportToolbarComboBox>
                    </Items>
                    <ClientSideEvents ItemClick="function(s, e) {
	if(e.item.name == 'PrintReport' || e.item.name == 'PrintPage')
	{
	    UpdateDuplicate();
	}
}" />
                </dxxr:ReportToolbar>
                <dxxr:ReportViewer ID="ReportViewer1" runat="server" OnCacheReportDocument="ReportViewer1_CacheReportDocument" OnRestoreReportDocumentFromCache="ReportViewer1_RestoreReportDocumentFromCache" PrintUsingAdobePlugIn="False">
                </dxxr:ReportViewer>
                
            </td>
        </tr>
        <tr>
            <td style="height: 82px">
                <asp:HiddenField ID="hdnInvoiceId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
