using System;
using System.IO;

namespace Alex_Component
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
            string path = $"{System.Windows.Forms.Application.StartupPath}\\log\\{DateTime.Now.ToString("yyyy-MM-dd")}.log";
            Files_Exist(path);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(AppName + DateTime.Now.ToString());
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace + Environment.NewLine);
            }
        }


        /// <summary>
        /// 建置檔案及路徑
        /// </summary>
        /// <param name="File_Path">檔案路徑</param>
        public void Files_Exist(string File_Path)
        {
            //路徑
            string folderPath = System.Windows.Forms.Application.StartupPath + @"\log";
            //如果缺少路徑就建立
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //若缺少檔案就建立
            if (!System.IO.File.Exists(File_Path))
            {
                File.Create(File_Path).Close();
            }
        }

    }

    internal class Msg_log
    {
        
    }

}
