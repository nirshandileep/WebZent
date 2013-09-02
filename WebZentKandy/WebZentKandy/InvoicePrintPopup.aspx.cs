using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.XtraReports.Web;
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.InvoiceManagement;
using System.Xml.Serialization;
using System.Xml;

using AjaxPro.Services;
using System.IO;

public partial class InvoicePrintPopup : System.Web.UI.Page
{
    private XRPrintInvoice report;

    public XRPrintInvoice Report
    {
        get
        {
            if (report == null)
            {
                report = new XRPrintInvoice();
            }
            return report;
        }
        set
        {

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            AjaxPro.Utility.RegisterTypeForAjax(typeof(InvoicePrintPopup));
            if (!IsPostBack)
            {
                this.CheckFromURL();
                this.GetInvoiceId();
            }

            DsPrintInvoice dataset = new DsPrintInvoice();
            DsPrintInvoiceTableAdapters.vw_Invoice_Detail_Sel_ReportTableAdapter dai = new DsPrintInvoiceTableAdapters.vw_Invoice_Detail_Sel_ReportTableAdapter();
            dai.Fill(dataset.vw_Invoice_Detail_Sel_Report, Int32.Parse(hdnInvoiceId.Value.Trim()));
            Report.FillParameters(Int32.Parse(hdnInvoiceId.Value.Trim()));
            Report.DataSource = dataset;
            Report.CreateDocument();

            if (report != null)
            {
                ReportViewer1.Report = Report;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + LankaTiles.Common.Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, LankaTiles.Common.Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    [AjaxPro.AjaxMethod]
    public void UpdateInvoice(string sd)
    {
        Invoice inv = new Invoice();
        inv.InvoiceId = Int32.Parse(sd.Trim());
        inv.GetInvoiceByInvoiceID();

        if (inv.Status == LankaTiles.Common.Structures.InvoiceStatus.Created)
        {
            inv.Status = Structures.InvoiceStatus.Printed;
            new InvoiceDAO().UpdateInvoiceStatus(inv);
        }
    }

    /// <summary>
    /// Fill FromURL to go back
    /// </summary>
    private void CheckFromURL()
    {
        try
        {
            if (Request.QueryString["FromURL"] != null && Request.QueryString["FromURL"].Trim() != String.Empty)
            {
                hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Fill InvoiceId from url
    /// </summary>
    private void GetInvoiceId()
    {
        try
        {
            if (Request.QueryString["InvoiceId"] != null && Request.QueryString["InvoiceId"].Trim() != String.Empty)
            {
                hdnInvoiceId.Value = Request.QueryString["InvoiceId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    //protected void ReportViewer1_Unload(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ((ReportViewer)(sender)).Report = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Data.Add("UILayerException", this.GetType().ToString() + LankaTiles.Common.Constant.Error_Seperator + "protected void ReportViewer1_Unload(object sender, EventArgs e)");
    //        Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, LankaTiles.Common.Constant.Database_Connection_Name, "Annonimous"), false);

    //    }
    //}

    protected void ReportViewer1_CacheReportDocument(object sender, CacheReportDocumentEventArgs e)
    {
        try
        {
            e.Key = Guid.NewGuid().ToString();
            Page.Session[e.Key] = e.SaveDocumentToMemoryStream();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ReportViewer1_RestoreReportDocumentFromCache(object sender, RestoreReportDocumentFromCacheEventArgs e)
    {
        try
        {
            Stream stream = Page.Session[e.Key] as Stream;
            if (stream != null)
            {
                e.RestoreDocumentFromStream(stream);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
