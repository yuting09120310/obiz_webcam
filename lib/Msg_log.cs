using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace obiz_load_data
{
    internal class Msg_log
    {
        /// <summary>
        /// log存檔
        /// </summary>
        /// <param name="AppName">app的名稱</param>
        /// <param name="ex">錯誤訊息</param>
        public void save_log(string AppName, Exception ex)
        {
            string path = $"{System.Windows.Forms.Application.StartupPath}\\log\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
            Files_Exist(path, "txt");

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"{AppName} | {DateTime.Now.ToString()} \r\n | {ex.Message} | {ex.StackTrace} \r\n");
            }
        }


        /// <summary>
        /// 建置檔案
        /// </summary>
        /// <param name="File_Path">檔案路徑</param>
        /// <param name="File_Type">副檔名</param>
        public void Files_Exist(string File_Path, string File_Type)
        {
            string folderPath = System.Windows.Forms.Application.StartupPath + @"\log";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!System.IO.File.Exists(File_Path))
            {
                File.Create(File_Path + DateTime.Now.ToString("yyyy-MM-dd") + ".txt").Close();

                if (File_Type.ToLower() == "json")
                {
                    using (StreamWriter sw = new StreamWriter(File_Path, true))
                    {
                        sw.WriteLine("[]");
                    }
                }
            }
        }

    }
}
