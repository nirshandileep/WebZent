<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" Title="Add User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language ="javascript" src="js/validations.js" type="text/javascript"></script>
     <table class="form" align="center">
       <tr id="trError"><!-- show hide this tr when show hiding error messages -->
            <td class="Error">
                <asp:Label ID="lblSuccess" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager></td>
        </tr>
         <tr>
             <td align="center"><p class="details_header">user details</p>
                 </td>
         </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="BoldTitle">
                            First Name:</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirstName"
                                Display="Dynamic" ErrorMessage="Cannot leave blank" ValidationGroup="vgNew"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="BoldTitle">
                            Last Name:</td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td >&nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="BoldTitle">
                            User Role:</td>
                        <td>
                            <asp:DropDownList ID="ddlUserRole" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="BoldTitle" >
                            Status:</td>
                        <td >
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="BoldTitle">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="BoldTitle">
                            Branch:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranches" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="Content2">
                <table>
                    <tr>
                        <td width="150" class="BoldTitle">
                            User Name:</td>
                        <td>
                            <asp:TextBox onKeyPress="return noSpaceBar(event)" ID="txtUserName" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Cannot leave blank" ValidationGroup="vgNew" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblUserNameExists" runat="server" CssClass="ErrorText" Text="The Username already exists" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <cc1:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtPassword">
                            </cc1:PasswordStrength>
                        </td>
                    </tr>
                    <tr>
                        <td class="BoldTitle" >
                            Password:</td>
                        <td >
                            <asp:TextBox onkeypress="return noSpaceBar(event)" ID="txtPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                ErrorMessage="Cannot leave blank" ValidationGroup="vgNew" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="BoldTitle">
                            Confirm Password:</td>
                        <td>
                            <asp:TextBox onkeypress="return noSpaceBar(event)" ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                                ErrorMessage="Cannot leave blank" ValidationGroup="vgNew" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords does not match" ValidationGroup="vgNew" Display="Dynamic"></asp:CompareValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnUserId" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="vgNew" />&nbsp;<asp:Button
                    ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" /></td>
        </tr>
    </table>
</asp:Content>

