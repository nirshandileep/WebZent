<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewQuotation.aspx.cs" Inherits="ViewQuotation" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" src="JS/Dialogs.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Quotation Print</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Scriptmanager1" runat="server">
        </asp:ScriptManager>
        <table class="form" style="width: 100%" align="center">
            <tr>
                <td style="text-align: center">
                    <span style="font-size: 36pt; text-decoration: underline">Quotation</span></td>
            </tr>
            <tr>
                <td class="Content2">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
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
                                Customer Name:</td>
                            <td align="left" valign="bottom" style="width: 164px">
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" style="width: 129px">
                                Date:</td>
                            <td align="left" style="width: 130px" valign="bottom">
                                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
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
                            <td align="left" style="width: 86px; height: 17px" valign="bottom">
                                Quoted By:</td>
                            <td align="left" style="width: 164px; height: 17px" valign="bottom">
                                <asp:Label ID="lblSalesPerson" runat="server"></asp:Label></td>
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
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-weight: bold">
                    Invoice Details</td>
            </tr>
            <tr id="trGrid" runat="server">
                <td class="Content2">
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                        CellPadding="0" CellSpacing="1" GridLines="Horizontal" Width="100%" DataKeyNames="ItemId,GroupId"
                        BorderColor="Black" BorderStyle="Solid" ForeColor="Black">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="Price" HeaderText="Selling Price" DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="Quantity" HeaderText="Qty" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="Line Price" DataFormatString="{0:F2}" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <strong>Total:</strong> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblInvAmount" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                    <strong>(Rs.)</strong> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr id="TrButtonPrint" runat="server">
                <td style="text-align: left">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="PrintSurvey()"
                        UseSubmitBehavior="False" /></td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnInvId" runat="server" Value="0" />
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
