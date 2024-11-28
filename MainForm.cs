using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Security.Policy;

namespace Perodua
{
    public partial class MainForm : Form
    {
        #region Global
        Timer t = new Timer();
        Timer ping = new Timer();
        Timer process = new Timer();
        String strPisIpAdd = ConfigurationManager.AppSettings["pisIpAdd"];
        int iPisConnInterval = Convert.ToInt32(ConfigurationManager.AppSettings["pisConnInterval"]);
        Boolean flagPIS;
        String UrnNo = "";
        #endregion

        #region Load
        public MainForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to initialize program.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

                t.Interval = 1000;  //in milliseconds, 1 second
                t.Tick += new EventHandler(this.t_tick);
                t.Start();

                btnStart_Click(btnStart, e);
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to load forms.");
            }
        }
        #endregion

        #region Method
        private void t_tick(object sender, EventArgs e)
        {
            try
            {
                #region Get Date & Time
                int yyyy = DateTime.Now.Year;
                int mmmm = DateTime.Now.Month;
                int dddd = DateTime.Now.Day;
                int hh = DateTime.Now.Hour;
                int mm = DateTime.Now.Minute;
                int ss = DateTime.Now.Second;
                #endregion

                #region Set Date
                string date = "";
                date += yyyy;
                date += "-";
                if (mmmm < 10)
                {
                    date += "0" + mmmm;
                }
                else
                {
                    date += mmmm;
                }
                date += "-";
                if (dddd < 10)
                {
                    date += "0" + dddd;
                }
                else
                {
                    date += dddd;
                }
                #endregion

                #region Set Time
                string time = "";
                if (hh < 10)
                {
                    time += "0" + hh;
                }
                else
                {
                    time += hh;
                }
                time += ":";
                if (mm < 10)
                {
                    time += "0" + mm;
                }
                else
                {
                    time += mm;
                }
                time += ":";
                if (ss < 10)
                {
                    time += "0" + ss;
                }
                else
                {
                    time += ss;
                }
                #endregion

                txtTimer.Text = date + "     " + time; // set date and time
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to set timer.");
            }
        }

        private void ping_tick(object sender, EventArgs e)
        {
            try
            {

                Boolean blConn = pingIP();
                if (blConn == false)
                {
                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "PIS connection error." + txtLog.Text;
                    csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "PIS connection error.");
                }
                else
                {
                    do_process();
                }
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to connect to PIS Server.");
            }
        }

        private Boolean pingIP()
        {
            try
            {
                Ping myPing = new Ping();
                PingReply replyPIS = myPing.Send(strPisIpAdd, 1000);
                if (replyPIS != null)
                {
                    flagPIS = false;
                    if (replyPIS.Status.ToString() == "Success")
                    {
                        flagPIS = true;
                    }

                    if (flagPIS)
                    {
                        BindDpsResultGridView();
                        pbPisConn.Image = global::Perodua.Properties.Resources.green;
                        return true;
                    }
                    else
                    {
                        pbPisConn.Image = global::Perodua.Properties.Resources.red;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "PIS connection error." + txtLog.Text;
                csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "PIS connection error.");
                csDatabase.Log(ex);
                MessageBox.Show("Unable to connect to PIS Server.");
                return false;
            }
        }

        private void BindDpsResultGridView()
        {
            try
            {
                DataSet dsDpsResult = new DataSet();
                DataTable dtDpsResult = new DataTable();

                gvPisTracking.AutoGenerateColumns = true;
                dsDpsResult = csDatabase.SrcDpsResult();

                dtDpsResult = dsDpsResult.Tables[0];
                //UrnNo = dtDpsResult.Rows[0]["URN No"].ToString();
                gvPisTracking.DataSource = dtDpsResult;

                gvPisTracking.FirstDisplayedScrollingRowIndex = gvPisTracking.RowCount - 1;
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to connect to show PIS Tracking Table.");
            }
        }

        private void BindDpsResultConvGridView()
        {
            try
            {
                DataSet dsDpsConvResult = new DataSet();
                DataTable dtDpsConvResult = new DataTable();

                gvResultConv.AutoGenerateColumns = true;
                dsDpsConvResult = csDatabase.SrcDpsConvResult();

                dtDpsConvResult = dsDpsConvResult.Tables[0];
                gvResultConv.DataSource = dtDpsConvResult;

                //DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
                //linkColumn.HeaderText = "URN";
                //linkColumn.Name = "URN";
                //linkColumn.DataPropertyName = "URN"; // Bind the URN column value to the link text
                ////gvResultConv.Columns.Add(linkColumn);
                //gvResultConv.Columns.Insert(2, linkColumn);

                if (dtDpsConvResult.Columns.Contains("URN No"))
                {
                    AddLinkColumnIfNotExists("URN No");
                    //gvResultConv.CellContentClick -= gvResultConv_CellContentClick;
                    //gvResultConv.CellContentClick += gvResultConv_CellContentClick;
                    gvPisTracking.FirstDisplayedScrollingRowIndex = gvPisTracking.RowCount - 1;
                }
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to connect to show DPS Result Conversion Table.");
            }
        }

        private void do_process()
        {
            try
            {
                BindDpsResultGridView();
                BindDpsResultConvGridView();

                DataSet dsNewDpsResult = new DataSet();
                DataTable dtNewDpsResult = new DataTable();

                dsNewDpsResult = csDatabase.GetNewDpsResult();
                dtNewDpsResult = dsNewDpsResult.Tables[0];

                if (dtNewDpsResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtNewDpsResult.Rows.Count; i++)
                    {
                        #region Initialize Variables
                        String strIdNo = "";
                        String strIdVer = "";
                        String strChassisNo = "";
                        String strModel = "";
                        String strColor = "";
                        String strBodySeq = "";
                        String strSfx = "";
                        String strCurDateTime = "";
                        String strInsCode = "";
                        String strURN = "";
                        String strLine = "";
                        #endregion

                        #region Get New Dps Result Info
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["IDN"]).Trim() != "")
                        {
                            strIdNo = Convert.ToString(dtNewDpsResult.Rows[i]["IDN"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["IDVer"]).Trim() != "")
                        {
                            strIdVer = Convert.ToString(dtNewDpsResult.Rows[i]["IDVer"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["ChassisNo"]).Trim() != "")
                        {
                            strChassisNo = Convert.ToString(dtNewDpsResult.Rows[i]["ChassisNo"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["Model"]).Trim() != "")
                        {
                            strModel = Convert.ToString(dtNewDpsResult.Rows[i]["Model"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["Color"]).Trim() != "")
                        {
                            strColor = Convert.ToString(dtNewDpsResult.Rows[i]["Color"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["BodySeq"]).Trim() != "")
                        {
                            //strBodySeq = Convert.ToString(dtNewDpsResult.Rows[i]["BodySeq"]);
                            string fullBodySeq = Convert.ToString(dtNewDpsResult.Rows[i]["BodySeq"]).Trim();

                            // Get the last 4 digits of the string, assuming the string length is always 10
                            if (fullBodySeq.Length >= 4)
                            {
                                // Extract the last 4 characters and assign it back to strBodySeq
                                strBodySeq = fullBodySeq.Substring(fullBodySeq.Length - 4);
                            }
                            else
                            {
                                // If the string is shorter than 4 characters, assign the full value to strBodySeq
                                strBodySeq = fullBodySeq;
                            }
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["Sfx"]).Trim() != "")
                        {
                            strSfx = Convert.ToString(dtNewDpsResult.Rows[i]["Sfx"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["URN"]).Trim() != "")
                        {
                            strURN = Convert.ToString(dtNewDpsResult.Rows[i]["URN"]);
                        }
                        if (Convert.ToString(dtNewDpsResult.Rows[i]["Line"]).Trim() != "")
                        {
                            strLine = Convert.ToString(dtNewDpsResult.Rows[i]["Line"]);
                        }
                        #endregion

                        pbPulling.Image = global::Perodua.Properties.Resources.green;
                        strCurDateTime = DateTime.Now.ToString();

                        if (strIdNo == "4Z0000000000")
                        {
                            strInsCode = "0";
                        }
                        else if (strIdNo == "4ZCC00000000")
                        {
                            strInsCode = "0";
                        }
                        else if (strIdNo.StartsWith("4ZCC"))
                        {
                            strInsCode = "0";
                        }
                        else
                        {
                            strInsCode = csDatabase.ChkInsCode(strModel, strSfx, strColor);
                        }

                        if (strInsCode == "")
                        {
                            pbGenIns.Image = global::Perodua.Properties.Resources.red;
                            txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] No Instruction Code Data found" + txtLog.Text;
                            csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] No Instruction Code Data found");
                            break;
                        }

                        DataSet dsDpsPlcMst = new DataSet();
                        DataTable dtDpsPlcMst = new DataTable();

                        dsDpsPlcMst = csDatabase.SrcDpsPlcMaster();
                        dtDpsPlcMst = dsDpsPlcMst.Tables[0];

                        if (dtDpsPlcMst.Rows.Count > 0)
                        {
                            Boolean blExist = true;
                            String strPlcNo = "";

                            for (int iPlcChk = 0; iPlcChk < dtDpsPlcMst.Rows.Count; iPlcChk++)
                            {
                                if (Convert.ToString(dtDpsPlcMst.Rows[iPlcChk]["plc_no"]).Trim() != "")
                                {
                                    strPlcNo = Convert.ToString(dtDpsPlcMst.Rows[iPlcChk]["plc_no"]).Trim();
                                }

                                String strWritePointer = csDatabase.GetWritePointer(strPlcNo);

                                if (strWritePointer == "")
                                {
                                    blExist = false;
                                }
                            }

                            if (blExist == false)
                            {
                                pbGenIns.Image = global::Perodua.Properties.Resources.red;

                                txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WRITE POINTER NOT FOUND" + txtLog.Text;
                                csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WRITE POINTER NOT FOUND");
                                break;
                            }

                            for (int iPlcCnt = 0; iPlcCnt < dtDpsPlcMst.Rows.Count; iPlcCnt++)
                            {
                                #region Initialize Variables
                                String strWritePointer = "";
                                String strDpsRsConvId = "";
                                Boolean boolUpdConv = false;
                                Boolean boolUpdPointer = false;
                                Boolean boolUpdPis = false;
                                Boolean boolUpdPrtQty = false;
                                #endregion

                                if (Convert.ToString(dtDpsPlcMst.Rows[iPlcCnt]["plc_no"]).Trim() != "")
                                {
                                    strPlcNo = Convert.ToString(dtDpsPlcMst.Rows[iPlcCnt]["plc_no"]).Trim();
                                }

                                strWritePointer = csDatabase.GetWritePointer(strPlcNo);

                                if (strWritePointer == "")
                                {
                                    pbGenIns.Image = global::Perodua.Properties.Resources.red;

                                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WRITE POINTER NOT FOUND" + txtLog.Text;
                                    csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WRITE POINTER NOT FOUND");
                                    break;
                                }

                                if (strWritePointer == "1000")
                                {
                                    strWritePointer = "1";
                                }

                                Boolean boolExist = csDatabase.ChkDpsResultConv(strPlcNo, strWritePointer);
                                strDpsRsConvId = strPlcNo + "^" + strWritePointer;
                                if (boolExist)
                                {
                                    Boolean blRead = csDatabase.ChkReadStatus(strPlcNo, strWritePointer);

                                    if (blRead == false)
                                    {
                                        pbGenIns.Image = global::Perodua.Properties.Resources.red;

                                        txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WP [" + strWritePointer + "] WRITE POINTER NOT READ BY PLC" + txtLog.Text;
                                        csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC [" + strPlcNo + "] WP [" + strWritePointer + "] WRITE POINTER NOT READ BY PLC");
                                        break;
                                    }
                                    else
                                    {
                                        boolUpdConv = csDatabase.UpdDpsConv(strPlcNo, strWritePointer, strModel, strSfx, strColor, strInsCode, strBodySeq, strIdNo, strIdVer, strChassisNo, strDpsRsConvId, strLine);
                                        //boolUpdPrtQty = csDatabase.UpdNewPartQty(strURN, strPlcNo);
                                    }
                                }
                                else
                                {
                                    boolUpdConv = csDatabase.SvDpsConv(strPlcNo, strWritePointer, strModel, strSfx, strColor, strInsCode, strBodySeq, strIdNo, strIdVer, strChassisNo, strDpsRsConvId, strURN, strLine);
                                    //boolUpdPrtQty = csDatabase.UpdNewPartQty(strURN, strPlcNo);
                                }

                                if (boolUpdConv)
                                {
                                    BindDpsResultGridView();
                                    BindDpsResultConvGridView();

                                    pbGenIns.Image = global::Perodua.Properties.Resources.green;

                                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Normal: " + "[Upd DPS] OK, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC[" + i.ToString() + "] PO[" + strWritePointer + "] INS.CODE[" + strInsCode + "] SFX[" + strSfx + "] COLOR[" + strColor + "] UPDATED[" + strCurDateTime + "]" + txtLog.Text;
                                    csDatabase.Log("---[" + DateTime.Now + "]--- Normal: " + "[Upd DPS] OK, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] PLC[" + i.ToString() + "] PO[" + strWritePointer + "] INS.CODE[" + strInsCode + "] SFX[" + strSfx + "] COLOR[" + strColor + "] UPDATED[" + strCurDateTime + "]");

                                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Normal: " + "[Upd PIS] OK, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UPDATED[" + strCurDateTime + "]" + txtLog.Text;
                                    csDatabase.Log("---[" + DateTime.Now + "]--- Normal: " + "[Upd PIS] OK, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UPDATED[" + strCurDateTime + "]");
                                }
                                else
                                {
                                    BindDpsResultGridView();
                                    BindDpsResultConvGridView();

                                    pbGenIns.Image = global::Perodua.Properties.Resources.red;

                                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UPDATE DPS RESULT CONVERSION FAILED" + txtLog.Text;
                                    csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UPDATE DPS RESULT CONVERSION FAILED");

                                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd PIS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UNABLE TO UPDATE INTO PIS SERVER" + txtLog.Text;
                                    csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd PIS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] UNABLE TO UPDATE INTO PIS SERVER");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pbGenIns.Image = global::Perodua.Properties.Resources.yellow;

                            txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] NO PLC IS ENABLED" + txtLog.Text;
                            csDatabase.Log("---[" + DateTime.Now + "]--- Error: " + "[Upd DPS] NG, IDN[" + strIdNo + "] IDNVer[" + strIdVer + "] NO PLC IS ENABLED");
                        }

                        if (pbGenIns.Image != global::Perodua.Properties.Resources.green)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    pbPulling.Image = global::Perodua.Properties.Resources.yellow;
                    pbGenIns.Image = global::Perodua.Properties.Resources.yellow;

                    txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Normal: " + "No new PIS Tracking Data found." + txtLog.Text;
                    csDatabase.Log("---[" + DateTime.Now + "]--- Normal: " + "No new PIS Tracking Data found.");
                }
            }
            catch (Exception ex)
            {
                pbPulling.Image = global::Perodua.Properties.Resources.red;
                pbGenIns.Image = global::Perodua.Properties.Resources.red;
                csDatabase.Log(ex);
                MessageBox.Show("Unable to process.");
            }
        }

        private void stop_process()
        {
            try
            {

                ping.Stop();
                process.Stop();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                pbPulling.Image = global::Perodua.Properties.Resources.yellow;
                pbGenIns.Image = global::Perodua.Properties.Resources.yellow;
                pbPisConn.Image = global::Perodua.Properties.Resources.yellow;
                txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Normal: " + "Process end." + txtLog.Text;
                csDatabase.Log("---[" + DateTime.Now + "]--- Normal: " + "Process end.");
                //Boolean boolUpdPrtQty = csDatabase.UpdNewPartQty("U2024001882", "1");
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to stop process.");
            }
        }

        PassIn passIn = new PassIn();
        //private void gvResultConv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Check if the link column was clicked
        //    if (e.ColumnIndex == gvResultConv.Columns["URN No"].Index && e.RowIndex >= 0)
        //    {
        //        // Get the value of the "URN" column for the selected row
        //        string urn = gvResultConv.Rows[e.RowIndex].Cells["URN No"].Value.ToString();
        //        // Get the value of the "Details" column for the selected row
        //        string plcNo = gvResultConv.Rows[e.RowIndex].Cells["Plc No"].Value.ToString();
        //        // Show the details in a popup window
        //        //DetailForm detailForm = new DetailForm(urn, details);
        //        //detailForm.ShowDialog();

        //        passIn.URN = urn;
        //        passIn.PlcNo = plcNo;
        //        passIn.GwNo = "1";
        //        //openDialog(passIn);
        //    }
        //}

        //private void popUp_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    // Perform actions after the dialog form is closed
        //    if (((QuantityPopUpForm)sender).DialogResult == DialogResult.OK)
        //    {
        //        string updatedValue = ((QuantityPopUpForm)sender).SelectedGwNo;
        //        bool open = ((QuantityPopUpForm)sender).ReOpenInd;
        //        passIn.GwNo = updatedValue;
        //        if (open)
        //        {
        //            // Use a timer to ensure the form is fully closed before reopening a new one
        //            Timer timer = new Timer { Interval = 100 }; // 100 ms delay
        //            timer.Tick += (s, args) =>
        //            {
        //                timer.Stop();
        //                openDialog(passIn);
        //            };
        //            timer.Start();
        //        }
        //    }
        //}
        //private void openDialog(PassIn passIn)
        //{
        //    QuantityPopUpForm popUp = new QuantityPopUpForm(passIn);

        //    popUp.FormClosed -= popUp_FormClosed;
        //    popUp.FormClosed += popUp_FormClosed;

        //    int desiredWidth = (int)(this.Width * 0.8);
        //    int desiredHeight = (int)(this.Height * 0.7);
        //    popUp.Size = new Size(desiredWidth, desiredHeight);

        //    popUp.StartPosition = FormStartPosition.CenterParent;
        //    popUp.ShowDialog(this);
        //}

        private void AddLinkColumnIfNotExists(string columnName)
        {
            // Check if the column already exists
            //if (!gvResultConv.Columns.Cast<DataGridViewColumn>().Any(col => col.Name == columnName))
            //{
            //    // Add a link column
            //    DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
            //    linkColumn.HeaderText = columnName;
            //    linkColumn.Name = columnName;
            //    linkColumn.DataPropertyName = columnName; // Bind the column values as link text

            //    linkColumn.LinkColor = Color.Blue;
            //    linkColumn.TrackVisitedState = false;
            //    //linkColumn.UseColumnTextForLinkValue = true;
            //    linkColumn.LinkBehavior = LinkBehavior.AlwaysUnderline;

            //    gvResultConv.Columns.Insert(2, linkColumn); // Insert at the third position (index 2)
            //}
            var existingColumn = gvResultConv.Columns.Cast<DataGridViewColumn>().FirstOrDefault(col => col.Name == columnName);

            if (existingColumn != null && !(existingColumn is DataGridViewLinkColumn))
            {
                // If the existing column is not a link column, replace it with a link column
                int columnIndex = existingColumn.Index;
                gvResultConv.Columns.Remove(existingColumn);

                DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
                linkColumn.HeaderText = columnName;
                linkColumn.Name = columnName;
                linkColumn.DataPropertyName = columnName; // Bind the column values as link text

                // Customize the link appearance
                linkColumn.LinkColor = Color.Blue;
                linkColumn.TrackVisitedState = false;
                linkColumn.LinkBehavior = LinkBehavior.AlwaysUnderline;

                gvResultConv.Columns.Insert(columnIndex, linkColumn);
            }

        }


        #endregion

        #region Events
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                txtLog.Text = System.Environment.NewLine + "---[" + DateTime.Now + "]--- Normal: " + "Process start." + txtLog.Text;
                csDatabase.Log("---[" + DateTime.Now + "]--- Normal: " + "Process start.");

                //Boolean blConn = pingIP();
                //if (blConn == true)
                //{
                //    do_process();
                //}
                ping_tick(sender, e);

                if (iPisConnInterval < 3000)
                {
                    iPisConnInterval = 3000;
                }

                ping.Interval = iPisConnInterval;
                ping.Tick += new EventHandler(this.ping_tick);
                ping.Start();

                //process.Interval = 30000;
                //process.Tick += new EventHandler(this.process_tick);
                //process.Start();
            }
            catch (Exception ex)
            {
                csDatabase.Log(ex);
                MessageBox.Show("Unable to start process.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            stop_process();
        }

        private void process_tick(object sender, EventArgs e)
        {
            Boolean blConn = pingIP();
            if (blConn == true)
            {
                do_process();
            }
        }
        #endregion

        public class PassIn
        {
            public string PlcNo { get; set; }
            public string URN { get; set; }
            public string GwNo { get; set; }
        }
    }
}