<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="RecieveGoods.aspx.cs"
    Inherits="RecieveGoods" Title="Recieve Goods" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="JS/Dialogs.js"> </script>

    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False" CssClass="show_error"></asp:Label><asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
                    <ProgressTemplate>
                        <div style="color: blue">Processing...</div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">Receive Goods</p></td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            GRN Type:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:RadioButtonList ID="rblGRNType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" OnSelectedIndexChanged="rblGRNType_SelectedIndexChanged"
                                Width="192px" AutoPostBack="True">
                                <asp:ListItem Value="1">Purchase Order</asp:ListItem>
                                <asp:ListItem Value="2">Sales Return</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Date:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <dxe:ASPxDateEdit ID="dtpGRNDate" runat="server">
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpGRNDate"
                                Display="Dynamic" ErrorMessage="Date Required" ValidationGroup="vgRG"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" style="width: 86px">
                            PO Code:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlPOCode" runat="server" OnSelectedIndexChanged="ddlPOCode_SelectedIndexChanged"
                                                    Enabled="False" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvPOCode" runat="server" Display="Dynamic" ErrorMessage="PO Code Required"
                                                    ValidationGroup="vgRG" ControlToValidate="ddlPOCode" Enabled="False"></asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdnPOId" runat="server" Value="0" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                            PO Ammount:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPOAmmount" runat="server" ReadOnly="True"></asp:TextBox>(Rs.)
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlPOCode" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
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
                            Invoice Number:</td>
                        <td align="left" style="width: 150px" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtInvoiceNumber" runat="server" Enabled="False"></asp:TextBox>
                                                <asp:Button ID="btnConfirm" runat="server" Text="Check" ValidationGroup="vgInvoice"
                                                    OnClick="btnConfirm_Click" Enabled="False" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" ControlToValidate="txtInvoiceNumber"
                                                    Display="Dynamic" ErrorMessage="Invoice Number Required" ValidationGroup="vgRG"
                                                    Enabled="False"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="frvInvoiceOnly" runat="server" ControlToValidate="txtInvoiceNumber"
                                                    Display="Dynamic" ErrorMessage="Invoice Number Required" ValidationGroup="vgInvoice"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblInvError" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdnInvoiceId" runat="server" Value="0" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Invoive Ammount:</td>
                        <td align="left" style="width: 140px" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            Supplier Invoice Number:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:TextBox ID="txtSupplierInvNo" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdnOldInvNumber" runat="server" />
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Suppier Name:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlPOCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:RequiredFieldValidator ID="rfvInvoiceNumber" runat="server" ControlToValidate="txtSupplierInvNo"
                                Display="Dynamic" ErrorMessage="Invoice Number Required" ValidationGroup="vgRG"></asp:RequiredFieldValidator>&nbsp;</td>
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
                            <asp:TextBox ID="txtReceivedTotal" runat="server" ReadOnly="True">0</asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            GRN No.</td>
                        <td align="left" style="width: 150px" valign="bottom">
                            <asp:Label ID="lblGRNNo" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                        </td>
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
            </td>
        </tr>
        <tr>
            <td align="center"><p class="details_header">Add Items Section</p>
                </td>
        </tr>
        <tr>
            <td class="Content2">
            <div></div>
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="top" style="width: 86px">
                                Item Name:</td>
                            <td align="left" valign="top" style="width: 150px">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlItemCode" runat="server" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlPOCode" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btnConfirm" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Item Name:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtItemName" runat="server" Columns="30" ReadOnly="True" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                        <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                Receive Quantity:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Maximum Recievable:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtMaxRecievable" runat="server" ReadOnly="True">0</asp:TextBox>&nbsp;
                                        <asp:HiddenField ID="hdnItemValue" runat="server" Value="0" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Cannot Recieve Amount" Type="Integer" ControlToCompare="txtMaxRecievable"
                                    Operator="LessThanEqual" ValidationGroup="vgPOLI"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Quantity Cannot be empty" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr id="trCostPrice" runat="server">
                            <td align="left" style="width: 86px" valign="bottom">
                                Cost Price:(After Discount)</td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtCost" runat="server" ReadOnly="True"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlItemCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                    </Triggers>
                                </asp:UpdatePanel>
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
                <asp:HiddenField ID="hdnGRNDetailsId" runat="server" Value="0" />
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
                    DataKeyNames="ItemId,GRNDetailsId,Id" OnRowDeleting="gvItemList_RowDeleting"
                    OnRowDataBound="gvItemList_RowDataBound" Font-Size="Larger">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        <asp:BoundField DataField="ReceivedQty" HeaderText="Qty" />
                        <asp:BoundField DataField="ItemValue" HeaderText="Cost Per Unit" DataFormatString="{0:F2}">
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trNoRecords" runat="server" class="NoRecords">
            <td>
                no records
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<input id="txtLineItemsCount" type="text" style="display: none" runat="server"
                    value="0" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgRG"></asp:RequiredFieldValidator>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 16px">
                &nbsp;</td>
        </tr>
        <tr runat="server" id="trSalesReturn">
            <td style="height: 16px" class="Content2">
                <table cellpadding="0" cellspacing="0">
                    <tr style="width: 86px">
                        <td style="text-align: center">
                            <strong>Credit Note:</strong></td>
                    </tr>
                    <tr style="width: 86px">
                        <td>
                            <asp:TextBox ID="txtCreditNote" runat="server" Columns="40" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="width: 86px">
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCreditNote"
                                Display="Dynamic" ErrorMessage="Credit Note Required" ValidationGroup="vgRG"></asp:RequiredFieldValidator></td>
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
                            <asp:Button ID="btnAddGRN" runat="server" Text="Save GRN" ValidationGroup="vgRG"
                                OnClick="btnAddGRN_Click" OnClientClick="return AllowSave('vgRG');"/>
                        </td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="Print GRN" OnClick="btnPrint_Click"
                                Visible="False" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnGRNId" runat="server" Value="0" />
            </td>
        </tr>
    </table>
</asp:Content>
