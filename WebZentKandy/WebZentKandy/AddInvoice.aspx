<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddInvoice.aspx.cs"
    Inherits="AddInvoice" Title="Add Invoice" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="JS/Dialogs.js"></script>

    <table class="form" align="center" style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label><asp:ScriptManager
                    ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Invoice</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            Invoice Number:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <contenttemplate>
                                    <asp:TextBox ID="txtInvNo" runat="server" ReadOnly="True"></asp:TextBox>
                                </contenttemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chkIsHidden" EventName="CheckedChanged" />
                                </triggers>
                            </asp:UpdatePanel><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
                                DisplayAfter="1"><progresstemplate>
<SPAN style="COLOR: #0033ff">Generating number, please wait...</SPAN>
</progresstemplate>
                            </asp:UpdateProgress>
                            &nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <contenttemplate>
                                    <asp:CheckBox ID="chkIsHidden" runat="server" AutoPostBack="True" OnCheckedChanged="chkIsHidden_CheckedChanged"
                                        Text=" " />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Date:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <dxe:ASPxDateEdit ID="dpDate"
                                runat="server">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" style="width: 86px">
                            Payment Type:</td>
                        <td align="left" valign="bottom" style="width: 164px">
                            <asp:DropDownList ID="ddlPaymentType" runat="server" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged"
                                onchange="OnSelectedIndexChange()" AutoPostBack="True">
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                                <asp:ListItem Value="3">Credit Card</asp:ListItem>
                            </asp:DropDownList></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" valign="bottom" style="width: 129px">
                            Invoice Ammount:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:Label ID="lblInvAmount" runat="server"></asp:Label>
                            (Rs.)</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 150px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="top">
                            Customer Name:</td>
                        <td align="left" style="width: 150px" valign="top">
                            <asp:DropDownList ID="ddlCustomerCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCode_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="top">
                            Balance Payment:</td>
                        <td align="left" style="width: 140px" valign="top">
                            <asp:Label ID="lblBalancePayment" runat="server"></asp:Label>
                            (Rs.)</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomerCode"
                                Display="Dynamic" ErrorMessage="Customer Code Required" InitialValue="-1" ValidationGroup="vgInv"></asp:RequiredFieldValidator></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            Customer Address:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:UpdatePanel id="UpdatePanel6" runat="server">
                                <contenttemplate>
<asp:TextBox id="txtCus_Adress" runat="server" Enabled="False" TextMode="MultiLine" Rows="3" Columns="30" MaxLength="500"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlCustomerCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Unused GRN Id's:</td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:UpdatePanel id="UpdatePanel9" runat="server">
                                <contenttemplate>
<asp:TextBox id="txtGRNIds" runat="server" ReadOnly="True"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlCustomerCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr id="trCreditOption" runat="server">
                        <td align="left" style="width: 86px" valign="bottom">
                            Credit Option:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <asp:DropDownList ID="ddlCreditOption" runat="server" OnSelectedIndexChanged="ddlCreditOption_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="-1">--Please Select--</asp:ListItem>
                                <asp:ListItem Value="1">Customer</asp:ListItem>
                                <asp:ListItem Value="2">GRN</asp:ListItem>
                            </asp:DropDownList></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr id="trCreditOptionBel" runat="server">
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="top">
                            Customer Balance:</td>
                        <td align="left" style="width: 164px" valign="bottom">
                            <table cellpadding="0" cellspacing="0">
                                <tr id="trCustomer">
                                    <td>
                                        <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                            <contenttemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCustomerCredBal" runat="server" Enabled="False">0</asp:TextBox>(Rs.)</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkUseCredit" runat="server" Text="Reduce From Invoice" Visible="False" /></td>
                                </tr>
                            </table></contenttemplate>
                                            <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlCustomerCode" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                        </asp:UpdatePanel></td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trGRN">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtGRNId" runat="server" ValidationGroup="vggrn" ToolTip="Enter GRN Number"></asp:TextBox></td>
                                                <td>
                                                    <asp:Button ID="btnCheckGRNId" runat="server" OnClick="btnCheckGRNId_Click" Text="Check"
                                                        ValidationGroup="vggrn" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGRNError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtGRNId"
                                                        Display="Dynamic" ErrorMessage="GRN Number Required" ValidationGroup="vggrn"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvCustomerBal" runat="server" ControlToValidate="txtCustomerCredBal"
                                                        Display="Dynamic" ErrorMessage="Invalid Return Ammount" MaximumValue="0" MinimumValue="0"
                                                        Type="Currency" ValidationGroup="vgInv"></asp:RangeValidator></td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnGRNId" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            <div id="dvCardType" style="display: none">
                                Card Type:</div>
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <div id="dvCardTypeControl" style="display: none">
                                <asp:DropDownList ID="ddlCardType" runat="server">
                                    <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Master</asp:ListItem>
                                    <asp:ListItem Value="2">Visa</asp:ListItem>
                                    <asp:ListItem Value="3">American Express</asp:ListItem>
                                </asp:DropDownList>&nbsp;</div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 164px" valign="bottom">
                        </td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateCardType"
                                ControlToValidate="ddlCardType" Display="Dynamic" ErrorMessage="Select Card Type"
                                ValidationGroup="vgInv"></asp:CustomValidator></td>
                    </tr>
                    <tr id="tbChequeDetails" style="display: none">
                        <td align="left" style="width: 86px" valign="bottom">
                            Cheque Number:</td>
                        <td align="left" style="width: 150px" valign="bottom">
                            <asp:TextBox ID="txtChequeNumber" runat="server"></asp:TextBox></td>
                        <td align="left" style="width: 13px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px" valign="bottom">
                            Cheque Date:
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                            <dxe:ASPxDateEdit ID="dxdcChequeDate" runat="server">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr id="trChequeAr">
                        <td align="left" style="width: 86px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 150px" valign="bottom">
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateChequeNumber"
                                ControlToValidate="txtChequeNumber" Display="Dynamic" ErrorMessage="Cheque Number Required"
                                ValidationGroup="vgInv" ValidateEmptyText="True"></asp:CustomValidator></td>
                        <td align="left" style="width: 13px" valign="bottom">
                            &nbsp;</td>
                        <td align="left" style="width: 129px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px" valign="bottom">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px; height: 17px;" valign="top">
                            Total Paid:</td>
                        <td align="left" style="width: 164px; height: 17px;" valign="bottom">
                            <asp:TextBox ID="txtPaidAmount" runat="server"></asp:TextBox>(Rs.)</td>
                        <td align="left" style="width: 13px; height: 17px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 17px;" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px; height: 17px;" valign="bottom">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 86px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 164px; height: 17px" valign="bottom">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPaidAmount"
                                Display="Dynamic" ErrorMessage="Amount is invalid" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                ValidationGroup="vgInv"></asp:RegularExpressionValidator></td>
                        <td align="left" style="width: 13px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 129px; height: 17px" valign="bottom">
                        </td>
                        <td align="left" style="width: 130px; height: 17px" valign="bottom">
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Items Section</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <div style="text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="top" style="width: 86px">
                                Item Code:<asp:RadioButtonList ID="rblItemType" runat="server" Width="122%" Visible="False">
                                    <asp:ListItem Selected="True" Value="1">Item Code:</asp:ListItem>
                                    <asp:ListItem Value="2" Enabled="False">Group Code:</asp:ListItem>
                                </asp:RadioButtonList></td>
                            <td align="left" valign="top" style="width: 150px">
                                <asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>
                                <asp:Button ID="txtSearchItem" runat="server" OnClick="txtSearchItem_Click" Text="Search" /><asp:HiddenField
                                    ID="hdnItemIdFromSearch" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnGroupIdFromSearch" runat="server" Value="0" />
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" valign="top" style="width: 129px">
                                Item Name:</td>
                            <td align="left" style="width: 130px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtItemName" runat="server" ReadOnly="True" Columns="30" Rows="3"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode"
                                    Display="Dynamic" ErrorMessage="Item Code Cannot be empty" ValidationGroup="vgInvLi"></asp:RequiredFieldValidator>&nbsp;</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="top">
                                Order Quantity:</td>
                            <td align="left" style="width: 170px" valign="top">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <contenttemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                        <input id="txtAvailableQty" type="text" runat="server" style="display: none" value="0" />
                                    </contenttemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" style="width: 13px" valign="top">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Selling Price:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <contenttemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtItemSelPrice" runat="server" BackColor="MistyRose"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtMinSellingPrice" type="text" runat="server" style="display: none" value="0" /></td>
                                            </tr>
                                        </table>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlDiscount" EventName="SelectedIndexChanged" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQuantity"
                                    Display="Dynamic" ErrorMessage="Insufficient Quantity" Operator="LessThanEqual"
                                    Type="Integer" ValidationGroup="vgInvLi" ControlToCompare="txtAvailableQty" Enabled="False"></asp:CompareValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantity" Display="Dynamic"
                                        ErrorMessage="Quantity Required" ValidationGroup="vgInvLi"></asp:RequiredFieldValidator></td>
                            <td align="left" style="width: 13px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemSelPrice"
                                    Display="Dynamic" ErrorMessage="Selling Price Is Empty" ValidationGroup="vgInvLi"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtItemSelPrice"
                                        Display="Dynamic" ErrorMessage="Invalid Selling Price" ValidationExpression="(?!^0*$)(?!^0*\.0*$)^\d{1,6}(\.\d{1,3})?$"
                                        ValidationGroup="vgInvLi" Enabled="False"></asp:RegularExpressionValidator><asp:CompareValidator
                                            ID="cvCheckMinSellingPrice" runat="server" ControlToCompare="txtMinSellingPrice"
                                            ControlToValidate="txtItemSelPrice" Display="Dynamic" ErrorMessage="Invalid Selling Price"
                                            Operator="GreaterThanEqual" Type="Double" ValidationGroup="vgInvLi"></asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                            </td>
                            <td align="left" style="width: 170px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                                Discount(%):</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <contenttemplate>
                                        <asp:HiddenField ID="hdnPriceBeforeDiscount" runat="server" />
                                        <asp:DropDownList ID="ddlDiscount" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </contenttemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtSearchItem" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Button ID="btnSavePOItem" runat="server" Text="Add Item" ValidationGroup="vgInvLi"
                    OnClick="btnSavePOItem_Click" /></td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnItemId" runat="server" Value="0" />
                <asp:HiddenField ID="hdnFromURL" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr id="trTransferNote" runat="server" style="display: none">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 86px">
                            Transfer Note:
                        </td>
                        <td>
                            <textarea id="txtTransferNote" cols="60" rows="3" readonly="readOnly" runat="server"></textarea>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" style="display: none" id="Tr1">
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="report_header">
                Invoice Details</td>
        </tr>
        <tr id="trGrid" runat="server">
            <td class="Content2">
                <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="0" CellSpacing="1" Width="100%" DataKeyNames="ItemId,GroupId" OnRowDeleting="gvItemList_RowDeleting"
                    OnRowDataBound="gvItemList_RowDataBound" Font-Size="Larger">
                    <Columns>
                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                        <asp:BoundField DataField="Price" HeaderText="Selling Price" DataFormatString="{0:F2}" />
                        <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                        <asp:BoundField DataField="DiscountPerUnit" HeaderText="Discount(%)" />
                        <asp:BoundField DataField="TotalPrice" HeaderText="Line Price" DataFormatString="{0:F2}" />
                        <asp:CommandField ShowEditButton="True" Visible="False" />
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="trNoRecords" runat="server" class="NoRecords">
            <td>
                No Records
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr id="trRemarks" runat="server" class="Content2">
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 86px">
                            Cancel Note:
                        </td>
                        <td>
                            <textarea id="txtCancelNote" cols="60" rows="3" runat="server"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 86px">
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Cancel Note Required"
                                ValidationGroup="vgCancel" Display="Dynamic" ControlToValidate="txtCancelNote"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <input id="txtLineItemsCount" type="text" style="display: none" runat="server" value="0" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Atleast one line item must be added"
                    ControlToValidate="txtLineItemsCount" Display="Dynamic" InitialValue="0" ValidationGroup="vgInv"></asp:RequiredFieldValidator>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddInvoice" runat="server" Text="Save Invoice" ValidationGroup="vgInv"
                                OnClick="btnAddInvoice_Click" OnClientClick="return AllowSave('vgInv')" />
                        </td>
                        <td>
                        </td>
                        <td runat="server" id="tdPrintInvoice" visible="false">
                            <asp:Button ID="btnPrintInvoice" runat="server" OnClick="btnPrintInvoice_Click" Text="Print Invoice"
                                OnClientClick="return ShowPrintWindow()" /></td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnQuotation" runat="server" OnClick="btnQuotation_Click" Text="Print Quotation" /></td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel Invoice" ValidationGroup="vgCancel"
                                OnClick="btnCancel_Click" /></td>
                        <td>
                            <asp:Button ID="btnInvByChange" runat="server" OnClientClick="return ShowChangeRep()"
                                Text="Change Sales Rep" Visible="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnInvId" runat="server" Value="0" />
                <dxpc:ASPxPopupControl ID="dxpcChangeUser" runat="server" ClientInstanceName="ChangeUserPopup"
                    HeaderText="Change Creator" AllowDragging="True" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Width="236px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInvoicedBy" runat="server" Text="Invoiced By:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlInvoicedBy" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveRep" runat="server" OnClick="btnSaveRep_Click" Text="Change"
                                            OnClientClick="return AllowSave('vgInv')" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl ID="dxPrintInvoice" runat="server" AllowDragging="True" AllowResize="True"
                    ClientInstanceName="invoicePrint" CloseAction="CloseButton" DragElement="Window"
                    EnableClientSideAPI="True" HeaderText="Print Invoice" Height="650px" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="950px">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                &nbsp;&nbsp;</td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        OnSelectedIndexChange();
        function ShowPrintWindow() {
            invoicePrint.Show();
            return false;
        }
        
        function ShowChangeRep() {
            ChangeUserPopup.Show();
            return false;
        }
        
    </script>

</asp:Content>
