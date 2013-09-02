<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportItemHistory.aspx.cs"
    Inherits="ReportItemHistory" Title="Untitled Page" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ MasterType  VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                Item Name:</td>
                            <td>
                                <asp:DropDownList ID="ddlItems" runat="server" CausesValidation="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlItems_SelectedIndexChanged" ValidationGroup="vgItemReport">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnExtraColumns" runat="server" Text="Show Extra Columns" Visible="False"
                                    OnClientClick="return ShowHideCust();"></asp:Button></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Item Transaction History Report</td>
        </tr>
        <tr>
            <td>
                <dx:aspxgridview id="gvItemsTransactionHistory" runat="server" width="100%">
<SettingsPager PageSize="100"></SettingsPager>

<Settings ShowFilterRow="True"></Settings>
</dx:aspxgridview></td>
        </tr>
        <tr>
            <td>
                <dx:aspxgridviewexporter id="gveItemsHistoryExport" runat="server" gridviewid="gvItemsTransactionHistory"></dx:aspxgridviewexporter>
            </td>
        </tr>
    </table>
</asp:Content>
