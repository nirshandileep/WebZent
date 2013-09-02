<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchInvoice.aspx.cs"
    Inherits="SearchInvoice" Title="Search Invoices" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Search Invoices
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="InvoiceId">
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="Invoice No" FieldName="InvoiceId" ReadOnly="True"
                            ShowInCustomizationForm="False" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddInvoice.aspx?FromURL=SearchInvoice.aspx&amp;InvoiceId={0}"
                                TextField="InvoiceNo">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <EditFormSettings Visible="False" />
                            <CellStyle ForeColor="Black">
                            </CellStyle>
                            <Settings FilterMode="DisplayText" SortMode="DisplayText" />
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="Date" ReadOnly="True" ShowInCustomizationForm="False"
                            UnboundType="DateTime" VisibleIndex="1">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                            <Settings AllowGroup="True" GroupInterval="DateMonth" />
                            <EditFormSettings Visible="False" />
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GrandTotal" FieldName="GrandTotal" ReadOnly="True"
                            ShowInCustomizationForm="False" UnboundType="Decimal" VisibleIndex="2">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Branch" FieldName="BranchCode" ReadOnly="True"
                            ShowInCustomizationForm="False" VisibleIndex="3">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Due Amount" FieldName="DueAmount" ReadOnly="True"
                            ShowInCustomizationForm="False" UnboundType="Decimal" VisibleIndex="4">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDropDownEditColumn Caption="Invoiced By" FieldName="UserName"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataDropDownEditColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="Cus_Name" UnboundType="String"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem FieldName="DueAmount" ShowInColumn="Due Amount" SummaryType="Sum" ValueDisplayFormat="{0}" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="GrandTotal" ShowInColumn="GrandTotal" ShowInGroupFooterColumn="GrandTotal"
                            SummaryType="Sum" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
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
