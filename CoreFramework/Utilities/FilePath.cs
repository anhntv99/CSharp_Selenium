using System.Reflection;
using NUnit.Framework;


namespace CoreFramework.Utilities
{
    public class FilePath
    {
        public static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }
        public static void DeleteFolder(string path)
        {
            Directory.Delete(path);
        }
        public static void DeleteFile(string path)
        {
            File.Create(path);
        }

        public static string GetCurrentDirectoryPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\..\\..\\..";
            TestContext.Progress.WriteLine(path);
            return path;
        }

        public static void CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
