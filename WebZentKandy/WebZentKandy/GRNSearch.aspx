<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="GRNSearch.aspx.cs" Inherits="GRNSearch" Title="Search GRN" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="form" align="center" width="100%">
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
                                Sales Return ID:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:TextBox ID="txtSalesReturnID" runat="server"></asp:TextBox></td>
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
                                Supplier Invoice Number:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:TextBox ID="txtSupInvNumber" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                </td>
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
                            <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                Date From:</td>
                            <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                                <dxe:ASPxDateEdit ID="dtpFromDate" runat="server">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                Date To:</td>
                            <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                                <dxe:ASPxDateEdit ID="dtpToDate" runat="server">
                                </dxe:ASPxDateEdit>
                            </td>
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
                </div>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    ValidationGroup="vgSearch" />&nbsp;<asp:Button ID="btnExportData" runat="server"
                        OnClick="btnExportData_Click" Text="Export" /></td>
        </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter ID="gveGRNDetailsExporter" runat="server" FileName="GRNReport"
                    GridViewID="dxgvGRNDetails" PreserveGroupRowStates="True">
                </dxwgv:ASPxGridViewExporter>
            </td>
        </tr>
        <tr>
            <td align="center" class="report_header">
                GRN Details</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td>
                <asp:GridView ID="gvGRN" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvGRN_RowDataBound"
                    DataKeyNames="GRNId" Visible="False">
                    <Columns>
                        <asp:TemplateField HeaderText="GRN Id">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GRNId") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbGRNId" runat="server" CommandArgument='<%# Eval("GRNId") %>' OnClick="lbGRNId_Click">LinkButton</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="POCode" HeaderText="PO Code" />
                        <asp:BoundField DataField="SalesReturnID" HeaderText="Sales Return ID" />
                        <asp:BoundField DataField="Rec_Date" HeaderText="Date" />
                        <asp:BoundField DataField="SuplierInvNo" HeaderText="Suplier Invoice" />
                    </Columns>
                </asp:GridView>
                <dxwgv:ASPxGridView ID="dxgvGRNDetails" runat="server" AutoGenerateColumns="False"
                    OnCustomCallback="dxgvGRNDetails_CustomCallback" Width="100%" ForeColor="Black" KeyFieldName="GRNId">
<SettingsPager PageSize="100"></SettingsPager>

<Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFilterRow="True"></Settings>
<Columns>
    <dxwgv:GridViewDataHyperLinkColumn Caption="GRN ID" FieldName="GRNId" VisibleIndex="0" UnboundType="Integer">
        <PropertiesHyperLinkEdit NavigateUrlFormatString="RecieveGoods.aspx?FromURL=GRNSearch.aspx&amp;GRNId={0}">
            <Style ForeColor="Black"></Style>
        </PropertiesHyperLinkEdit>
        <Settings AllowDragDrop="False" AllowGroup="True" AllowSort="True" ShowFilterRowMenu="False" />
        <EditCellStyle ForeColor="Black">
        </EditCellStyle>
        <CellStyle ForeColor="#0000C0">
        </CellStyle>
    </dxwgv:GridViewDataHyperLinkColumn>
<dxwgv:GridViewDataTextColumn FieldName="POCode" Caption="PO Code" VisibleIndex="1" UnboundType="String">
<Settings AllowSort="True" AllowGroup="True" ShowFilterRowMenu="True"></Settings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="InvoiceNo" Caption="Invoice No" VisibleIndex="2" UnboundType="String"></dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataDateColumn FieldName="Rec_Date" UnboundType="DateTime" Caption="Date" VisibleIndex="3">
<PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy hh:mm:ss tt"></PropertiesDateEdit>

<Settings AllowSort="True" AllowGroup="True"></Settings>
    <CellStyle Wrap="False">
    </CellStyle>
</dxwgv:GridViewDataDateColumn>
    <dxwgv:GridViewDataTextColumn FieldName="SupplierName" UnboundType="String" VisibleIndex="4">
    </dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SuplierInvNo" Caption="Suplier Invoice" VisibleIndex="5" UnboundType="String"></dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="ReceivedTotal" UnboundType="Decimal"
        VisibleIndex="6">
    </dxwgv:GridViewDataTextColumn>
</Columns>
</dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
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

