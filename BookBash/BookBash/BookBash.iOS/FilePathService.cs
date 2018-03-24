using System;
using System.IO;
using BookBash.iOS;
using BookBash.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace BookBash.iOS
{
    public class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filename)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }

}