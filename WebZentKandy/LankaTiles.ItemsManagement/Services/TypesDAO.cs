using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using LankaTiles.Common;
using LankaTiles.ItemsManagement.Business_Entity;
using System.Data;

namespace LankaTiles.ItemsManagement.Services
{
    public class TypesDAO
    {
        #region Private Variables

        DbConnection connection;
        DbTransaction transaction;

        #endregion

        #region Add Types

        public bool AddType(ITypes type)
        {
            bool success = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Type_Insert);

                db.AddInParameter(dbCommand, "@sTypeName", DbType.String, type.TypeName);
                db.AddOutParameter(dbCommand, "@iTypeId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    int newItemID = (int)db.GetParameterValue(dbCommand, "@iTypeId");

                    if (newItemID > 0)
                    {
                        success = true;
                        type.TypeId = newItemID;
                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool AddType(Type type)");
                throw ex;
            }
            return success;

        }

        #endregion


        #region Get All Types

        public DataSet GetAllTypes()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Type_GetAll);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllItems()");
                return null;
                throw ex;
            }
        }

        #endregion

    }
}
