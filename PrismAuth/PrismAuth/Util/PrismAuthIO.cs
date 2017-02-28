using Newtonsoft.Json;
using PrismAuth.Account;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrismAuth.Util
{
    public static class PrismAuthIO
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

        public static Result WriteJsonToFile(string path, string json)
        {
            var result = new Result();
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
                {
                    writer.Write(json);
                }
                result.Successed = true;
            }
            catch (IOException ex)
            {
                result.Message = ex.Message;
                result.Successed = false;
            }

            return result;
        }

        public static Result<PlayerAccount> ReadJsonFromFile(string path)
        {
            Result<PlayerAccount> result = new Result<PlayerAccount>();

            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    var json = reader.ReadToEnd();
                    result.Data = JsonConvert.DeserializeObject<PlayerAccount>(json);
                    result.Successed = true;
                }
            }
            catch (IOException ex)
            {
                result.Message = ex.Message;
                result.Successed = false;
            }

            return result;
        }

        public static bool IsFileLocked(string filePath)
        {
            bool isLocked = true;
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
                isLocked = false;
            }
            catch (IOException e)
            {
                var errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(e) & ((1 << 16) - 1);
                isLocked = errorCode == 32 || errorCode == 33;
            }

            return isLocked;
        }

        public static Result DeleteFile(string filePath)
        {
            var result = new Result()
            {
                Successed = true
            };

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || IsFileLocked(filePath))
            {
                result.Successed = false;
            }

            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Successed = false;
            }

            return result;
        }
    }
}
