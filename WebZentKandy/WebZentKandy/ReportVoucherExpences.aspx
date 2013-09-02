<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportVoucherExpences.aspx.cs"
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
                                            Text="Show Extra Columns" />
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
                Expense Report</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvExpenseReport" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="VoucherID" ClientInstanceName="grid">
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="True" EnableRowHotTrack="True" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True"
                        ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="VoucherCode" UnboundType="String" VisibleIndex="0">
                            <Settings FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CreatedBy" Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="CreatedDate" UnboundType="DateTime" VisibleIndex="2">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ChequeNumber" UnboundType="String" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="ChequeDate" UnboundType="DateTime" Visible="False"
                            VisibleIndex="5">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Bank" UnboundType="String" Visible="False"
                            VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="BankBranch" UnboundType="String" Visible="False"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" UnboundType="String" VisibleIndex="8">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TotalAmount" UnboundType="Decimal" VisibleIndex="16">
                            <PropertiesTextEdit DisplayFormatString="c2">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="VoucherType" UnboundType="String" Visible="False"
                            VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDropDownEditColumn FieldName="PaymentType" UnboundType="String"
                            Visible="False" VisibleIndex="10">
                        </dxwgv:GridViewDataDropDownEditColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SupplierName" UnboundType="String" VisibleIndex="11">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Supplier Code" FieldName="Sup_Code" UnboundType="String"
                            Visible="False" VisibleIndex="12">
                            <Settings FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CustomerCode" UnboundType="String" Visible="False"
                            VisibleIndex="13">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="Cus_Name" UnboundType="String"
                            VisibleIndex="14">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Payment Date" FieldName="VoucherPaymentDate"
                            UnboundType="DateTime" VisibleIndex="3">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDropDownEditColumn FieldName="AccountName" UnboundType="String"
                            VisibleIndex="15">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataDropDownEditColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Voucher Branch" FieldName="VoucherBranch"
                            UnboundType="String" VisibleIndex="18">
                            <Settings AutoFilterCondition="Contains" FilterMode="DisplayText" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="TotalAmount" ShowInColumn="Total Amount"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="TotalAmount" ShowInColumn="Total Amount"
                            ShowInGroupFooterColumn="Total Amount" SummaryType="Sum" />
                    </TotalSummary>
                    <SettingsCustomizationWindow Enabled="True" />
                    <Templates>
                        <DetailRow>
                            &nbsp;
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ExportMode="Expanded" />
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
