<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchCustomer.aspx.cs"
    Inherits="SearchCustomer" Title="Search Customers" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="report_header">Customers List
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:aspxgridview id="dxgvCustomers" runat="server" autogeneratecolumns="False"
                    keyfieldname="CustomerID" width="100%" ForeColor="Black" ClientInstanceName="grid" OnRowUpdating="dxgvCustomers_RowUpdating" OnRowValidating="dxgvCustomers_RowValidating">
<SettingsPager PageSize="50"></SettingsPager>

<Settings ShowHeaderFilterButton="True" ShowGroupPanel="True" ShowFilterRow="True"></Settings>
<Columns>
    <dxwgv:GridViewCommandColumn VisibleIndex="0">
        <EditButton Visible="True">
        </EditButton>
        <CellStyle Font-Underline="True" ForeColor="Black">
        </CellStyle>
        <FooterCellStyle ForeColor="Black">
        </FooterCellStyle>
    </dxwgv:GridViewCommandColumn>
    <dxwgv:GridViewDataHyperLinkColumn Caption="Customer Code" FieldName="CustomerID"
        ReadOnly="True" ShowInCustomizationForm="False" VisibleIndex="1">
        <PropertiesHyperLinkEdit NavigateUrlFormatString="AddCustomer.aspx?CustomerID={0}&amp;FromURL=SearchCustomer.aspx"
            TextField="CustomerCode">
            <Style ForeColor="Black"></Style>
        </PropertiesHyperLinkEdit>
        <EditFormSettings Visible="False" />
        <CellStyle ForeColor="Black">
        </CellStyle>
    </dxwgv:GridViewDataHyperLinkColumn>
    <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="Cus_Name" ReadOnly="True"
        ShowInCustomizationForm="False" VisibleIndex="2">
        <EditFormSettings Visible="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Cus_Address" ReadOnly="True"
        VisibleIndex="3">
        <EditFormSettings Visible="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Phone" FieldName="Cus_Tel" ReadOnly="True"
        VisibleIndex="4">
        <EditFormSettings Visible="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Contact Name" FieldName="Cus_Contact" VisibleIndex="5" ReadOnly="True">
        <EditFormSettings Visible="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="IsActive"
        UnboundType="Boolean" VisibleIndex="6" ShowInCustomizationForm="False">
        <EditFormSettings Visible="False" />
    </dxwgv:GridViewDataTextColumn>
    <dxwgv:GridViewDataTextColumn Caption="Credit Ammount" FieldName="Cus_CreditTotal"
        Name="Cus_CreditTotal" UnboundType="Decimal" VisibleIndex="7">
        <PropertiesTextEdit DisplayFormatString="{0:F2}">
            <ValidationSettings CausesValidation="True">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </PropertiesTextEdit>
        <EditFormSettings Visible="True" />
    </dxwgv:GridViewDataTextColumn>
</Columns>
                    <ClientSideEvents RowClick="function(s, e) {
	//grid.StartEditRow(e.visibleIndex);
}" />
</dxwgv:aspxgridview>
            </td>
        </tr>
        <tr>
            <td>
                <dxwgv:aspxgridviewexporter id="gveCustomerExport" runat="server" filename="Customer List"
                    gridviewid="dxgvCustomers"></dxwgv:aspxgridviewexporter>
                &nbsp;</td>
        </tr>
          <tr>
            <td>
                <asp:Button ID="btnExportToReport" runat="server" OnClick="btnExportToReport_Click"
                    Text="Customer Report" />&nbsp;<asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                        Text="Update" /></td>
        </tr>
          <tr>
            <td>
            </td>
        </tr>
          <tr>
            <td>
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
