<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportCheques.aspx.cs"
    Inherits="ReportCheques" Title="Sales Report" %>

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
                                        <dx:ASPxButton ID="btnSearch" runat="server" Text="Search" ValidationGroup="vgsearch"
                                            AutoPostBack="False" UseSubmitBehavior="False" OnClick="btnSearch_Click">
                                            <ClientSideEvents Click="function(s, e) {
	grid.PerformCallback();
}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExtraColumns" runat="server" OnClientClick="return ShowHideCust();"
                                            Text="Show Extra Columns" Visible="False" /></td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Supplier cheque Report</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="VoucherID" ClientInstanceName="grid">
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Visible"
                        ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataDateColumn Caption="Issue Date" FieldName="VoucherPaymentDate"
                            UnboundType="DateTime" VisibleIndex="0">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PO No" FieldName="POCode" UnboundType="String"
                            VisibleIndex="7">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Cheque Date" FieldName="ChequeDate" UnboundType="DateTime"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="TotalAmount" UnboundType="Decimal"
                            VisibleIndex="6">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Chq. No" FieldName="ChequeNumber" UnboundType="String"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Bank" UnboundType="String" Visible="False"
                            VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Inv. No" FieldName="SupInvNo" UnboundType="String"
                            VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Age (Days)" FieldName="DaysCount" UnboundType="Integer"
                            VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Supplier Name" FieldName="SupplierName" UnboundType="String"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Comment" UnboundType="String" VisibleIndex="11">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="GrandTotal" ShowInGroupFooterColumn="GrandTotal"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem FieldName="TotalProfit" ShowInGroupFooterColumn="Profit" SummaryType="Sum"
                            DisplayFormat="Sum = {0}" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="DueAmount" ShowInGroupFooterColumn="Due Amount"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="PaidAmmount" ShowInGroupFooterColumn="Paid Ammount"
                            SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Sum = {0}" FieldName="ReturnValue" ShowInGroupFooterColumn="Return Value"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="TotalAmount" ShowInColumn="Amount"
                            ShowInGroupFooterColumn="Amount" SummaryType="Sum" ValueDisplayFormat="{0:F2}" />
                    </TotalSummary>
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsDetail ExportMode="Expanded" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnExportReport" runat="server" OnClick="btnExportReport_Click" Text="Export To Report" /><dxwgv:ASPxGridViewExporter
                    ID="gveInvoice" runat="server" FileName="Supplier Cheque Report" GridViewID="dxgvInvoice"
                    PaperKind="A4">
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
