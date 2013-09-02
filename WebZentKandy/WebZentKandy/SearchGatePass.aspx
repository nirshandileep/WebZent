<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchGatePass.aspx.cs" Inherits="SearchGatePass" Title="Search GatePass" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="form" align="center" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>&nbsp;</td>
        </tr>
        <tr >
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="bottom" style="width: 86px">
                                GP Code:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:TextBox ID="txtGPCode" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr style="display:none">
                            <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                Recieved:</td>
                            <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                                <asp:DropDownList ID="ddlIssuedBy" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                Invoice Number:</td>
                            <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                                <asp:TextBox ID="txtInvoiceNumber" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr style="display:none">
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
                                &nbsp;</td>
                            <td align="left" style="width: 164px" valign="bottom">
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
                </td>
        </tr>
    <tr>
        <td>
                <asp:Button ID="btnCancel" runat="server" Text="Clear" Visible="False" OnClick="btnCancel_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    ValidationGroup="vgSearch" />
            <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                        Text="Gate Pass Report" Visible="False" /></td>
    </tr>
        <tr>
            <td>
                <dxwgv:ASPxGridViewExporter id="vgeGatePass" runat="server" FileName="GatePass Report"
                    GridViewID="dxgvGPSearch">
                </dxwgv:ASPxGridViewExporter></td>
        </tr>
        <tr>
            <td class="report_header">
                Gate Pass Results</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td>
                <dxwgv:ASPxGridView ID="dxgvGPSearch" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="GPId">
                    <Settings ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                    <Columns>
                        <dxwgv:GridViewDataHyperLinkColumn Caption="GP Code" FieldName="GPId" UnboundType="String"
                            VisibleIndex="0">
                            <PropertiesHyperLinkEdit TextField="GPCode" NavigateUrlFormatString="AddGatePass.aspx?GPId={0}&amp;FromURL=SearchGatePass.aspz">
                                <Style ForeColor="Black"></Style>
                            </PropertiesHyperLinkEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dxwgv:GridViewDataHyperLinkColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="InvoiceNo" UnboundType="String" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Issued By" FieldName="CreatedUserName" VisibleIndex="2" UnboundType="String">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="CreatedDate" VisibleIndex="3" UnboundType="DateTime">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                </dxwgv:ASPxGridView>
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

