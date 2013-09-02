<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportSupplierPayments.aspx.cs"
    Inherits="ReportSupplierPayments" Title="Supplier Report" %>

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
                                            <ValidationSettings Display="Dynamic" ValidationGroup="vgSupReport">
                                                <RequiredField ErrorText="From Date Required" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        To Date:
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="dtpToDate" runat="server">
                                            <ValidationSettings Display="Dynamic" ValidationGroup="vgSupReport">
                                                <RequiredField ErrorText="To Date Required" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Supplier Name:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" ValidationGroup="vgSupReport" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" AutoPostBack="True" CausesValidation="True">
                                        </asp:DropDownList></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search"
                                            ValidationGroup="vgSupReport">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExtraColumns" runat="server" OnClientClick="return ShowHideCust();"
                                            Text="Show Extra Columns" Visible="False" /></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSupplier"
                                            Display="Dynamic" ErrorMessage="Supplier Required" InitialValue="-1" ValidationGroup="vgSupReport"></asp:RequiredFieldValidator></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
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
                Supplier Report</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvSupplierPaymentReport" runat="server" AutoGenerateColumns="False"
                    ClientInstanceName="grid" KeyFieldName="ID" Width="100%">
                    <Templates>
                        <DetailRow>
                            &nbsp;
                        </DetailRow>
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" AutoExpandAllGroups="True"
                        ColumnResizeMode="Control" EnableRowHotTrack="True" />
                    <SettingsPager PageSize="100">
                    </SettingsPager>
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
                        <dxwgv:ASPxSummaryItem DisplayFormat="Balance = {0}" FieldName="Total" ShowInColumn="Total"
                            ShowInGroupFooterColumn="Total" SummaryType="Sum" />
                    </TotalSummary>
                    <Columns>
                        <dxwgv:GridViewDataDateColumn FieldName="Date" UnboundType="DateTime" VisibleIndex="0" ReadOnly="True" Width="10%">
                            <PropertiesDateEdit DisplayFormatString="dd MMM yyyy" DisplayFormatInEditMode="True">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Reference" UnboundType="String" VisibleIndex="1" Width="12%">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" UnboundType="String" VisibleIndex="2"
                            Width="50%">
                            <Settings AllowDragDrop="False" AllowGroup="False" AutoFilterCondition="Contains" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Debit" VisibleIndex="3" Width="15%" Visible="False">
                            <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="c2">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Credit" VisibleIndex="4" Width="15%" Visible="False">
                            <PropertiesTextEdit DisplayFormatString="c2">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Total" VisibleIndex="5" UnboundType="Decimal" Width="13%">
                            <PropertiesTextEdit DisplayFormatString="c2">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Settings GridLines="Horizontal" ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways"
                        ShowGroupPanel="True" ShowFilterRow="True" />
                    <SettingsDetail ExportMode="All" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnExportReport" runat="server" OnClick="btnExportReport_Click" Text="Export To Report" /><dxwgv:ASPxGridViewExporter
                    ID="gveExpenceReport" runat="server" FileName="Supplier Report" GridViewID="dxgvSupplierPaymentReport" PaperKind="A4">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
