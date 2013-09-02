<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPO.aspx.cs" Inherits="PrintPO" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" src="JS/Dialogs.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Print PO</title>
</head>
<body>
    <form id="Form2" runat="server">
        <table class="form" style="width: 100%" align="center">
            <tr>
                <td style="text-align: center">
                    <span style="font-size: 36pt; text-decoration: underline">Purchase Order</span></td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                PO Code:</td>
                            <td align="left" style="width: 164px" valign="bottom">
                                <asp:Label ID="lblPOCode" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
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
                                Supplier:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:Label ID="lblSupplier" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                PO Ammount:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblPOAmount" runat="server"></asp:Label>
                                (Rs.)</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 150px" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px" valign="bottom">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left" style="width: 86px" valign="top">
                                Total Paid:</td>
                            <td align="left" style="width: 150px" valign="top">
                                <asp:Label ID="lblTotalPaid" runat="server"></asp:Label>(Rs.)</td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px" valign="top">
                                Balance Payment:</td>
                            <td align="left" style="width: 140px" valign="top">
                                &nbsp;<asp:Label ID="lblBalancePayment" runat="server"></asp:Label>
                                &nbsp;(Rs.)</td>
                        </tr>
                        <tr style="display: none">
                            <td align="left" style="width: 86px; height: 20px;" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 164px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 13px; height: 20px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 20px;" valign="bottom">
                                &nbsp;</td>
                            <td align="left" style="width: 130px; height: 20px;" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 86px; height: 17px;" valign="bottom">
                                Requested By:</td>
                            <td align="left" style="width: 164px; height: 17px;" valign="bottom">
                                <asp:Label ID="lblRequestedBy" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px; height: 17px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 129px; height: 17px;" valign="bottom">
                            </td>
                            <td align="left" style="width: 130px; height: 17px;" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center; font-weight: bold; text-decoration: underline;">
                    PO Items</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                        CellPadding="0" CellSpacing="1" GridLines="None" Width="100%" DataKeyNames="ItemId">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                            <asp:BoundField DataField="POItemCost" HeaderText="Unit Cost" DataFormatString="{0:F2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Discount(%)" DataField="DiscountPerUnit">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Qty" HeaderText="Qty">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LineCost" HeaderText="Line Cost" DataFormatString="{0:F2}">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="TrButtonPrint" runat="server">
                <td style="text-align: left">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="PrintSurvey()"
                        UseSubmitBehavior="False" /></td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnPOId" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>..................................
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>Authorized By
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
