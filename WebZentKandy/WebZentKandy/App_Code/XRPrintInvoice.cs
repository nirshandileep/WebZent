using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using LankaTiles.InvoiceManagement;
using LankaTiles.CustomerManagement;
using LankaTiles.UserManagement;
using LankaTiles.Common;

/// <summary>
/// Summary description for XRPrintInvoice
/// </summary>
public class XRPrintInvoice : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private PageHeaderBand PageHeader;
    private XRTableCell xrTableCell9;
    private XRTableCell xrTableCell8;
    protected ReportHeaderBand ReportHeader;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell10;
    public DevExpress.XtraReports.Parameters.Parameter InvoiceId;
    public DevExpress.XtraReports.Parameters.Parameter InvoiceNumber;
    public DevExpress.XtraReports.Parameters.Parameter InvDate;
    public DevExpress.XtraReports.Parameters.Parameter CustomerCode;
    public DevExpress.XtraReports.Parameters.Parameter CustomerName;
    public DevExpress.XtraReports.Parameters.Parameter GrandTotal;
    public DevExpress.XtraReports.Parameters.Parameter AmmountDue;
    private XRLabel xrLabel11;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    public DevExpress.XtraReports.Parameters.Parameter PaymentType;
    private XRLabel xrLabel5;
    public XRLabel xrLabel4;
    public DevExpress.XtraReports.Parameters.Parameter DueAmmount;
    protected ReportFooterBand ReportFooter;
    public DevExpress.XtraReports.Parameters.Parameter TotalPaid;
    private XRControlStyle xrControlStyle1;
    private XRTableCell xrTableCell12;
    private DsPrintInvoice dsPrintInvoice1;
    private XRLabel xrLabel3;
    private XRLabel xrLabel14;
    private XRLabel xrLabel17;
    public XRLabel xrLabel12;
    private XRLabel xrLabel2;
    public DevExpress.XtraReports.Parameters.Parameter SPCode;
    public DevExpress.XtraReports.Parameters.Parameter SPName;
    private XRLabel xrLabel8;
    protected PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel1;
    private XRLabel lblNote;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRLabel lblDuplicate;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XRPrintInvoice()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    public XRPrintInvoice(Int32 InvNo)
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //

        this.FillParameters(InvNo);
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        string resourceFileName = "XRPrintInvoice.resx";
        DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
        this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
        this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
        this.GrandTotal = new DevExpress.XtraReports.Parameters.Parameter();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
        this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
        this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
        this.lblDuplicate = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
        this.SPName = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
        this.CustomerName = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
        this.CustomerCode = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
        this.PaymentType = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
        this.InvDate = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
        this.InvoiceNumber = new DevExpress.XtraReports.Parameters.Parameter();
        this.InvoiceId = new DevExpress.XtraReports.Parameters.Parameter();
        this.AmmountDue = new DevExpress.XtraReports.Parameters.Parameter();
        this.DueAmmount = new DevExpress.XtraReports.Parameters.Parameter();
        this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
        this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
        this.TotalPaid = new DevExpress.XtraReports.Parameters.Parameter();
        this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
        this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
        this.dsPrintInvoice1 = new DsPrintInvoice();
        this.SPCode = new DevExpress.XtraReports.Parameters.Parameter();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        this.lblNote = new DevExpress.XtraReports.UI.XRLabel();
        this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
        this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
        this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsPrintInvoice1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
        this.Detail.Dpi = 254F;
        this.Detail.HeightF = 61F;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // xrTable2
        // 
        this.xrTable2.BorderColor = System.Drawing.Color.Transparent;
        this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTable2.Dpi = 254F;
        this.xrTable2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
        this.xrTable2.Name = "xrTable2";
        this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
        this.xrTable2.SizeF = new System.Drawing.SizeF(2238F, 61F);
        this.xrTable2.StylePriority.UseBorderColor = false;
        this.xrTable2.StylePriority.UseBorders = false;
        this.xrTable2.StylePriority.UseFont = false;
        // 
        // xrTableRow2
        // 
        this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell10,
            this.xrTableCell12});
        this.xrTableRow2.Dpi = 254F;
        this.xrTableRow2.Name = "xrTableRow2";
        this.xrTableRow2.Weight = 1;
        // 
        // xrTableCell4
        // 
        this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Detail_Sel_Report.ItemCode")});
        this.xrTableCell4.Dpi = 254F;
        this.xrTableCell4.Font = new System.Drawing.Font("Times New Roman", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell4.Name = "xrTableCell4";
        this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell4.StylePriority.UseFont = false;
        this.xrTableCell4.StylePriority.UseTextAlignment = false;
        this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        this.xrTableCell4.Weight = 0.083109919571045576;
        // 
        // xrTableCell5
        // 
        this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Detail_Sel_Report.ItemDescription")});
        this.xrTableCell5.Dpi = 254F;
        this.xrTableCell5.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell5.Name = "xrTableCell5";
        this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell5.StylePriority.UseFont = false;
        this.xrTableCell5.StylePriority.UseTextAlignment = false;
        this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        this.xrTableCell5.Weight = 0.57327971403038425;
        // 
        // xrTableCell6
        // 
        this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Detail_Sel_Report.Quantity")});
        this.xrTableCell6.Dpi = 254F;
        this.xrTableCell6.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell6.Name = "xrTableCell6";
        this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell6.StylePriority.UseFont = false;
        this.xrTableCell6.StylePriority.UseTextAlignment = false;
        this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        this.xrTableCell6.Weight = 0.055406613047363718;
        // 
        // xrTableCell10
        // 
        this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Detail_Sel_Report.Price", "{0:0.00}")});
        this.xrTableCell10.Dpi = 254F;
        this.xrTableCell10.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell10.Name = "xrTableCell10";
        this.xrTableCell10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell10.StylePriority.UseFont = false;
        this.xrTableCell10.StylePriority.UseTextAlignment = false;
        this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        this.xrTableCell10.Weight = 0.13717605004468275;
        // 
        // xrTableCell12
        // 
        this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrTableCell12.CanShrink = true;
        this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Detail_Sel_Report.TotalPrice", "{0:0.00}")});
        this.xrTableCell12.Dpi = 254F;
        this.xrTableCell12.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrTableCell12.Name = "xrTableCell12";
        this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrTableCell12.StylePriority.UseBorders = false;
        this.xrTableCell12.StylePriority.UseFont = false;
        this.xrTableCell12.StylePriority.UseTextAlignment = false;
        this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
        this.xrTableCell12.Weight = 0.15102770330652368;
        // 
        // GrandTotal
        // 
        this.GrandTotal.Name = "GrandTotal";
        this.GrandTotal.Type = typeof(decimal);
        this.GrandTotal.Value = 0;
        // 
        // PageHeader
        // 
        this.PageHeader.Dpi = 254F;
        this.PageHeader.HeightF = 0F;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageHeader.StylePriority.UseTextAlignment = false;
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
        // 
        // xrTableCell9
        // 
        this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Sel_Report.Has Line Item.Quantity")});
        this.xrTableCell9.Name = "xrTableCell9";
        this.xrTableCell9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrTableCell9.Weight = 0;
        // 
        // xrTableCell8
        // 
        this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "vw_Invoice_Sel_Report.Has Line Item.Price")});
        this.xrTableCell8.Name = "xrTableCell8";
        this.xrTableCell8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
        this.xrTableCell8.Weight = 0;
        // 
        // ReportHeader
        // 
        this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblDuplicate,
            this.xrLabel8,
            this.xrLabel11,
            this.xrLabel7,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4});
        this.ReportHeader.Dpi = 254F;
        this.ReportHeader.HeightF = 638F;
        this.ReportHeader.Name = "ReportHeader";
        // 
        // lblDuplicate
        // 
        this.lblDuplicate.BackColor = System.Drawing.Color.Transparent;
        this.lblDuplicate.Dpi = 254F;
        this.lblDuplicate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
        this.lblDuplicate.LocationFloat = new DevExpress.Utils.PointFloat(1270F, 445F);
        this.lblDuplicate.Name = "lblDuplicate";
        this.lblDuplicate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblDuplicate.SizeF = new System.Drawing.SizeF(297.9999F, 58.41995F);
        this.lblDuplicate.StylePriority.UseBackColor = false;
        this.lblDuplicate.StylePriority.UseFont = false;
        this.lblDuplicate.Text = "DUPLICATE";
        this.lblDuplicate.Visible = false;
        // 
        // xrLabel8
        // 
        this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.SPName, "Text", "")});
        this.xrLabel8.Dpi = 254F;
        this.xrLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(1270F, 339F);
        this.xrLabel8.Name = "xrLabel8";
        this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel8.SizeF = new System.Drawing.SizeF(254F, 64F);
        this.xrLabel8.StylePriority.UseFont = false;
        this.xrLabel8.Text = "xrLabel8";
        // 
        // SPName
        // 
        this.SPName.Name = "SPName";
        this.SPName.Value = "";
        // 
        // xrLabel11
        // 
        this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.CustomerName, "Text", "")});
        this.xrLabel11.Dpi = 254F;
        this.xrLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(100F, 234F);
        this.xrLabel11.Multiline = true;
        this.xrLabel11.Name = "xrLabel11";
        this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel11.SizeF = new System.Drawing.SizeF(339F, 64F);
        this.xrLabel11.StylePriority.UseFont = false;
        this.xrLabel11.Text = "xrLabel11";
        // 
        // CustomerName
        // 
        this.CustomerName.Name = "CustomerName";
        this.CustomerName.Value = "";
        // 
        // xrLabel7
        // 
        this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.CustomerCode, "Text", "")});
        this.xrLabel7.Dpi = 254F;
        this.xrLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(100F, 444F);
        this.xrLabel7.Name = "xrLabel7";
        this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel7.SizeF = new System.Drawing.SizeF(254F, 64F);
        this.xrLabel7.StylePriority.UseFont = false;
        this.xrLabel7.Text = "xrLabel7";
        // 
        // CustomerCode
        // 
        this.CustomerCode.Name = "CustomerCode";
        this.CustomerCode.Value = "";
        // 
        // xrLabel6
        // 
        this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.PaymentType, "Text", "")});
        this.xrLabel6.Dpi = 254F;
        this.xrLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(1935F, 445F);
        this.xrLabel6.Name = "xrLabel6";
        this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel6.SizeF = new System.Drawing.SizeF(254F, 64F);
        this.xrLabel6.StylePriority.UseFont = false;
        this.xrLabel6.Text = "xrLabel6";
        // 
        // PaymentType
        // 
        this.PaymentType.Name = "PaymentType";
        this.PaymentType.Value = "";
        // 
        // xrLabel5
        // 
        this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.InvDate, "Text", "{0:dd-MMM-yyyy}")});
        this.xrLabel5.Dpi = 254F;
        this.xrLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1935F, 234F);
        this.xrLabel5.Name = "xrLabel5";
        this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel5.SizeF = new System.Drawing.SizeF(347F, 64F);
        this.xrLabel5.StylePriority.UseFont = false;
        xrSummary1.FormatString = "dd/MMM/yyyy{0}";
        this.xrLabel5.Summary = xrSummary1;
        this.xrLabel5.Text = "xrLabel5";
        // 
        // InvDate
        // 
        this.InvDate.Name = "InvDate";
        this.InvDate.Type = typeof(System.DateTime);
        this.InvDate.Value = new System.DateTime(((long)(0)));
        // 
        // xrLabel4
        // 
        this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.InvoiceNumber, "Text", "")});
        this.xrLabel4.Dpi = 254F;
        this.xrLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1935F, 339F);
        this.xrLabel4.Name = "xrLabel4";
        this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel4.SizeF = new System.Drawing.SizeF(361F, 64F);
        this.xrLabel4.StylePriority.UseFont = false;
        this.xrLabel4.Text = "xrLabel4";
        // 
        // InvoiceNumber
        // 
        this.InvoiceNumber.Name = "InvoiceNumber";
        this.InvoiceNumber.Value = "";
        // 
        // InvoiceId
        // 
        this.InvoiceId.Name = "InvoiceId";
        this.InvoiceId.Type = typeof(int);
        this.InvoiceId.Value = 0;
        // 
        // AmmountDue
        // 
        this.AmmountDue.Name = "AmmountDue";
        this.AmmountDue.Type = typeof(decimal);
        this.AmmountDue.Value = 0;
        // 
        // DueAmmount
        // 
        this.DueAmmount.Name = "DueAmmount";
        this.DueAmmount.Type = typeof(decimal);
        this.DueAmmount.Value = 0;
        // 
        // ReportFooter
        // 
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel3,
            this.xrLabel14,
            this.xrLabel17,
            this.xrLabel12,
            this.xrLabel2});
        this.ReportFooter.Dpi = 254F;
        this.ReportFooter.HeightF = 274F;
        this.ReportFooter.Name = "ReportFooter";
        this.ReportFooter.PrintAtBottom = true;
        // 
        // xrLabel1
        // 
        this.xrLabel1.Dpi = 254F;
        this.xrLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(1569F, 130F);
        this.xrLabel1.Name = "xrLabel1";
        this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel1.SizeF = new System.Drawing.SizeF(294F, 63F);
        this.xrLabel1.StylePriority.UseFont = false;
        this.xrLabel1.Text = "Amount";
        // 
        // xrLabel3
        // 
        this.xrLabel3.Dpi = 254F;
        this.xrLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(1469F, 66F);
        this.xrLabel3.Name = "xrLabel3";
        this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel3.SizeF = new System.Drawing.SizeF(397F, 58F);
        this.xrLabel3.StylePriority.UseFont = false;
        this.xrLabel3.Text = "Recieved Amount";
        // 
        // xrLabel14
        // 
        this.xrLabel14.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.DueAmmount, "Text", "")});
        this.xrLabel14.Dpi = 254F;
        this.xrLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel14.ForeColor = System.Drawing.Color.Maroon;
        this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(1900F, 133F);
        this.xrLabel14.Name = "xrLabel14";
        this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel14.SizeF = new System.Drawing.SizeF(346F, 64F);
        this.xrLabel14.StylePriority.UseFont = false;
        this.xrLabel14.StylePriority.UseForeColor = false;
        this.xrLabel14.StylePriority.UseTextAlignment = false;
        this.xrLabel14.Text = "xrLabel14";
        this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel17
        // 
        this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.TotalPaid, "Text", "")});
        this.xrLabel17.Dpi = 254F;
        this.xrLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(1900F, 67F);
        this.xrLabel17.Name = "xrLabel17";
        this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel17.SizeF = new System.Drawing.SizeF(346F, 64F);
        this.xrLabel17.StylePriority.UseFont = false;
        this.xrLabel17.StylePriority.UseTextAlignment = false;
        this.xrLabel17.Text = "xrLabel17";
        this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
        // 
        // TotalPaid
        // 
        this.TotalPaid.Name = "TotalPaid";
        this.TotalPaid.Type = typeof(decimal);
        this.TotalPaid.Value = 0;
        // 
        // xrLabel12
        // 
        this.xrLabel12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.GrandTotal, "Text", "")});
        this.xrLabel12.Dpi = 254F;
        this.xrLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(1900F, 210F);
        this.xrLabel12.Name = "xrLabel12";
        this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel12.SizeF = new System.Drawing.SizeF(346F, 64F);
        this.xrLabel12.StylePriority.UseFont = false;
        this.xrLabel12.StylePriority.UseTextAlignment = false;
        this.xrLabel12.Text = "xrLabel12";
        this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
        // 
        // xrLabel2
        // 
        this.xrLabel2.Dpi = 254F;
        this.xrLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(1468F, 130F);
        this.xrLabel2.Name = "xrLabel2";
        this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 63F);
        this.xrLabel2.StylePriority.UseFont = false;
        this.xrLabel2.Text = "Due";
        // 
        // xrControlStyle1
        // 
        this.xrControlStyle1.BorderColor = System.Drawing.SystemColors.Desktop;
        this.xrControlStyle1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                    | DevExpress.XtraPrinting.BorderSide.Right)
                    | DevExpress.XtraPrinting.BorderSide.Bottom)));
        this.xrControlStyle1.Name = "xrControlStyle1";
        // 
        // dsPrintInvoice1
        // 
        this.dsPrintInvoice1.DataSetName = "DsPrintInvoice";
        this.dsPrintInvoice1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        // 
        // SPCode
        // 
        this.SPCode.Name = "SPCode";
        this.SPCode.Value = "";
        // 
        // PageFooter
        // 
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNote,
            this.xrPageInfo1});
        this.PageFooter.Dpi = 254F;
        this.PageFooter.HeightF = 65.00002F;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // lblNote
        // 
        this.lblNote.Dpi = 254F;
        this.lblNote.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
        this.lblNote.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5.579924F);
        this.lblNote.Name = "lblNote";
        this.lblNote.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.lblNote.SizeF = new System.Drawing.SizeF(1246.188F, 58.42F);
        this.lblNote.StylePriority.UseFont = false;
        this.lblNote.Text = "Cash returns are not accepted. This invoice should be submitted for any returns.";
        // 
        // xrPageInfo1
        // 
        this.xrPageInfo1.Dpi = 254F;
        this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 11F);
        this.xrPageInfo1.Format = "Page {0} of {1}";
        this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1989F, 0F);
        this.xrPageInfo1.Name = "xrPageInfo1";
        this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
        this.xrPageInfo1.SizeF = new System.Drawing.SizeF(254F, 64F);
        this.xrPageInfo1.StylePriority.UseFont = false;
        // 
        // topMarginBand1
        // 
        this.topMarginBand1.Dpi = 254F;
        this.topMarginBand1.HeightF = 5F;
        this.topMarginBand1.Name = "topMarginBand1";
        // 
        // bottomMarginBand1
        // 
        this.bottomMarginBand1.Dpi = 254F;
        this.bottomMarginBand1.HeightF = 5F;
        this.bottomMarginBand1.Name = "bottomMarginBand1";
        // 
        // XRPrintInvoice
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.ReportHeader,
            this.ReportFooter,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
        this.DataMember = "vw_Invoice_Detail_Sel_Report";
        this.DataSource = this.dsPrintInvoice1;
        this.Dpi = 254F;
        this.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
        this.PageHeight = 2000;
        this.PageWidth = 2317;
        this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
        this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.InvoiceId,
            this.InvoiceNumber,
            this.InvDate,
            this.CustomerCode,
            this.CustomerName,
            this.GrandTotal,
            this.AmmountDue,
            this.PaymentType,
            this.DueAmmount,
            this.TotalPaid,
            this.SPCode,
            this.SPName});
        this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1});
        this.Version = "11.1";
        ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dsPrintInvoice1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    /// <summary>
    /// This method is used to fill the parameter values
    /// </summary>
    /// <param name="invoiceId"></param>
    public void FillParameters(Int32 invoiceId)
    {
        try
        {
            this.InvoiceId.Value = invoiceId;
            DsPrintInvoiceTableAdapters.vw_Invoice_Detail_Sel_ReportTableAdapter tableAdopter = new DsPrintInvoiceTableAdapters.vw_Invoice_Detail_Sel_ReportTableAdapter();
            tableAdopter.Fill(this.dsPrintInvoice1.vw_Invoice_Detail_Sel_Report, invoiceId);

            Invoice invoice = new Invoice();
            invoice.InvoiceId = invoiceId;
            invoice.GetInvoiceByInvoiceID();

            this.InvoiceNumber.Value = invoice.InvoiceNo;

            this.InvDate.Value = invoice.Date.ToShortDateString();
            switch (invoice.PaymentType)
            {
                case "1":
                    this.PaymentType.Value = "Cash";
                    break;
                case "2":
                    this.PaymentType.Value = "Cheque";
                    break;
                case "3":
                    this.PaymentType.Value = "Credit Card";
                    break;
                default:
                    this.PaymentType.Value = "N/A";
                    break;
            }

            if (invoice.Status != LankaTiles.Common.Structures.InvoiceStatus.Created)
            {
                lblDuplicate.Visible = true;
            }

            Customer customer = new Customer();
            customer.CustomerID = (Int32)invoice.CustomerID;
            customer.GetCustomerByID();

            this.CustomerCode.Value = customer.CustomerCode.Trim();
            this.CustomerName.Value = customer.Cus_Name.Trim();
            this.GrandTotal.Value = Decimal.Round(invoice.GrandTotal, 2);
            this.TotalPaid.Value = Decimal.Round(invoice.GrandTotal - invoice.DueAmount, 2);
            this.DueAmmount.Value = Decimal.Round(invoice.DueAmount, 2);

            User user = new User();
            user.UserId = invoice.CreatedUser;
            user.GetUserByID();

            this.SPName.Value = user.FirstName.Trim();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
