<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PurchaseReturns.aspx.cs"
    Inherits="RecieveGoods" Title="Recieve Goods" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="JS/Dialogs.js"> </script>

    <table class="form" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False" CssClass="show_error"></asp:Label>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Purchase Returns</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            PR Code:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:Label ID="lblPRCode" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
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
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            GRN Number:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGrnId" runat="server" ValidationGroup="vggrn"></asp:TextBox><asp:Button
                                            ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Check" ValidationGroup="vggrn" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ControlToValidate="txtGrnId"
                                            Display="Dynamic" Enabled="False" ErrorMessage="GRN Id required" ValidationGroup="vggrn"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblGRNError" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Date:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <dxe:ASPxDateEdit ID="dtpReturnDate" runat="server">
                                <ValidationSettings CausesValidation="True" Display="Dynamic" ValidationGroup="vgGRNSave">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxDateEdit>
                        </td>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpReturnDate"
                                Display="Dynamic" ErrorMessage="Date Required" ValidationGroup="vgGRNSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" style="width: 86px">
                            PO Code:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:Label ID="lblPOCode" runat="server"></asp:Label></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                            Suppier Name:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
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
                    <tr>
                        <td align="left" style="width: 86px" valign="top">
                            Supplier Invoice Number:</td>
                        <td align="left" style="width: 150px" valign="top">
                            <asp:Label ID="lblSupInvNo" runat="server"></asp:Label></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Total Return Value:</td>
                        <td align="left" style="width: 140px" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInvoiceTotal" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        (Rs.)
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Add return Items</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <div>
                </div>
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="top" style="width: 86px">
                                Item Name:</td>
                            <td align="left" valign="top" style="width: 150px">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <contenttemplate>
<asp:DropDownList id="ddlItemCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged">
                                        </asp:DropDownList> 
</contenttemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Item Name:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <contenttemplate>
<asp:TextBox id="txtItemName" runat="server" TextMode="MultiLine" Rows="3" Columns="30" ReadOnly="True"></asp:TextBox> <asp:HiddenField id="hdnItemId" runat="server" Value="0"></asp:HiddenField> 
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemCode"
                                    Display="Dynamic" ErrorMessage="Item Code required" InitialValue="-1" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                Quantity:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:UpdatePanel id="UpdatePanel3" runat="server">
                                    <contenttemplate>
<dxe:ASPxSpinEdit id="dxseQty" runat="server" Height="21px" Number="0" NumberType="Integer" AllowNull="False" AutoPostBack="True" OnNumberChanged="dxseQty_NumberChanged">
    <ValidationSettings ValidationGroup="vgPOLI">
    </ValidationSettings>
</dxe:ASPxSpinEdit>
                                <asp:RangeValidator ID="rvQty" runat="server" ControlToValidate="dxseQty" Display="Dynamic"
                                    ErrorMessage="Invalid Quantity" MaximumValue="1" MinimumValue="1" Type="Integer"
                                    ValidationGroup="vgPOLI"></asp:RangeValidator>
</contenttemplate>
                                </asp:UpdatePanel></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Maximum Recievable:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <contenttemplate>
<asp:TextBox id="txtMaxRecievable" runat="server" ReadOnly="True">0</asp:TextBox>
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
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
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Unit Cost:</td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <contenttemplate>
<asp:TextBox id="txtCost" runat="server" ReadOnly="True"></asp:TextBox>
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Total Cost:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtTotalCost" runat="server" ReadOnly="True"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dxseQty" EventName="NumberChanged" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnAddItem" runat="server" OnClick="btnAddItem_Click" Text="Add Item"
                    ValidationGroup="vgPOLI" /></td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnPRDetailsId" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Items LIST</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td class="Content2">
                <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="0" CellSpacing="1" GridLines="Horizontal" Width="100%" OnRowEditing="gvItemList_RowEditing"
                    DataKeyNames="GRNDetailsId" OnRowDeleting="gvItemList_RowDeleting" OnRowDataBound="gvItemList_RowDataBound"
                    Font-Size="Larger">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Item Description">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Qty" HeaderText="Qty">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UnitCost" HeaderText="Cost Per Unit" DataFormatString="{0:F2}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TotalCost" DataFormatString="{0:F2}" HeaderText="Total Cost">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trNoRecords" runat="server" class="NoRecords" style="display: none">
            <td>
                no records
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<input id="txtLineItemsCount" type="text" style="display: none" runat="server"
                    value="0" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgGRNSave"></asp:RequiredFieldValidator>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 16px">
                &nbsp;</td>
        </tr>
        <tr id="trSalesReturn">
            <td style="height: 16px" class="Content2">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 86px">
                            <strong>Return Note:</strong></td>
                        <td>
                            <asp:TextBox ID="txtCreditNote" runat="server" Columns="40" Rows="4" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px">
                            &nbsp;</td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCreditNote"
                                Display="Dynamic" ErrorMessage="Return Note Required" ValidationGroup="vgGRNSave"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save Purchase Return" ValidationGroup="vgGRNSave"
                                OnClick="btnSave_Click" OnClientClick="return AllowSave('vgGRNSave');" />
                        </td>
                        <td>
                            <asp:Button ID="btnPrintVoucher" runat="server" OnClientClick="return ShowPrintWindow()"
                                Text="Print" Visible="False" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnPRId" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxPopupControl ID="dxPrintPR" runat="server" Width="950px" PopupVerticalAlign="WindowCenter"
                    PopupHorizontalAlign="WindowCenter" Modal="True" Height="650px" HeaderText="Purchase Returns"
                    EnableClientSideAPI="True" DragElement="Window" CloseAction="CloseButton" ClientInstanceName="voucherPrint"
                    AllowResize="True" AllowDragging="True">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function ShowPrintWindow() {
        voucherPrint.Show();
        return false;
    }
    </script>
</asp:Content>
