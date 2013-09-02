using System;
using System.Collections.Generic;
using System.Text;
using LankaTiles.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace LankaTiles.ItemsManagement
{
    public class CategoryDAO
    {
        #region Is Category Exists

        public bool IsCategoryExists(string categoryType)
        {
            bool isTrue = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_ChkIsExists);

                db.AddInParameter(cmd, "@sCategoryType", DbType.String, categoryType);

                db.AddOutParameter(cmd, "@bIsExists", DbType.Boolean, 1);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    isTrue = (bool) db.GetParameterValue(cmd, "@bIsExists");
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + " public bool IsCategoryExists(string categoryType)");
                throw ex;
            }
            return isTrue;
        }

        #endregion

        #region Delete Category

        public bool DeleteCategory(Category category)
        {
            bool result = false;
            try
            { 
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_Delete);

                db.AddInParameter(cmd, "@iCategoryId", DbType.Int32, category.CategoryId);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = true;
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + " public bool IsCategoryExists(string categoryType)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Add Category

        public bool AddCategory(Category category)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_Insert);

                db.AddInParameter(cmd, "@sCategoryType", DbType.String, category.CategoryType);
                db.AddInParameter(cmd, "@sDescription", DbType.String, category.CategoryDesc);

                db.AddOutParameter(cmd, "@iCategoryId", DbType.Int32, 4);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    int newCatgID = (int)db.GetParameterValue(cmd, "@iCategoryId");
                    if (newCatgID > 0)
                    {
                        category.CategoryId = newCatgID;
                        result = true;
                    }
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateCategory(Category objCategory)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Update Category

        public bool UpdateCategory(Category category)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_Update);

                db.AddInParameter(cmd, "@iCategoryId", DbType.Int32, category.CategoryId);
                db.AddInParameter(cmd, "@sCategoryType", DbType.String, category.CategoryType);
                db.AddInParameter(cmd, "@sDescription", DbType.String, category.CategoryDesc);

                if (db.ExecuteNonQuery(cmd) > 0)
                {
                    result = true;
                    
                }

            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool UpdateCategory(Category objCategory)");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get All Categories

        /// <summary>
        /// get categoryid and category name
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataSet GetAllCategories()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_GetAll);
                
                return db.ExecuteDataSet(cmd);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllCategories()");
                throw ex;
                return null;
            }
        }

        #endregion

        #region Get Categories By ID

        /// <summary>
        /// get categoryid and category name
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public bool GetCategoriesByID(Category category)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_GetByID);

                db.AddInParameter(cmd, "@iCategoryId", DbType.Int32, category.CategoryId);
                             
                IDataReader reader = db.ExecuteReader(cmd);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        category.CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"].ToString()) : 0;
                        category.CategoryType = reader["CategoryType"].ToString();
                        category.CategoryDesc = reader["CategoryDesc"].ToString();

                        result = true;

                    }
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllCategories()");
                throw ex;
            }
            return result;
        }

        #endregion

        #region Get Brands By Category ID

        /// <summary>
        /// get brands by category ID
        /// </summary>
        /// <param name="objCategory"></param>
        /// <returns></returns>
        public DataSet GetBrandsByCategoryID(Category category)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_Get_BrandsByCategoryID);

                db.AddInParameter(cmd, "@iCategoryId", DbType.Int32, category.CategoryId);

                return db.ExecuteDataSet(cmd);
                
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetBrandsByCategoryID(Category category)");
                throw ex;
                return null;
            }
        }

        #endregion

        #region Search Categories

        public DataSet SearchCategories(Category category)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Constant.Database_Connection_Name);
                DbCommand cmd = db.GetStoredProcCommand(Constant.SP_Categories_Search);

                db.AddInParameter(cmd, "@sCategoryType", DbType.String, category.CategoryType);
                db.AddInParameter(cmd, "@sDescription", DbType.String, category.CategoryDesc);

                return db.ExecuteDataSet(cmd);
                
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public DataSet GetAllCategories()");
                return null;
                throw ex;
            }
  
        }

        #endregion
        
    }
}
