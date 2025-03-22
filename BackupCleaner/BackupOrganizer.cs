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
                var folderName = fileHelper.GetBaseAndDateName(Path.GetFileName(file));
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                string destFile = Path.Combine(folderName, Path.GetFileName(file));
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
                var folderName = fileHelper.GetBaseAndNextDateName(Path.GetFileName(file));
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
                string destFile = Path.Combine(folderName, Path.GetFileName(file));
                File.Move(file, destFile, true);
            }

        }
    }
}
