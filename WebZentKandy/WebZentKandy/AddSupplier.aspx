<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddSupplier.aspx.cs" Inherits="AddSupplier" Title="Add Supplier" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label id="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
<table class="form" align="center">
    <tr>
        <td style="width: 130px">
            Supplier Code:</td>
        <td width="10">
        </td>
        <td style="width: 263px">
            <asp:TextBox ID="txtSupplierCode" runat="server" ReadOnly="True"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td width="10">
        </td>
        <td style="width: 263px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSupplierCode"
                Display="Dynamic" ErrorMessage="Item Code cannot be empty" ValidationGroup="vgAddSupplier"></asp:RequiredFieldValidator></td>
    </tr>
        <tr>
            <td style="width: 130px">
                Supplier Name:</td>
            <td width="10">
            </td>
            <td style="width: 263px">
                <asp:TextBox ID="txtSupplierName" runat="server" MaxLength="100" Width="306px"></asp:TextBox></td>
        </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td width="10">
        </td>
        <td style="width: 263px">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSupplierName"
                    Display="Dynamic" ErrorMessage="Description Cannot be empty" ValidationGroup="vgAddSupplier"></asp:RequiredFieldValidator></td>
    </tr>
        <tr>
            <td style="width: 130px">
                Phone:</td>
            <td>
            </td>
            <td style="width: 263px">
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox></td>
        </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhone"
                Display="Dynamic" ErrorMessage="Invalid Phone Number" ValidationExpression="^[0-9]*$" ValidationGroup="vgAddSupplier"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td style="width: 130px">
            Contact Person:</td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:TextBox ID="txtContactPerson" runat="server" MaxLength="50" ValidationGroup="vgItemSave"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td>
        </td>
        <td style="width: 263px">
            </td>
    </tr>
    <tr>
        <td style="width: 130px">
            Supplier Address:</td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:TextBox ID="txtSupplierAddress" runat="server" MaxLength="200" ValidationGroup="vgItemSave" Width="293px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td>
        </td>
        <td style="width: 263px" align="left">
            </td>
    </tr>
    <tr>
        <td style="width: 130px">
            Status:</td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 130px">
        </td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStatus"
                Display="Dynamic" ErrorMessage="Status Required" InitialValue="-1" ValidationGroup="vgAddSupplier"></asp:RequiredFieldValidator></td>
    </tr>
        <tr>
            <td align="right" style="width: 130px">
                &nbsp;</td>
            <td>
            </td>
            <td style="width: 263px">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgAddSupplier" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
        </tr>
    <tr>
        <td align="right" style="width: 130px">
        </td>
        <td>
        </td>
        <td style="width: 263px">
            <asp:HiddenField ID="hdnFromURL" runat="server" />
            <asp:HiddenField ID="hdnSupplierId" runat="server" Value="0" />
        </td>
    </tr>
    </table>
</asp:Content>

