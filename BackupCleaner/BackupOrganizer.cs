namespace ClearBackups
{
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class BackupOrganizer(IFileHelper fileHelper)
    {
        public void DoDiffs(string folder)
        {
            Regex reg = new Regex(@"_diff\.bak$");

            var files = Directory.GetFiles(folder, "*.bak")
                                 .Where(path => reg.IsMatch(path))
                                 .ToList();

            foreach (var file in files)
            {
                Console.WriteLine($"DoDiffs. file: {file}");
                var folderName = fileHelper.GetBaseAndDateName(Path.GetFileName(file));
                var destFolder =  Path.Combine(folder, folderName);
                Console.WriteLine($"DoDiffs. Destination folder: {destFolder}");
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                    Console.WriteLine($"DoDiffs. Destination folder created");
                }

                string destFile = Path.Combine(destFolder, Path.GetFileName(file));
                Console.WriteLine($"DoDiffs. Move file folder: {destFolder}");
                File.Move(file, destFile, true);
            }
        }

        public void DoFulls(string folder)
        {
            Regex reg = new Regex(@"_Full\.bak$");

            var files = Directory.GetFiles(folder, "*.bak")
                                 .Where(path => reg.IsMatch(path))
                                 .ToList();

            foreach (var file in files)
            {
                Console.WriteLine($"DoFulls. file: {file}");
                var folderName = fileHelper.GetBaseAndNextDateName(Path.GetFileName(file));
                var destFolder = Path.Combine(folder, folderName);
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                    Console.WriteLine($"DoFulls. Destination folder created");
                }
                string destFile = Path.Combine(destFolder, Path.GetFileName(file));
                File.Move(file, destFile, true);
                Console.WriteLine($"DoFulls. Move file folder: {destFolder}");
            }

        }
    }
}
