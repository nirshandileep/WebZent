<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPurchaseReturns.aspx.cs"
    Inherits="PrintPurchaseReturns" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" src="JS/Dialogs.js" type="text/javascript"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Returns Print</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False" CssClass="show_error"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center">
                        <p class="details_header" style="font-size: 30pt; text-decoration: underline">
                            Purchase Returns</p>
                    </td>
                </tr>
                <tr>
                    <td class="Content2">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="left" style="width: 86px" valign="bottom">
                                    PR Code:</td>
                                <td align="left" style="width: 164px" valign="bottom">
                                    <asp:Label ID="lblPRCode" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label></td>
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
                                <td align="left" style="width: 86px" valign="bottom">
                                    GRN Number:</td>
                                <td align="left" style="width: 164px" valign="bottom">
                                    <asp:Label ID="lblGRNId" runat="server"></asp:Label></td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" style="width: 129px" valign="bottom">
                                    Date:</td>
                                <td align="left" style="width: 130px" valign="bottom">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label></td>
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
                                    PO Code:</td>
                                <td align="left" valign="bottom" style="width: 164px">
                                    <asp:Label ID="lblPOCode" runat="server"></asp:Label></td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" valign="bottom" style="width: 129px">
                                    Suppier Name:</td>
                                <td align="left" style="width: 130px" valign="bottom">
                                    <asp:Label ID="lblSupplierName" runat="server"></asp:Label></td>
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
                            <tr>
                                <td align="left" style="width: 86px" valign="top">
                                    Sup Inv No:</td>
                                <td align="left" style="width: 150px" valign="top">
                                    <asp:Label ID="lblSupInvNo" runat="server"></asp:Label></td>
                                <td align="left" style="width: 13px" valign="bottom">
                                </td>
                                <td align="left" style="width: 129px" valign="top">
                                    Total Return Value:</td>
                                <td align="left" style="width: 140px" valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblInvoiceTotal" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                (Rs.)
                                            </td>
                                        </tr>
                                    </table>
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
                                    &nbsp;</td>
                                <td align="left" style="width: 130px" valign="bottom">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="Content2">
                        <div style="text-align: left">
                            &nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnFromURL" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="report_header" style="font-weight: bold; font-size: 14pt" align="center">
                        Return Items</td>
                </tr>
                <tr id="trGrid" runat="server">
                    <td class="Content2">
                        <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                            CellPadding="4" GridLines="Horizontal" Width="100%" DataKeyNames="GRNDetailsId"
                            Font-Size="Larger" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                            ForeColor="Black">
                            <Columns>
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                                <asp:BoundField DataField="ItemDescription" HeaderText="Item Description">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Qty">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UnitCost" HeaderText="Cost Per Unit" DataFormatString="{0:F2}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCost" DataFormatString="{0:F2}" HeaderText="Total Cost">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 16px">
                        &nbsp;</td>
                </tr>
                <tr id="trSalesReturn">
                    <td style="height: 16px" class="Content2">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" style="width: 86px">
                                    <strong>Return Note:</strong></td>
                                <td>
                                    <asp:Label ID="lblReturnNote" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 86px">
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 16px">
                        &nbsp;</td>
                </tr>
                <tr id="TrButtonPrint" runat="server">
                    <td>
                        <asp:Button ID="btnPrint" runat="server" OnClientClick="PrintSurvey()" Text="Print"
                            UseSubmitBehavior="False" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnPRId" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
