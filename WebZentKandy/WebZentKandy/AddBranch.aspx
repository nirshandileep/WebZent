<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddBranch.aspx.cs"
    Inherits="AddBranch" Title="Add Branch" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td align="center"><p class="details_header">Branch</p>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td width="100">
                            <asp:Label ID="lblError" runat="server" Text="Label" Visible="False" ForeColor="Red"
                                CssClass="show_error"></asp:Label></td>
                        <td width="10">
                        </td>
                        <td width="158">
                        </td>
                    </tr>
                    <tr>
                        <td width="100">
                            Branch Code:</td>
                        <td width="10">
                        </td>
                        <td width="158">
                            <asp:TextBox ID="txtBranchCode" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="100">
                        </td>
                        <td width="10">
                        </td>
                        <td width="158">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBranchCode"
                                Display="Dynamic" ErrorMessage="Branch Code Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td width="100">
                            Branch Name:</td>
                        <td width="10">
                        </td>
                        <td width="158">
                            <asp:TextBox ID="txtBranchName" runat="server" MaxLength="50"></asp:TextBox>
                            Inv Code:
                            <asp:TextBox ID="txtInvCode" runat="server" Columns="2" MaxLength="2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td width="100">
                        </td>
                        <td width="10">
                        </td>
                        <td width="158">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtInvCode"
                                Display="Dynamic" ErrorMessage="Invoicing code Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBranchName"
                                Display="Dynamic" ErrorMessage="Branch Name Required"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Address1:</td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress1" runat="server" MaxLength="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress1"
                                Display="Dynamic" ErrorMessage="Address1 Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Address2:</td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddress2" runat="server" MaxLength="500"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Telephone:</td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTelPhone" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTelPhone"
                                Display="Dynamic" ErrorMessage="Telephone Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Contact Name:</td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContact" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContact"
                                Display="Dynamic" ErrorMessage="Contact Name Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            Status:</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStatus"
                                Display="Dynamic" ErrorMessage="Status Required" ValidationGroup="vgSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="vgSave" />&nbsp;<asp:Button
                                ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" /></td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnFromURL" runat="server" />
                            <asp:HiddenField ID="hdnBranchId" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
