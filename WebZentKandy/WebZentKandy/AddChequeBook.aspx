<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AddChequeBook.aspx.cs"
    Inherits="AddChequeBook" Title="Create Cheque Book" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/Main.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="form" align="center" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td align="center">
                <p class="details_header">
                    Create Cheque Book</p>
            </td>
        </tr>
        <tr>
            <td class="Content2">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            No Of Cheques:</td>
                        <td>
                            <dx:ASPxSpinEdit ID="seNoOfChqs" runat="server" Height="21px" Number="0" NumberType="Integer"
                                MaxValue="100">
                                <ValidationSettings ValidationGroup="vgChqBook">
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                        <td>
                            First Cheque No:</td>
                        <td>
                            <dx:ASPxTextBox ID="txtFirstChqNo" runat="server" Width="170px">
                                <ValidationSettings Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgChqBook">
                                    <RegularExpression ValidationExpression="^[0-9]+$" />
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bank Name:</td>
                        <td>
                            <asp:DropDownList ID="ddlBankName" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>HSBC</asp:ListItem>
                                <asp:ListItem>HNB</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            Last Cheque No:</td>
                        <td>
                            <dx:ASPxTextBox ID="txtLastChqNo" runat="server" Width="170px" ReadOnly="True">
                                <ValidationSettings Display="Dynamic" ErrorTextPosition="Bottom">
                                    <RegularExpression ValidationExpression="^[0-9]+$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="ddlBankName"
                                Display="Dynamic" ErrorMessage="Bank Name is required" ValidationGroup="vgChqBook"></asp:RequiredFieldValidator></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Bank Branch:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranchLocation" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Nugegoda</asp:ListItem>
                                <asp:ListItem>Nawala</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            Cheque Book No:</td>
                        <td>
                            <asp:Label ID="lblChequeBookId" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvBankBranch" runat="server" ControlToValidate="ddlBranchLocation"
                                Display="Dynamic" ErrorMessage="Branch Name is required" ValidationGroup="vgChqBook"></asp:RequiredFieldValidator></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnCreateCheques" runat="server" Text="Generate Cheque Book" ValidationGroup="vgChqBook"
                                OnClick="btnCreateCheques_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="vgSave">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="report_header">
                Cheque Details
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="dxgvChequeDetails" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="ChqId" Width="100%" OnRowUpdating="dxgvChequeDetails_RowUpdating">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="ChequeNo" ShowInCustomizationForm="False" UnboundType="String"
                            VisibleIndex="1">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Amount" ShowInCustomizationForm="False" UnboundType="Decimal"
                            VisibleIndex="2">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Comment" ShowInCustomizationForm="False" UnboundType="String"
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Status" FieldName="StatusName" UnboundType="String"
                            VisibleIndex="4">
                            <PropertiesComboBox ValueType="System.String">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataDateColumn FieldName="WrittenDate" ShowInCustomizationForm="False"
                            UnboundType="DateTime" VisibleIndex="5">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="ChqDate" ShowInCustomizationForm="False" UnboundType="DateTime"
                            VisibleIndex="6">
                            <PropertiesDateEdit DisplayFormatString="">
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="WrittenBy" ShowInCustomizationForm="False"
                            UnboundType="String" VisibleIndex="7" Visible="False">
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn VisibleIndex="0" Width="50px" Caption="Edit">
                            <EditButton Visible="True">
                            </EditButton>
                            <UpdateButton Visible="True">
                            </UpdateButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <SettingsBehavior AutoExpandAllGroups="True" ColumnResizeMode="Control" ConfirmDelete="True"
                        EnableRowHotTrack="True" />
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways"
                        ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="Total = {0}" FieldName="Amount" ShowInColumn="Amount"
                            ShowInGroupFooterColumn="Amount" SummaryType="Sum" />
                    </TotalSummary>
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnChequeBook" runat="server" Value="0" />
                <input id="hdnRowCount" runat="server" style="display:none" type="text" />
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="hdnRowCount"
                    Display="Dynamic" ErrorMessage="There should be atleast one cheque in the cheque book"
                    Font-Bold="True" MinimumValue="0" Type="Integer" ValidationGroup="vgSave" MaximumValue="101"></asp:RangeValidator>
            </td>
        </tr>
    </table>
</asp:Content>
