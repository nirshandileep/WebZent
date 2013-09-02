<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.XtraCharts.v11.1.Web, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>
<%@ Register Assembly="DevExpress.XtraCharts.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v11.1, Version=11.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ceramic Homes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>1
        </div>
        <div>
            <dx:ASPxPivotGrid ID="pgSalesSummary" runat="server" EnableCallBacks="False" Width="100%">
                <Fields>
                    <dx:PivotGridField ID="fieldDate" Area="ColumnArea" AreaIndex="1" Caption="Month"
                        FieldName="Date" GroupInterval="DateMonth" UnboundFieldName="fieldDate">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldGrandTotal" Area="DataArea" AreaIndex="0" Caption="Grand Total"
                        FieldName="GrandTotal">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldBranchCode" Area="RowArea" AreaIndex="0" Caption="Branch"
                        FieldName="BranchCode">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldTotalProfit" Area="DataArea" AreaIndex="1" Caption="Profit"
                        FieldName="TotalProfit">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldDate1" Area="ColumnArea" AreaIndex="0" Caption="Year"
                        FieldName="Date" GroupInterval="DateYear" UnboundFieldName="fieldDate1">
                    </dx:PivotGridField>
                    <dx:PivotGridField ID="fieldUserName" Area="RowArea" AreaIndex="1" Caption="Name"
                        FieldName="UserName">
                    </dx:PivotGridField>
                </Fields>
                <OptionsChartDataSource FieldValuesProvideMode="DisplayText" ProvideColumnGrandTotals="False"
                    ProvideColumnTotals="False" ProvideDataByColumns="False" ProvideRowGrandTotals="False"
                    ProvideRowTotals="False" />
                <OptionsPager RenderMode="Lightweight" RowsPerPage="50">
                </OptionsPager>
                <OptionsView ShowHorizontalScrollBar="True" />
            </dx:ASPxPivotGrid>
            &nbsp;
        </div>
        <div>2
        </div>
        <div>3
            <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" DataSourceID="pgSalesSummary"
                Height="600px" SeriesDataMember="Series" Width="1800px">
                <legend maxhorizontalpercentage="30"></legend>
                <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values"><ViewSerializable>
<cc1:LineSeriesView></cc1:LineSeriesView>
</ViewSerializable>
<LabelSerializable>
<cc1:PointSeriesLabel LineVisible="True" ResolveOverlappingMode="Default">
<FillStyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
</cc1:PointSeriesLabel>
</LabelSerializable>
<PointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</PointOptionsSerializable>
<LegendPointOptionsSerializable>
<cc1:PointOptions></cc1:PointOptions>
</LegendPointOptionsSerializable>
</seriestemplate>
                <fillstyle><OptionsSerializable>
<cc1:SolidFillOptions></cc1:SolidFillOptions>
</OptionsSerializable>
</fillstyle>
                <diagramserializable>
<cc1:XYDiagram>
<AxisX VisibleInPanesSerializable="-1">
<Label Staggered="True"></Label>

<Range SideMarginsEnabled="True"></Range>
</AxisX>

<AxisY VisibleInPanesSerializable="-1">
<Range SideMarginsEnabled="True"></Range>
</AxisY>
</cc1:XYDiagram>
</diagramserializable>
                <borderoptions visible="False"></borderoptions>
            </dxchartsui:WebChartControl>
        </div>
    </form>
</body>
</html>
