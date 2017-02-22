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
            var path = Path.Combine(Path.GetDirectoryName(assembly), ContextConstants.DirectoryName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.GetFullPath(path);
        }

        public static string GetFilePath(string fileName)
        {
            return Path.GetFullPath(Path.Combine(GetAccountDirectoryPath(), fileName));
        }
    }
}
