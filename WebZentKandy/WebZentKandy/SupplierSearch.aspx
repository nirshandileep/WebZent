<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SupplierSearch.aspx.cs" Inherits="SupplierSearch" Title="View Suppliers" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="form" align="center">
    <tr>
        <td>
            <asp:Label ID="lblError" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>&nbsp;</td>
    </tr>
        <tr  style="display:none">
          <td align="center"><p class="details_header"> Search Supplier </p></td>                
         </tr>
        <tr style="display:none">
            <td class="Content2">
            
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                   
                    <tr>
                        <td width="150">
                            Supplier Code:</td>
                        <td width="200">
                            <asp:TextBox ID="txtSupplierCode" runat="server"></asp:TextBox></td>
                        <td width="35">
                            &nbsp;</td>
                        <td width="150">
                            Supplier Name:</td>
                        <td>
                            <asp:TextBox ID="txtSupplierName" runat="server" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td >
                            Contact Person:</td>
                        <td >
                            <asp:TextBox ID="txtContactPerson" runat="server" MaxLength="50" ValidationGroup="vgItemSave"></asp:TextBox></td>
                        <td >
                        </td>
                        <td >
                            Supplier Address:</td>
                        <td >
                            <asp:TextBox ID="txtSupplierAddress" runat="server" MaxLength="200" ValidationGroup="vgItemSave"></asp:TextBox></td>
                    </tr>
                    <tr style="display:none">
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status:</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="display:none">
            <td >
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                <asp:Button ID="Button1" runat="server" OnClick="btnClear_Click" Text="Clear" />&nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="GridBg">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="report_header">
                            Supplierr List</td>
                    </tr>
                    <tr id="trGridView" runat="server">
                        <td class="Content2">
                            <dxwgv:aspxgridview id="dxgvSupplierList" runat="server" autogeneratecolumns="False"
                                keyfieldname="SupId" width="100%" OnRowUpdating="dxgvSupplierList_RowUpdating" OnRowValidating="dxgvSupplierList_RowValidating">
<SettingsPager PageSize="50"></SettingsPager>

<Settings ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRow="True"></Settings>
<Columns>
    <dxwgv:GridViewCommandColumn VisibleIndex="0">
        <EditButton Visible="True">
        </EditButton>
        <ClearFilterButton Visible="True">
        </ClearFilterButton>
    </dxwgv:GridViewCommandColumn>
<dxwgv:GridViewDataHyperLinkColumn FieldName="SupId" Caption="Supplier Code" ShowInCustomizationForm="False" VisibleIndex="1">
<PropertiesHyperLinkEdit NavigateUrlFormatString="AddSupplier.aspx?FromURL=SupplierSearch.aspx&amp;SupplierId={0}" TextField="Sup_Code">
<Style ForeColor="Black"></Style>
</PropertiesHyperLinkEdit>

<EditFormSettings Visible="False"></EditFormSettings>
</dxwgv:GridViewDataHyperLinkColumn>
<dxwgv:GridViewDataTextColumn FieldName="SupplierName" Caption="Name" ShowInCustomizationForm="False" VisibleIndex="2">
<EditFormSettings Visible="False"></EditFormSettings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SupplierPhone" Caption="Phone" ShowInCustomizationForm="False" VisibleIndex="3">
<EditFormSettings Visible="False"></EditFormSettings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="SupplierAddress" Caption="Address" ShowInCustomizationForm="False" VisibleIndex="4">
<EditFormSettings Visible="False"></EditFormSettings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="IsActive" UnboundType="Boolean" Caption="Is Active" ShowInCustomizationForm="False" VisibleIndex="5">
<EditFormSettings Visible="False"></EditFormSettings>
</dxwgv:GridViewDataTextColumn>
<dxwgv:GridViewDataTextColumn FieldName="CreditAmmount" UnboundType="Decimal" Caption="Credit Ammount" VisibleIndex="6">
<EditFormSettings Visible="True"></EditFormSettings>
</dxwgv:GridViewDataTextColumn>
</Columns>
</dxwgv:aspxgridview>
                        </td>
                    </tr>
                    <tr id="trNoRecords" runat ="server" visible="false" >
                        <td class="NoRecords">
                            No Records</td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr>
        <td class="GridBg">
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
            <asp:Button
                    ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export To Report" /></td>
    </tr>
    </table>
    <asp:HiddenField ID="hdnFromURL" runat="server" />
    <dxwgv:ASPxGridViewExporter id="gveSupplierList" runat="server" FileName="Supplier Report"
        GridViewID="dxgvSupplierList">
    </dxwgv:ASPxGridViewExporter>
</asp:Content>

