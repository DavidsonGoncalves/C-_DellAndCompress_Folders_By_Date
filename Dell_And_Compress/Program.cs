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
            var OriginalPath = new DirectoryInfo(@"D:\!Documents\Projects\Project DeleteFolders\Folder"); //Set original path for backup Files
            try
            {
                Zip_File(OriginalPath.ToString());

                Delete_OldFile(OriginalPath);
            }
            catch (System.IO.DirectoryNotFoundException e) //case not found directory
            {
                Log_Write($"[{DateTime.Now.ToString()}] - ERROR!! " + e.Message);
            }
            catch (System.InvalidOperationException e)
            {
                Log_Write($"[{DateTime.Now.ToString()}] - ERROR!! {e.Message} to compress!"); //case have no file to compress
            }
            catch (Exception e)
            {
                Log_Write($"[{DateTime.Now.ToString()}] - UNEXPETED ERROR!! {e.Message}"); //case error unexpeted
            }

        }


        public static void Zip_File(string path) 
        {
            IEnumerable<string> folder = Directory.GetDirectories(path);

            string lastFolder = folder.Last().ToString();  //take last file

            ZipFile.CreateFromDirectory(lastFolder, lastFolder + ".zip", CompressionLevel.Optimal, true);   //compress code (max level)
            Directory.Delete(lastFolder, true);   //delete leftover folder

            string LogText = $"[{DateTime.Now.ToString()}] - File Compressed Succssesfuly!";   
            Log_Write(LogText); //write in log
        }

        public static void Delete_OldFile(DirectoryInfo path)
        {
            var files = path.GetFiles();
            var Files_Order = files.OrderBy(x => x.CreationTime);  // sort collected files by GetFiles
            var frist_File = Files_Order.First();  //take first file

            File.Delete(frist_File.ToString()); //delete first file who was created

            DateTime now = DateTime.Now;
            string LogText = $"[{DateTime.Now.ToString()}] - Older File Deleted succssesfuly!"; 
            Log_Write(LogText);  //write in log
        }

        public static void Log_Write(string text)
        {
            string pathLog = @"D:\!Documents\Projects\Project DeleteFolders\Event_Log.txt";  //define the log path

            using (StreamWriter sw = File.AppendText(pathLog))  //open or create a log file in path
            {
                sw.WriteLine(text);   //write Log
            }
        }
    }
}
