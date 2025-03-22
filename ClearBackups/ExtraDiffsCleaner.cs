namespace ClearBackups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    internal class ExtraDiffsCleaner(IFileHelper fileHelper)
    {
        
        public void Run(string folder)
        {
            Regex reg = new Regex(@"_diff\.bak$");

            var files = Directory.GetFiles(folder, "*.bak", SearchOption.AllDirectories)
                                 .Where(path => reg.IsMatch(path))
                                 .ToList();

            var fileDict = new Dictionary<DateTime, List<FileModel>>();
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var date = fileHelper.GetDate(Path.GetFileName(file));

                fileHelper.AddFile(fileDict, date, file);
            }


            foreach (var key in fileDict.Keys)
            {
                var list = fileDict[key];
                for (int i = 0; i < list.Count - 2; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].Path) && File.Exists(list[i].Path))
                    {
                        File.Delete(list[i].Path!);
                    }

                }

            }
        }
    }
}
