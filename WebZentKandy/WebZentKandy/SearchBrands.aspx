<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="SearchBrands.aspx.cs" Inherits="SearchBrands" Title="Search Brands" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="form" align="center">
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                </td>
        </tr>
        <tr align="left">
            <td>
                <table class="form" style="width: 100%" align="center">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="Content2">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="left" style="width: 86px" valign="bottom">
                                        Brand Name:</td>
                                    <td align="left" style="width: 164px" valign="bottom">
                                        <asp:TextBox ID="txtBrandName" runat="server"></asp:TextBox></td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                        </td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px" valign="bottom">
                                    &nbsp;
                                    </td>
                                    <td align="left" style="width: 164px" valign="bottom">
                                        </td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        </td>
                                </tr>
                            </table>
                            <asp:Button ID="btnSearchTop" runat="server" OnClick="btnSearch_Click" Text="Search" />
                            <asp:Button ID="btnAddNewBrandTop" runat="server" Text="Add New Brand" OnClick="btnAddNewBrand_Click" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnBrandId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnFromURL" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="report_header">
                            Brands List</td>
                    </tr>
                    <tr id="trGrid" runat="server">
                        <td>
                            <asp:GridView ID="gvBrands" runat="server" AutoGenerateColumns="False"
                                CellPadding="0" CellSpacing="1" DataKeyNames="BrandId" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibtnEdit" runat="server" CommandArgument='<%# Eval("BrandId") %>'
                                                ImageUrl="~/Images/icoEdit.gif" OnClick="ibtnEdit_Click" ToolTip="Edit Brand" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trNoRecords" runat="server" class="NoRecords">
                        <td>
                            no records</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSearchBottom" runat="server" OnClick="btnSearch_Click" Text="Search" />
                            <asp:Button ID="btnAddNewBrandBottom" runat="server" Text="Add New Brand" OnClick="btnAddNewBrand_Click" /></td>
                    </tr>
                    <tr>
                        <td>
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

