using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelHelper
{
    public class ExcelHelper
    {
        /// <summary>
        /// 读取物品表
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <returns></returns>
        public static List<FengNiao.GMTools.Database.Model.tbl_item> GetItemTable(string excelFilePath, string sheetName)
        {
            List<FengNiao.GMTools.Database.Model.tbl_item> items = new List<FengNiao.GMTools.Database.Model.tbl_item>();
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档

            IWorkbook workbook = null;
            try
            {
                //2003以下的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new HSSFWorkbook(stream);
                }
            }
            catch
            {
                //2007-2010的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new XSSFWorkbook(stream);
                }
            }


            for (int n = 0; n < workbook.NumberOfSheets; n++)
            {

                //获取与之相对应的Excel工作表
                ISheet sheet = workbook.GetSheetAt(n);
                if (!sheet.SheetName.Equals(sheetName))
                {
                    continue;
                }
                int rowCount = sheet.LastRowNum;
                try
                {
                    for (int i = 4; i < sheet.LastRowNum + 1; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            ICell idCell = row.GetCell(0);
                            ICell nameCell = row.GetCell(1);
                            ICell descCell = row.GetCell(2);
                            if (idCell != null && nameCell != null)
                            {
                                string strID = idCell.ToString();
                                string strName = nameCell.ToString();
                                string strDesc = string.Empty;
                                if (descCell != null)
                                {
                                    strDesc = descCell.ToString();
                                }
                                FengNiao.GMTools.Database.Model.tbl_item item = new FengNiao.GMTools.Database.Model.tbl_item();
                                int id = -1;
                                if (int.TryParse(strID, out id))
                                {
                                    item.item_id = id;
                                    item.name = strName;
                                    item.describe = strDesc;
                                    items.Add(item);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }

                sheet = null;
            }

            workbook = null;


            return items;
        }




        public static List<FengNiao.GMTools.Database.Model.tbl_activity> GetActivityTable(string excelFilePath, string sheetName)
        {
            List<FengNiao.GMTools.Database.Model.tbl_activity> activitys = new List<FengNiao.GMTools.Database.Model.tbl_activity>();
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档

            IWorkbook workbook = null;
            try
            {
                //2003以下的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new HSSFWorkbook(stream);
                }
            }
            catch
            {
                //2007-2010的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new XSSFWorkbook(stream);
                }
            }


            for (int n = 0; n < workbook.NumberOfSheets; n++)
            {

                //获取与之相对应的Excel工作表
                ISheet sheet = workbook.GetSheetAt(n);
                if (!sheet.SheetName.Equals(sheetName))
                {
                    continue;
                }
                int rowCount = sheet.LastRowNum;
                try
                {
                    for (int i = 4; i < sheet.LastRowNum + 1; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            ICell idCell = row.GetCell(0);
                            ICell nameCell = row.GetCell(5);
                            if (idCell != null && nameCell != null)
                            {
                                string strID = idCell.ToString();
                                string strName = nameCell.ToString();
                                string strDesc = string.Empty;

                                FengNiao.GMTools.Database.Model.tbl_activity activity = new FengNiao.GMTools.Database.Model.tbl_activity();
                                int id = -1;
                                if (int.TryParse(strID, out id))
                                {
                                    activity.id = id;
                                    activity.name = strName;
                                    activitys.Add(activity);
                                }
                            }
                        }
                    }
                }
                catch (Exception )
                {
                }

                sheet = null;
            }

            workbook = null;


            return activitys;
        }

        public static string textPath;
        public static string GetRoleID(string excelFilePath, string sheetName)
        {
            //List<FengNiao.GMTools.Database.Model.tbl_activity> activitys = new List<FengNiao.GMTools.Database.Model.tbl_activity>();
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档

            IWorkbook workbook = null;
            try
            {
                //2003以下的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new HSSFWorkbook(stream);
                }
            }
            catch
            {
                //2007-2010的Excel
                using (Stream stream = File.Open(excelFilePath, FileMode.Open))
                {
                    workbook = new XSSFWorkbook(stream);
                }
            }

            //string floderPath = AppDomain.CurrentDomain.BaseDirectory;
            string floderPath = "D:\\RoleID";
            if (!Directory.Exists(floderPath))
                Directory.CreateDirectory(floderPath);
            string timeNow = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            textPath = string.Format("{0}\\{1}.txt",
                                            floderPath,
                                            timeNow);

            FileStream fs = new FileStream(textPath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
          
           
            for (int n = 0; n < workbook.NumberOfSheets; n++)
            {

                //获取与之相对应的Excel工作表
                ISheet sheet = workbook.GetSheetAt(n);
                if (!sheet.SheetName.Equals(sheetName))
                {
                    continue;
                }
                int rowCount = sheet.LastRowNum;
                try
                {
                    for (int i = 0; i < sheet.LastRowNum + 1; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            //ICell idCell = row.GetCell(0);
                            ICell nameCell = row.GetCell(1);
                            if (nameCell != null)
                            {
                                //开始写入
                                if (i != sheet.LastRowNum)
                                    sw.Write(nameCell + ",");
                                else
                                    sw.Write(nameCell);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();

                sheet = null;
            }

            workbook = null;


            return null;
        }
        
    }
}
