<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintGRNPopUp.aspx.cs" Inherits="PrintGRNPopUp" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>GRN Print</title>
</head>
<body>
    <form id="form1" runat="server">

        <script language="javascript" type="text/javascript" src="JS/Dialogs.js"></script>
        <script language="javascript" type="text/javascript" src="JS/jQueryLib.js"></script>

        <table class="form" style="width: 95%" align="center">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitle" runat="server" Font-Overline="False" Font-Size="32pt" Font-Underline="True"
                        Text="GRN"></asp:Label></td>
            </tr>
            <tr>
                <td class="Content2">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                GRN Type:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:Label ID="lblGRNType" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Date:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr id="trPOCode">
                            <td align="left" valign="bottom" style="width: 86px">
                                PO Code:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:Label ID="lblPOCode" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                PO Ammount:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblPOAmount" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="trPOCodeE">
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 150px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr id="trInvoice">
                            <td align="left" style="width: 150px" valign="top">
                                Invoice Number:</td>
                            <td align="left" style="width: 150px" valign="top">
                                <asp:Label ID="lblInvoiceNo" runat="server"></asp:Label>&nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Invoive Ammount:</td>
                            <td align="left" style="width: 140px" valign="top">
                                <asp:Label ID="lblInvoiceTotal" runat="server"></asp:Label>
                                (Rs.)</td>
                        </tr>
                        <tr id="trInvoiceE">
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr id="trCustomer">
                            <td align="left" style="width: 86px" valign="bottom">
                                Customer Name:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Customer Code:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblCustomerCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="trCustomerE">
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr id="trSupplierInvNo">
                            <td align="left" style="width: 164px" valign="bottom">
                                Supplier Invoice Number:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:Label ID="lblSupplierInvNo" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Supplier Name:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="trSupplierInvNoE">
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Received Total:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:Label ID="lblReceivedTotal" runat="server"></asp:Label>&nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                GRN Number:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblGRNNo" runat="server" Font-Bold="True" Font-Size="15pt"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                    </table>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnGRNType" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td class="GridTitle" style="text-align: center; text-decoration: underline;" >
                    <strong>Recieved Items</strong></td>
            </tr>
            <tr id="trGrid" runat="server">
                <td class="Content2">
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                        CellPadding="0" CellSpacing="1" GridLines="Horizontal" Width="100%" DataKeyNames="ItemId,GRNDetailsId"
                        BorderColor="Black" BorderStyle="Solid">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ReceivedQty" HeaderText="Qty">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemValue" HeaderText="Price" DataFormatString="{0:F2}">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr id="trSalesReturn">
                <td class="Content2">
                    <table cellpadding="0" cellspacing="0">
                        <tr style="width: 86px">
                            <td style="text-align: center">
                                <strong>Credit Note:</strong></td>
                        </tr>
                        <tr style="width: 86px">
                            <td id="txtCreditNote" runat="server">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TrButtonPrint" runat="server">
                <td style="text-align: left">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="PrintSurvey()"
                        UseSubmitBehavior="False" /></td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnGRNId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnPOId" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </form>

    <script language="javascript" type="text/javascript">
    OnVoucherTypeSelectChange();
    function OnVoucherTypeSelectChange()
    {
        var hdnGRNType = document.getElementById("hdnGRNType");
     
        var trPOCode = document.getElementById("trPOCode");
        var trPOCodeE = document.getElementById("trPOCodeE");
        var trSupplierInvNo = document.getElementById("trSupplierInvNo");
        var trSupplierInvNoE = document.getElementById("trSupplierInvNoE");
        var trInvoice = document.getElementById("trInvoice");
        var trInvoiceE = document.getElementById("trInvoiceE");
        var trSalesReturn = document.getElementById("trSalesReturn");
        var trCustomer = document.getElementById("trCustomer");
        var trCustomerE = document.getElementById("trCustomerE");
        
        if(hdnGRNType.value=='1')//purchase order
        {
            $(trPOCode).show('slow', '');
            $(trPOCodeE).show('slow', '');
            $(trSupplierInvNo).show('slow', '');
            $(trSupplierInvNoE).show('slow', '');
            $(trInvoice).hide('slow', '');
            $(trInvoiceE).hide('slow', '');
            $(trSalesReturn).hide('slow', '');
            $(trCustomer).hide('slow', '');
            $(trCustomerE).hide('slow', '');
        }
        else if(hdnGRNType.value=='2')//invoice
        {
            $(trPOCode).hide('slow', '');
            $(trPOCodeE).hide('slow', '');
            $(trSupplierInvNo).hide('slow', '');
            $(trSupplierInvNoE).hide('slow', '');
            $(trInvoice).show('slow', '');
            $(trInvoiceE).show('slow', '');
            $(trSalesReturn).show('slow', '');
            $(trCustomer).show('slow', '');
            $(trCustomerE).show('slow', '');
        }
    }
    </script>

</body>
</html>
