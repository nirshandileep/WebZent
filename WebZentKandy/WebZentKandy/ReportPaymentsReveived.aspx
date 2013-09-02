<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ReportPaymentsReveived.aspx.cs"
    Inherits="ReportPurchaseOrderMain" Title="Payments Received Report" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="JS/popcalendar.js" type="text/javascript" language="javascript"></script>

    <script src="JS/Validations.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript">
        function ShowHideCust() {            if(grid.IsCustomizationWindowVisible())            {                grid.HideCustomizationWindow();            }            else            {                grid.ShowCustomizationWindow();            }            return false;        }
    </script>

    <table width="100%" class="form" align="center">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="top">
                                From Date:</td>
                            <td align="left" valign="bottom">
                                <dxe:ASPxDateEdit ID="dtpFromDate" runat="server">
                                    <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="Text"
                                        ValidationGroup="vg">
                                        <RequiredField ErrorText="Date Required" IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" valign="bottom">
                                &nbsp;</td>
                            <td align="left" valign="top">
                                To Date:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <dxe:ASPxDateEdit ID="dtpToDate" runat="server">
                                    <ValidationSettings CausesValidation="True" Display="Dynamic" ErrorDisplayMode="Text"
                                        SetFocusOnError="True" ValidationGroup="vg">
                                        <RequiredField ErrorText="Date Required" IsRequired="True" />
                                    </ValidationSettings>
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" valign="bottom">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                    ValidationGroup="vg" /></td>
                            <td align="left" valign="bottom">
                                &nbsp;</td>
                            <td align="left" valign="bottom">
                                <asp:Button ID="btnExtraColumns" runat="server" OnClientClick="return ShowHideCust();"
                                    Text="Show Extra Columns" /></td>
                        </tr>
                    </table>
                </div>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="vgePaymentsReceivable" runat="server" FileName="Payments Received Report"
                    GridViewID="dxgvPaymentsReceived" PaperKind="A4">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td class="report_header">
                payments received REPORT</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td>
                <dxwgv:ASPxGridView ID="dxgvPaymentsReceived" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" ClientInstanceName="grid">
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterRow="True"
                        ShowFooter="True" ShowGroupedColumns="True" ShowGroupFooter="VisibleAlways" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="PaymentCode" VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="InvoiceNo" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Cust. Code" FieldName="CustomerCode" Visible="False"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Cust. Name" FieldName="Cus_Name" UnboundType="String"
                            VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PaymentType" UnboundType="String" Visible="False"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" UnboundType="String" Visible="False"
                            VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CardCommisionRate" UnboundType="Decimal"
                            Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="CardType" UnboundType="String" Visible="False"
                            VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ChequeDate" UnboundType="DateTime" VisibleIndex="9">
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="ChequeNo" UnboundType="String" VisibleIndex="10">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="PaymentDate" UnboundType="DateTime" VisibleIndex="11">
                            <Settings GroupInterval="DateMonth" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Amount" UnboundType="Decimal" VisibleIndex="12">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior EnableRowHotTrack="True" />
                    <GroupSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="Amount"
                            SummaryType="Sum" ShowInGroupFooterColumn="Amount" />
                    </GroupSummary>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="Amount" ShowInColumn="Amount"
                            ShowInGroupFooterColumn="Amount" SummaryType="Sum" />
                    </TotalSummary>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Report" /></td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
