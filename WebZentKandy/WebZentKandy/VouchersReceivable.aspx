<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="VouchersReceivable.aspx.cs"
    Inherits="VouchersReceivable" Title="Vouchers Receivable" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Vouchers Receivable</p>
            </td>
        </tr>
        <tr>
            <td id="trPaymentCode" runat="server" visible="false" class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 86px">
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
                            <td align="left" valign="top" style="width: 86px">
                                Customer Name:</td>
                            <td align="left" valign="top" style="width: 150px">
                                <asp:DropDownList ID="ddlCustomerCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCode_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Payment Date:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <dx:ASPxDateEdit ID="dtpPaymentDate" runat="server">
                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="Payment Date Required" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerCode"
                                    Display="Dynamic" ErrorMessage="Customer Code Required" InitialValue="-1" ValidationGroup="voucher"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpPaymentDate"
                                    Display="Dynamic" ErrorMessage="Payment Date Required" ValidationGroup="voucher"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                Payment Type:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:DropDownList ID="ddlPaymentType" runat="server" onchange="ChangePaymentType()">
                                    <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                    <asp:ListItem Value="2">Cheque</asp:ListItem>
                                    <asp:ListItem Value="3">Credit Card</asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Payment Amount</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtPaymentAmount" runat="server" Width="170px" ReadOnly="True">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text">
                                                    <RequiredField ErrorText="Payment Amount is Required" IsRequired="True" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPaymentType"
                                    Display="Dynamic" ErrorMessage="Payment Type Required" InitialValue="-1" ValidationGroup="voucher"></asp:RequiredFieldValidator>&nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPaymentAmount"
                                    Display="Dynamic" ErrorMessage="Payment Amount Required" ValidationGroup="voucher"></asp:RequiredFieldValidator></td>
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
                        <td align="left" style="width: 86px" valign="bottom">
                            Cheque/Card Number:</td>
                        <td align="left" style="width: 170px" valign="bottom">
                            <asp:TextBox ID="txtChequeNumber" runat="server"></asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            <div id="divChkDate">
                                Cheque Date:</div>
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <div id="divChkDatePicker">
                                <dx:ASPxDateEdit ID="dtpChequeDate" runat="server">
                                    <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="Text"
                                        ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="Payment Date Required" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 170px" valign="bottom">
                            &nbsp;</td>
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
                            <asp:DropDownList ID="ddlCardType" runat="server">
                                <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                <asp:ListItem Value="1">Master</asp:ListItem>
                                <asp:ListItem Value="2">Visa</asp:ListItem>
                                <asp:ListItem Value="3">American Express</asp:ListItem>
                            </asp:DropDownList></td>
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
            <td class="report_header">
                Voucher Details</td>
        </tr>
        <tr>
            <td class="Content2">
                <dx:ASPxGridView ID="dxgvPaymentDetails" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="InvoiceId" Width="100%" OnRowUpdating="dxgvPaymentDetails_RowUpdating">
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
                        <dx:GridViewDataTextColumn FieldName="DueAmount" ReadOnly="True" ShowInCustomizationForm="False"
                            VisibleIndex="4" Caption="Due Amount">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="InvoiceId" ReadOnly="True" ShowInCustomizationForm="False"
                            UnboundType="Integer" Visible="False" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PaymentDetailID" ReadOnly="True" ShowInCustomizationForm="False"
                            UnboundType="Integer" Visible="False" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Caption="#">
                            <EditButton Visible="True">
                            </EditButton>
                            <UpdateButton Visible="True">
                            </UpdateButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Amount" UnboundType="Decimal" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GrandTotal" UnboundType="Decimal" VisibleIndex="3">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Date" UnboundType="DateTime" VisibleIndex="2">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="True" />
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
                            <asp:TextBox ID="txtComment" runat="server" Columns="40" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage('voucher');"
                    ValidationGroup="voucher" />&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Print" ValidationGroup="vgPrint" Visible="False"
                    OnClientClick="return ShowPrintWindow()" />&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="Back" ValidationGroup="vgBack" Visible="False" /></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <dxpc:ASPxPopupControl ID="dxPrintInvoice" runat="server" AllowDragging="True" AllowResize="True"
                    ClientInstanceName="invoicePrint" CloseAction="CloseButton" DragElement="Window"
                    EnableClientSideAPI="True" HeaderText="Print Invoice" Height="650px" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="850px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
    ChangePaymentType();
    function ShowPrintWindow() {
        invoicePrint.Show();
        return false;
    }
        
    function ChangePaymentType()
    {
        var divChkDate        = document.getElementById("divChkDate");
        var divChkDatePicker  = document.getElementById("divChkDatePicker");
        var trCardType        = document.getElementById("trCardType");
        var trPaymentModes    = document.getElementById("trPaymentModes");
        var ddlPaymentType    = document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
        
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

</asp:Content>
