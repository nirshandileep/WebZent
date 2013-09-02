<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddItemBrand.aspx.cs" Inherits="AddItemBrand" Title="Add Brand" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label id="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
<table class="form" align="center">
        <tr>
            <td width="100">
                Brand Name:</td>
            <td width="10">
            </td>
            <td width="158">
                <asp:TextBox ID="txtBrandName" runat="server" ValidationGroup="vgBrand"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvCatType" runat="server" ControlToValidate="txtBrandName"
                    Display="Dynamic" ErrorMessage="Category Type Cannot Be Empty" ValidationGroup="vgBrand"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Category Type:</td>
            <td>
            </td>
            <td>
                <asp:CheckBoxList ID="cblCategoryType" runat="server" RepeatColumns="4" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                </asp:CheckBoxList></td>
        </tr>
        <tr>
            <td align="left">
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnBrandId" runat="server" Value="0" />
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="vgBrand" />&nbsp;<asp:Button
                    ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
        </tr>
    </table>
</asp:Content>

