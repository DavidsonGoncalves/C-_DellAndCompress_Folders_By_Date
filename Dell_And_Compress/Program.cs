using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;


namespace Dell_And_Compress
{
    class Program
    {
        static void Main()
        {
            var OriginalPath = new DirectoryInfo(@"D:\!Documents\Projects\Project DeleteFolders\Folder");

            Zip_File(OriginalPath.ToString());

            Delete_OldFile(OriginalPath);

        }


        public static void Zip_File(string path) 
        {
            IEnumerable<string> folder = Directory.GetDirectories(path);

            string lastFolder = folder.Last().ToString(); 

            ZipFile.CreateFromDirectory(lastFolder, lastFolder + ".zip", CompressionLevel.Optimal, true);  
            Directory.Delete(lastFolder, true); 

            string LogText = $"[{DateTime.Now.ToString()}] - File Compressed Succssesfuly!";  
            Log_Write(LogText); 
        }

        public static void Delete_OldFile(DirectoryInfo path)
        {
            var files = path.GetFiles();
            var Files_Order = files.OrderBy(x => x.CreationTime); 
            var frist_File = Files_Order.First(); 

            File.Delete(frist_File.ToString()); 

            DateTime now = DateTime.Now;
            string LogText = $"[{DateTime.Now.ToString()}] - Older File Deleted succssesfuly!"; 
            Log_Write(LogText); 
        }

        public static void Log_Write(string text)
        {
            string pathLog = @"D:\!Documents\Projects\Project DeleteFolders\Event_Log.txt"; 

            using (StreamWriter sw = File.AppendText(pathLog))  
            {
                sw.WriteLine(text); 
            }
        }
    }
}
