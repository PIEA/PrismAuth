using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Util
{
    public class IO
    {
        private static string assembly = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;

        public static string GetAccountDirectoryPath()
        {
            var path = Path.Combine(assembly, ContextConstants.DirectoryName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return new Uri(path).LocalPath;
        }

        public static string GetFilePath(string fileName)
        {
            return new Uri(Path.Combine(GetAccountDirectoryPath(), fileName)).LocalPath;
        }
    }
}
