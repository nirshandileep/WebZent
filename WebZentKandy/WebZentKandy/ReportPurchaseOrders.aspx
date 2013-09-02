<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportPurchaseOrders.aspx.cs"
    Inherits="ReportPurchaseOrders" Title="Purchase Report By Item" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ShowHideCust() {            if(grid.IsCustomizationWindowVisible())            {                grid.HideCustomizationWindow();            }            else            {                grid.ShowCustomizationWindow();            }            return false;        }
    </script>

    <table class="form" align="center" width="100%">
        <tr>
            <td class="report_header">
                Purchase Report by item</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            From Date:
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpFromDate" runat="server" PopupVerticalAlign="WindowCenter">
                                <ValidationSettings Display="Dynamic" ValidationGroup="vgsearch">
                                    <RequiredField ErrorText="From Date Required" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            To Date:
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpToDate" runat="server">
                                <ValidationSettings Display="Dynamic" ValidationGroup="vgsearch">
                                    <RequiredField ErrorText="To Date Required" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                ValidationGroup="vgsearch">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <asp:Button ID="btnExtraColumns" runat="server" OnClientClick="return ShowHideCust();"
                                Text="Show Extra Columns" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dxgvPurchaseByItems" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="ItemId" Width="100%" ClientInstanceName="grid">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemCode" GroupIndex="0" SortIndex="0" SortOrder="Ascending"
                            VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Description" FieldName="ItemDescription" UnboundType="String"
                            VisibleIndex="1">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="PO No" FieldName="POCode" UnboundType="String"
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Item Cost" FieldName="POItemCost" UnboundType="Decimal"
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sold Qty" FieldName="SoldQty" UnboundType="Integer"
                            Visible="False" VisibleIndex="15">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="QIH" FieldName="QuantityInHand" UnboundType="Integer"
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Discount" FieldName="DiscountPerUnit" UnboundType="Decimal"
                            VisibleIndex="6">
                            <PropertiesTextEdit DisplayFormatString="{0}%">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sup Name" FieldName="SupplierName" UnboundType="String"
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ordered Qty" FieldName="OrderedQty" UnboundType="Integer"
                            VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Order Cost" FieldName="LineCost" UnboundType="Decimal"
                            VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="RecievedQty" UnboundType="Integer" VisibleIndex="13">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True"
                        ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowFilterBar="Auto" ShowGroupedColumns="True"
                        ShowPreview="True"></Settings>
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <GroupSummary>
                        <dx:ASPxSummaryItem FieldName="SoldQuantity" ShowInGroupFooterColumn="Sold Qty" SummaryType="Sum" DisplayFormat="{0}" />
                        <dx:ASPxSummaryItem FieldName="OrderedQty" SummaryType="Sum" DisplayFormat="{0}" ShowInGroupFooterColumn="Ordered Qty" />
                        <dx:ASPxSummaryItem FieldName="RecievedQty" SummaryType="Sum" DisplayFormat="{0}" ShowInGroupFooterColumn="Recieved Qty" />
                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="LineCost" ShowInGroupFooterColumn="Order Cost"
                            SummaryType="Min" />
                    </GroupSummary>
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior AutoExpandAllGroups="True" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridViewExporter ID="gveSalesReport" runat="server" FileName="Sales By Item"
                    GridViewID="dxgvSalesByItems">
                </dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export Report">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
