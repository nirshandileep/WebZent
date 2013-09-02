<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchChequeBooks.aspx.cs"
    Inherits="SearchChequeBooks" Title="Cheque Search" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Search cheques</td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dxgvChequeBooks" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ChqBookId">
                    <SettingsBehavior EnableRowHotTrack="True"></SettingsBehavior>
                    <Columns>
                        <dx:GridViewDataHyperLinkColumn Caption="Book No" FieldName="ChqBookId" UnboundType="Integer"
                            VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddChequeBook.aspx?FromURL=SearchChequeBooks.aspx&amp;ChqBookId={0}">
                            </PropertiesHyperLinkEdit>
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataTextColumn FieldName="NoOfCheques" UnboundType="Integer" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FirstChqNo" UnboundType="Integer" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="LastChqNo" UnboundType="Integer" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Created By" FieldName="CreatedByName" UnboundType="String"
                            VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="Created Date" FieldName="CreatedDate" UnboundType="DateTime"
                            VisibleIndex="8">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="BankName" UnboundType="String" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BankBranch" UnboundType="String" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True">
                    </Settings>
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridViewExporter ID="gveChequeReport" runat="server" FileName="Cheque book search results"
                    GridViewID="dxgvChequeBooks">
                </dx:ASPxGridViewExporter>
                <dx:ASPxButton ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export Report">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
