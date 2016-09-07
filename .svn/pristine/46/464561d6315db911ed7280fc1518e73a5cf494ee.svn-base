using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameTools.Util;
using System.IO;

namespace GameToolsClient
{
    public partial class AIAndKismetCdnManager : BaseForm
    {
        public AIAndKismetCdnManager()
        {
            InitializeComponent();
            this.Text = "AI/Kismet CDN打包";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsAcceptResize = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMostBox = false;
            this.IsShowCaptionImage = false;
            this.IsShowCaptionText = false;
            this.IsTitleSplitLine = false;
            this.tbVersion.Text = "1.1.9.60";
            this.Image = Properties.Resources.TK_2icon;
        }

        private string aiPath = "cache/AI/";
        private string kismetPath = "cache/Kismit/";
        private string aiPathTitle = "AI";

        private string cndFullName = string.Empty;
        private string dataFullName = string.Empty;

        private void btnView_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AI/Kismet|*.bytes";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gvDataList.Rows.Clear();
      
                string[] fileNames = openFileDialog.FileNames;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    FileInfo fi = new FileInfo(fileNames[i]);
                    int rowIndex = gvDataList.Rows.Add(Guid.NewGuid().ToString(),
                        fi.Name,
                        aiPathTitle);
                    gvDataList.Rows[rowIndex].Tag = fi.FullName;
                }
            }
        }

        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 3)
                {
                    gvDataList.Rows.RemoveAt(e.RowIndex);
                }
            }
        }


        private void CreateCDNData(int cdnType)
        {
            this.cndFullName = "";
            this.dataFullName = "";
            if (string.IsNullOrEmpty(tbPackageName.Text))
            {
                CustomMessageBox.Error(this, "打包文件名无效");
                return;
            }
            if (string.IsNullOrEmpty(tbVersion.Text))
            {
                CustomMessageBox.Error(this, "文件版本无效");
                return;
            }

            //if (gvDataList.RowCount < 1)
            //{
            //    CustomMessageBox.Error(this, "没有添加任何要打包的图片");
            //    return;
            //}
            List<string> aiList = GetAIList();
            byte[] manifestDatas = GenManifest(cdnType);
            Dictionary<string, byte[]> datas = new Dictionary<string, byte[]>();
            datas.Add("Manifest.txt", manifestDatas);
            byte[] bytes = CDNHelper.EncodeCDNFiles(aiList, datas);
            string cdnPackageName = tbPackageName.Text;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string cndFullName = fbd.SelectedPath + "\\" + cdnPackageName + ".txt";
                string dataFullName = fbd.SelectedPath + "\\" + cdnPackageName + ".zip";
                using (FileStream fs = File.Open(dataFullName, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
                string md5Hash = GlobalObject.GetMd5Hash(dataFullName);
                using (FileStream fs = File.Open(cndFullName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        string content = string.Empty;
                        content += "md5Hash=" + md5Hash + "\r\n";
                        content += "dataFileName=" + cdnPackageName + ".zip" + "\r\n";
                        sw.Write(content);
                    }
                }
                this.cndFullName = cndFullName;
                this.dataFullName = dataFullName;
                CustomMessageBox.Info(this, "生成成功");
            }
        }

        public List<string> GetAIList()
        {
            List<string> aiList = new List<string>();
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                string fileName = gvDataList.Rows[i].Tag as string;
                if (!string.IsNullOrEmpty(fileName))
                {
                    aiList.Add(fileName);
                }
            }
            return aiList;
        }


        public byte[] GenManifest(int cdnType)
        {
            StringBuilder manifest = new StringBuilder();
            aiPath = string.Format("{0}{1}/", aiPath, tbVersion.Text);
            kismetPath = string.Format("{0}{1}/", kismetPath, tbVersion.Text);
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                string aiName = gvDataList.Rows[i].Cells[1].Value.ToString();
                string fileName = gvDataList.Rows[i].Tag as string;
                if (!string.IsNullOrEmpty(fileName))
                {
                    string strDirectory = cdnType == 1 ? aiPath : kismetPath;
                    manifest.Append(aiName + "|" + strDirectory + (i == gvDataList.Rows.Count - 1 ? "" : "\r\n"));
                }
            }
            byte[] bytes = Encoding.UTF8.GetBytes(manifest.ToString());
            return bytes;
        }

        private void tbPackageName_ButtonCustomClick(object sender, EventArgs e)
        {
            tbPackageName.Text = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
        }
        UploadCDNProgress frmUploadCDNProgress = null;
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cndFullName))
            {
                CustomMessageBox.Error(this, "没有生成CDN数据");
                return;
            }
            if (string.IsNullOrEmpty(dataFullName))
            {
                CustomMessageBox.Error(this, "没有生成CDN数据");
                return;
            }
            frmUploadCDNProgress = new UploadCDNProgress();
            frmUploadCDNProgress.AddFile(cndFullName);
            frmUploadCDNProgress.AddFile(dataFullName);
            frmUploadCDNProgress.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateAI_Click(object sender, EventArgs e)
        {
            CreateCDNData(1);
        }

        private void btnCreateKismet_Click(object sender, EventArgs e)
        {
            CreateCDNData(2);
        }

    }
}
