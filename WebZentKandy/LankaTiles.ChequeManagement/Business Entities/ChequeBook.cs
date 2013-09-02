using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.Common;

namespace LankaTiles.ChequeManagement
{
    [Serializable]
    public class ChequeBook
    {
        private int chqBookId;
        private int noOfCheques;
        private Int64 firstChqNo;
        private Int64 lastChqNo;
        private int createdBy;
        private string createdByName;
        private DateTime createdDate;
        private int ?modifiedBy;
        private string modifiedByName;
        private DateTime ?modifiedDate;
        private string bankName;
        private string bankBranch;
        private DataSet dsCheques;

        public int ChqBookId
        {
            get { return chqBookId; }
            set { chqBookId = value; }
        }

        public int NoOfCheques
        {
            get { return noOfCheques; }
            set { noOfCheques = value; }
        }

        public Int64 FirstChqNo
        {
            get { return firstChqNo; }
            set { firstChqNo = value; }
        }

        public Int64 LastChqNo
        {
            get { return lastChqNo; }
            set { lastChqNo = value; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public string CreatedByName
        {
            get { return createdByName; }
            set { createdByName = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public int ?ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        public string ModifiedByName
        {
            get { return modifiedByName; }
            set { modifiedByName = value; }
        }

        public DateTime ?ModifiedDate
        {
            get { return modifiedDate; }
            set { modifiedDate = value; }
        }

        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        public string BankBranch
        {
            get { return bankBranch; }
            set { bankBranch = value; }
        }

        public DataSet DsCheques
        {
            get 
            {
                return dsCheques; 
            }
            set { dsCheques = value; }
        }

        public bool Save()
        {
            if (this.chqBookId > 0)
            {
                //update
                return true;
            }
            else
            {
                //create chqbook
                return new ChequeBookDAO().Create(this);
            }
        }

        public bool SelectByBookId()
        {
            try
            {
                return new ChequeBookDAO().GetChqBookById(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateChequeBook()
        {
            bool success = true;
            try
            {
                Int64 firstChequeNumber = this.firstChqNo;
                Int64 lastChequeNumber = 0;
                Int32 numberOfCheques = this.noOfCheques;
                Int64 currentChequeNumber = 0;

                DataView dvCheques = this.dsCheques.Tables[0].DefaultView;
                dvCheques.Sort = "ChequeNo";
                int rowId = -1;

                for (int leafCount = 0; leafCount < this.noOfCheques; leafCount++)
                {
                    currentChequeNumber = firstChequeNumber + leafCount;

                    rowId = dvCheques.Find(currentChequeNumber);

                    if (rowId == -1)
                    {
                        DataRow dr = this.dsCheques.Tables[0].NewRow();
                        dr["ChqId"] = leafCount + 1;
                        dr["ChequeNo"] = currentChequeNumber.ToString().Trim();
                        dr["ChqStatusId"] = (Int16)Structures.ChqStatusId.Created;
                        this.DsCheques.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = this.dsCheques.Tables[0].Rows[rowId];
                        dr.BeginEdit();
                        dr["ChqId"] = leafCount + 1;
                        dr["ChequeNo"] = currentChequeNumber.ToString().Trim();
                        dr["ChqStatusId"] = (Int16)Structures.ChqStatusId.Created;
                        dr.EndEdit();
                    }
                }

                this.LastChqNo = currentChequeNumber;
            }
            catch (Exception ex)
            {
                success = false;
                throw ex;
            }
            return success;
        }
    }
}
