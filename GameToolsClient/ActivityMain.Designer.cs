﻿namespace GameToolsClient
{
    partial class ActivityMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.metroTilePanel1 = new DevComponents.DotNetBar.Metro.MetroTilePanel();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.mtiActivityImport = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.mtiActivityDetail = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.mtiSolution = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.mtiRecommendManager = new DevComponents.DotNetBar.Metro.MetroTileItem();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelX1.Location = new System.Drawing.Point(166, 19);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(113, 23);
            this.labelX1.TabIndex = 50;
            this.labelX1.Text = "活动配置";
            // 
            // metroTilePanel1
            // 
            // 
            // 
            // 
            this.metroTilePanel1.BackgroundStyle.Class = "MetroTilePanel";
            this.metroTilePanel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroTilePanel1.ContainerControlProcessDialogKey = true;
            this.metroTilePanel1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.metroTilePanel1.Location = new System.Drawing.Point(12, 60);
            this.metroTilePanel1.Name = "metroTilePanel1";
            this.metroTilePanel1.Size = new System.Drawing.Size(418, 256);
            this.metroTilePanel1.TabIndex = 44;
            this.metroTilePanel1.Text = "metroTilePanel1";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.itemContainer1.ItemSpacing = 20;
            this.itemContainer1.MultiLine = true;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.mtiActivityImport,
            this.mtiActivityDetail,
            this.mtiSolution,
            this.mtiRecommendManager});
            // 
            // mtiActivityImport
            // 
            this.mtiActivityImport.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mtiActivityImport.Name = "mtiActivityImport";
            this.mtiActivityImport.Text = "活动导入";
            this.mtiActivityImport.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Green;
            this.mtiActivityImport.TileSize = new System.Drawing.Size(180, 100);
            // 
            // 
            // 
            this.mtiActivityImport.TileStyle.BackgroundImageAlpha = ((byte)(30));
            this.mtiActivityImport.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.mtiActivityImport.Click += new System.EventHandler(this.mtiActivityImport_Click);
            // 
            // mtiActivityDetail
            // 
            this.mtiActivityDetail.Name = "mtiActivityDetail";
            this.mtiActivityDetail.Text = "活动条目";
            this.mtiActivityDetail.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Default;
            // 
            // 
            // 
            this.mtiActivityDetail.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.mtiActivityDetail.Click += new System.EventHandler(this.mtiActivityDetail_Click);
            // 
            // mtiSolution
            // 
            this.mtiSolution.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mtiSolution.Name = "mtiSolution";
            this.mtiSolution.Text = "方案条目";
            this.mtiSolution.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Magenta;
            this.mtiSolution.TileSize = new System.Drawing.Size(180, 100);
            // 
            // 
            // 
            this.mtiSolution.TileStyle.BackgroundImageAlpha = ((byte)(40));
            this.mtiSolution.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.mtiSolution.Click += new System.EventHandler(this.mtiSolution_Click);
            // 
            // mtiRecommendManager
            // 
            this.mtiRecommendManager.ImageTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mtiRecommendManager.Name = "mtiRecommendManager";
            this.mtiRecommendManager.Text = "推荐管理";
            this.mtiRecommendManager.TileColor = DevComponents.DotNetBar.Metro.eMetroTileColor.Teal;
            this.mtiRecommendManager.TileSize = new System.Drawing.Size(180, 100);
            // 
            // 
            // 
            this.mtiRecommendManager.TileStyle.BackgroundImageAlpha = ((byte)(40));
            this.mtiRecommendManager.TileStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.mtiRecommendManager.Click += new System.EventHandler(this.mtiRecommendManager_Click);
            // 
            // ActivityMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(444, 338);
            this.CloseBox = true;
            this.Controls.Add(this.metroTilePanel1);
            this.Controls.Add(this.labelX1);
            this.IsShowCaptionImage = false;
            this.IsShowCaptionText = false;
            this.IsTitleSplitLine = false;
            this.Name = "ActivityMain";
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.metroTilePanel1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Metro.MetroTilePanel metroTilePanel1;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.Metro.MetroTileItem mtiActivityImport;
        private DevComponents.DotNetBar.Metro.MetroTileItem mtiSolution;
        private DevComponents.DotNetBar.Metro.MetroTileItem mtiRecommendManager;
        private DevComponents.DotNetBar.Metro.MetroTileItem mtiActivityDetail;
    }
}
