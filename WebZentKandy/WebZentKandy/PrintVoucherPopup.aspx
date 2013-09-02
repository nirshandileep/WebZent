<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintVoucherPopup.aspx.cs"
    Inherits="PrintVoucherPopup" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" src="JS/jQueryLib.js" type="text/javascript"></script>

<script language="javascript" src="JS/Dialogs.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Voucher Print</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="form" align="center">
                <tr>
                    <td style="text-align: center">
                        <span style="font-size: 36pt; text-decoration: underline">Voucher</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%" align="center">
                            <tr>
                                <td style="width: 150px">
                                    Voucher Code:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblVoucherCode" runat="server" Font-Bold="True"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    </td>
                                <td width="10">
                                </td>
                                <td>
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="display:none">
                    <td>
                        <table style="width: 100%" align="center">
                            <tr>
                                <td style="width: 150px">
                                    Voucher Type:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlVoucherType" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                            <asp:ListItem Value="0">Other</asp:ListItem>
                                            <asp:ListItem Value="1">Purchase Order</asp:ListItem>
                                            <asp:ListItem Value="2">Suppliers</asp:ListItem>
                                            <asp:ListItem Value="3">Customers</asp:ListItem>
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                    </td>
                                <td colspan="1" width="10">
                                </td>
                                <td colspan="1" style="width: 263px">
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trAccountType">
                    <td>
                        <table style="width: 100%" align="center">
                            <tr>
                                <td style="width: 150px">
                                    Account:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlAccountTypes" runat="server">
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                        </table>
                        </td>
                </tr>
                <tr id="trSupplier">
                    <td>
                        <table style="width: 100%" align="center">
                            <tr>
                                <td style="width: 150px">
                                    Supplier:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblSupplier" runat="server"></asp:Label>
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True">
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                        </table>
                        </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%" align="center" id="trCustomer">
                            <tr>
                                <td style="width: 150px">
                                    Customer:</td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlCustomer" runat="server">
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trSelectionMode" style="display: none">
                    <td>
                        </td>
                </tr>
                <tr id="trPOHeader" style="display: none">
                    <td align="center" class="report_header">
                        <strong>Purchase Orders</strong></td>
                </tr>
                <tr id="trPOGrid">
                    <td class="Content2">
                        <dxwgv:ASPxGridView ID="dxgvPODetails" runat="server" Width="100%" AutoGenerateColumns="False"
                            ClientInstanceName="grid" KeyFieldName="GRNId">
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="PO Code" FieldName="POCode" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="PO Amount" FieldName="POAmount" UnboundType="Decimal"
                                    VisibleIndex="3" ShowInCustomizationForm="False" ReadOnly="True">
                                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:F2}">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Balance Amount" FieldName="BalanceAmount"
                                    UnboundType="Decimal" VisibleIndex="4" ShowInCustomizationForm="False" ReadOnly="True"
                                    Visible="False">
                                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:F2}">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="SupplierName" FieldName="SupplierName" UnboundType="String"
                                    VisibleIndex="7" ShowInCustomizationForm="False" ReadOnly="True">
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="SuplierInvNo" VisibleIndex="5" ShowInCustomizationForm="False"
                                    ReadOnly="True">
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="GRNTotal" UnboundType="Decimal" VisibleIndex="6"
                                    Caption="Paid Ammount">
                                    <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:F2}">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="GRN Number" FieldName="GRNId" UnboundType="Integer"
                                    VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="POId" UnboundType="Integer"
                                    VisibleIndex="1" ReadOnly="True" ShowInCustomizationForm="False">
                                    <EditFormSettings Visible="False" />
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem DisplayFormat="Total Ordered = {0:F2}" FieldName="BalanceAmount"
                                    ShowInColumn="Balance Amount" ShowInGroupFooterColumn="Balance Amount" SummaryType="Sum" />
                                <dxwgv:ASPxSummaryItem DisplayFormat="Total Received = {0:F2}" FieldName="GRNTotal"
                                    ShowInColumn="GRN Due" ShowInGroupFooterColumn="GRN Due" SummaryType="Sum" />
                            </TotalSummary>
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
                                <td style="width: 150px">
                                    Ammount:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblAmmount" runat="server"></asp:Label></td>
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
                                <td style="width: 150px">
                                    Payment Description:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblDescription" runat="server"></asp:Label></td>
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
                                <td style="width: 150px">
                                    Voucher Payment Date:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
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
                                <td style="width: 150px">
                                    Payment Type:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblPaymentType" runat="server"></asp:Label>
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlPaymentType" runat="server" onchange="ChangePaymentType()">
                                            <asp:ListItem Value="-1">--Please Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                            <asp:ListItem Value="2">Cheque</asp:ListItem>
                                            <asp:ListItem Value="3">Credit Card</asp:ListItem>
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
                                </td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                </td>
                            </tr>
                            <tr id="trBankName">
                                <td style="width: 150px">
                                    Bank Name:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                    <div style="display: none">
                                        <asp:DropDownList ID="ddlBankName" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>HSBC</asp:ListItem>
                                            <asp:ListItem>HNB</asp:ListItem>
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                            <tr id="trBankNameAftr">
                                <td style="width: 130px">
                                </td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                </td>
                            </tr>
                            <tr id="trBranch">
                                <td style="width: 130px">
                                    Branch Location:</td>
                                <td width="10">
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblBranch" runat="server"></asp:Label><div style="display: none">
                                        <asp:DropDownList ID="ddlBranchLocation" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Nugegoda</asp:ListItem>
                                            <asp:ListItem>Nawala</asp:ListItem>
                                        </asp:DropDownList></div>
                                </td>
                            </tr>
                            <tr id="trBranchAftr">
                                <td style="width: 130px">
                                </td>
                                <td>
                                </td>
                                <td style="width: 263px">
                                </td>
                            </tr>
                            <tr id="trChequeNo">
                                <td style="width: 130px">
                                    Cheque/Card Number:</td>
                                <td>
                                </td>
                                <td style="width: 263px">
                                    <asp:Label ID="lblChequeNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 130px">
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
                                    <asp:Label ID="lblChequeDate" runat="server"></asp:Label></td>
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
                        <table style="width: 100%" align="center">
                            <tr id="TrButtonPrint" runat="server">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintSurvey()" Text="Print"
                                        UseSubmitBehavior="False" /></td>
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
                        <table width="100%">
                            <tr>
                                <td>
                                    ................................</td>
                                <td>
                                    ................................</td>
                            </tr>
                            <tr>
                                <td>
                                    Authorized By</td>
                                <td>
                                    Received By</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>

            <script language="javascript" type="text/javascript">
    OnVoucherTypeSelectChange();
    ChangePaymentType();
    function OnVoucherTypeSelectChange()
    {
        var dropDown1 = document.getElementById("ddlVoucherType");
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
        var ddlPaymentType  = document.getElementById("ddlPaymentType");

        if(ddlPaymentType.value==2 || ddlPaymentType.value==3)
        {
            $(trBankName1).show('slow', '');
            $(trBankNameAftr1).show('slow', '');
            $(trChequeNo1).show('slow', '');
            $(trBranch1).show('slow', '');
            $(trChequeDate1).show('slow', '');
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
        var ddlVoucher = document.getElementById("ddlVoucherType");
        
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
        var ddlVoucher = document.getElementById("ddlVoucherType");
        
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
        var ddlVoucher = document.getElementById("ddlVoucherType");
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

        </div>
    </form>
</body>
</html>
