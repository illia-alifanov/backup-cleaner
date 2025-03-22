namespace ClearBackups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class FileModel
    {
        public string? Path { get; set; }
        public DateTime Date { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            return this.Date.Equals(((FileModel)obj).Date);
        }
        public override int GetHashCode()
        {
            return this.Date.GetHashCode();
        }
    }
}
