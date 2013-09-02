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
using LankaTiles.ChequeManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using DevExpress.Web.ASPxGridView;

public partial class AddChequeBook : System.Web.UI.Page
{
    private ChequeBook chequeBook;

    public ChequeBook ChequeBook
    {
        get 
        {
            if (chequeBook == null)
            {
                if (Session["ChequeBook"] == null)
                {
                    chequeBook = new ChequeBook();
                    chequeBook.ChqBookId = Int32.Parse(hdnChequeBook.Value.Trim() == String.Empty ? "0" : hdnChequeBook.Value.Trim());

                    chequeBook.SelectByBookId();
                    Session["ChequeBook"] = chequeBook;
                }
                else
                {
                    chequeBook = (ChequeBook)(Session["ChequeBook"]);
                }
            }
            return chequeBook;
        }
        set 
        {
            chequeBook = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["ChequeBook"] = null;
                this.AddAttributes();
                this.CheckIfEditChqBook();
                this.SetData();

                if (hdnChequeBook.Value != "0")
                {
                    dxgvChequeDetails.Columns["Edit"].Visible = true;
                    btnCreateCheques.Visible = false;
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    seNoOfChqs.Enabled = false;
                    ddlBankName.Enabled = false;
                    ddlBranchLocation.Enabled = false;
                    txtFirstChqNo.Enabled = false;
                    txtLastChqNo.Enabled = false;

                }
                else
                {
                    dxgvChequeDetails.Columns["Edit"].Visible = false;
                    btnCreateCheques.Visible = true;
                    btnSave.Enabled = false;
                    btnUpdate.Visible = false;
                }

            }

            #region 

            if (IsCallback)
            {
                if (ChequeBook.DsCheques != null)
                {
                    dxgvChequeDetails.DataSource = ChequeBook.DsCheques;
                    dxgvChequeDetails.DataBind();
                }
                GridViewDataComboBoxColumn combo = dxgvChequeDetails.Columns["StatusName"] as GridViewDataComboBoxColumn;
                combo.PropertiesComboBox.ValueType = typeof(int);
                combo.PropertiesComboBox.Items.Add("Created", 1);
                combo.PropertiesComboBox.Items.Add("Issued", 2);
                combo.PropertiesComboBox.Items.Add("Cancelled", 3);
                combo.PropertiesComboBox.Items.Add("Lost", 4);
            }
            
            #endregion
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void CheckIfEditChqBook()
    {
        try
        {
            if (Request.QueryString["ChqBookId"] != null && Request.QueryString["ChqBookId"].Trim() != String.Empty)
            {
                hdnChequeBook.Value = Request.QueryString["ChqBookId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Adds client side attributes to controls
    /// </summary>
    private void AddAttributes()
    {
        txtFirstChqNo.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        txtLastChqNo.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    protected void btnCreateCheques_Click(object sender, EventArgs e)
    {
        try
        {
            ChequeBook.BankBranch = ddlBranchLocation.SelectedValue;
            ChequeBook.BankName = ddlBankName.SelectedValue;
            ChequeBook.FirstChqNo = Int64.Parse(txtFirstChqNo.Text.Trim());
            ChequeBook.NoOfCheques = Int32.Parse(seNoOfChqs.Text.Trim());

            //Fill the chequebook object and call
            if (ChequeBook.CreateChequeBook())
            {
                if (new ChequeBookDAO().ValidateCheckNumbersByRange(ChequeBook))
                {
                    this.SetData();
                    btnSave.Enabled = true;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Some of the Cheque Numbers in the given range already exist in the system";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Error, Cheque book was not created with the details provided";
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCreateCheques_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void SetData()
    {
        try
        {
            seNoOfChqs.Text = ChequeBook.NoOfCheques.ToString().Trim();
            txtFirstChqNo.Text = ChequeBook.FirstChqNo.ToString();
            txtLastChqNo.Text = ChequeBook.LastChqNo.ToString();
            ddlBankName.SelectedValue = ChequeBook.BankName;
            ddlBranchLocation.SelectedValue = ChequeBook.BankBranch;

            dxgvChequeDetails.DataSource = ChequeBook.DsCheques;
            dxgvChequeDetails.DataBind();

            hdnRowCount.Value = ChequeBook.DsCheques.Tables[0].Rows.Count.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ChequeBook.BankBranch = ddlBranchLocation.SelectedValue.Trim();
            ChequeBook.BankName = ddlBankName.SelectedValue.Trim();
            ChequeBook.CreatedBy = Master.LoggedUser.UserId;
            ChequeBook.FirstChqNo = Int64.Parse(txtFirstChqNo.Text.Trim());
            ChequeBook.LastChqNo = Int64.Parse(txtLastChqNo.Text.Trim());
            ChequeBook.ModifiedBy = Master.LoggedUser.UserId;
            ChequeBook.NoOfCheques = Int32.Parse(seNoOfChqs.Text);

            if (ChequeBook.Save())
            {
                btnCreateCheques.Visible = false;
                btnSave.Visible = false;
                hdnChequeBook.Value = ChequeBook.ChqBookId.ToString();

                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSave_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Update the cheque details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgvChequeDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            string newComment = e.NewValues["Comment"].ToString();
            string statusNewValue = e.NewValues["StatusName"].ToString();
            int statusOldValue;

            switch (e.OldValues["StatusName"].ToString())
            {
                case "Cancelled":
                    statusOldValue = (int)Structures.ChqStatusId.Cancelled;
                    break;
                case "Created":
                    statusOldValue = (int)Structures.ChqStatusId.Created;
                    break;
                case "Issued":
                    statusOldValue = (int)Structures.ChqStatusId.Issued;
                    break;
                case "Lost":
                    statusOldValue = (int)Structures.ChqStatusId.Lost;
                    break;
                default:
                    statusOldValue = -1;
                    break;
            }


            DataRow[] dr = ((ChequeBook)Session["ChequeBook"]).DsCheques.Tables[0].Select("ChqId=" + e.Keys[0].ToString());
            if (dr.Length > 0)
            {

                int newValue;

                if (statusOldValue > 0)
                {
                    if (statusOldValue != (int)Structures.ChqStatusId.Created)
                    {
                        return;
                    }
                    else
                    {
                        if (Int32.TryParse(statusNewValue, out newValue))
                        {

                            dr[0]["ChqStatusId"] = statusNewValue;
                            if (newValue == (int)Structures.ChqStatusId.Cancelled)
                            {
                                dr[0]["StatusName"] = Structures.ChqStatusId.Cancelled.ToString();
                                dr[0]["Comment"] = newComment;
                                //dr[0]["Amount"] = 0;
                                //dr[0]["WrittenBy"] = Master.LoggedUser.UserId;
                                dr[0]["ModifiedBy"] = Master.LoggedUser.UserId;

                                e.Cancel = true;
                                dxgvChequeDetails.CancelEdit();
                                dxgvChequeDetails.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    return;
                }

            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvChequeDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (new ChequeBookDAO().UpdateCheques(this.ChequeBook))
            {
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                lblError.Visible = true;
            }
            else
            {
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnUpdate_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }
}
