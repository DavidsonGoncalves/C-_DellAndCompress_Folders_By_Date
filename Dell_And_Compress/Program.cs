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

           


        }


        public static void Zip_File(string path)
        {
            IEnumerable<string> folder = Directory.GetDirectories(path);

            string lastFolder = folder.Last().ToString();

            ZipFile.CreateFromDirectory(lastFolder, lastFolder + ".zip", CompressionLevel.Optimal, true);
            Directory.Delete(lastFolder, true);

        }

        public static void Delete_OldFile(DirectoryInfo path)
        {
            var files = path.GetFiles();
            var Files_Order = files.OrderBy(x => x.CreationTime);
            var frist_File = Files_Order.First();

            File.Delete(frist_File.ToString());

        }

    }
}
