<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchReceivableVouchers.aspx.cs"
    Inherits="SearchReceivableVouchers" Title="Vouchers Received" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="report_header">
                Search Vouchers Reveived
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dxgvReceivedVouchers" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="PaymentID">
                    <Columns>
                        <dx:GridViewDataHyperLinkColumn FieldName="PaymentID" UnboundType="String" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="VouchersReceivable.aspx?PaymentID={0}"
                                TextField="PaymentCode">
                            </PropertiesHyperLinkEdit>
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn Caption="Customer Name" FieldName="Cus_Name" UnboundType="String"
                            VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Amount" FieldName="PaymentAmount" UnboundType="Decimal"
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PaymentDate" UnboundType="DateTime" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PaymentType" UnboundType="String" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Created By" FieldName="CreatedUser" UnboundType="String"
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CreatedDate" UnboundType="DateTime" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ModifiedUser" UnboundType="String" Visible="False"
                            VisibleIndex="11">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ModifiedDate" UnboundType="DateTime" Visible="False"
                            VisibleIndex="12">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ChequeNo" UnboundType="String" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ChequeDate" UnboundType="DateTime" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CardType" UnboundType="String" VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Comment" UnboundType="String" VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowGroupPanel="True" ShowFilterRow="True" />
                    <SettingsPager PageSize="50" RenderMode="Lightweight">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridViewExporter ID="gveVoucherReport" runat="server" FileName="Vouchers Reveived">
                </dx:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Export Report" OnClick="Button1_Click" /></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
