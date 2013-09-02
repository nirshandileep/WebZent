<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddCategory.aspx.cs" Inherits="AddCategory" Title="Add Category"%>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label id="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
<table class="form" align="center">
        <tr>
            <td width="100">
                Category Type:</td>
            <td width="10">
            </td>
            <td width="158">
                <asp:TextBox ID="txtCatType" runat="server" ValidationGroup="vgCat"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvCatType" runat="server" ControlToValidate="txtCatType"
                    Display="Dynamic" ErrorMessage="Category Type Cannot Be Empty" ValidationGroup="vgCat"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Description:</td>
            <td>
            </td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnCategory" runat="server" Value="0" />
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="vgCat" />&nbsp;<asp:Button
                    ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
        </tr>
    </table>
</asp:Content>

