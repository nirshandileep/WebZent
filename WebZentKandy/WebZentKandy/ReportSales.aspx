<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportSales.aspx.cs"
    Inherits="ReportSales" Title="Sales Report" %>

<%@ Register Assembly="DevExpress.Xpo.v11.1.Web, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Xpo" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function ShowHideCust() {            if(grid.IsCustomizationWindowVisible())            {                grid.HideCustomizationWindow();            }            else            {                grid.ShowCustomizationWindow();            }            return false;        }
    </script>

    <table class="form" align="center" width="100%">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td align="left">
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
                                        To Date:</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dtpToDate" runat="server">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="vgsearch">
                                                <RequiredField ErrorText="To Date Required" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnSearch" runat="server" Text="Search"
                                            ValidationGroup="vgsearch" AutoPostBack="False" UseSubmitBehavior="False">
                                            <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExtraColumns" runat="server" OnClientClick="return ShowHideCust();"
                                            Text="Show Extra Columns" /></td>
                                </tr>
                            </table>
                                        </td>
                        <td align="right">
                            &nbsp;<table>
                                <tr>
                                    <td>
                                        Report Option:
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="rblReportOption" runat="server"
                                            ClientInstanceName="rbtnOptions" RepeatDirection="Horizontal" ValueType="System.Int32" SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Text="All" Value="0" Selected="True" />
                                                <dx:ListEditItem Text="All Active" Value="1" />
                                                <dx:ListEditItem Text="All Cancelled" Value="2" />
                                                <dx:ListEditItem Text="Pending Payment" Value="3" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Sales Report</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="InvoiceId" ClientInstanceName="grid" PreviewFieldName="ChequeNumber">
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Visible"
                        ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="Invoice No" FieldName="InvoiceId" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddInvoice.aspx?FromURL=SearchInvoice.aspx&amp;InvoiceId={0}"
                                TextField="InvoiceNo">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <Settings FilterMode="DisplayText" SortMode="DisplayText" />
                            <CellStyle ForeColor="Black">
                            </CellStyle>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="Date" UnboundType="DateTime"
                            VisibleIndex="1">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GrandTotal" FieldName="GrandTotal" UnboundType="Decimal"
                            VisibleIndex="8">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Branch" FieldName="BranchCode" UnboundType="String"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Due Amount" FieldName="DueAmount" UnboundType="Decimal"
                            VisibleIndex="7">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Invoiced By" FieldName="UserName" UnboundType="String"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ChequeNumber" ReadOnly="True" UnboundType="String"
                            Visible="False" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ChequeDate" UnboundType="DateTime" Visible="False"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Remarks" UnboundType="String" Visible="False"
                            VisibleIndex="15">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CardType" UnboundType="String" Visible="False"
                            VisibleIndex="19">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Banked Ammount" FieldName="Banked_Ammount"
                            UnboundType="Decimal" Visible="False" VisibleIndex="18">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="BankComision" UnboundType="Decimal" Visible="False"
                            VisibleIndex="17">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CardComisionRate" UnboundType="Decimal"
                            Visible="False" VisibleIndex="16">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Profit(%)" FieldName="ProfitPercentage" Visible="False"
                            VisibleIndex="13">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total Cost" FieldName="TotalCostOfInvoice"
                            UnboundType="Decimal" Visible="False" VisibleIndex="14">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Profit" FieldName="TotalProfit" UnboundType="Decimal"
                            Visible="False" VisibleIndex="12">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="Cus_Name" VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Age (Days)" FieldName="DaysCount" UnboundType="Integer"
                            VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PaidAmmount" UnboundType="Decimal" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ReturnValue" UnboundType="Decimal" VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="GrandTotal" ShowInGroupFooterColumn="GrandTotal"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="TotalProfit" ShowInGroupFooterColumn="Profit" SummaryType="Sum" DisplayFormat="Sum = {0}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="DueAmount" ShowInGroupFooterColumn="Due Amount"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="PaidAmmount" ShowInGroupFooterColumn="Paid Ammount"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="ReturnValue" ShowInGroupFooterColumn="Return Value"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="GrandTotal" ShowInColumn="GrandTotal"
                            ShowInGroupFooterColumn="GrandTotal" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Cost = {0}" FieldName="TotalCostOfInvoice"
                            ShowInColumn="Total Cost" ShowInGroupFooterColumn="Total Cost" SummaryType="Sum"
                            ValueDisplayFormat="{0:F2}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Profit = {0}" FieldName="TotalProfit" ShowInColumn="Profit"
                            ShowInGroupFooterColumn="Profit" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="PaidAmmount" ShowInColumn="Paid Ammount"
                            ShowInGroupFooterColumn="Paid Ammount" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="DueAmount" ShowInColumn="Due Amount"
                            ShowInGroupFooterColumn="Due Amount" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="ReturnValue" ShowInColumn="Return Value"
                            ShowInGroupFooterColumn="Return Value" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                    </TotalSummary>
                    <SettingsCustomizationWindow Enabled="True" />
                    <Templates>
                        <DetailRow>
                            &nbsp;<dxwgv:ASPxGridView ID="dxgvInvoiceLineItems" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="InvoiceId" OnBeforePerformDataSelect="dxgvInvoiceLineItems_BeforePerformDataSelect"
                                Width="100%">
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="ItemCode" UnboundType="String" Visible="False"
                                        VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ItemDescription" UnboundType="String" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Quantity" UnboundType="Integer" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Unit Sold Price" FieldName="Price" UnboundType="Decimal"
                                        VisibleIndex="3">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TotalPrice" UnboundType="Decimal" VisibleIndex="6">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="IssuedQTY" UnboundType="Integer" VisibleIndex="10">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Discount" FieldName="DiscountPerUnit" UnboundType="Decimal"
                                        VisibleIndex="14">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PriceBeforeDiscount" VisibleIndex="13">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Profit(%)" FieldName="ProfitPercentage" ToolTip="(Selling Price - Cost)/Cost * 100"
                                        VisibleIndex="12">
                                        <PropertiesTextEdit DisplayFormatString="{0}%">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="Profit" UnboundType="Decimal" VisibleIndex="11">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Unit Cost" FieldName="ItemCost" UnboundType="Decimal"
                                        VisibleIndex="5">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="TotalCost" UnboundType="Decimal" VisibleIndex="9">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Unit Selling Price" FieldName="SellingPrice"
                                        UnboundType="Decimal" VisibleIndex="4">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Discount(%)" FieldName="DiscountPercentage"
                                        VisibleIndex="8">
                                        <PropertiesTextEdit DisplayFormatString="{0}%">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Total of Selling Price" FieldName="TotalSellingPrice"
                                        UnboundType="Decimal" VisibleIndex="7">
                                        <PropertiesTextEdit DisplayFormatString="{0:F2}">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <SettingsPager PageSize="25">
                                </SettingsPager>
                                <TotalSummary>
                                    <dxwgv:ASPxSummaryItem DisplayFormat="Cost={0}" FieldName="TotalCost" ShowInColumn="Total Cost"
                                        ShowInGroupFooterColumn="Total Cost" SummaryType="Sum" />
                                    <dxwgv:ASPxSummaryItem DisplayFormat="Price={0}" FieldName="TotalPrice" ShowInColumn="Total Price"
                                        ShowInGroupFooterColumn="Total Price" SummaryType="Sum" />
                                    <dxwgv:ASPxSummaryItem DisplayFormat="Profit={0}" FieldName="Profit" ShowInColumn="Profit"
                                        ShowInGroupFooterColumn="Profit" SummaryType="Sum" />
                                    <dxwgv:ASPxSummaryItem DisplayFormat="Tot Price = {0}" FieldName="TotalSellingPrice"
                                        ShowInColumn="Total of Selling Price" ShowInGroupFooterColumn="Total of Selling Price"
                                        SummaryType="Sum" />
                                </TotalSummary>
                                <GroupSummary>
                                    <dxwgv:ASPxSummaryItem FieldName="Profit" ShowInColumn="Profit" SummaryType="Sum" />
                                    <dxwgv:ASPxSummaryItem FieldName="TotalCost" ShowInColumn="Total Cost" SummaryType="Sum" />
                                    <dxwgv:ASPxSummaryItem FieldName="TotalPrice" ShowInColumn="Total Price" SummaryType="Sum" />
                                </GroupSummary>
                                <Settings ShowFooter="True" />
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ExportMode="Expanded" ShowDetailRow="True" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnExportReport" runat="server" OnClick="btnExportReport_Click" Text="Export To Report" /><dxwgv:ASPxGridViewExporter
                    ID="gveInvoice" runat="server" FileName="Invoice Report" GridViewID="dxgvInvoice" PaperKind="A4">
                </dxwgv:ASPxGridViewExporter>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
