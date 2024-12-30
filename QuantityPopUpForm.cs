using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Perodua.MainForm;

namespace Perodua
{
    public partial class QuantityPopUpForm : Form
    {
        public string UrnNo;
        public string plcNo;
        public string GwNo;
        private string selectedGwNo;
        private bool reOpenInd;
        public string SelectedGwNo
        {
            get { return selectedGwNo; }
        }
        public bool ReOpenInd
        {
            get { return reOpenInd; }
        }
        public QuantityPopUpForm(PassIn passIn)
        {
            UrnNo = passIn.URN;
            plcNo = passIn.PlcNo;
            GwNo = passIn.GwNo;

            InitializeComponent();
            //this.DoubleBuffered = true;
            //this.ResizeRedraw = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Resize += PopupForm_Resize;
            SetupControls();
            cbGwNo.SelectionChangeCommitted -= cbGwNo_SelectionChangeCommitted;
            cbGwNo.SelectionChangeCommitted += cbGwNo_SelectionChangeCommitted;
            PopulateTableLayoutPanel(GetQuantityValue());
        }

        private void cbGwNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Perform your function here when the ComboBox value changes or is selected
            selectedGwNo = cbGwNo.SelectedItem?.ToString();
            reOpenInd = true;
            //this.Close();
            this.DialogResult = DialogResult.OK;
            this.Close();
            //PopulateTableLayoutPanel(GetQuantityValue(selectedValue));
            //MessageBox.Show($"Selected Value: {selectedValue}");
        }

        private void PopupForm_Resize(object sender, EventArgs e)
        {
            // Adjust the GroupBox's size when the form is resized
            UpdateGroupBoxSize();
        }

        private void UpdateGroupBoxSize()
        {
            // Set the height to 80% of the form's height
            gbQuantity.Height = (int)(this.ClientSize.Height * 0.8);
            // Set the width to match the form's width
            gbQuantity.Width = this.ClientSize.Width;
        }

        private int[] GetQuantityValue()
        {
            DataSet dsDpsQuantity = new DataSet();
            DataTable dtDpsQuantity = new DataTable();

            dsDpsQuantity = csDatabase.SrcQuantityGearUp(UrnNo, plcNo, GwNo);

            dtDpsQuantity = dsDpsQuantity.Tables[0];

            int[] data = new int[64];
            for (int i = 0; i < data.Length; i++)
            {
                string columnName = $"QtyGw{GwNo}Lm{i + 1}";
                if (dtDpsQuantity.Columns.Contains(columnName))
                {
                    data[i] = dtDpsQuantity.Rows[0].Field<int?>(columnName) ?? 0;
                }

            }
            return data;
        }

        private ComboBox cbGwNo;
        private TextBox llUrn;
        private TextBox tbPlcNo;
        private TableLayoutPanel DetailsPanel;
        private void SetupControls()
        {
            // Create and configure the TableLayoutPanel
            DetailsPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                RowCount = 1,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            // Add columns
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            DetailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            // Create and configure the ComboBox for GW No
            cbGwNo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 50,
            };
            DataSet GwNoDs = new DataSet();
            DataTable GwNoDt = new DataTable();

            GwNoDs = csDatabase.GetBlockGW_DD();
            GwNoDt = GwNoDs.Tables[0];

            foreach (DataRow row in GwNoDt.Rows)
            {
                cbGwNo.Items.Add(row["desc"].ToString()); // Add the "desc" column values
            }

            //cbGwNo.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            if (!string.IsNullOrEmpty(GwNo) && cbGwNo.Items.Contains(GwNo))
            {
                cbGwNo.SelectedItem = GwNo;
            }
            else
            {
                cbGwNo.SelectedIndex = 0;
            }

            // Create and configure the LinkLabel for URN
            llUrn = new TextBox
            {
                Text = UrnNo,
                Width = 150,
            };

            // Create and configure the TextBox for Plc No
            tbPlcNo = new TextBox
            {
                Width = 50,
                Text = plcNo // Example Plc No
            };

            // Add controls to the TableLayoutPanel
            DetailsPanel.Controls.Add(new Label { Text = "GW No", Anchor = AnchorStyles.Right, AutoSize = true }, 0, 0);
            DetailsPanel.Controls.Add(cbGwNo, 1, 0);
            cbGwNo.Anchor = AnchorStyles.Left;
            DetailsPanel.Controls.Add(new Label { Text = "URN", Anchor = AnchorStyles.Right, AutoSize = true }, 2, 0);
            DetailsPanel.Controls.Add(llUrn, 3, 0);
            llUrn.Anchor = AnchorStyles.Left;
            DetailsPanel.Controls.Add(new Label { Text = "Plc No", Anchor = AnchorStyles.Right, AutoSize = true }, 4, 0);
            DetailsPanel.Controls.Add(tbPlcNo, 5, 0);
            tbPlcNo.Anchor = AnchorStyles.Left;

            llUrn.ReadOnly = true;
            tbPlcNo.ReadOnly = true;

            //DetailsPanel.BackColor = Color.Red;
            //DetailsPanel.Dock = DockStyle.Top;
            DetailsPanel.Dock = DockStyle.Fill;
            //DetailsPanel.Height = (int)(this.ClientSize.Height * 0.05);

            // Add the TableLayoutPanel to the form
            this.Controls.Add(DetailsPanel);
        }

        GroupBox gbQuantity = new GroupBox();
        private void PopulateTableLayoutPanel(int[] values)
        {
            int valueCount = values.Length;
            int columnCount = 10;
            int rowCount = (int)Math.Ceiling(valueCount / (double)columnCount) * 2; // Double the row count for labels and values

            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = columnCount,
                RowCount = rowCount,
                Dock = DockStyle.Fill,
                //AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            //tableLayoutPanel.SuspendLayout();

            for (int i = 0; i < columnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columnCount));
            }
            for (int i = 0; i < rowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rowCount));
            }

            for (int i = 0; i < valueCount; i++)
            {
                Label label = new Label
                {
                    Name = $"LabelL{i + 1}",
                    Text = $"L{i + 1}",
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Font = new Font(this.Font, FontStyle.Bold),
                    Margin = new Padding(0)
                    //BorderStyle = BorderStyle.FixedSingle
                };

                Label value = new Label
                {
                    Name = $"ValueLabel{i + 1}",
                    Text = values[i].ToString(),
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    //BorderStyle = BorderStyle.FixedSingle
                };

                if (i % 2 == 0 || i % 2 == 1)
                {
                    label.BackColor = Color.LightGray; // Set the background color for even rows
                }

                tableLayoutPanel.Controls.Add(label, i % columnCount, (i / columnCount) * 2);
                tableLayoutPanel.Controls.Add(value, i % columnCount, (i / columnCount) * 2 + 1);
            }

            for (int row = 0; row < tableLayoutPanel.RowCount; row++)
            {
                for (int col = 0; col < tableLayoutPanel.ColumnCount; col++)
                {
                    if (tableLayoutPanel.GetControlFromPosition(col, row) == null)
                    {
                        Label emptyLabel = new Label
                        {
                            Dock = DockStyle.Fill,
                            Margin = new Padding(0),
                            BackColor = Color.LightGray,
                        };
                        tableLayoutPanel.Controls.Add(emptyLabel, col, row);
                    }
                }
            }

            //tableLayoutPanel.ResumeLayout();

            Panel borderPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle // Add border to the panel
            };

            borderPanel.Controls.Add(tableLayoutPanel);

            borderPanel.Height = this.ClientSize.Height;
            borderPanel.Width = this.ClientSize.Width;
            //borderPanel.BackColor = Color.Green;

            gbQuantity.Controls.Add(borderPanel);
            gbQuantity.Dock = DockStyle.Bottom;
            gbQuantity.Text = "Parts Quantity";
            //gbQuantity.BackColor = Color.Yellow;
            gbQuantity.Width = this.ClientSize.Width;
            gbQuantity.Height = this.ClientSize.Height;


            this.Controls.Add(gbQuantity);
        }
    }
}
