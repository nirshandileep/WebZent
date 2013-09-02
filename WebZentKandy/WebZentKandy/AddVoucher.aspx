<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddVoucher.aspx.cs"
    Inherits="AddVoucher" Title="Add Voucher" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
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
                <table style="width: 100%" align="center">
                    <tr>
                        <td style="width: 130px">
                            Voucher Code:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtVoucherCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" align="center">
                    <tr>
                        <td style="width: 130px">
                            Voucher Type:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:DropDownList ID="ddlVoucherType" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged"
                                runat="server" AutoPostBack="True">
                                <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                <asp:ListItem Value="0">Other</asp:ListItem>
                                <asp:ListItem Value="1">Purchase Order</asp:ListItem>
                                <asp:ListItem Value="2">Suppliers</asp:ListItem>
                                <asp:ListItem Value="3">Customers</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td colspan="1" width="10">
                        </td>
                        <td colspan="1" style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlVoucherType"
                                Display="Dynamic" ErrorMessage="Voucher Type Cannot Be Empty" InitialValue="-1"
                                ValidationGroup="voucher"></asp:RequiredFieldValidator>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trAccountType">
            <td>
                <table style="width: 100%" align="center">
                    <tr>
                        <td style="width: 130px">
                            Account:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <contenttemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlAccountTypes" runat="server">
                                                </asp:DropDownList><asp:TextBox ID="txtAccountType" runat="server" ValidationGroup="vgAccount"
                                                    Visible="False"></asp:TextBox></td>
                                            <td>
                                                <asp:Button ID="btnAddAccount" runat="server" OnClick="btnAddAccount_Click" Text="Add New"
                                                    ValidationGroup="vgAccount" />
                                                <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="OtherVoucherTypes"
                                                    ControlToValidate="ddlAccountTypes" Display="Dynamic" ErrorMessage="Account Required"
                                                    ValidationGroup="voucher"></asp:CustomValidator></td>
                                        </tr>
                                    </table>
                                    <asp:RequiredFieldValidator ID="rfvAccountReq" runat="server" ControlToValidate="txtAccountType"
                                        Display="Dynamic" Enabled="False" ErrorMessage="Account Type Required" ValidationGroup="vgAccount"></asp:RequiredFieldValidator>
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trSupplier">
            <td>
                <table style="width: 100%" align="center">
                    <tr>
                        <td style="width: 130px">
                            Supplier:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateSupplier"
                                            ControlToValidate="ddlSuppliers" Display="Dynamic" ErrorMessage="Supplier Required"
                                            ValidationGroup="voucher"></asp:CustomValidator></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" align="center" id="trCustomer">
                    <tr>
                        <td style="width: 130px">
                            Customer:</td>
                        <td style="width: 10px">
                        </td>
                        <td style="width: 263px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateCustomer"
                                            ControlToValidate="ddlCustomer" Display="Dynamic" ErrorMessage="Customer Required"
                                            ValidationGroup="voucher"></asp:CustomValidator></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr id="trSelectionMode" style="display: none">
            <td>
            </td>
        </tr>
        <tr id="trPOHeader" style="display: none">
            <td align="center" class="report_header" style="height: 28px">
                <strong>Purchase Orders</strong></td>
        </tr>
        <tr id="trPOGrid">
            <td class="Content2">
                <dxwgv:ASPxGridView ID="dxgvPODetails" runat="server" Width="100%" AutoGenerateColumns="False"
                    OnSelectionChanged="dxgvPODetails_SelectionChanged" ClientInstanceName="grid"
                    KeyFieldName="GRNId" OnAfterPerformCallback="dxgvPODetails_AfterPerformCallback"
                    OnCustomCallback="dxgvPODetails_CustomCallback" OnHtmlCommandCellPrepared="Grid_HtmlCommandCellPrepared"
                    OnRowUpdating="dxgvPODetails_RowUpdating">
                    <Columns>
                        <dxwgv:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <EditButton Visible="True">
                            </EditButton>
                        </dxwgv:GridViewCommandColumn>
                        <dxwgv:GridViewDataTextColumn Caption="GRN Number" FieldName="GRNId" UnboundType="Integer"
                            VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PO Code" FieldName="POCode" VisibleIndex="3"
                            ShowInCustomizationForm="False" ReadOnly="True">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="PO Amount" FieldName="POAmount" UnboundType="Decimal"
                            VisibleIndex="4" ShowInCustomizationForm="False" ReadOnly="True">
                            <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Balance Amount" FieldName="BalanceAmount"
                            UnboundType="Decimal" VisibleIndex="5" ShowInCustomizationForm="False" ReadOnly="True">
                            <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:F2}">
                            </PropertiesTextEdit>
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="SupplierName" FieldName="SupplierName" UnboundType="String"
                            VisibleIndex="8" ShowInCustomizationForm="False" ReadOnly="True">
                            <Settings AllowHeaderFilter="True" />
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="SuplierInvNo" VisibleIndex="6" ShowInCustomizationForm="False"
                            ReadOnly="True">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="GRNTotal" UnboundType="Decimal" VisibleIndex="7"
                            Caption="GRN Due">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="POId" UnboundType="Integer" Visible="False"
                            VisibleIndex="1" ReadOnly="True" ShowInCustomizationForm="False">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ProcessSelectionChangedOnServer="True" />
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total Ordered = {0:F2}" FieldName="BalanceAmount"
                            ShowInColumn="Balance Amount" ShowInGroupFooterColumn="Balance Amount" SummaryType="Sum" />
                        <dxwgv:ASPxSummaryItem DisplayFormat="Total Received = {0:F2}" FieldName="GRNTotal"
                            ShowInColumn="GRN Due" ShowInGroupFooterColumn="GRN Due" SummaryType="Sum" />
                    </TotalSummary>
                    <Settings ShowFooter="True" />
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" align="center">
                    <tr>
                        <td style="width: 130px">
                            Ammount:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <contenttemplate>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAmmount" runat="server" MaxLength="50"></asp:TextBox>
                                            </td>
                                            <td>
                                                (Rs.)
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" /></td>
                                        </tr>
                                    </table>
                                </contenttemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlVoucherType" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSuppliers" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCustomer" EventName="SelectedIndexChanged" />
                                </triggers>
                            </asp:UpdatePanel>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmmount"
                                Display="Dynamic" ErrorMessage="Amount Cannot be empty" ValidationGroup="voucher"></asp:RequiredFieldValidator><asp:CompareValidator
                                    ID="CompareValidator1" runat="server" ControlToValidate="txtAmmount" Display="Dynamic"
                                    ErrorMessage="Amount is invalid" Operator="GreaterThan" Type="Double" ValidationGroup="voucher"
                                    ValueToCompare="0"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Payment Description:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtDescription" runat="server" Columns="30" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Voucher Branch:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:DropDownList ID="ddlBranch" runat="server" ValidationGroup="voucher">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch"
                                Display="Dynamic" ErrorMessage="Voucher Branch Required" InitialValue="-1" ValidationGroup="voucher"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Voucher Payment Date:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <dxe:ASPxDateEdit ID="dtpPaymentDate" runat="server" EditFormat="Custom" EditFormatString="dd/MMM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpPaymentDate"
                                Display="Dynamic" ErrorMessage="Payment date required" ValidationGroup="voucher"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            Payment Type:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:DropDownList onchange="ChangePaymentType()" ID="ddlPaymentType" runat="server"
                                OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">
                                <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                <asp:ListItem Value="1">Cash</asp:ListItem>
                                <asp:ListItem Value="2">Cheque</asp:ListItem>
                                <asp:ListItem Value="3" Enabled="False">Credit Card</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                        </td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPaymentType"
                                Display="Dynamic" ErrorMessage="Payment Type Required" InitialValue="-1" ValidationGroup="voucher"></asp:RequiredFieldValidator>&nbsp;</td>
                    </tr>
                    <tr id="trBankName">
                        <td style="width: 130px">
                            Bank Name:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:UpdatePanel id="UpdatePanel3" runat="server">
                                <contenttemplate>
                            <asp:DropDownList ID="ddlBankName" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>HSBC</asp:ListItem>
                                <asp:ListItem>HNB</asp:ListItem>
                            </asp:DropDownList>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlChequeNumbers" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                    </tr>
                    <tr id="trBankNameAftr">
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr id="trBranch">
                        <td style="width: 130px">
                            Bank
                            Branch Location:</td>
                        <td width="10">
                        </td>
                        <td style="width: 263px">
                            <asp:UpdatePanel id="UpdatePanel4" runat="server">
                                <contenttemplate>
                            <asp:DropDownList ID="ddlBranchLocation" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Nugegoda</asp:ListItem>
                                <asp:ListItem>Nawala</asp:ListItem>
                            </asp:DropDownList>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="ddlChequeNumbers" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                    </tr>
                    <tr id="trBranchAftr">
                        <td style="width: 130px">
                            &nbsp;</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr id="trChequeNo">
                        <td style="width: 130px">
                            Cheque Number:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <asp:TextBox ID="txtChequeNumber" runat="server" Width="277px"></asp:TextBox><asp:DropDownList
                                ID="ddlChequeNumbers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChequeNumbers_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 130px">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td style="width: 263px">
                        </td>
                    </tr>
                    <tr id="trChequeDate">
                        <td style="width: 130px">
                            Cheque Date:</td>
                        <td>
                        </td>
                        <td style="width: 263px">
                            <dxe:ASPxDateEdit ID="dtpChequeDate" runat="server" EditFormat="Custom" EditFormatString="dd/MMM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%" align="center">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="voucher" OnClick="btnSave_Click"
                                OnClientClick="return ValidatePage('voucher');" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnPrintVoucher" runat="server" OnClientClick="return ShowPrintWindow()"
                                Text="Print Voucher" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnFromURL" runat="server" />
                            <asp:HiddenField ID="hdnVoucherId" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <dx:aspxpopupcontrol id="dxPrintVoucher" runat="server" allowdragging="True" allowresize="True"
                    clientinstancename="voucherPrint" closeaction="CloseButton" dragelement="Window"
                    enableclientsideapi="True" headertext="Print Voucher" height="650px" modal="True"
                    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter" width="950px"><ContentCollection>
<dx:PopupControlContentControl runat="server"></dx:PopupControlContentControl>
</ContentCollection>
</dx:aspxpopupcontrol>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
    
    OnSelectedIndexChange();
    function ShowPrintWindow() {
        voucherPrint.Show();
        return false;
    }
    
    OnVoucherTypeSelectChange();
    ChangePaymentType();
    function OnVoucherTypeSelectChange()
    {
        var dropDown1 = document.getElementById("ctl00_ContentPlaceHolder1_ddlVoucherType");
        var trPOHeader1 = document.getElementById("trPOHeader");
        var trSelectMode1 = document.getElementById("trSelectionMode");
        var trPOGrid1 = document.getElementById("trPOGrid");
        var trSupp1 = document.getElementById("trSupplier");
        var trCustomer1 = document.getElementById("trCustomer");
        var trAccountType = document.getElementById("trAccountType");

        if(dropDown1.value=='1')//purchase order
        { 
            $('#trSelectionMode').show('slow', '');
            $(trSupp1).hide('slow', '');
            $(trCustomer1).hide('slow', '');
            $(trPOGrid1).show('slow', '');
            $(trPOHeader1).show('slow', '');
            $(trAccountType).hide('slow', '');
            return true;
//            trPOHeader.style.display = 'block'; 
//            trPOGrid.style.display = 'block'; 
//            trSelectionMode.style.display = 'block';
//            trSupp.style.display = 'none';
//            trCustomer.style.display = 'none';
        }
        else if(dropDown1.value=='2')//Supplier
        {
            $(trSupp1).show('slow', '');
            $(trCustomer1).hide('slow', '');
            $(trPOHeader1).hide('slow', '');
            $(trPOGrid1).hide('slow', '');
            $('#trSelectionMode').hide('slow', '');
            $(trAccountType).hide('slow', '');
            return true;
//            trSupp.style.display = 'block';
//            trPOHeader.style.display = 'none'; 
//            trPOGrid.style.display = 'none'; 
//            trSelectionMode.style.display = 'none'; 
//            trCustomer.style.display = 'none';
        }
        else if(dropDown1.value=='3')//Customer
        {
            $(trCustomer1).show('slow', '');
            $(trSupp1).hide('slow', '');
            $(trPOHeader1).hide('slow', '');
            $(trPOGrid1).hide('slow', '');
            $('#trSelectionMode').hide('slow', '');
            $(trAccountType).hide('slow', '');
            return true;  
//            trSupp.style.display = 'none';
//            trPOHeader.style.display = 'none'; 
//            trPOGrid.style.display = 'none'; 
//            trSelectionMode.style.display = 'none'; 
//            trCustomer.style.display = 'block';
        }
        else if(dropDown1.value=='0')//Other
        {
            $(trPOHeader1).hide('slow', '');
            $(trPOGrid1).hide('slow', '');
            $(trSupp1).hide('slow', '');
            $(trCustomer1).hide('slow', '');
            $('#trSelectionMode').hide('slow', '');
            $(trAccountType).show('slow', '');
            return true;
        }
        else
        {
            $(trPOHeader1).hide('slow', '');
            $(trPOGrid1).hide('slow', '');
            $(trSupp1).hide('slow', '');
            $(trCustomer1).hide('slow', '');
            $('#trSelectionMode').hide('slow', '');
            $(trAccountType).hide('slow', '');
            return true;
        }
    }
    
    function ChangePaymentType()
    {
        var trBankName1      = document.getElementById("trBankName");
        var trBankNameAftr1  = document.getElementById("trBankNameAftr");
        var trChequeNo1      = document.getElementById("trChequeNo");
        var trBranch1        = document.getElementById("trBranch");
        var trChequeDate1    = document.getElementById("trChequeDate");
        var ddlPaymentType  = document.getElementById("ctl00_ContentPlaceHolder1_ddlPaymentType");
        
        if(ddlPaymentType.value==2 || ddlPaymentType.value==3)
        {
            $(trBankName1).show('slow', '');
            $(trBankNameAftr1).show('slow', '');
            $(trChequeNo1).show('slow', '');
            $(trBranch1).show('slow', '');
            $(trChequeDate1).show('slow', '');
           
            
//            trBankName.style.display        = 'block';
//            trBankNameAftr.style.display    = 'block'; 
//            trChequeNo.style.display        = 'block'; 
//            trBranch.style.display          = 'block'; 
//            trChequeDate.style.display      = 'block'; 
        }
        else
        {
            $(trBankName1).hide('slow', '');
            $(trBankNameAftr1).hide('slow', '');
            $(trChequeNo1).hide('slow', '');
            $(trBranch1).hide('slow', '');
            $(trChequeDate1).hide('slow', '');
        }
    }

    function ValidateSupplier(source, arguments) 
    { 
        var ddlVoucher = document.getElementById("ctl00_ContentPlaceHolder1_ddlVoucherType");
        
        if (ddlVoucher.value=='2')
        { 
            if(arguments.Value=='-1')//Default Value
            {
                arguments.IsValid = false;
            }
        }
        else
        {
            arguments.IsValid = true;
        }	
         
    }
    
    function ValidateCustomer(source, arguments) 
    { 
        var ddlVoucher = document.getElementById("ctl00_ContentPlaceHolder1_ddlVoucherType");
        
        if (ddlVoucher.value=='3')
        { 
            if(arguments.Value=='-1')//Default Value
            {
                arguments.IsValid = false;
            }
        }
        else
        {
            arguments.IsValid = true;
        }	
         
    }
    
    function OtherVoucherTypes(source, arguments)
    {
        var ddlVoucher = document.getElementById("ctl00_ContentPlaceHolder1_ddlVoucherType");
        if (ddlVoucher.value=='0')
        { 
            if(arguments.Value=='-1')//Default Value
            {
                arguments.IsValid = false;
            }
        }
        else
        {
            arguments.IsValid = true;
        }
    }
    
    </script>

</asp:Content>
