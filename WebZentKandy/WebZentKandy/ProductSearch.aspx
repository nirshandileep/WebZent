<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ProductSearch.aspx.cs"
    Inherits="ProductSearch" Title="Search Items" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td align="center">
                <p class="details_header">
                    Search Items</p>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="bottom" style="width: 86px">
                                Item Code:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                Category:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:DropDownList ID="ddlCategory" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Description:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:TextBox ID="txtItemDescription" runat="server" Columns="30" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Brand:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:DropDownList ID="ddlBrands" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Price (Less than):</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:TextBox ID="txtPrice" runat="server"></asp:TextBox>(Rs.)</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                ROL (Less than):</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:TextBox ID="txtROL" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPrice"
                                    Display="Dynamic" ErrorMessage="Cost is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                    ValidationGroup="vgSearch"></asp:RegularExpressionValidator>&nbsp;</td>
                            <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtROL"
                                    Display="Dynamic" ErrorMessage="ROL is invalid" ValidationExpression="^[+]?\d*$"
                                    ValidationGroup="vgSearch"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                Status:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Showroom:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:DropDownList ID="ddlBranch1" runat="server" Visible="False">
                                </asp:DropDownList>
                                <dxe:ASPxComboBox ID="ddlBranch" runat="server" ValueType="System.Int32">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	grid.PerformCallback();
}" />
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left" style="width: 86px" valign="bottom">
                                Supplier:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:DropDownList ID="ddlSupplier" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Quantity In Hand (Less than):</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:TextBox ID="txtQuantityInHand" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtQuantityInHand"
                                    Display="Dynamic" ErrorMessage="Quantity is invalid" ValidationExpression="^[+]?\d*$"
                                    ValidationGroup="vgSearch"></asp:RegularExpressionValidator></td>
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
                </div>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" Visible="False" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    ValidationGroup="vgSearch" />&nbsp;<asp:Button ID="btnExport" runat="server" Text="Export To Report"
                        OnClick="btnExport_Click" />&nbsp;<asp:Button ID="btnItemsInInvoice" runat="server"
                            OnClientClick="return ShowInvoiceWindow()" Text="Items in Invoice" Visible="False" /></td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="gveItemList" runat="server" FileName="Item List Report"
                    GridViewID="dxgvItemList">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Item List&nbsp;</td>
        </tr>
        <tr id="trItemList" runat="server">
            <td>
                <dxwgv:ASPxGridView ID="dxgvItemList" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ItemId" OnRowUpdating="dxgvItemList_RowUpdating" OnRowValidating="dxgvItemList_RowValidating"
                    OnInit="dxgvItemList_Init" Font-Overline="False" OnAutoFilterCellEditorInitialize="dxgvItemList_AutoFilterCellEditorInitialize"
                    ClientInstanceName="grid">
                    <Settings ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRow="True"
                        ShowFilterRowMenu="True" ShowFooter="True"></Settings>
                    <Columns>
                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="15px">
                            <EditButton Text="Edit" Visible="True">
                            </EditButton>
                            <CellStyle BackColor="#E0E0E0" ForeColor="Black">
                            </CellStyle>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="I-Code" FieldName="ItemId" Name="ItemCode"
                            ReadOnly="True" ShowInCustomizationForm="False" VisibleIndex="1" Width="35px">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddItem.aspx?ItemId={0}&amp;FromURL=ProductSearch.aspx"
                                TextField="ItemCode">
                                <Style Font-Underline="True" ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                            <EditFormSettings Visible="False" />
                            <CellStyle ForeColor="Black">
                            </CellStyle>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ItemDescription" ShowInCustomizationForm="False"
                            VisibleIndex="2" Width="250px">
                            <PropertiesTextEdit>
                                <Style Font-Size="Smaller"></Style>
                            </PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" />
                            <EditFormSettings Visible="False" />
                            <EditCellStyle Font-Size="X-Large">
                            </EditCellStyle>
                            <FilterCellStyle Font-Size="Larger">
                            </FilterCellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Selling Price" FieldName="SellingPrice" ShowInCustomizationForm="False"
                            UnboundType="Decimal" VisibleIndex="3" Width="35px">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Cost" FieldName="Cost" ShowInCustomizationForm="False"
                            UnboundType="Decimal" VisibleIndex="4" Width="35px">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="MS Price" FieldName="MinSellingPrice" ShowInCustomizationForm="False"
                            UnboundType="Decimal" VisibleIndex="5" Width="35px">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ROL" FieldName="ROL" ShowInCustomizationForm="False"
                            UnboundType="Integer" VisibleIndex="6" Width="20px">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="TypeName" ShowInCustomizationForm="False"
                            UnboundType="String" VisibleIndex="7" Width="35px">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Category" FieldName="CategoryType" ShowInCustomizationForm="False"
                            VisibleIndex="8" Width="40px">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Brand" FieldName="BrandName" ShowInCustomizationForm="False"
                            VisibleIndex="9" Width="35px">
                            <EditFormSettings Visible="False" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TrueQIH" UnboundType="Integer" VisibleIndex="14"
                            Width="20px" ShowInCustomizationForm="False">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="QIH" FieldName="QuantityInHand" UnboundType="Integer"
                            VisibleIndex="10" Width="20px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Invoiced Qty" FieldName="InvoicedQty" ShowInCustomizationForm="False"
                            UnboundType="Integer" VisibleIndex="11" Width="15px">
                            <PropertiesTextEdit NullDisplayText="0">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="IsActive" UnboundType="Boolean"
                            VisibleIndex="12" Width="25px">
                            <PropertiesComboBox DropDownStyle="DropDown" ValueType="System.Boolean">
                                <Items>
                                    <dxe:ListEditItem Text="True" Value="True">
                                    </dxe:ListEditItem>
                                    <dxe:ListEditItem Text="False" Value="False">
                                    </dxe:ListEditItem>
                                </Items>
                            </PropertiesComboBox>
                            <EditFormSettings Visible="True" />
                        </dxwgv:GridViewDataComboBoxColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total Value" FieldName="TotalValue" ReadOnly="True"
                            UnboundType="Decimal" VisibleIndex="13" Width="40px" ShowInCustomizationForm="False">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TrueStockValue" UnboundType="Decimal" VisibleIndex="15"
                            Width="40px" ShowInCustomizationForm="False">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <StylesEditors>
                        <Hyperlink ForeColor="Black">
                        </Hyperlink>
                        <ButtonEdit ForeColor="Black">
                        </ButtonEdit>
                        <ButtonEditButton ForeColor="Black">
                        </ButtonEditButton>
                    </StylesEditors>
                    <SettingsBehavior ColumnResizeMode="Control" EnableRowHotTrack="True" />
                    <Styles>
                        <Header Wrap="True">
                        </Header>
                    </Styles>
                    <SettingsCustomizationWindow Enabled="True" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="TotalValue" ShowInColumn="Total Value"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total Value = {0}" FieldName="TrueStockValue"
                            ShowInColumn="True Stock Value" SummaryType="Sum" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" /></td>
        </tr>
        <tr>
            <td style="height: 74px">
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnBranchId" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxPopupControl ID="dxpcItemsInInvoice" runat="server" AllowDragging="True"
                                AllowResize="True" ClientInstanceName="InvoiceItems" HeaderText="Items in Invoice" SaveStateToCookies="True">
                                <ContentCollection>
                                    <dx:PopupControlContentControl runat="server">
                                        <dxwgv:ASPxGridView runat="server" AutoGenerateColumns="False" ID="gvInvoiceItems" Width="100%">
                                            <Columns>
                                                <dxwgv:GridViewDataTextColumn FieldName="ItemCode" UnboundType="String" Caption="Item Code"
                                                    VisibleIndex="0">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="ItemDescription" UnboundType="String" Caption="Description"
                                                    VisibleIndex="1">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Price" Caption="Selling Price" VisibleIndex="2">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="Quantity" UnboundType="Integer" Caption="Qty"
                                                    VisibleIndex="3">
                                                </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn FieldName="TotalPrice" Caption="Line Price" VisibleIndex="4">
                                                </dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                        </dxwgv:ASPxGridView>
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script language="Javascript" type="text/javascript">
            function ShowInvoiceWindow() {
            InvoiceItems.Show();
            return false;
        }
    </script>

</asp:Content>
