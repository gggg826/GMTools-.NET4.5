namespace GameToolsClient
{
    partial class AIAndKismetCdnManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// <summary>
        /// 必需的设计器变量。
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
            this.gvDataList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selected = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.btnUpload = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnCreateCDN = new DevComponents.DotNetBar.ButtonX();
            this.btnCreateAI = new DevComponents.DotNetBar.ButtonItem();
            this.btnCreateKismet = new DevComponents.DotNetBar.ButtonItem();
            this.tbPackageName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lbVersion = new DevComponents.DotNetBar.LabelX();
            this.tbVersion = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataList)).BeginInit();
            this.SuspendLayout();
            // 
            // gvDataList
            // 
            this.gvDataList.AllowUserToAddRows = false;
            this.gvDataList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gvDataList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvDataList.BackgroundColor = System.Drawing.Color.White;
            this.gvDataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDataList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDataList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.selected});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvDataList.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvDataList.EnableHeadersVisualStyles = false;
            this.gvDataList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.gvDataList.Location = new System.Drawing.Point(7, 145);
            this.gvDataList.Name = "gvDataList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvDataList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvDataList.RowHeadersVisible = false;
            this.gvDataList.RowTemplate.Height = 23;
            this.gvDataList.Size = new System.Drawing.Size(453, 332);
            this.gvDataList.TabIndex = 74;
            this.gvDataList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvDataList_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "文件名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "存放路径";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 210;
            // 
            // selected
            // 
            this.selected.HeaderText = "";
            this.selected.Image = global::GameToolsClient.Properties.Resources.delete;
            this.selected.Name = "selected";
            this.selected.Width = 20;
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Image = global::GameToolsClient.Properties.Resources.edit;
            this.btnView.Location = new System.Drawing.Point(374, 87);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(82, 23);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnView.TabIndex = 78;
            this.btnView.Text = "浏览(&V)";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpload.Image = global::GameToolsClient.Properties.Resources.system_software_update_3;
            this.btnUpload.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnUpload.Location = new System.Drawing.Point(177, 498);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(100, 28);
            this.btnUpload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpload.TabIndex = 81;
            this.btnUpload.Text = "上传数据";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Image = global::GameToolsClient.Properties.Resources.dialog_cancel_3;
            this.btnCancel.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnCancel.Location = new System.Drawing.Point(294, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 80;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateCDN
            // 
            this.btnCreateCDN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCreateCDN.AutoExpandOnClick = true;
            this.btnCreateCDN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCreateCDN.Image = global::GameToolsClient.Properties.Resources.package;
            this.btnCreateCDN.ImageFixedSize = new System.Drawing.Size(24, 24);
            this.btnCreateCDN.Location = new System.Drawing.Point(43, 498);
            this.btnCreateCDN.Name = "btnCreateCDN";
            this.btnCreateCDN.Size = new System.Drawing.Size(116, 28);
            this.btnCreateCDN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCreateCDN.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCreateAI,
            this.btnCreateKismet});
            this.btnCreateCDN.TabIndex = 79;
            this.btnCreateCDN.Text = "生成CDN数据";
            // 
            // btnCreateAI
            // 
            this.btnCreateAI.GlobalItem = false;
            this.btnCreateAI.Name = "btnCreateAI";
            this.btnCreateAI.Text = "生成AI数据";
            this.btnCreateAI.Click += new System.EventHandler(this.btnCreateAI_Click);
            // 
            // btnCreateKismet
            // 
            this.btnCreateKismet.GlobalItem = false;
            this.btnCreateKismet.Name = "btnCreateKismet";
            this.btnCreateKismet.Text = "生成Kismet数据";
            this.btnCreateKismet.Click += new System.EventHandler(this.btnCreateKismet_Click);
            // 
            // tbPackageName
            // 
            this.tbPackageName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tbPackageName.Border.Class = "TextBoxBorder";
            this.tbPackageName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbPackageName.ButtonCustom.Image = global::GameToolsClient.Properties.Resources.user_new;
            this.tbPackageName.ButtonCustom.Visible = true;
            this.tbPackageName.ForeColor = System.Drawing.Color.Black;
            this.tbPackageName.Location = new System.Drawing.Point(91, 49);
            this.tbPackageName.Name = "tbPackageName";
            this.tbPackageName.Size = new System.Drawing.Size(365, 22);
            this.tbPackageName.TabIndex = 83;
            this.tbPackageName.ButtonCustomClick += new System.EventHandler(this.tbPackageName_ButtonCustomClick);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(15, 49);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(81, 23);
            this.labelX1.TabIndex = 82;
            this.labelX1.Text = "打包文件名";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(15, 87);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(81, 23);
            this.labelX2.TabIndex = 84;
            this.labelX2.Text = "文件列表";
            // 
            // lbVersion
            // 
            // 
            // 
            // 
            this.lbVersion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbVersion.Location = new System.Drawing.Point(15, 116);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(70, 23);
            this.lbVersion.TabIndex = 60;
            this.lbVersion.Text = "文件版本";
            // 
            // tbVersion
            // 
            this.tbVersion.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.tbVersion.Border.Class = "TextBoxBorder";
            this.tbVersion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbVersion.ButtonCustom.Image = global::GameToolsClient.Properties.Resources.edit;
            this.tbVersion.ForeColor = System.Drawing.Color.Black;
            this.tbVersion.Location = new System.Drawing.Point(91, 116);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.ReadOnly = true;
            this.tbVersion.Size = new System.Drawing.Size(365, 21);
            this.tbVersion.TabIndex = 59;
            // 
            // AIAndKismetCdnManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 552);
            this.CloseBox = true;
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.tbPackageName);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateCDN);
            this.Controls.Add(this.gvDataList);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.tbVersion);
            this.Controls.Add(this.lbVersion);
            this.Name = "AIAndKismetCdnManager";
            this.Controls.SetChildIndex(this.lbVersion, 0);
            this.Controls.SetChildIndex(this.tbVersion, 0);
            this.Controls.SetChildIndex(this.btnView, 0);
            this.Controls.SetChildIndex(this.gvDataList, 0);
            this.Controls.SetChildIndex(this.btnCreateCDN, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.btnUpload, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.tbPackageName, 0);
            this.Controls.SetChildIndex(this.labelX2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gvDataList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX gvDataList;
        private DevComponents.DotNetBar.ButtonX btnView;
        private DevComponents.DotNetBar.ButtonX btnUpload;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnCreateCDN;
        private DevComponents.DotNetBar.Controls.TextBoxX tbPackageName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonItem btnCreateAI;
        private DevComponents.DotNetBar.ButtonItem btnCreateKismet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewImageColumn selected;
        private DevComponents.DotNetBar.LabelX lbVersion;
        private DevComponents.DotNetBar.Controls.TextBoxX tbVersion;
    }
}