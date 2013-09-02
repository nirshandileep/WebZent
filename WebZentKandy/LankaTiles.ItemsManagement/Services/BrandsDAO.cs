using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LankaTiles.Common;
using System.Data.Common;

namespace LankaTiles.ItemsManagement
{
    public class BrandsDAO
    {
        #region Private Variables

        private DbTransaction transaction;
        private DbConnection connection;

        #endregion

        #region Add Brands

        public bool AddBrands(Brand brand)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Brands_Insert);


                db.AddInParameter(dbCommand, "@sBrandName", DbType.String, brand.BrandName);

                db.AddOutParameter(dbCommand, "@iBrandId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    Int32 newBrandID = Convert.ToInt32(db.GetParameterValue(dbCommand, "@iBrandId"));
                    if (newBrandID > 0)
                    {
                        brand.BrandId = newBrandID;
                        if (this.UpdateBrandCategories(brand, db, transaction))
                        {
                            transaction.Commit();
                            result = true;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Update Brands

        public bool UpdateBrands(Brand brand)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                connection = db.CreateConnection();
                connection.Open();

                transaction = connection.BeginTransaction();

                DbCommand dbCommand = db.GetStoredProcCommand(Constant.SP_Brands_Update);

                db.AddInParameter(dbCommand, "@iBrandId", DbType.String, brand.BrandId);
                db.AddInParameter(dbCommand, "@sBrandName", DbType.String, brand.BrandName);

                if (db.ExecuteNonQuery(dbCommand, transaction) > 0)
                {
                    if (this.UpdateBrandCategories(brand, db, transaction))
                    {
                        transaction.Commit();
                        result = true;
                    }

                }
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion

        #region Update Brand Categories

        private bool UpdateBrandCategories(Brand brand, Database db, DbTransaction transaction)
        {
            bool result = false;
            try
            {
                DbCommand insCommand = db.GetStoredProcCommand(Constant.SP_BrandCategories_InsBrandCategories);
                db.AddInParameter(insCommand, "@iBrandId", DbType.Int32, brand.BrandId);
                db.AddInParameter(insCommand, "@iCategoryId", DbType.Int32, "CategoryId", DataRowVersion.Current);

                DbCommand DelCommand = db.GetStoredProcCommand(Constant.SP_BrandCategories_DelBrandCategories);
                db.AddInParameter(DelCommand, "@iBrandId", DbType.Int32, brand.BrandId);
                db.AddInParameter(DelCommand, "@iCategoryId", DbType.Int32, "CategoryId", DataRowVersion.Current);

                db.UpdateDataSet(brand.DsCategories, brand.DsCategories.Tables[0].TableName, insCommand, null, DelCommand, transaction);
                result = true;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get All Brands

        /// <summary>
        /// return BrandId and 
        /// </summary>
        /// <param name="objBrand"></param>
        /// <returns></returns>
        public DataSet GetAllBrands()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Brands_GetAll);

                return db.ExecuteDataSet(cmd);

            }
            catch (System.Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

        #region Get Brand By ID
        public bool GetBrandByID(Brand brand)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Brands_GetByID);

                db.AddInParameter(cmd, "@iBrandId", DbType.Int32, brand.BrandId);

                IDataReader reader = db.ExecuteReader(cmd);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        brand.BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"].ToString()) : 0;
                        brand.BrandName = reader["BrandName"].ToString();
                    }

                    brand.DsCategories = this.GetBrandCategoriesByBrandID(brand);
                    result = true;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Brand Categories By BrandID

        public DataSet GetBrandCategoriesByBrandID(Brand brand)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Brands_SelCategoriesByBrandID);

                db.AddInParameter(cmd, "@iBrandId", DbType.Int32, brand.BrandId);

                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Search Brands
        public DataSet SearchBrands(Brand brand)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Brands_Search);

                db.AddInParameter(cmd, "@sBrandName", DbType.String, brand.BrandName);
                
                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

    }
}
