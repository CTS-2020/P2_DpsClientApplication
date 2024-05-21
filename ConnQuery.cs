using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Perodua
{
    class ConnQuery
    {
        #region Open Connection
        public static SqlConnection ConnectToDpsSql()
        {
            SqlConnection sqlConn = new SqlConnection();
            string strConnSetting = ConfigurationManager.ConnectionStrings["ConnDpsSQL"].ToString();
            sqlConn.ConnectionString = strConnSetting;
            sqlConn.Open();
            return sqlConn;
        }

        public static SqlConnection ConnectToPisSql()
        {
            SqlConnection sqlConn = new SqlConnection();
            string strConnSetting = ConfigurationManager.ConnectionStrings["ConnPisSQL"].ToString();
            sqlConn.ConnectionString = strConnSetting;
            sqlConn.Open();
            return sqlConn;
        }
        #endregion

        #region Binding Dataset
        public static DataSet getDpsBindingDatasetData(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            DataSet DsResult = new DataSet();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery);

            try
            {
                sqlConn = ConnectToDpsSql();
                sqlCommand.Connection = sqlConn;
                sqlAdapter.SelectCommand = sqlCommand;

                sqlAdapter.Fill(DsResult);
                return DsResult;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
                sqlAdapter.Dispose();
            }
        }

        public static DataSet getPisBindingDatasetData(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            DataSet DsResult = new DataSet();
            SqlCommand sqlCommand = new SqlCommand(sqlQuery);

            try
            {
                sqlConn = ConnectToPisSql();
                sqlCommand.Connection = sqlConn;
                sqlAdapter.SelectCommand = sqlCommand;

                sqlAdapter.Fill(DsResult);
                return DsResult;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
            finally
            {
                sqlConn.Close();
                sqlConn.Dispose();
                sqlAdapter.Dispose();
            }
        }
        #endregion

        #region Check Exist Data in Database
        public static Boolean chkDpsExistData(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;

            Boolean ExistRow = false;

            sqlConn = ConnectToDpsSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();
            if (sqlReader.HasRows)
            {
                if (Convert.ToInt32(sqlReader["CountRow"]) > 0)
                {
                    ExistRow = true;
                }
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return ExistRow;
        }

        public static Boolean chkPisExistData(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;

            Boolean ExistRow = false;

            sqlConn = ConnectToPisSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();
            if (sqlReader.HasRows)
            {
                if (Convert.ToInt32(sqlReader["CountRow"]) > 0)
                {
                    ExistRow = true;
                }
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return ExistRow;
        }
        #endregion

        #region get Identity Execute Reader
        public static String getDpsIdentityExecuteReader(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;
            String strNewId = "";

            sqlConn = ConnectToDpsSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            if (sqlReader.HasRows)
            {
                strNewId = Convert.ToString(sqlReader.GetString(0));
            }
            else
            {
                strNewId = "";
            }
            sqlConn.Close();

            return strNewId;
        }

        public static String getPisIdentityExecuteReader(String sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;
            String strNewId = "";

            sqlConn = ConnectToPisSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            if (sqlReader.HasRows)
            {
                strNewId = Convert.ToString(sqlReader.GetString(0));
            }
            else
            {
                strNewId = "";
            }
            sqlConn.Close();

            return strNewId;
        }
        #endregion

        #region get Return Field Execute Reader
        public static String getDpsReturnFieldExecuteReader(string sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;
            String strReturnField = "";

            sqlConn = ConnectToDpsSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            if (sqlReader.HasRows)
            {
                strReturnField = Convert.ToString(sqlReader["ReturnField"]);
            }
            else
            {
                strReturnField = "";
            }
            sqlConn.Close();

            return strReturnField;
        }

        public static String getPisReturnFieldExecuteReader(string sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            SqlDataReader sqlReader = null;
            String strReturnField = "";

            sqlConn = ConnectToPisSql();
            sqlCommand = new SqlCommand(sqlQuery, sqlConn);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            if (sqlReader.HasRows)
            {
                strReturnField = Convert.ToString(sqlReader["ReturnField"]);
            }
            else
            {
                strReturnField = "";
            }
            sqlConn.Close();

            return strReturnField;
        }
        #endregion

        #region Execute Query
        public static Boolean ExecuteDpsQuery(string sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            Boolean ExecuteResult = false;

            try
            {
                sqlConn = ConnectToDpsSql();
                sqlCommand = new SqlCommand(sqlQuery, sqlConn);
                if (sqlCommand.ExecuteNonQuery() >= 1)
                {
                    ExecuteResult = true;
                }
                else
                {
                    ExecuteResult = false;
                }
                sqlConn.Close();
                return ExecuteResult;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                ExecuteResult = false;
            }
            finally
            {
                try
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlCommand.Dispose();
                }
                catch { }

            }

            return ExecuteResult;
        }

        public static Boolean ExecutePisQuery(string sqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCommand = null;
            Boolean ExecuteResult = false;

            try
            {
                sqlConn = ConnectToPisSql();
                sqlCommand = new SqlCommand(sqlQuery, sqlConn);
                if (sqlCommand.ExecuteNonQuery() >= 1)
                {
                    ExecuteResult = true;
                }
                else
                {
                    ExecuteResult = false;
                }
                sqlConn.Close();
                return ExecuteResult;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                ExecuteResult = false;
            }
            finally
            {
                try
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlCommand.Dispose();
                }
                catch { }

            }

            return ExecuteResult;
        }

        public static Boolean ExecuteTransQuery(string sqlQueryUpdConv, string sqlQueryUpdPointer, string sqlQueryUpdPis)
        {
            SqlConnection sqlConnDps = null;
            SqlConnection sqlConnPis = null;
            SqlCommand sqlCmdUpdConv = null;
            SqlCommand sqlCmdUpdPointer = null;
            SqlCommand sqlCmdUpdPis = null;
            SqlTransaction sqlTransDps = null;
            SqlTransaction sqlTransPis = null;
            Boolean ExecuteResult = false;

            try
            {
                sqlConnDps = ConnectToDpsSql();
                sqlConnPis = ConnectToPisSql();
                sqlTransDps = sqlConnDps.BeginTransaction();
                sqlTransPis = sqlConnPis.BeginTransaction();

                sqlCmdUpdConv = new SqlCommand(sqlQueryUpdConv, sqlConnDps, sqlTransDps);
                sqlCmdUpdPointer = new SqlCommand(sqlQueryUpdPointer, sqlConnDps, sqlTransDps);
                sqlCmdUpdPis = new SqlCommand(sqlQueryUpdPis, sqlConnPis, sqlTransPis);

                if (sqlCmdUpdConv.ExecuteNonQuery() >= 1)
                {
                    ExecuteResult = true;
                    if (sqlCmdUpdPointer.ExecuteNonQuery() >= 1)
                    {
                        ExecuteResult = true;
                        if (sqlCmdUpdPis.ExecuteNonQuery() >= 1)
                        {
                            ExecuteResult = true;
                            sqlTransDps.Commit();
                            sqlTransPis.Commit();
                        }
                        else
                        {
                            ExecuteResult = false;
                            sqlTransDps.Rollback();
                            sqlTransPis.Rollback();
                        }
                    }
                    else
                    {
                        ExecuteResult = false;
                        sqlTransDps.Rollback();
                    }
                }
                else
                {
                    ExecuteResult = false;
                    sqlTransDps.Rollback();
                }

                sqlConnDps.Close();
                sqlConnPis.Close();

                return ExecuteResult;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                ExecuteResult = false;
                if (sqlTransDps != null) sqlTransDps.Rollback();
                if (sqlTransPis != null) sqlTransPis.Rollback();
            }
            finally
            {
                try
                {
                    sqlConnDps.Close();
                    sqlConnPis.Close();
                    sqlConnDps.Dispose();
                    sqlConnPis.Dispose();
                    sqlCmdUpdConv.Dispose();
                    sqlCmdUpdPointer.Dispose();
                    sqlCmdUpdPis.Dispose();
                    sqlTransDps.Dispose();
                    sqlTransPis.Dispose();
                }
                catch { }
            }

            return ExecuteResult;
        }
        #endregion
    }
}
