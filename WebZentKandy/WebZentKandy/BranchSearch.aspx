<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="BranchSearch.aspx.cs"
    Inherits="BranchSearch" Title="Veiw Branch" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" class="form" align="center">
        <tr>
            <td align="center" class="report_header">
                All Branches
            </td>
        </tr>
        <tr id="trGrid" runat="server">
            <td >
                <asp:GridView ID="gvBranches" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                    Width="100%" OnRowCommand="gvBranches_RowCommand" DataKeyNames="BranchId">
                    <Columns>
                        <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" />
                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Address1" HeaderText="Address" />
                        <asp:BoundField DataField="Telephone" HeaderText="Telephone" />
                        <asp:BoundField DataField="ContactName" HeaderText="ContactName" />
                        <asp:BoundField DataField="IsActive" HeaderText="Status" />
                        <asp:BoundField DataField="InvPrefix" HeaderText="Inv Code" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="~/Images/icoEdit.gif" CommandArgument = '<%# Eval("BranchId") %>' OnClick="ibtnEdit_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
            </td>
        </tr>
        <tr id="trNoRecords" runat ="server" visible="false" >
                        <td class="NoRecords">
                            No Records</td>
                    </tr>
        <tr runat="server">
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
