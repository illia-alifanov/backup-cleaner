namespace ClearBackups;

internal class Program
{
    static void Main(string[] args)
    {

        //string path = @"C:\temp\backup";
        if (args.Length == 0)
            return;
        if (string.IsNullOrEmpty(args[0]))
            return;

        string path = args[0];
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
            cleaner.Run(path);
        }
        catch (Exception ex)
        {
            throw;
        }




    }
}


