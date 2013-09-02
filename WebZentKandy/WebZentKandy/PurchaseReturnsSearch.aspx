<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PurchaseReturnsSearch.aspx.cs"
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
                    <tr style="display:none">
                        <td align="left" style="width: 86px" valign="bottom">
                            PR Code:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:TextBox ID="txtPRNumber" runat="server"></asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Supplier Inv No.:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:TextBox ID="txtSupInvNo" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px; height: 14px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 164px; height: 14px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px; height: 14px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 14px;" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 130px; height: 14px;" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px" valign="top">
                            Return Date From:</td>
                        <td align="left" style="width: 164px" valign="top">
                            <dxe:ASPxDateEdit ID="dtpFromDate" runat="server">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Return Date To:</td>
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
                    <tr style="display:none">
                        <td align="left" style="width: 86px" valign="bottom">
                            Issued:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:DropDownList ID="ddlIssueStatus" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Totally Issued</asp:ListItem>
                                <asp:ListItem Value="2">Partially Issued</asp:ListItem>
                            </asp:DropDownList></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
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
                    Search purchase returns</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="dxgvPurchaseReturns" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="PRId">
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="Invoice No." FieldName="SuplierInvNo" UnboundType="String"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="POCode" UnboundType="String" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="PR Code" FieldName="PRId" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="PurchaseReturns.aspx?PRId={0}"
                                TextField="PRCode">
                                <Style Font-Underline="True" ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GRNId" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CreatedUser" UnboundType="String"
                            VisibleIndex="4" Visible="False">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CreatedDate" UnboundType="DateTime"
                            VisibleIndex="5" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ModifiedUser" VisibleIndex="6" UnboundType="DateTime" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ModifiedDate" UnboundType="String"
                            VisibleIndex="7" Visible="False">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="TotalReturns" UnboundType="Decimal" VisibleIndex="8">
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="ReturnDate" UnboundType="DateTime" VisibleIndex="9">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
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
                <dxwgv:ASPxGridViewExporter ID="gvePurchaseReturn" runat="server" FileName="Purchase Returns" GridViewID="dxgvPurchaseReturns" PaperKind="A4">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
    </table>
</asp:Content>
