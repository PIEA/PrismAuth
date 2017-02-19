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
        private static string assembly = Assembly.GetExecutingAssembly().GetName().CodeBase;

        public static void AppendDirectory()
        {
            var path = GetAccountDirectoryPath();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetAccountDirectoryPath()
        {
            return Path.Combine(new Uri(Path.GetDirectoryName(assembly)).LocalPath, ContextConstants.DirectoryName);
        }
    }
}
