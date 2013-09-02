<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchTransfers.aspx.cs"
    Inherits="SearchTransfers" Title="Items Transfer Search" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label><dxwgv:aspxgridviewexporter
                    id="dxgveTransferReport" runat="server" gridviewid="dxgvTransferList" FileName="Items Transfer Details"></dxwgv:aspxgridviewexporter>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Transfered Items LIST</td>
        </tr>
        <tr>
            <td>
                <dxwgv:aspxgridview id="dxgvTransferList" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn FieldName="TransferId" UnboundType="Integer" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="ItemTransfer.aspx?TransferId={0}">
                            </PropertiesHyperLinkEdit>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn Caption="From" FieldName="BranchFromCode" UnboundType="String"
                            VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="To" FieldName="BranchCodeTo" UnboundType="String"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="TransferQty" UnboundType="Integer"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Transfered By" FieldName="TransferUserName"
                            UnboundType="String" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="TransferDate" UnboundType="DateTime"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
</dxwgv:aspxgridview>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" Visible="False" /><asp:Button ID="btnExportToReport" runat="server" OnClick="btnExportToReport_Click"
                    Text="Items Transfer Report" /></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
