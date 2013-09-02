<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportDayBook.aspx.cs"
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
                                            Text="Show Extra Columns" Visible="False" />
                                    </td>
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
                Daybook Report</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvExpenseReport" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="ID" ClientInstanceName="grid">
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" ColumnResizeMode="Control" AutoExpandAllGroups="True" AllowSort="False" />
                    <Settings ShowGroupPanel="True"
                        ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" GridLines="Horizontal" />
                    <Columns>
                        <dxwgv:GridViewDataDateColumn FieldName="Date" GroupIndex="0" SortIndex="0" SortOrder="Ascending"
                            UnboundType="DateTime" VisibleIndex="0" Visible="False">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Reference" UnboundType="String" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" UnboundType="String" VisibleIndex="2" Width="50%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Debit" VisibleIndex="3" Width="15%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Credit" VisibleIndex="4" Width="15%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Total" Visible="False" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Day total = {0}" FieldName="Debit" ShowInGroupFooterColumn="Debit"
                            SummaryType="Sum" Tag="A" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Day total = {0}" FieldName="Credit" ShowInGroupFooterColumn="Credit"
                            SummaryType="Sum" Tag="B" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Diff. = {0}" FieldName="Total" ShowInGroupFooterColumn="Credit"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="Debit" ShowInColumn="Debit"
                            ShowInGroupFooterColumn="Debit" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="Credit" ShowInColumn="Credit"
                            ShowInGroupFooterColumn="Credit" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="C/F = {0}" FieldName="Total" ShowInColumn="Credit"
                            ShowInGroupFooterColumn="Credit" SummaryType="Sum" />
                    </TotalSummary>
                    <Templates>
                        <DetailRow>
                            &nbsp;
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ExportMode="All" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnExportReport" runat="server" OnClick="btnExportReport_Click" Text="Export To Report" /><dxwgv:ASPxGridViewExporter
                    ID="gveExpenceReport" runat="server" FileName="Expense Report" GridViewID="dxgvExpenseReport">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
