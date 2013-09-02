<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ItemTransfer.aspx.cs"
    Inherits="ItemTransfer" Title="Transfer Items" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            From Branch:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:DropDownList ID="ddlFromBranch" runat="server" OnSelectedIndexChanged="ddlFromBranch_SelectedIndexChanged"
                                AutoPostBack="True" ValidationGroup="vgIT">
                            </asp:DropDownList></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            To Branch:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlToBranch" runat="server" ValidationGroup="vgIT">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlFromBranch" EventName="SelectedIndexChanged">
                                    </asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlFromBranch"
                                Display="Dynamic" ErrorMessage="Select Branch" ValidationGroup="vgIT" InitialValue="-1"></asp:RequiredFieldValidator></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlToBranch"
                                Display="Dynamic" ErrorMessage="Select Branch" ValidationGroup="vgIT" InitialValue="-1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" style="width: 86px">
                            Date:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:Label ID="lblDate" runat="server"></asp:Label></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="ddlFromBranch"
                                ControlToValidate="ddlToBranch" Display="Dynamic" ErrorMessage="Both locations cannot be the same"
                                Operator="NotEqual" Type="Integer" ValidationGroup="vgITLI"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                        </td>
                        <td align="left" style="width: 150px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp</td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Transfer Items Section</p>
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
                                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                                <asp:Button ID="txtSearchItem" runat="server" OnClick="txtSearchItem_Click" Text="Search" />
                                <asp:HiddenField ID="hdnItemIdFromSearch" runat="server" Value="0" />
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Item Name:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode"
                                    Display="Dynamic" ErrorMessage="Item Code Cannot be empty" ValidationGroup="vgITLI"></asp:RequiredFieldValidator>&nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                Quantity:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Category:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <input id="txtItemCount" type="text" value="0" runat="server" style="display: none" />
                                        <asp:TextBox ID="txtCategory" runat="server" ReadOnly="True"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Quantity Required" ValidationGroup="vgITLI"></asp:RequiredFieldValidator><asp:CompareValidator
                                        ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity" Display="Dynamic"
                                        ErrorMessage="Invalid Quantity" Operator="LessThanEqual" Type="Integer" ValidationGroup="vgITLI"
                                        ValueToCompare="0" ControlToCompare="txtItemCount"></asp:CompareValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCategory"
                                    Display="Dynamic" Enabled="False" ErrorMessage="Category Required" ValidationGroup="vgITLI"></asp:RequiredFieldValidator></td>
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
                <asp:Button ID="btnSavePOItem" runat="server" OnClick="btnSavePOItem_Click" Text="Add Item"
                    ValidationGroup="vgITLI" /></td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnFromURL" runat="server" />
                <asp:HiddenField ID="hdnTransferBy" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td class="report_header">
                Items list</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td>
                <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="0" CellSpacing="1" Width="100%" OnRowEditing="gvItemList_RowEditing"
                    DataKeyNames="ItemId" OnRowDeleting="gvItemList_RowDeleting" Font-Size="Larger">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
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
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgIT"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSaveTransfer" runat="server" Text="Save Transfer" ValidationGroup="vgIT"
                    OnClick="btnSaveTransfer_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnTransferID" runat="server" Value="0" />
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
