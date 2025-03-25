namespace ClearBackups;

internal class Program
{
    static void Main(string[] args)
    {
        string path = "";
        if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
        {
            path = args[0];
        }
        else
        {
            path = AppDomain.CurrentDomain.BaseDirectory;
        }

        if (!Directory.Exists(path))
            return;

        if (!Directory.Exists(path))
        {
            return;
        }

        var organizer = new BackupOrganizer(new FileHelper());
        organizer.DoDiffs(path);
        organizer.DoFulls(path);

        var cleaner = new ExtraDiffsCleaner(new FileHelper());
        try
        {
            cleaner.Run(path, skipLast: true);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}


