namespace DEKI_LabelPrinting
{
    partial class FormWending
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWending));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbSyncvOrders = new System.Windows.Forms.PictureBox();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.txtItemNo = new System.Windows.Forms.TextBox();
            this.txtRoutingNo = new System.Windows.Forms.TextBox();
            this.pbRefresh = new System.Windows.Forms.PictureBox();
            this.cbProductionNo = new System.Windows.Forms.ComboBox();
            this.cbWorkCenterGroup = new System.Windows.Forms.ComboBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.dgvWeight = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblGrossWeight = new System.Windows.Forms.Label();
            this.lblNetWeight = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PktNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PacketWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtOperationNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProductionDate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblItemWeight = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.pbSearchOrderNo = new System.Windows.Forms.PictureBox();
            this.timerGetWeight = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblWeightTolerance = new System.Windows.Forms.Label();
            this.lblWeightInclTolerance = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSyncvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchOrderNo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.step_bar;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pbSyncvOrders);
            this.panel1.Controls.Add(this.pbSettings);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 74);
            this.panel1.TabIndex = 2;
            // 
            // pbSyncvOrders
            // 
            this.pbSyncvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSyncvOrders.BackColor = System.Drawing.Color.Transparent;
            this.pbSyncvOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSyncvOrders.Image = global::DEKI_LabelPrinting.Properties.Resources._4771267;
            this.pbSyncvOrders.Location = new System.Drawing.Point(831, 5);
            this.pbSyncvOrders.Margin = new System.Windows.Forms.Padding(2);
            this.pbSyncvOrders.Name = "pbSyncvOrders";
            this.pbSyncvOrders.Size = new System.Drawing.Size(64, 64);
            this.pbSyncvOrders.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSyncvOrders.TabIndex = 5;
            this.pbSyncvOrders.TabStop = false;
            this.toolTip1.SetToolTip(this.pbSyncvOrders, "Sync Orders");
            this.pbSyncvOrders.Click += new System.EventHandler(this.pbSyncvOrders_Click);
            // 
            // pbSettings
            // 
            this.pbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSettings.BackColor = System.Drawing.Color.Transparent;
            this.pbSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSettings.Image = global::DEKI_LabelPrinting.Properties.Resources._4771267;
            this.pbSettings.Location = new System.Drawing.Point(912, 5);
            this.pbSettings.Margin = new System.Windows.Forms.Padding(2);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(64, 64);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSettings.TabIndex = 4;
            this.pbSettings.TabStop = false;
            this.toolTip1.SetToolTip(this.pbSettings, "Sync Items");
            this.pbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(62, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Back";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox3.Image = global::DEKI_LabelPrinting.Properties.Resources.Close64x64;
            this.pictureBox3.Location = new System.Drawing.Point(1072, 5);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox3, "Close Application");
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::DEKI_LabelPrinting.Properties.Resources.LogOut64X64;
            this.pictureBox2.Location = new System.Drawing.Point(992, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, "Signout Application");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::DEKI_LabelPrinting.Properties.Resources.left_arrow_32x32;
            this.pictureBox1.Location = new System.Drawing.Point(21, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Production No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(487, 186);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "LOT No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 186);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Item No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 143);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Routing No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(401, 144);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Work Center Group";
            // 
            // btnGetData
            // 
            this.btnGetData.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.lg;
            this.btnGetData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetData.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGetData.Location = new System.Drawing.Point(522, 375);
            this.btnGetData.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(597, 50);
            this.btnGetData.TabIndex = 8;
            this.btnGetData.Text = "Get Weight";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            this.btnGetData.Enter += new System.EventHandler(this.btnGetData_Enter);
            this.btnGetData.Leave += new System.EventHandler(this.btnGetData_Leave);
            this.btnGetData.MouseLeave += new System.EventHandler(this.btnGetData_MouseLeave);
            this.btnGetData.MouseHover += new System.EventHandler(this.btnGetData_MouseHover);
            // 
            // txtLotNo
            // 
            this.txtLotNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNo.Location = new System.Drawing.Point(558, 186);
            this.txtLotNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Size = new System.Drawing.Size(194, 28);
            this.txtLotNo.TabIndex = 7;
            // 
            // txtItemNo
            // 
            this.txtItemNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemNo.Location = new System.Drawing.Point(137, 186);
            this.txtItemNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(295, 28);
            this.txtItemNo.TabIndex = 6;
            // 
            // txtRoutingNo
            // 
            this.txtRoutingNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoutingNo.Location = new System.Drawing.Point(137, 141);
            this.txtRoutingNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtRoutingNo.Name = "txtRoutingNo";
            this.txtRoutingNo.Size = new System.Drawing.Size(262, 28);
            this.txtRoutingNo.TabIndex = 2;
            // 
            // pbRefresh
            // 
            this.pbRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRefresh.Image = global::DEKI_LabelPrinting.Properties.Resources.Refresh32x32;
            this.pbRefresh.Location = new System.Drawing.Point(388, 95);
            this.pbRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.pbRefresh.Name = "pbRefresh";
            this.pbRefresh.Size = new System.Drawing.Size(32, 32);
            this.pbRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRefresh.TabIndex = 14;
            this.pbRefresh.TabStop = false;
            this.toolTip1.SetToolTip(this.pbRefresh, "Refresh Order Nos");
            this.pbRefresh.Click += new System.EventHandler(this.pbRefresh_Click);
            // 
            // cbProductionNo
            // 
            this.cbProductionNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbProductionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductionNo.FormattingEnabled = true;
            this.cbProductionNo.Location = new System.Drawing.Point(137, 96);
            this.cbProductionNo.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductionNo.Name = "cbProductionNo";
            this.cbProductionNo.Size = new System.Drawing.Size(220, 30);
            this.cbProductionNo.TabIndex = 0;
            this.cbProductionNo.SelectedIndexChanged += new System.EventHandler(this.cbProductionNo_SelectedIndexChanged);
            this.cbProductionNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbProductionNo_KeyUp);
            // 
            // cbWorkCenterGroup
            // 
            this.cbWorkCenterGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbWorkCenterGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWorkCenterGroup.FormattingEnabled = true;
            this.cbWorkCenterGroup.Location = new System.Drawing.Point(553, 141);
            this.cbWorkCenterGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cbWorkCenterGroup.Name = "cbWorkCenterGroup";
            this.cbWorkCenterGroup.Size = new System.Drawing.Size(198, 30);
            this.cbWorkCenterGroup.TabIndex = 3;
            this.cbWorkCenterGroup.SelectedIndexChanged += new System.EventHandler(this.cbWorkCenterGroup_SelectedIndexChanged);
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.Location = new System.Drawing.Point(524, 257);
            this.lblWeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(595, 118);
            this.lblWeight.TabIndex = 15;
            this.lblWeight.Text = "001.143";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvWeight
            // 
            this.dgvWeight.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWeight.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWeight.ColumnHeadersHeight = 50;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWeight.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWeight.Location = new System.Drawing.Point(26, 257);
            this.dgvWeight.Margin = new System.Windows.Forms.Padding(2);
            this.dgvWeight.Name = "dgvWeight";
            this.dgvWeight.RowHeadersVisible = false;
            this.dgvWeight.RowHeadersWidth = 51;
            this.dgvWeight.RowTemplate.Height = 24;
            this.dgvWeight.Size = new System.Drawing.Size(446, 436);
            this.dgvWeight.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(26, 229);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 20);
            this.label7.TabIndex = 17;
            this.label7.Text = "Weighing Details";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.blue;
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSubmit.Location = new System.Drawing.Point(520, 607);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(493, 72);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit Details";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label8.Location = new System.Drawing.Point(525, 434);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 31);
            this.label8.TabIndex = 19;
            this.label8.Text = "Gross Weight";
            // 
            // lblGrossWeight
            // 
            this.lblGrossWeight.AutoSize = true;
            this.lblGrossWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblGrossWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrossWeight.Location = new System.Drawing.Point(722, 433);
            this.lblGrossWeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGrossWeight.Name = "lblGrossWeight";
            this.lblGrossWeight.Size = new System.Drawing.Size(67, 31);
            this.lblGrossWeight.TabIndex = 20;
            this.lblGrossWeight.Text = "0.00";
            // 
            // lblNetWeight
            // 
            this.lblNetWeight.AutoSize = true;
            this.lblNetWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblNetWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetWeight.Location = new System.Drawing.Point(722, 497);
            this.lblNetWeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNetWeight.Name = "lblNetWeight";
            this.lblNetWeight.Size = new System.Drawing.Size(71, 33);
            this.lblNetWeight.TabIndex = 22;
            this.lblNetWeight.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(525, 497);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 31);
            this.label10.TabIndex = 21;
            this.label10.Text = "Net Weight";
            // 
            // PktNo
            // 
            this.PktNo.HeaderText = "Sr. No";
            this.PktNo.Name = "PktNo";
            // 
            // PacketWeight
            // 
            this.PacketWeight.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PacketWeight.FillWeight = 400F;
            this.PacketWeight.HeaderText = "Packet Weight";
            this.PacketWeight.Name = "PacketWeight";
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOperationNo.Location = new System.Drawing.Point(866, 141);
            this.txtOperationNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(76, 38);
            this.txtOperationNo.TabIndex = 4;
            this.txtOperationNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(759, 141);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "Operation No";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(991, 141);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(128, 38);
            this.txtQuantity.TabIndex = 5;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(952, 144);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 20);
            this.label12.TabIndex = 26;
            this.label12.Text = "Qty.";
            // 
            // txtProductionDate
            // 
            this.txtProductionDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductionDate.Location = new System.Drawing.Point(556, 96);
            this.txtProductionDate.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductionDate.Name = "txtProductionDate";
            this.txtProductionDate.Size = new System.Drawing.Size(197, 28);
            this.txtProductionDate.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(428, 100);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 20);
            this.label13.TabIndex = 28;
            this.label13.Text = "Production Date";
            // 
            // lblItemWeight
            // 
            this.lblItemWeight.AutoSize = true;
            this.lblItemWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblItemWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemWeight.Location = new System.Drawing.Point(862, 184);
            this.lblItemWeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemWeight.Name = "lblItemWeight";
            this.lblItemWeight.Size = new System.Drawing.Size(67, 31);
            this.lblItemWeight.TabIndex = 30;
            this.lblItemWeight.Text = "0.00";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(759, 189);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 20);
            this.label15.TabIndex = 29;
            this.label15.Text = "Item Weight";
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.red;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClear.Image = global::DEKI_LabelPrinting.Properties.Resources._899025;
            this.btnClear.Location = new System.Drawing.Point(1041, 605);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(78, 72);
            this.btnClear.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btnClear, "Delete Weighing Lines");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pbSearchOrderNo
            // 
            this.pbSearchOrderNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSearchOrderNo.Image = global::DEKI_LabelPrinting.Properties.Resources.Search_16x16;
            this.pbSearchOrderNo.Location = new System.Drawing.Point(359, 96);
            this.pbSearchOrderNo.Margin = new System.Windows.Forms.Padding(2);
            this.pbSearchOrderNo.Name = "pbSearchOrderNo";
            this.pbSearchOrderNo.Size = new System.Drawing.Size(24, 27);
            this.pbSearchOrderNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSearchOrderNo.TabIndex = 32;
            this.pbSearchOrderNo.TabStop = false;
            this.toolTip1.SetToolTip(this.pbSearchOrderNo, "Sarch Order NO");
            this.pbSearchOrderNo.Click += new System.EventHandler(this.pbSearchOrderNo_Click);
            // 
            // timerGetWeight
            // 
            this.timerGetWeight.Interval = 3000;
            this.timerGetWeight.Tick += new System.EventHandler(this.timerGetWeight_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(534, 468);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "(Item Weight X Quantity)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(529, 531);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(198, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "(Sum of Weight Captured (pkt weighing))";
            // 
            // lblWeightTolerance
            // 
            this.lblWeightTolerance.AutoSize = true;
            this.lblWeightTolerance.BackColor = System.Drawing.Color.Transparent;
            this.lblWeightTolerance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeightTolerance.Location = new System.Drawing.Point(534, 562);
            this.lblWeightTolerance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWeightTolerance.Name = "lblWeightTolerance";
            this.lblWeightTolerance.Size = new System.Drawing.Size(92, 13);
            this.lblWeightTolerance.TabIndex = 35;
            this.lblWeightTolerance.Text = "Weight Tolerance";
            // 
            // lblWeightInclTolerance
            // 
            this.lblWeightInclTolerance.AutoSize = true;
            this.lblWeightInclTolerance.BackColor = System.Drawing.Color.Transparent;
            this.lblWeightInclTolerance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeightInclTolerance.Location = new System.Drawing.Point(534, 588);
            this.lblWeightInclTolerance.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWeightInclTolerance.Name = "lblWeightInclTolerance";
            this.lblWeightInclTolerance.Size = new System.Drawing.Size(92, 13);
            this.lblWeightInclTolerance.TabIndex = 36;
            this.lblWeightInclTolerance.Text = "Weight Tolerance";
            // 
            // FormWending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DEKI_LabelPrinting.Properties.Resources.BG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1156, 705);
            this.Controls.Add(this.lblWeightInclTolerance);
            this.Controls.Add(this.lblWeightTolerance);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pbSearchOrderNo);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblItemWeight);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtProductionDate);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtOperationNo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblNetWeight);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblGrossWeight);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvWeight);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.cbWorkCenterGroup);
            this.Controls.Add(this.cbProductionNo);
            this.Controls.Add(this.pbRefresh);
            this.Controls.Add(this.txtRoutingNo);
            this.Controls.Add(this.txtItemNo);
            this.Controls.Add(this.txtLotNo);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormWending";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wending";
            this.Load += new System.EventHandler(this.FormWending_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormWending_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormWending_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSyncvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchOrderNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGetData;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.TextBox txtItemNo;
        private System.Windows.Forms.TextBox txtRoutingNo;
        private System.Windows.Forms.PictureBox pbRefresh;
        private System.Windows.Forms.ComboBox cbProductionNo;
        private System.Windows.Forms.ComboBox cbWorkCenterGroup;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.DataGridView dgvWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblGrossWeight;
        private System.Windows.Forms.Label lblNetWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn PktNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PacketWeight;
        private System.Windows.Forms.TextBox txtOperationNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProductionDate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.Label lblItemWeight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pbSyncvOrders;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Timer timerGetWeight;
        private System.Windows.Forms.PictureBox pbSearchOrderNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblWeightTolerance;
        private System.Windows.Forms.Label lblWeightInclTolerance;
    }
}