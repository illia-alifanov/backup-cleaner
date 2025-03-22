namespace ClearBackups
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IFileHelper
    {
        DateTime GetDate(string file);
        string GetBaseAndDateName(string file);
        string GetBaseAndNextDateName(string file);
        void AddFile(Dictionary<DateTime, List<FileModel>> dict, DateTime date, string file);
        
    }

    internal class FileHelper : IFileHelper
    {
        public void AddFile(Dictionary<DateTime, List<FileModel>> dict, DateTime date, string file)
        {
            var list = dict.ContainsKey(date.Date) ? dict[date.Date] : [];
            list.Add(new FileModel { Date = date, Path = file });

            dict[date.Date] = list;
        }

        public DateTime GetDate(string file)
        {
            var fileNameParts = file.Split('_');
            string dateString = fileNameParts[1];
            string datePart = dateString.Split('T')[0];
            string timeString = dateString.Split('T')[1];
            string timePart = timeString.Replace('-', ':');
            dateString = $"{datePart}T{timePart}:00";

            DateTime date = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            return date;
        }

        public string GetBaseAndDateName(string file)
        {
            var fileNameParts = file.Split('_');
            string dateString = fileNameParts[1];
            string datePart = dateString.Split('T')[0];
            string folderName = $"{fileNameParts[0]}_{datePart}";

            return folderName;
        }

        public string GetBaseAndNextDateName(string file)
        {
            var fileNameParts = file.Split('_');
            string dateString = fileNameParts[1];
            string datePart = dateString.Split('T')[0];
            DateTime date = DateTime.Parse(datePart, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            DateTime nextDate = date.Date.AddDays(1);

            string folderName = $"{fileNameParts[0]}_{nextDate.ToString("yyyy-MM-dd")}";

            return folderName;
        }
    }
}
