<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddItem.aspx.cs"
    Inherits="AddItem" Title="Add Item" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label><asp:ScriptManager
                    ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxTabControl ID="tabControlItems" runat="server" ActiveTabIndex="0" OnTabClick="tabControlItems_TabClick"
                    Width="100%" AutoPostBack="True" OnActiveTabChanged="tabControlItems_ActiveTabChanged">
                    <Tabs>
                        <dx:Tab Text="Item Details">
                        </dx:Tab>
                        <dx:Tab Text="Stock Adjustment">
                        </dx:Tab>
                    </Tabs>
                </dx:ASPxTabControl>
            </td>
        </tr>
        <tr id="trItemDetails" runat="server">
            <td>
                <table style="width: 100%" class="form" align="center">
                    <tr>
                        <td style="width: 130px">
                            Item Code:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtItemCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtItemCode"
                                Display="Dynamic" ErrorMessage="Item Code cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Item Description:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtItemDesc" runat="server" MaxLength="100" ValidationGroup="vgItemSave"
                                Width="420px" Columns="30" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemDesc"
                                Display="Dynamic" ErrorMessage="Description Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="trCost" runat="server">
                        <td style="width: 130px">
                            Cost:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtCost" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox>(Rs.)</td>
                    </tr>
                    <tr id="trCostValidation" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCost"
                                Display="Dynamic" ErrorMessage="Cost Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCost" Display="Dynamic"
                                    ErrorMessage="Cost is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                    ValidationGroup="vgItemSave"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr id="trSelPrice" runat="server">
                        <td style="width: 130px">
                            Selling Price:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtSelPrice" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox>(Rs.)
                        </td>
                    </tr>
                    <tr id="trSelPriceVal" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtCost"
                                ControlToValidate="txtSelPrice" ErrorMessage="Selling Price cannot be below Cost"
                                ValidationGroup="vgItemSave" Display="Dynamic" Operator="GreaterThanEqual" Type="Currency"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSelPrice"
                                Display="Dynamic" ErrorMessage="Selling Price Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSelPrice"
                                Display="Dynamic" ErrorMessage="Cost is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                ValidationGroup="vgItemSave"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr id="trMinSellPrice" runat="server">
                        <td style="width: 130px">
                            Minimum Selling Price:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtMinSelPrice" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox>(Rs.)</td>
                    </tr>
                    <tr id="trMinSellPriceValidation" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMinSelPrice"
                                Display="Dynamic" ErrorMessage="Minimum Selling Price Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMinSelPrice"
                                Display="Dynamic" ErrorMessage="Cost is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                ValidationGroup="vgItemSave"></asp:RegularExpressionValidator><asp:CompareValidator
                                    ID="CompareValidator2" runat="server" ControlToCompare="txtCost" ControlToValidate="txtMinSelPrice"
                                    Display="Dynamic" ErrorMessage="Minimum Selling Price cannot be below Cost" Operator="GreaterThanEqual"
                                    Type="Currency" ValidationGroup="vgItemSave"></asp:CompareValidator></td>
                    </tr>
                    <tr id="trbrandCode" runat="server">
                        <td style="width: 130px">
                            Brand Code:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:DropDownList ID="ddlBrandCode" runat="server" ValidationGroup="vgItemSave" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBrandCode_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="btnAddBrand" runat="server" Text="..." OnClick="btnAddBrand_Click" /></td>
                    </tr>
                    <tr id="trBrandCodeVal" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlBrandCode"
                                Display="Dynamic" ErrorMessage="Brand Code cannot be empty" InitialValue="-1"
                                ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="trType" runat="server">
                        <td style="width: 130px">
                            Type:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlType" runat="server">
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:Button ID="btnAddType" runat="server" Text="Add Type" OnClick="btnAddType_Click" /></td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtType" runat="server" ValidationGroup="type" Visible="False"></asp:TextBox></td>
                                                        <td>
                                                            <asp:Button ID="btnTypeSave" runat="server" OnClick="btnTypeSave_Click" Text="Save"
                                                                ValidationGroup="type" Visible="False" /></td>
                                                    </tr>
                                                </table>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtType"
                                                    Enabled="False" ErrorMessage="Type Required" ValidationGroup="type" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr id="trROL" runat="server">
                        <td style="width: 130px; height: 26px;">
                            ROL:</td>
                        <td style="height: 26px">
                        </td>
                        <td style="width: 263px; height: 26px;">
                            <asp:TextBox ID="txtROL" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trROLVal" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtROL"
                                Display="Dynamic" ErrorMessage="ROL Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtROL" Display="Dynamic"
                                    ErrorMessage="ROL is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,5}(\.\d{1,3})?$"
                                    ValidationGroup="vgItemSave"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr id="trCatCode" runat="server">
                        <td style="width: 130px">
                            Category Code:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlCatCode" runat="server">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnAddNewCategory" runat="server" OnClick="btnAddNewCategory_Click"
                                        Text="..." />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlBrandCode" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="trCatCodeVal" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCatCode"
                                Display="Dynamic" ErrorMessage="Category Code Cannot be empty" InitialValue="-1"
                                ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="trStatus" runat="server">
                        <td style="width: 130px">
                            Status:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trStatusVal" runat="server">
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlStatus"
                                Display="Dynamic" ErrorMessage="Status cannot be empty" InitialValue="-1" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr id="trQIH" runat="server">
                        <td style="width: 130px">
                            Qty In Hand:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:Label ID="lblQIH" runat="server">0</asp:Label>
                            Units</td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr id="trInvoicedQTY" runat="server">
                        <td style="width: 130px">
                            Invoiced Qty:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:Label ID="lblInvoicedQty" runat="server">0</asp:Label>
                            Units</td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 130px">
                            &nbsp;</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgItemSave"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trItemStockChanges" runat="server" visible="false">
            <td>
                <table style="width: 100%" class="form" align="center">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>Description:</td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" Rows="5" Columns="25" TextMode="MultiLine" Width="271px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtDescription"
                                            Display="Dynamic" ErrorMessage="Description Required"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td>
                                        Quantity:</td>
                                    <td>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="seQuantity" runat="server" Height="21px" Number="0" NumberType="Integer">
                                            <ValidationSettings Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnSaveAdj" runat="server" OnClick="btnSaveAdj_Click" Text="Save">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <dx:ASPxGridView ID="gvItemsStockChanges" runat="server" Width="100%" AutoGenerateColumns="False">
                                <SettingsPager PageSize="50">
                                </SettingsPager>
                                <Settings ShowFilterRow="True" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Alter Reason" FieldName="Description" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Altered Quantity" FieldName="Qty" ReadOnly="True"
                                        UnboundType="Integer" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Date" FieldName="Date" UnboundType="DateTime"
                                        VisibleIndex="2">
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
