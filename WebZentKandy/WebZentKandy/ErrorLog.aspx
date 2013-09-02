<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorLog.aspx.cs" Inherits="ErrorLog" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Log</title>
</head>
<body>
    <form id="form1" runat="server">
        <div title="Error Log">
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="LogId" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="LogId" ReadOnly="True" VisibleIndex="0" SortIndex="0" SortOrder="Descending" UnboundType="Integer">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="CreatedDate" VisibleIndex="1" UnboundType="DateTime">
                        <Settings GroupInterval="DateMonth" />
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="User" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClassName" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FunctionName" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Message1" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Message2" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager NumericButtonCount="15" PageSize="50">
                </SettingsPager>
                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                    EnableRowHotTrack="True" />
                <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="LogId" ShowInColumn="Log Id" ShowInGroupFooterColumn="Log Id"
                        SummaryType="Count" />
                </GroupSummary>
                <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" />
            </dx:ASPxGridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LankaTiles %>"
                SelectCommand="SELECT [LogId], [CreatedDate], [User], [ClassName], [FunctionName], [Message1], [Message2] FROM [tblLogMessages]">
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
