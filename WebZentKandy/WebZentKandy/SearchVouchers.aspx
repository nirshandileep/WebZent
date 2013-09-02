<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchVouchers.aspx.cs"
    Inherits="SearchVouchers" Title="Search Vouchers" %>

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
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            Cheque Number From:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:TextBox ID="txtChqNoFrom" runat="server"></asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Cheque Number To:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:TextBox ID="txtChqNoTo" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                        </td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px" valign="top">
                            Cheque Date From:</td>
                        <td align="left" style="width: 164px" valign="top">
                            <dxe:ASPxDateEdit ID="dtpFromDate" runat="server">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Cheque Date To:</td>
                        <td align="left" style="width: 130px" valign="top">
                            <dxe:ASPxDateEdit ID="dtpToDate" runat="server">
                            </dxe:ASPxDateEdit>
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
                            &nbsp;</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            &nbsp;</td>
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
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;<asp:Button
                    ID="btnClearSearch" runat="server" Text="Clear" OnClick="btnClearSearch_Click" /></td>
        </tr>
        <tr>
            <td class="report_header">
                    Search Vouchers
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvVouchers" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="VoucherID">
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="Voucher Code" FieldName="VoucherID" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddVoucher.aspx?VoucherID={0}"
                                TextField="VoucherCode">
                                <Style Font-Underline="True" ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="VoucherType" UnboundType="String" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="Amount" UnboundType="Decimal"
                            VisibleIndex="2">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Payment Type" FieldName="PaymentType" UnboundType="String"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ChequeNumber" FieldName="ChequeNumber" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Created By" FieldName="CreatedUser" UnboundType="String"
                            VisibleIndex="5" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="VoucherPaymentDate" UnboundType="DateTime"
                            VisibleIndex="6">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="Bank" UnboundType="String"
                            VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Bank Branch" FieldName="BankBranch" UnboundType="String"
                            VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Voucher Branch" FieldName="VoucherBranch"
                            UnboundType="String" VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Account" FieldName="AccountName" UnboundType="String"
                            VisibleIndex="9">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="Amount" ShowInGroupFooterColumn="Amount"
                            SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="Amount" ShowInColumn="Amount"
                            SummaryType="Sum" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnVouchExport" runat="server" OnClick="Button1_Click" Text="Export To Report" /></td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="gveVoucher" runat="server" FileName="Voucher Report">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
    </table>
</asp:Content>
