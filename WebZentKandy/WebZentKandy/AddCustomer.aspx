<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddCustomer.aspx.cs"
    Inherits="AddCustomer" Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    <%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label><asp:ScriptManager
                    ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" class="form" align="center">
                    <tr>
                        <td style="width: 130px">
                            Customer Code:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtCustomerCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Customer Name:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtCust_Name" runat="server" MaxLength="100" ValidationGroup="vgItemSave"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCust_Name"
                                Display="Dynamic" ErrorMessage="Customer Name Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Customer Address:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtCus_Adress" runat="server" MaxLength="500" ValidationGroup="vgItemSave" Width="475px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCus_Adress"
                                Display="Dynamic" ErrorMessage="Address Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Telephone:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone"
                                Display="Dynamic" ErrorMessage="Phone Number Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Contact Person:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtContactName" runat="server" MaxLength="10" ValidationGroup="vgItemSave"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactName"
                                Display="Dynamic" ErrorMessage="Contact Person Name Cannot be empty" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlStatus"
                                Display="Dynamic" ErrorMessage="Status cannot be empty" InitialValue="-1" ValidationGroup="vgItemSave"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px; text-align: left;">
                            Is Credit Allowed:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:CheckBox ID="chkIsCreditAllowed" runat="server" Checked="True" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 130px">
                            &nbsp;</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="vgItemSave"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 130px">
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:HiddenField ID="hdnFromURL" runat="server" />
                            <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
