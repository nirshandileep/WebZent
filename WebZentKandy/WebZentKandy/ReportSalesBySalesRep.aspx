<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportSalesBySalesRep.aspx.cs"
    Inherits="ReportSalesByItem" Title="Sales report - Item" %>

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
            <td>
                <table>
                    <tr>
                        <td>
                            From Date:
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dtpFromDate" runat="server">
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
                            <asp:Button ID="btnExtraColumns" runat="server" Text="Show Extra Columns" OnClientClick="return ShowHideCust();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Sales Report by item By sales rep</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dxgvSalesByItems" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="ItemId" Width="100%" ClientInstanceName="grid">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ItemCode" GroupIndex="0" SortIndex="0" SortOrder="Ascending"
                            UnboundType="String" VisibleIndex="0">
                            <Settings AllowGroup="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Description" FieldName="ItemDescription" UnboundType="String"
                            VisibleIndex="1">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Category" FieldName="CategoryType" UnboundType="String"
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sold Qty" FieldName="SoldQuantity" UnboundType="Integer"
                            VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total Cost" FieldName="ItemCost" UnboundType="Decimal"
                            VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BrandName" UnboundType="String" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Profit" UnboundType="Decimal" VisibleIndex="12" ShowInCustomizationForm="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Type" FieldName="TypeName" UnboundType="String"
                            VisibleIndex="7" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Rep Name" FieldName="FirstName" UnboundType="String"
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Branch" FieldName="BranchCode" UnboundType="String"
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="InvoiceNo" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Price" VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Inv Date" FieldName="Date" Visible="False" VisibleIndex="14">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowGroupPanel="True"
                        ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowFilterBar="Visible" ShowGroupedColumns="True"></Settings>
                    <SettingsBehavior AutoExpandAllGroups="True" AllowFocusedRow="True" EnableRowHotTrack="True" ColumnResizeMode="Control" />
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <GroupSummary>
                        <dx:ASPxSummaryItem DisplayFormat="Qty={0}" FieldName="SoldQuantity" ShowInGroupFooterColumn="Sold Qty"
                            SummaryType="Sum" />
                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TotalSales" ShowInGroupFooterColumn="Value"
                            SummaryType="Sum" />
                        <dx:ASPxSummaryItem DisplayFormat="Cost={0}" FieldName="ItemCost" ShowInGroupFooterColumn="Total Cost"
                            SummaryType="Sum" ShowInColumn="Total Cost" />
                        <dx:ASPxSummaryItem DisplayFormat="Profit={0}" FieldName="Profit" ShowInGroupFooterColumn="Profit"
                            SummaryType="Sum" ShowInColumn="Profit" />
                    </GroupSummary>
                    <SettingsCustomizationWindow Enabled="True" />
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="dxgvReportsSalesDetails" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="InvoiceId" OnBeforePerformDataSelect="dxgvReportsSalesDetails_BeforePerformDataSelect"
                                Width="100%">
                                <TotalSummary>
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="Quantity" ShowInColumn="Qty" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TotalCost" ShowInColumn="Total Cost"
                                        SummaryType="Sum" />
                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TotalPrice" ShowInColumn="Total Price"
                                        SummaryType="Sum" />
                                </TotalSummary>
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="InvoiceNo" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Date" VisibleIndex="1" UnboundType="DateTime">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Branch" FieldName="BranchCode" VisibleIndex="2" UnboundType="String">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Invoiced By" FieldName="FirstName" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Qty" FieldName="Quantity" VisibleIndex="8" UnboundType="Integer">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Price" VisibleIndex="4" Caption="Unit Price" UnboundType="Decimal">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Unit Cost" FieldName="ItemCost" VisibleIndex="6" UnboundType="Decimal">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="TotalCost" VisibleIndex="7" UnboundType="Decimal">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="TotalPrice" UnboundType="Decimal" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" />
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ExportMode="Expanded" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="ItemCost" ShowInColumn="Total Cost"
                            ShowInGroupFooterColumn="Total Cost" SummaryType="Sum" />
                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="Profit" ShowInColumn="Profit"
                            ShowInGroupFooterColumn="Profit" SummaryType="Sum" />
                    </TotalSummary>
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridViewExporter ID="gveSalesReport" runat="server" FileName="Sales report by sales rep"
                    GridViewID="dxgvSalesByItems" PaperKind="A4">
                </dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export Report">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
