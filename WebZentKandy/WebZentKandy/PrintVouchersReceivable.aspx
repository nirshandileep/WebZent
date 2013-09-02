<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintVouchersReceivable.aspx.cs"
    Inherits="PrintVouchersReceivable" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script language="javascript" src="JS/jQueryLib.js" type="text/javascript"></script>

    <script language="javascript" src="JS/Dialogs.js" type="text/javascript"></script>

    <title>Payment Print</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="form" align="center">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="report_header">
                        <p class="details_header" style="font-weight: bold; font-size: 16pt; text-transform: uppercase;
                            text-decoration: underline">
                            Voucher Print</p>
                    </td>
                </tr>
                <tr>
                    <td id="trPaymentCode" runat="server" visible="false" class="Content2">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 110px">
                                    Payment Code:
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentCode" runat="server" Font-Bold="True" Font-Size="Larger"
                                        ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Content2">
                        <div style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="left" valign="top" style="width: 110px">
                                        Customer Name:</td>
                                    <td align="left" valign="top" style="width: 150px">
                                        <div style="display: none">
                                            <asp:DropDownList ID="ddlCustomerCode" runat="server">
                                            </asp:DropDownList></div>
                                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
                                    <td align="left" style="width: 13px" valign="top">
                                    </td>
                                    <td align="left" valign="top" style="width: 129px">
                                        Payment Date:</td>
                                    <td align="left" style="width: 130px" valign="top">
                                        <asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 170px" valign="top">
                                    </td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px" valign="top">
                                        Payment Type:</td>
                                    <td align="left" style="width: 170px" valign="top">
                                        <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                        <div style="display: none">
                                            <asp:DropDownList ID="ddlPaymentType" runat="server" onchange="ChangePaymentType()">
                                                <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                <asp:ListItem Value="3">Credit Card</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td align="left" style="width: 13px" valign="top">
                                    </td>
                                    <td align="left" style="width: 129px" valign="top">
                                        Payment Amount</td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label></td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr id="trPaymentModes">
                    <td class="Content2">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="width: 110px" valign="bottom">
                                    Cheque/Card Number:</td>
                                <td align="left" style="width: 170px" valign="bottom">
                                    <asp:Label ID="lblCardNo" runat="server"></asp:Label></td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" style="width: 129px" valign="bottom">
                                    <div id="divChkDate">
                                        Cheque Date:</div>
                                </td>
                                <td align="left" style="width: 130px" valign="bottom">
                                    <div id="divChkDatePicker">
                                        <asp:Label ID="lblChqDate" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 86px" valign="bottom">
                                </td>
                                <td align="left" style="width: 170px" valign="bottom">
                                </td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" style="width: 129px" valign="bottom">
                                </td>
                                <td align="left" style="width: 130px" valign="bottom">
                                </td>
                            </tr>
                            <tr id="trCardType">
                                <td align="left" style="width: 86px" valign="bottom">
                                    Card Type:</td>
                                <td align="left" style="width: 170px" valign="bottom">
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlCardType" runat="server">
                                            <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Master</asp:ListItem>
                                            <asp:ListItem Value="2">Visa</asp:ListItem>
                                            <asp:ListItem Value="3">American Express</asp:ListItem>
                                        </asp:DropDownList></div>
                                    <asp:Label ID="lblCardType" runat="server"></asp:Label></td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" style="width: 129px" valign="bottom">
                                </td>
                                <td align="left" style="width: 130px" valign="bottom">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="report_header" align="center" style="font-weight: bold; text-transform: uppercase;
                        text-decoration: underline">
                        Voucher Details</td>
                </tr>
                <tr>
                    <td class="Content2">
                        <dx:ASPxGridView ID="dxgvPaymentDetails" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="InvoiceId" Width="100%">
                            <SettingsBehavior ProcessSelectionChangedOnServer="True"></SettingsBehavior>
                            <TotalSummary>
                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Amount" ShowInColumn="Amount" ShowInGroupFooterColumn="Amount">
                                </dx:ASPxSummaryItem>
                                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="DueAmount" ShowInColumn="Due Amount"
                                    ShowInGroupFooterColumn="Due Amount"></dx:ASPxSummaryItem>
                            </TotalSummary>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="InvoiceNo" ReadOnly="True" ShowInCustomizationForm="False"
                                    UnboundType="String" VisibleIndex="1">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Due Amount" FieldName="DueAmount" ReadOnly="True"
                                    ShowInCustomizationForm="False" VisibleIndex="4">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="InvoiceId" ReadOnly="True" ShowInCustomizationForm="False"
                                    UnboundType="Integer" Visible="False" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PaymentDetailID" ReadOnly="True" ShowInCustomizationForm="False"
                                    UnboundType="Integer" Visible="False" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Amount" UnboundType="Decimal" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="GrandTotal" UnboundType="Decimal" VisibleIndex="3">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Date" UnboundType="DateTime" VisibleIndex="2">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnPaymentID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnFromURL" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="width: 86px" valign="bottom">
                                    Comment:</td>
                                <td align="left" style="width: 170px" valign="bottom">
                                    <asp:Label ID="lblComment" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="TrButtonPrint" runat="server">
                    <td>
                        <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintSurvey()" Text="Print"
                            UseSubmitBehavior="False" /></td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

<script language="javascript" type="text/javascript">
    ChangePaymentType();
    function ChangePaymentType()
    {
        var divChkDate        = document.getElementById("divChkDate");
        var divChkDatePicker  = document.getElementById("divChkDatePicker");
        var trCardType        = document.getElementById("trCardType");
        var trPaymentModes    = document.getElementById("trPaymentModes");
        var ddlPaymentType    = document.getElementById("ddlPaymentType");
        
        if(1 == ddlPaymentType.value || (2!=ddlPaymentType.value && 3!=ddlPaymentType.value))//Cash
        {
            $(trPaymentModes).hide('slow', '');
        }
        else
        {
            $(trPaymentModes).show('slow', '');
            if(2 == ddlPaymentType.value)//Cheque
            {
                $(divChkDate).show('slow', '');
                $(divChkDatePicker).show('slow', '');
                $(trCardType).hide('slow', '');
            }
            else if(3 == ddlPaymentType.value)//card
            {
                $(divChkDate).hide('slow', '');
                $(divChkDatePicker).hide('slow', '');
                $(trCardType).show('slow', '');
            }
            else
            {
                $(divChkDate).hide('slow', '');
                $(divChkDatePicker).hide('slow', '');
                $(trCardType).hide('slow', '');
            }
        }
    }
</script>

</html>
