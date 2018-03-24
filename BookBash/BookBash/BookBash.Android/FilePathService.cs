using System;
using System.IO;
using BookBash.Droid;
using BookBash.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace BookBash.Droid
{
    public class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }

}