<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="POAdd.aspx.cs"
    Inherits="POAdd" Title="Add Purchase Order" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Purchase Order</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            PO Code:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:TextBox ID="txtPOCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            PO Date:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <dx:ASPxDateEdit ID="dtpPODate" runat="server" EditFormat="Custom" EditFormatString="dd/MMM/yyyy">
                                <ValidationSettings Display="Dynamic" ValidationGroup="vgPO">
                                    <RequiredField ErrorText="Date Required" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
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
                            Supplier:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:DropDownList ID="ddlSupplier" runat="server">
                            </asp:DropDownList>&nbsp;<asp:Button ID="btnAddNewSupplier" runat="server" OnClick="btnAddNewSupplier_Click"
                                Text="..." ToolTip="Add Supplier" /></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                            PO Ammount:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:Label ID="lblPOAmount" runat="server"></asp:Label>
                            (Rs.)</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                        </td>
                        <td align="left" style="width: 150px" valign="bottom">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSupplier"
                                Display="Dynamic" ErrorMessage="Supplier Required" ValidationGroup="vgPO" InitialValue="-1"></asp:RequiredFieldValidator>&nbsp;</td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td align="left" style="width: 86px" valign="top">
                            Total Paid:</td>
                        <td align="left" style="width: 150px" valign="top">
                            <asp:TextBox ID="txtPaidAmount" runat="server">0</asp:TextBox>
                            (Rs.)</td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Balance Payment:</td>
                        <td align="left" style="width: 140px" valign="top">
                            &nbsp;<asp:Label ID="lblBalancePayment" runat="server"></asp:Label>
                            &nbsp;(Rs.)</td>
                    </tr>
                    <tr style="display: none">
                        <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPaidAmount"
                                Display="Dynamic" ErrorMessage="Amount is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                ValidationGroup="vgPO" Enabled="False"></asp:RegularExpressionValidator>&nbsp;</td>
                        <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px; height: 17px;" valign="bottom">
                            Requested By:</td>
                        <td align="left" style="width: 164px; height: 17px;" valign="bottom">
                            <asp:DropDownList ID="ddlRequestBy" runat="server">
                            </asp:DropDownList></td>
                        <td align="left" style="width: 13px; height: 17px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 17px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px; height: 17px;" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 164px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px; height: 17px" valign="bottom">
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    PO Items Section</p>
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
                                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                                <asp:Button ID="txtSearchItem" runat="server" OnClick="txtSearchItem_Click" Text="Search"
                                    ValidationGroup="vgSearch" />
                                <asp:HiddenField ID="hdnItemIdFromSearch" runat="server" Value="0" />
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Item Name:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True" Columns="30" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode"
                                    Display="Dynamic" ErrorMessage="Item Code Cannot be empty" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator>&nbsp;</td>
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
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Old Cost:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtOldCost" runat="server" ReadOnly="True"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Invalid Quantity" Operator="GreaterThanEqual"
                                    Type="Integer" ValidationGroup="vgPOLI" ValueToCompare="0"></asp:CompareValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantity" Display="Dynamic"
                                        ErrorMessage="Quantity Required" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            Discount(%):</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlDiscount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged"
                                                ValidationGroup="vgPOLI">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                New Item Cost:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtCostBeforeDiscount" runat="server" ValidationGroup="vgPOLI"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDiscount" EventName="SelectedIndexChanged" />
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
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCostBeforeDiscount"
                                    Display="Dynamic" ErrorMessage="Invalid Cost" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                    ValidationGroup="vgPOLI" Enabled="False"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCostBeforeDiscount"
                                        Display="Dynamic" ErrorMessage="Item Cost Cannot be empty" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Discounted Cost:</td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtItemCost" runat="server" BackColor="MistyRose"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDiscount" EventName="SelectedIndexChanged" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Selling Price:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtSellingPrice" runat="server" ReadOnly="True"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtItemCost"
                                    Display="Dynamic" ErrorMessage="Invalid Cost" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                    ValidationGroup="vgPOLI" Enabled="False"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemCost" Display="Dynamic"
                                        ErrorMessage="Item Cost Cannot be empty" ValidationGroup="vgPOLI"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnSavePOItem" runat="server" OnClick="btnSavePOItem_Click" Text="Add Item"
                    ValidationGroup="vgPOLI" />&nbsp;<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click"
                        Text="Clear" />
            </td>
        </tr>
        <tr id="loadPrint">
            <td>
                <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="report_header">
                PO Items</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td class="Content2">
                <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="0" CellSpacing="1" Width="100%" OnRowEditing="gvItemList_RowEditing"
                    DataKeyNames="ItemId" OnRowDeleting="gvItemList_RowDeleting" BackColor="#FFE0C0"
                    Font-Size="Larger">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                        <asp:BoundField DataField="POItemCost" HeaderText="Unit Cost" DataFormatString="{0:F2}" />
                        <asp:BoundField HeaderText="Discount(%)" DataField="DiscountPerUnit" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" />
                        <asp:BoundField DataField="LineCost" HeaderText="Line Cost" DataFormatString="{0:F2}" />
                        <asp:CommandField ShowEditButton="True" />
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
            <td style="height: 26px">
                <input id="txtLineItemsCount" type="text" style="display: none" runat="server" value="0" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgPO"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 86px">
                            PO Comment:</td>
                        <td>
                            <textarea id="txtCancelNote" runat="server" cols="60" rows="3"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86px">
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCancelNote"
                                Display="Dynamic" ErrorMessage="Comment Required" ValidationGroup="vgPOCancel"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddPO" runat="server" Text="Save PO" ValidationGroup="vgPO" OnClick="btnAddPO_Click"
                    OnClientClick="return AllowSave('vgPO');" />&nbsp;<asp:Button ID="btnPrintPO" runat="server"
                        OnClientClick="return ShowLoginWindow()" Text="Print PO" />&nbsp;<asp:Button ID="btnCancel"
                            runat="server" OnClick="btnCancel_Click" Text="Cancel PO" Visible="False" ValidationGroup="vgPOCancel" />
                <dxpc:ASPxPopupControl ID="dxPrintPopup" runat="server" ClientInstanceName="poPrint"
                    AllowDragging="True" AllowResize="True" DragElement="Window" EnableClientSideAPI="True"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    CloseAction="CloseButton" HeaderText="PO Print" Height="600px" Width="800px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnPOId" runat="server" Value="0" />
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
    function ShowLoginWindow() {
        poPrint.Show();
        return false;
    }

    </script>

</asp:Content>
