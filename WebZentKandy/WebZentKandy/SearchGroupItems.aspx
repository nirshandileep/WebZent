<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchGroupItems.aspx.cs"
    Inherits="SearchGroupItems" Title="Untitled Page" %>

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
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td class="report_header">
                Group Items LIST</td>
        </tr>
        <tr>
            <td >
                <dxwgv:ASPxGridView ID="dxgvGroupItems" runat="server" AutoGenerateColumns="False" KeyFieldName="GroupId" Width="100%" OnHtmlRowPrepared="dxgvGroupItems_HtmlRowPrepared">
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="Select" FieldName="GroupId" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="AddInvoice.aspx?FromURL=SearchGroupItems.aspx&amp;GroupId={0}"
                                TextFormatString="Use">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <CellStyle ForeColor="Black">
                            </CellStyle>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="Group Code" FieldName="GroupId" VisibleIndex="1">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="GroupItemAdd.aspx?FromURL=SearchGroupItems.aspx&amp;GroupId={0}"
                                TextField="GroupCode">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Group Name" FieldName="GroupName" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Item Count" FieldName="ItemCount" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="IsActive" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="gveGroupItems" runat="server" FileName="Group Items" GridViewID="dxgvGroupItems">
                </dxwgv:ASPxGridViewExporter>
                <asp:Button ID="btnExportReport" runat="server" Text="Export To Report" OnClick="btnExportReport_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
