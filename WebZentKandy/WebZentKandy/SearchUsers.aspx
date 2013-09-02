<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchUsers.aspx.cs" Inherits="SearchUsers" Title="Search Users" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="js/Dialogs.js" type="text/javascript"></script>
    <table class="form" align="center">
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td width="150">
                            First Name:</td>
                        <td width="200">
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
                        <td width="35">
                            &nbsp;</td>
                        <td width="150">
                            Branch:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranches" runat="server">
                            </asp:DropDownList></td>
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
                    <tr>
                        <td >
                            Last Name:</td>
                        <td >
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                        <td >
                        </td>
                        <td >
                            User Name:</td>
                        <td >
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
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
                            User Role:</td>
                        <td>
                            <asp:DropDownList ID="ddlUserRole" runat="server">
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
            <td >
                <asp:Button ID="Button2" runat="server" OnClick="btnSearch_Click" Text="Search" />
                <asp:Button ID="Button1" runat="server" OnClick="btnClear_Click" Text="Clear" /></td>
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
                            User List</td>
                    </tr>
                    <tr id="trGridView" runat="server">
                        <td>
                            <asp:GridView ID="gvUserList" runat="server" BorderWidth="0px" CellPadding="0" CellSpacing="1"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="UserId" OnRowDataBound="gvUserList_RowDataBound" >
                                <RowStyle CssClass="GridItems" />
                                <HeaderStyle CssClass="GridHeading" />
                                <Columns>
                                    <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                    <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                    <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                    <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" />
                                    <asp:BoundField HeaderText="Status" DataField="IsActive" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit" ImageUrl="~/Images/icoEdit.gif" CommandArgument = '<%# Eval("UserId") %>' OnClick="btnEdit_Click" ToolTip="Edit User" />
                                        </ItemTemplate>
                                        <ItemStyle Width="15px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trNoRecords" runat ="server" visible="false" >
                        <td class="NoRecords">
                            No Records</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

