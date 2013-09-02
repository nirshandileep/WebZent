<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="GroupItemAdd.aspx.cs"
    Inherits="GroupItemAdd" Title="Add Group Items" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                <asp:ScriptManager id="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lbtnSingleItem" runat="server" PostBackUrl="~/AddItem.aspx">Add Single Item</asp:LinkButton>
                &nbsp;<asp:Label ID="lblGroupItem" runat="server" Text="Add Group Item"></asp:Label></td>
        </tr>
        <tr>
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
                                        Group Code:</td>
                                    <td align="left" style="width: 164px" valign="bottom">
                                        <asp:TextBox ID="txtGICode" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                        Group Name:</td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtGICode"
                                            Display="Dynamic" ErrorMessage="Group Code Required" ValidationGroup="vgGI"></asp:RequiredFieldValidator></td>
                                    <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtGroupName"
                                            Display="Dynamic" ErrorMessage="Group Name Required" ValidationGroup="vgGI"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px" valign="bottom">
                                        Description:</td>
                                    <td align="left" style="width: 164px" valign="bottom">
                                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                        Status:</td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        <asp:DropDownList ID="ddlStatus" runat="server">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 86px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 164px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 13px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 129px" valign="bottom">
                                    </td>
                                    <td align="left" style="width: 130px" valign="bottom">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlStatus"
                                            Display="Dynamic" ErrorMessage="Status Required" InitialValue="-1" ValidationGroup="vgGI"></asp:RequiredFieldValidator></td>
                                </tr>
                            </table>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp
                            </td>
                    </tr>
                    <tr>
                        <td align="center"><p class="details_header">Add Items</p>
                            </td>
                    </tr>
                    <tr>
                        <td class="Content2">
                            <div style="text-align: left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td align="left" valign="top" style="width: 86px">
                                            Item Code:</td>
                                        <td align="left" valign="top" style="width: 150px">
                                            <asp:TextBox ID="txtItemCode" runat="server" ></asp:TextBox>
                                            <asp:Button ID="btnSearchItem" runat="server" OnClick="btnSearchItem_Click" Text="Search"
                                                ValidationGroup="vgSearch" />
                                            <asp:HiddenField ID="hdnItemIdFromSearch" runat="server" Value="0" />
                                        </td>
                                        <td align="left" style="width: 13px" valign="top">
                                        </td>
                                        <td align="left" valign="top" style="width: 129px">
                                            Item Name:</td>
                                        <td align="left" style="width: 130px" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <contenttemplate>
                                        <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True"></asp:TextBox>
                                    </contenttemplate>
                                                <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearchItem" EventName="Click" />
                                    </triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                            &nbsp;</td>
                                        <td align="left" style="width: 170px; height: 20px;" valign="top">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode"
                                                Display="Dynamic" ErrorMessage="Item Code Cannot be empty" ValidationGroup="vgGItem"></asp:RequiredFieldValidator>&nbsp;</td>
                                        <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                            &nbsp;</td>
                                        <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtItemName"
                                                Display="Dynamic" ErrorMessage="Item Name Required" ValidationGroup="vgGItem"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 86px" valign="top">
                                            Brand:</td>
                                        <td align="left" style="width: 170px" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
<asp:TextBox id="txtBrand" runat="server" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnSearchItem" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td align="left" style="width: 13px" valign="top">
                                        </td>
                                        <td align="left" style="width: 129px" valign="top">
                                            Category:</td>
                                        <td align="left" style="width: 130px" valign="bottom">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
<asp:TextBox id="txtCategory" runat="server" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnSearchItem" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 86px" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 170px" valign="bottom">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBrand"
                                                Display="Dynamic" ErrorMessage="Brand Name Required" ValidationGroup="vgGItem" Enabled="False"></asp:RequiredFieldValidator></td>
                                        <td align="left" style="width: 13px" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 129px" valign="bottom">
                                            &nbsp;</td>
                                        <td align="left" style="width: 130px" valign="bottom">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCategory"
                                                Display="Dynamic" ErrorMessage="Category Required" ValidationGroup="vgGItem" Enabled="False"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 86px" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 170px" valign="bottom">
                                            &nbsp;</td>
                                        <td align="left" style="width: 13px" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 129px" valign="bottom">
                                        </td>
                                        <td align="left" style="width: 130px" valign="bottom">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Button ID="btnAddItem" runat="server" OnClick="btnAddItem_Click" Text="Add Item"
                                ValidationGroup="vgGItem" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnGroupItemId" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnFromURL" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="report_header">
                            Items In Group</td>
                    </tr>
                    <tr id="trGrid" runat="server">
                        <td class="Content2">
                            <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                                CellPadding="0" CellSpacing="1" GridLines="Horizontal" Width="100%"
                                DataKeyNames="ItemId" OnRowDeleting="gvItemList_RowDeleting" Font-Size="Larger">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                    <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                                    <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                    <asp:CommandField ShowDeleteButton="True" />
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
                            <input id="txtLineItemsCount" type="text" style="display: none" runat="server" value="0" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                                ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgGI"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddGroup" runat="server" Text="Save Group" ValidationGroup="vgGI" OnClick="btnAddGroup_Click" />&nbsp;<asp:Button
                                ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hdnGroupId" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
