using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.ComponentModel;

namespace Perodua
{
    class csDatabase
    {
        public static DataSet SrcDpsResult()
        {
            String sqlQuery = "SELECT URN AS 'URN No', Line AS 'Line', BodySeq AS 'Body Seq', IDN AS 'Id No', IDVer AS 'Id Ver', ChassisNo AS 'Chassis No', Model AS 'Model', Sfx AS 'Sfx', Color AS 'Color', DPSImportDate AS 'Dps Import Date' FROM DPSResult";
            //String sqlQuery = "SELECT Line AS 'Line', BodySeq AS 'Body Seq', IDN AS 'Id No', IDVer AS 'Id Ver', ChassisNo AS 'Chassis No', Model AS 'Model', Sfx AS 'Sfx', Color AS 'Color', DPSImportDate AS 'Dps Import Date' FROM DPSResult";

            try
            {
                return ConnQuery.getPisBindingDatasetData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        public static DataSet GetNewDpsResult()
        {
            String sqlQuery = "SELECT * FROM DPSResult WHERE DPSImportDate IS NULL ORDER BY ScanDate ASC";

            try
            {
                return ConnQuery.getPisBindingDatasetData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        public static Boolean UpdNewDpsResult(String strIdNo, String strIdVer)
        {
            String sqlQuery = "UPDATE DPSResult SET DPSImportDate = CURRENT_TIMESTAMP WHERE IDN = '" + strIdNo + "' AND IDVer = '" + strIdVer + "'";

            try
            {
                return ConnQuery.ExecutePisQuery(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return false;
            }
        }

        public static DataSet SrcDpsConvResult()
        {
            String sqlQuery = "SELECT plc_no AS 'Plc No',Line AS 'Line', TRIM(URN) AS 'URN No', write_pointer AS 'Write Pointer', dps_ins_code AS 'Instruction Code', id_no AS 'ID No', id_ver AS 'ID Ver', chassis_no AS 'Chassis No', bseq AS 'Body Seq', model AS 'Model', sfx AS 'Sfx', color_code AS 'Color', last_updated AS 'Last Updated' FROM dt_DpsResultConv ORDER BY last_updated DESC";
            //String sqlQuery = "SELECT plc_no AS 'Plc No',Line AS 'Line', write_pointer AS 'Write Pointer', dps_ins_code AS 'Instruction Code', id_no AS 'ID No', id_ver AS 'ID Ver', chassis_no AS 'Chassis No', bseq AS 'Body Seq', model AS 'Model', sfx AS 'Sfx', color_code AS 'Color', last_updated AS 'Last Updated' FROM dt_DpsResultConv ORDER BY last_updated DESC";

            try
            {
                return ConnQuery.getDpsBindingDatasetData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        public static String ChkInsCode(String strModel, String strSfx, String strColor)
        {
            String sqlQuery = "SELECT ins_code AS ReturnField FROM dt_DpsInsCodeMst WHERE model = '" + strModel + "' AND sfx = '" + strSfx + "' AND color = '" + strColor + "'";
            try
            {
                return ConnQuery.getDpsReturnFieldExecuteReader(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return "";
            }
        }

        public static DataSet SrcDpsPlcMaster()
        {
            String sqlQuery = "SELECT * FROM dt_DpsPlcMst WHERE enable = 'True'";

            try
            {
                return ConnQuery.getDpsBindingDatasetData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        public static String GetWritePointer(String strPlcNo)
        {
            String sqlQuery = "SELECT pointer as ReturnField FROM dt_PlcPointerMst WHERE flag_type = 'W' AND plc_no = '" + strPlcNo + "'";

            try
            {
                return ConnQuery.getDpsReturnFieldExecuteReader(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        public static Boolean ChkReadStatus(String strPlcNo, String strWritePointer)
        {
            String sqlQuery = "SELECT COUNT(dps_rs_conv_id) AS CountRow FROM dt_DpsResultConv WHERE plc_no = '" + strPlcNo + "' AND write_pointer = '" + strWritePointer + "' AND read_flag = '1'";

            try
            {
                return ConnQuery.chkDpsExistData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return false;
            }
        }

        public static Boolean ChkDpsResultConv(String strPlcNo, String strWritePointer)
        {
            String sqlQuery = "SELECT COUNT(dps_rs_conv_id) AS CountRow FROM dt_DpsResultConv WHERE plc_no = '" + strPlcNo + "' AND write_pointer = '" + strWritePointer + "'";

            try
            {
                return ConnQuery.chkDpsExistData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return false;
            }
        }

        //public static Boolean UpdDpsRsConv(String strPlcNo, String strPointer, String strModel, String strSfx, String strColor, String strInsCode, String strBseq, String strIdNo, String strIdVer, String strChassisNo, String strDpsRsConvId)
        //{
        //    String sqlQuery = "UPDATE dt_DpsResultConv SET read_flag = '0', plc_no = '" + strPlcNo + "', write_pointer = '" + strPointer + "', model = '" + strModel + "', sfx = '" + strSfx + "', color_code = '" + strColor + "', dps_ins_code = '" + strInsCode + "', bseq = '" + strBseq + "', id_no = '" + strIdNo + "', id_ver = '" + strIdVer + "', chassis_no = '" + strChassisNo + "', last_updated = CURRENT_TIMESTAMP WHERE dps_rs_conv_id = '" + strDpsRsConvId + "'";
        //    try
        //    {
        //        return ConnQuery.ExecuteDpsQuery(sqlQuery);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(Convert.ToString(ex), "Error Message");
        //        return false;
        //    }
        //}

        //public static Boolean SvDpsRsConv(String strPlcNo, String strPointer, String strModel, String strSfx, String strColor, String strInsCode, String strBseq, String strIdNo, String strIdVer, String strChassisNo, String strDpsRsConvId)
        //{
        //    String sqlQuery = "INSERT INTO dt_DpsResultConv (read_flag, plc_no, write_pointer, model, sfx, color_code, dps_ins_code, bseq, id_no, id_ver, chassis_no, last_updated, dps_rs_conv_id) VALUES ('0', '" + strPlcNo + "', '" + strPointer + "', '" + strModel + "', '" + strSfx + "', '" + strColor + "', '" + strInsCode + "', '" + strBseq + "', '" + strIdNo + "', '" + strIdVer + "', '" + strChassisNo + "', CURRENT_TIMESTAMP, '" + strDpsRsConvId + "')";
        //    try
        //    {
        //        return ConnQuery.ExecuteDpsQuery(sqlQuery);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(Convert.ToString(ex), "Error Message");
        //        return false;
        //    }
        //}

        //public static Boolean UpdDpsWritePointer(String strPlcNo, String strPointer)
        //{
        //    String sqlQuery = "UPDATE dt_PlcPointerMst SET pointer = '" + strPointer + "' WHERE plc_no = '" + strPlcNo + "' AND flag_type = 'W'";
        //    try
        //    {
        //        return ConnQuery.ExecuteDpsQuery(sqlQuery);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(Convert.ToString(ex), "Error Message");
        //        return false;
        //    }
        //}

        public static Boolean UpdDpsConv(String strPlcNo, String strPointer, String strModel, String strSfx, String strColor, String strInsCode, String strBseq, String strIdNo, String strIdVer, String strChassisNo, String strDpsRsConvId, String strURN, String strLine)
        {
            try
            {
                String sqlQueryUpdConv = "UPDATE dt_DpsResultConv SET Line = '" + strLine + "', read_flag = '0', plc_no = '" + strPlcNo + "', write_pointer = '" + strPointer + "', model = '" + strModel + "', sfx = '" + strSfx + "', color_code = '" + strColor + "', dps_ins_code = '" + strInsCode + "', bseq = '" + strBseq + "', id_no = '" + strIdNo + "', id_ver = '" + strIdNo + "', URN = '" + strURN + "', chassis_no = '" + strChassisNo + "', last_updated = CURRENT_TIMESTAMP WHERE dps_rs_conv_id = '" + strDpsRsConvId + "';";
                String sqlQueryUpdPointer = "UPDATE dt_PlcPointerMst SET pointer = '" + Convert.ToString(Convert.ToInt32(strPointer) + 1) + "' WHERE plc_no = '" + strPlcNo + "' AND flag_type = 'W';";
                String sqlQueryUpdPis = "UPDATE DPSResult SET DPSImportDate = CURRENT_TIMESTAMP WHERE IDN = '" + strIdNo + "' AND IDVer = '" + strIdVer + "';";

                return ConnQuery.ExecuteTransQuery(sqlQueryUpdConv, sqlQueryUpdPointer, sqlQueryUpdPis);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return false;
            }
        }

        public static Boolean SvDpsConv(String strPlcNo, String strPointer, String strModel, String strSfx, String strColor, String strInsCode, String strBseq, String strIdNo, String strIdVer, String strChassisNo, String strDpsRsConvId, String strURN, String strLine)
        {
            try
            {
                String sqlQueryUpdConv = "INSERT INTO dt_DpsResultConv (read_flag, plc_no, write_pointer, model, sfx, color_code, dps_ins_code, bseq, id_no, id_ver, chassis_no, last_updated, dps_rs_conv_id, URN,Line) VALUES ('0', '" + strPlcNo + "', '" + strPointer + "', '" + strModel + "', '" + strSfx + "', '" + strColor + "', '" + strInsCode + "', '" + strBseq + "', '" + strIdNo + "', '" + strIdVer + "', '" + strChassisNo + "', CURRENT_TIMESTAMP, '" + strDpsRsConvId + "', '" + strURN + "', '" + strLine + "')";
                String sqlQueryUpdPointer = "UPDATE dt_PlcPointerMst SET pointer = '" + Convert.ToString(Convert.ToInt32(strPointer) + 1) + "' WHERE plc_no = '" + strPlcNo + "' AND flag_type = 'W';";
                String sqlQueryUpdPis = "UPDATE DPSResult SET DPSImportDate = CURRENT_TIMESTAMP WHERE IDN = '" + strIdNo + "' AND IDVer = '" + strIdVer + "';";

                return ConnQuery.ExecuteTransQuery(sqlQueryUpdConv, sqlQueryUpdPointer, sqlQueryUpdPis);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return false;
            }
        }

        public static void Log(string sEvent)
        {
            try
            {
                string path = @"C:\Logs\Application_Log\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + "DPS_Monitor_" + DateTime.Today.ToString("ddMMyyyy") + ".log";

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(sEvent + "\r");
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(sEvent + "\r");
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch { }
            {
            }
        }

        public static void Log(Exception Ex)
        {
            try
            {
                string path = @"C:\Logs\Application_Log\Web_Exception\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + "DPS_MonException_" + DateTime.Today.ToString("ddMMyyyy") + ".log";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + Ex + "\r");
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + Ex + "\r");
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch { }
            {
            }
        }

        public static DataSet SrcQuantityGearUp(string URN, string Plc, string GwNo)
        {
            //String sqlQuery = "SELECT plc_no AS 'Plc No',Line AS 'Line', URN AS 'URN No', write_pointer AS 'Write Pointer', dps_ins_code AS 'Instruction Code', id_no AS 'ID No', id_ver AS 'ID Ver', chassis_no AS 'Chassis No', bseq AS 'Body Seq', model AS 'Model', sfx AS 'Sfx', color_code AS 'Color', last_updated AS 'Last Updated' FROM dt_DpsResultConv ORDER BY last_updated DESC";
            int lmStart = 1;
            int lmEnd = 64;

            StringBuilder columnNames = new StringBuilder();
            for (int lm = lmStart; lm <= lmEnd; lm++)
            {
                if (lm > lmStart) // Add a comma before all but the first column
                {
                    columnNames.Append(", ");
                }
                columnNames.Append($"QtyGw{GwNo}Lm{lm}");
            }
            String sqlQuery = $"SELECT {columnNames} FROM dt_DpsResultConv WHERE URN = '{URN}' AND plc_no = '{Plc}'";

            try
            {
                return ConnQuery.getDpsBindingDatasetData(sqlQuery);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                return null;
            }
        }

        //public static Boolean UpdNewPartQty(String strUrn, String strPlcNo)
        //{
        //    String sqlQuery1 = "DECLARE @sql NVARCHAR(MAX) = '';";
        //    String sqlQuery = sqlQuery1 + $" SELECT @sql = @sql + ' UPDATE [PGMDPS].[dbo].[dt_DpsResultConv] SET QtyGw'+ CAST(GwNo AS NVARCHAR(10)) +'Lm' + CAST(LmPhysicalAddress AS NVARCHAR(10)) + ' = ' + CAST(PartQty AS NVARCHAR(10)) + ' WHERE URN = ''' + URN + ''' AND plc_no = ' + CAST(PlcNo AS NVARCHAR(10)) + ';' FROM (SELECT URN, PlcNo, GwNo, LmPhysicalAddress, PartQty FROM [PGMDPS].[dbo].[view_UnpivotedPrtQuantity] WHERE URN = '{strUrn}' AND PlcNo = {strPlcNo}) AS Sub;";
        //    sqlQuery = sqlQuery + " EXEC sp_executesql @sql; ";

        //    try
        //    {
        //        return ConnQuery.ExecuteDpsQuery(sqlQuery);
        //    }
        //    catch (Exception ex)
        //    {
        //        csDatabase.Log(ex);
        //        return false;
        //    }
        //}
        public void ExecuteBatchUpdates(string urn, int plcNo)
        {
            // Retrieve the connection string from the Web.config file
            //string strConnSetting = ConfigurationManager.ConnectionStrings["ConnDpsSQL"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnDpsSQL"].ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("ExecuteBatchQuantityUpdates", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.Add(new SqlParameter("@ip_URN", urn));
                        command.Parameters.Add(new SqlParameter("@ip_plc", plcNo));

                        // Open the connection
                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
            }

        }
    }
}
