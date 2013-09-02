<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddGatePass.aspx.cs"
    Inherits="AddGatePass" Title="Add Gate Pass" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            Gate Pass Code:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:TextBox ID="txtGatepassCode" runat="server" ReadOnly="True"></asp:TextBox></td>
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
                        <td align="left" valign="bottom" style="width: 86px">
                            Invoice Number:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:DropDownList ID="ddlInvoiceNumber" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceNumber_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;</td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                            Invoice Ammount:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtInvAmmount" runat="server" ReadOnly="True"></asp:TextBox>(Rs.)
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlInvoiceNumber" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 150px" valign="bottom">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlInvoiceNumber"
                                Display="Dynamic" ErrorMessage="Invoice Number Required" InitialValue="-1" ValidationGroup="vgGP"></asp:RequiredFieldValidator></td>
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
            <td align="center">
                <p class="details_header">
                    Gate Pass Items</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="top" style="width: 86px">
                                Item Code:</td>
                            <td align="left" valign="top" style="width: 150px">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlItemCode" runat="server" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlInvoiceNumber" EventName="SelectedIndexChanged" />
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
                                        <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True" Columns="30" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                        <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnInvDetId" runat="server" Value="0" />
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
                                Issue Quantity:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Remaining Quantity:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtRemainingQty" runat="server" ReadOnly="True">0</asp:TextBox><asp:HiddenField
                                            ID="hdnItemValue" runat="server" Value="0" />
                                        <asp:TextBox ID="hdnQIH" runat="server" Style="display: none">0</asp:TextBox>
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
                                    Display="Dynamic" ErrorMessage="Cannot Issue Amount" Type="Integer" ControlToCompare="txtRemainingQty"
                                    Operator="LessThanEqual" ValidationGroup="vgPOLI"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Quantity Cannot be empty" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Insuficient Stocks" Operator="LessThanEqual"
                                    Type="Integer" ValidationGroup="vgPOLI" ControlToCompare="hdnQIH"></asp:CompareValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                &nbsp;</td>
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
                <asp:HiddenField ID="hdnGatepassDetailsId" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Issued Items</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td class="Content2">
                <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                    CellPadding="0" CellSpacing="1" GridLines="None" Width="100%" DataKeyNames="GPId,InvDetID"
                    OnRowDeleting="gvItemList_RowDeleting" OnRowDataBound="gvItemList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                            <ControlStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty">
                            <ControlStyle Width="10px" />
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
            </td>
        </tr>
        <tr>
            <td style="height: 16px">
                <input id="txtLineItemsCount" type="text" style="display: none" runat="server" value="0" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgGP"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddGatePass" runat="server" Text="Save Gate Pass" ValidationGroup="vgGP"
                    OnClick="btnAddGatePass_Click" OnClientClick="return AllowSave('vgGP');" />&nbsp;<asp:Button
                        ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" Visible="False" UseSubmitBehavior="False" /></td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnGatePassId" runat="server" Value="0" />
                <dx:ASPxPopupControl ID="dxPrintPopup" runat="server" AllowDragging="True" AllowResize="True"
                    CloseAction="CloseButton" DragElement="Window" HeaderText="Print  Gatepass" Height="650px"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="950px" ClientInstanceName="gatepassPrint">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        function ShowPrintWindow() {
            gatepassPrint.Show();
            return false;
        }
    </script>
</asp:Content>
