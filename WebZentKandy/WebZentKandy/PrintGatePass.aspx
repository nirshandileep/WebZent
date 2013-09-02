<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintGatePass.aspx.cs" Inherits="PrintGatePass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gate Pass Print</title>
</head>
<body>
    <form id="form1" runat="server">

        <script language="javascript" type="text/javascript" src="JS/Dialogs.js"></script>

        <table class="form" width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="lblTitle" runat="server" ForeColor="Black" Visible="true" Font-Size="32pt" Font-Underline="True">Gate Pass</asp:Label></td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td class="Content2">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td align="left" valign="bottom">
                                Gate Pass Code:</td>
                            <td align="left" valign="bottom">
                                <asp:Label ID="lblGatepassCode" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom">
                            </td>
                            <td align="left" style="" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left"  valign="bottom">
                                &nbsp;</td>
                            <td align="left"  valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left"  valign="bottom">
                            </td>
                            <td align="left" style="" valign="bottom">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="bottom" >
                                Invoice Number:</td>
                            <td align="left" valign="bottom" >
                                <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label></td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left" valign="bottom" >
                                Invoice Ammount:</td>
                            <td align="left" style="" valign="bottom">
                                <asp:Label ID="lblInvoiceAmount" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left"  valign="bottom">
                                &nbsp;</td>
                            <td align="left"  valign="bottom">
                            </td>
                            <td align="left" style="width: 13px" valign="bottom">
                            </td>
                            <td align="left"  valign="bottom">
                            </td>
                            <td align="left" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <p>
                        </p>
                </td>
            </tr>
            <tr>
                <td class="report_header" style="font-weight: bold; text-decoration: underline" align="center">
                    Issued Items</td>
            </tr>
            <tr>
                <td align="center" class="report_header" style="font-weight: bold; text-decoration: underline">
                </td>
            </tr>
            <tr id="trGrid" runat="server">
                <td class="Content2">
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                        CellPadding="0" CellSpacing="1" Width="100%" DataKeyNames="GPId,InvDetID">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                <ControlStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty">
                                <ControlStyle Width="10px" />
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
                    <asp:HiddenField ID="hdnGatePassId" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
