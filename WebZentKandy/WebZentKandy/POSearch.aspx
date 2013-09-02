<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="POSearch.aspx.cs"
    Inherits="POSearch" Title="View Purchase Orders" %>

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

    <table width="100%" class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="bottom" style="width: 86px">
                                PO Code:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:TextBox ID="txtPOCode" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                Supplier:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:DropDownList ID="ddlSupplier" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                PO Ammount (Less than):</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:TextBox ID="txtPOAmmount" runat="server"></asp:TextBox>(Rs.)</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Payment Due PO:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:DropDownList ID="ddlPaymentDue" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPOAmmount"
                                    Display="Dynamic" ErrorMessage="Cost is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                    ValidationGroup="vgSearch"></asp:RegularExpressionValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                Recieved:&nbsp;</td>
                            <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                                <asp:DropDownList ID="ddlPORecievedOption" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                From Date:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <dxe:ASPxDateEdit ID="dtpFromDate" runat="server">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                To Date:</td>
                            <td align="left" style="width: 130px" valign="bottom">
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
                    </table>
                </div>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" Visible="False" OnClick="btnCancel_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    ValidationGroup="vgSearch" />&nbsp;<asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                        Text="PO Report" /></td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter id="vgePOSearch" runat="server" FileName="Purchase Order Report"
                    GridViewID="dxgvPOSearch">
                </dxwgv:ASPxGridViewExporter></td>
        </tr>
        <tr>
            <td class="report_header">
                Purchase Orders list</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td>
                <dxwgv:ASPxGridView ID="dxgvPOSearch" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="POId" OnBeforePerformDataSelect="dxgvPOSearch_BeforePerformDataSelect">
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterBar="Auto" ShowFilterRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="PO Code" FieldName="POId" VisibleIndex="0">
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="POAdd.aspx?POId={0}" TextField="POCode">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <Settings AllowGroup="False" AllowSort="True" AutoFilterCondition="Contains" FilterMode="DisplayText" />
                            <CellStyle ForeColor="Black">
                            </CellStyle>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PO Amount" FieldName="POAmount" UnboundType="Decimal"
                            VisibleIndex="1">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <Settings AllowSort="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Balance Amount" FieldName="BalanceAmount"
                            UnboundType="Decimal" VisibleIndex="2">
                            <PropertiesTextEdit DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <Settings AllowSort="True" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="PO Date" FieldName="PODate" UnboundType="DateTime"
                            VisibleIndex="3">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Supplier" FieldName="SupplierName" UnboundType="String"
                            VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Created By" FieldName="CreatedByName" UnboundType="String"
                            VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Last Modified" FieldName="ModifiedByName"
                            UnboundType="String" Visible="False" VisibleIndex="7">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Date Modified" FieldName="POLastModifiedDate"
                            UnboundType="DateTime" Visible="False" VisibleIndex="8">
                            <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt">
                            </PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="RequestedBy" UnboundType="String" VisibleIndex="6">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100" RenderMode="Lightweight">
                    </SettingsPager>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="dxgvPODetails" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="POId" OnBeforePerformDataSelect="dxgvPODetails_BeforePerformDataSelect"
                                Width="100%">
                                <SettingsPager PageSize="15">
                                </SettingsPager>
                                <GroupSummary>
                                    <dxwgv:ASPxSummaryItem FieldName="LineCost" ShowInColumn="Total Cost" ShowInGroupFooterColumn="Total Cost"
                                        SummaryType="Sum" />
                                </GroupSummary>
                                <TotalSummary>
                                    <dxwgv:ASPxSummaryItem DisplayFormat="{0}" FieldName="LineCost" ShowInColumn="Total Cost"
                                        ShowInGroupFooterColumn="Total Cost" SummaryType="Sum" />
                                </TotalSummary>
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn FieldName="ItemCode" UnboundType="String" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="ItemDescription" UnboundType="String" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Item Cost" FieldName="POItemCost" UnboundType="Decimal"
                                        VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Qty Ordered" FieldName="Qty" UnboundType="Integer"
                                        VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Total Cost" FieldName="LineCost" UnboundType="Decimal"
                                        VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Qty Received" FieldName="TotalReceived" UnboundType="Integer"
                                        VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Qty Remaining" FieldName="TotalRemaining"
                                        UnboundType="Integer" Visible="False" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Discount" FieldName="DiscountPerUnit" UnboundType="Decimal"
                                        VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" />
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail ExportMode="Expanded" ShowDetailRow="True" />
                </dxwgv:ASPxGridView>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr id="trNoRecords" runat="server" visible="false">
            <td class="NoRecords">
                No Records</td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
