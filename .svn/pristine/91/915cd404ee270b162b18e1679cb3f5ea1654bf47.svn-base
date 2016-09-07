using System.IO;

namespace FengNiao.GameTools.Util
{
    public class TXTHelper
    {
        public static void SaveToTXT(string folder, string fileName, string meg)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string textPath = string.Format("{0}\\{1}.txt",
                                        folder,
                                        fileName);
            if (!File.Exists(textPath))
                File.Create(textPath);

            FileStream fs = new FileStream(textPath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine(meg);

            sw.Close();
            fs.Close();
        }
    }
}
