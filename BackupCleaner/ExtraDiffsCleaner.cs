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
        
        public void Run(string folder, bool skipLast = false)
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

            var orderedDict = fileDict.OrderBy(key => key.Key).ToDictionary(obj => obj.Key, obj => obj.Value);
            foreach (var key in orderedDict.Keys)
            {
                
                if (skipLast) 
                {
                    var lastIdx = orderedDict.Keys.Count - 1;
                    var curIdx = orderedDict.Keys.ToList().IndexOf(key);
                    if (curIdx == lastIdx)
                        continue;
                }
    
                var list = orderedDict[key];
                for (int i = 0; i < list.Count - 2; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].Path) && File.Exists(list[i].Path))
                    {
                        Console.WriteLine($"Cleaner. Delete file: {list[i].Path!}");
                        File.Delete(list[i].Path!);
                    }

                }

            }
        }
    }
}
