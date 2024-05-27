namespace Perodua
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblNow = new System.Windows.Forms.Label();
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.gbStatusInd = new System.Windows.Forms.GroupBox();
            this.pbGenIns = new System.Windows.Forms.PictureBox();
            this.pbPisConn = new System.Windows.Forms.PictureBox();
            this.pbPulling = new System.Windows.Forms.PictureBox();
            this.lblPulling = new System.Windows.Forms.Label();
            this.lblGenIns = new System.Windows.Forms.Label();
            this.lblPisConn = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbPisTracking = new System.Windows.Forms.GroupBox();
            this.gvPisTracking = new System.Windows.Forms.DataGridView();
            this.gbResultConv = new System.Windows.Forms.GroupBox();
            this.gvResultConv = new System.Windows.Forms.DataGridView();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnPopUp = new System.Windows.Forms.Button();
            this.gbStatusInd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGenIns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPisConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPulling)).BeginInit();
            this.gbPisTracking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPisTracking)).BeginInit();
            this.gbResultConv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvResultConv)).BeginInit();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNow
            // 
            this.lblNow.AutoSize = true;
            this.lblNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNow.Location = new System.Drawing.Point(18, 25);
            this.lblNow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNow.Name = "lblNow";
            this.lblNow.Size = new System.Drawing.Size(65, 25);
            this.lblNow.TabIndex = 0;
            this.lblNow.Text = "NOW";
            // 
            // txtTimer
            // 
            this.txtTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimer.Location = new System.Drawing.Point(120, 20);
            this.txtTimer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(253, 30);
            this.txtTimer.TabIndex = 1;
            this.txtTimer.Text = "0000-00-00     00:00:00";
            this.txtTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbStatusInd
            // 
            this.gbStatusInd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStatusInd.Controls.Add(this.pbGenIns);
            this.gbStatusInd.Controls.Add(this.pbPisConn);
            this.gbStatusInd.Controls.Add(this.pbPulling);
            this.gbStatusInd.Controls.Add(this.lblPulling);
            this.gbStatusInd.Controls.Add(this.lblGenIns);
            this.gbStatusInd.Controls.Add(this.lblPisConn);
            this.gbStatusInd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStatusInd.Location = new System.Drawing.Point(765, 0);
            this.gbStatusInd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbStatusInd.Name = "gbStatusInd";
            this.gbStatusInd.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbStatusInd.Size = new System.Drawing.Size(501, 68);
            this.gbStatusInd.TabIndex = 2;
            this.gbStatusInd.TabStop = false;
            this.gbStatusInd.Text = "Status Indicator";
            // 
            // pbGenIns
            // 
            this.pbGenIns.Image = global::Perodua.Properties.Resources.yellow;
            this.pbGenIns.Location = new System.Drawing.Point(134, 29);
            this.pbGenIns.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbGenIns.Name = "pbGenIns";
            this.pbGenIns.Size = new System.Drawing.Size(30, 31);
            this.pbGenIns.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGenIns.TabIndex = 5;
            this.pbGenIns.TabStop = false;
            // 
            // pbPisConn
            // 
            this.pbPisConn.Image = global::Perodua.Properties.Resources.yellow;
            this.pbPisConn.Location = new System.Drawing.Point(320, 29);
            this.pbPisConn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPisConn.Name = "pbPisConn";
            this.pbPisConn.Size = new System.Drawing.Size(30, 31);
            this.pbPisConn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPisConn.TabIndex = 6;
            this.pbPisConn.TabStop = false;
            // 
            // pbPulling
            // 
            this.pbPulling.Image = global::Perodua.Properties.Resources.yellow;
            this.pbPulling.Location = new System.Drawing.Point(9, 29);
            this.pbPulling.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPulling.Name = "pbPulling";
            this.pbPulling.Size = new System.Drawing.Size(30, 31);
            this.pbPulling.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPulling.TabIndex = 5;
            this.pbPulling.TabStop = false;
            // 
            // lblPulling
            // 
            this.lblPulling.AutoSize = true;
            this.lblPulling.Location = new System.Drawing.Point(42, 37);
            this.lblPulling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPulling.Name = "lblPulling";
            this.lblPulling.Size = new System.Drawing.Size(66, 20);
            this.lblPulling.TabIndex = 5;
            this.lblPulling.Text = "Pulling";
            // 
            // lblGenIns
            // 
            this.lblGenIns.AutoSize = true;
            this.lblGenIns.Location = new System.Drawing.Point(168, 35);
            this.lblGenIns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGenIns.Name = "lblGenIns";
            this.lblGenIns.Size = new System.Drawing.Size(135, 20);
            this.lblGenIns.TabIndex = 6;
            this.lblGenIns.Text = "Generate Instr.";
            // 
            // lblPisConn
            // 
            this.lblPisConn.AutoSize = true;
            this.lblPisConn.Location = new System.Drawing.Point(352, 35);
            this.lblPisConn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPisConn.Name = "lblPisConn";
            this.lblPisConn.Size = new System.Drawing.Size(138, 20);
            this.lblPisConn.TabIndex = 7;
            this.lblPisConn.Text = "PIS Connection";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(417, 14);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 46);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(543, 14);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 46);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gbPisTracking
            // 
            this.gbPisTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPisTracking.Controls.Add(this.gvPisTracking);
            this.gbPisTracking.Location = new System.Drawing.Point(22, 69);
            this.gbPisTracking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbPisTracking.Name = "gbPisTracking";
            this.gbPisTracking.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbPisTracking.Size = new System.Drawing.Size(1244, 391);
            this.gbPisTracking.TabIndex = 11;
            this.gbPisTracking.TabStop = false;
            this.gbPisTracking.Text = "Pis Tracking";
            // 
            // gvPisTracking
            // 
            this.gvPisTracking.AllowUserToAddRows = false;
            this.gvPisTracking.AllowUserToDeleteRows = false;
            this.gvPisTracking.AllowUserToResizeRows = false;
            this.gvPisTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPisTracking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvPisTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPisTracking.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvPisTracking.Location = new System.Drawing.Point(9, 29);
            this.gvPisTracking.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvPisTracking.Name = "gvPisTracking";
            this.gvPisTracking.ReadOnly = true;
            this.gvPisTracking.RowHeadersVisible = false;
            this.gvPisTracking.RowHeadersWidth = 62;
            this.gvPisTracking.RowTemplate.Height = 18;
            this.gvPisTracking.RowTemplate.ReadOnly = true;
            this.gvPisTracking.Size = new System.Drawing.Size(1226, 354);
            this.gvPisTracking.TabIndex = 9;
            // 
            // gbResultConv
            // 
            this.gbResultConv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResultConv.Controls.Add(this.gvResultConv);
            this.gbResultConv.Location = new System.Drawing.Point(22, 462);
            this.gbResultConv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbResultConv.Name = "gbResultConv";
            this.gbResultConv.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbResultConv.Size = new System.Drawing.Size(1244, 394);
            this.gbResultConv.TabIndex = 12;
            this.gbResultConv.TabStop = false;
            this.gbResultConv.Text = "Result Conversion";
            // 
            // gvResultConv
            // 
            this.gvResultConv.AllowUserToAddRows = false;
            this.gvResultConv.AllowUserToDeleteRows = false;
            this.gvResultConv.AllowUserToResizeRows = false;
            this.gvResultConv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvResultConv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvResultConv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvResultConv.DefaultCellStyle = dataGridViewCellStyle4;
            this.gvResultConv.Location = new System.Drawing.Point(9, 29);
            this.gvResultConv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gvResultConv.Name = "gvResultConv";
            this.gvResultConv.ReadOnly = true;
            this.gvResultConv.RowHeadersVisible = false;
            this.gvResultConv.RowHeadersWidth = 62;
            this.gvResultConv.Size = new System.Drawing.Size(1226, 354);
            this.gvResultConv.TabIndex = 10;
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Location = new System.Drawing.Point(22, 858);
            this.gbLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLog.Name = "gbLog";
            this.gbLog.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbLog.Size = new System.Drawing.Size(1244, 212);
            this.gbLog.TabIndex = 13;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(9, 29);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(1224, 173);
            this.txtLog.TabIndex = 12;
            this.txtLog.WordWrap = false;
            
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1290, 1102);
            this.Controls.Add(this.btnPopUp);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbResultConv);
            this.Controls.Add(this.gbPisTracking);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.gbStatusInd);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.lblNow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Perodua Dps Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbStatusInd.ResumeLayout(false);
            this.gbStatusInd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGenIns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPisConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPulling)).EndInit();
            this.gbPisTracking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvPisTracking)).EndInit();
            this.gbResultConv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvResultConv)).EndInit();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.TextBox txtTimer;
        private System.Windows.Forms.GroupBox gbStatusInd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPulling;
        private System.Windows.Forms.Label lblGenIns;
        private System.Windows.Forms.Label lblPisConn;
        private System.Windows.Forms.PictureBox pbGenIns;
        private System.Windows.Forms.PictureBox pbPisConn;
        private System.Windows.Forms.PictureBox pbPulling;
        private System.Windows.Forms.GroupBox gbPisTracking;
        private System.Windows.Forms.DataGridView gvPisTracking;
        private System.Windows.Forms.GroupBox gbResultConv;
        private System.Windows.Forms.DataGridView gvResultConv;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnPopUp;
    }
}

