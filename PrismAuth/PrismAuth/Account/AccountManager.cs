using MiNET;
using Newtonsoft.Json;
using PrismAuth.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrismAuth.Account
{
    public static class AccountManager
    {
        public static bool Add(string userName, string password)
        {
            IO.AppendDirectory();
            var completed = false;
            try
            {
                var path = Path.Combine(IO.GetAccountDirectoryPath(), $"{userName}.json");
                if (TryEncryptPassword(password, out string digest))
                {
                    var authPlayer = new PlayerAccount() { Name = userName, Digest = digest };
                    var json = JsonConvert.SerializeObject(authPlayer, Formatting.Indented);
                    @File.WriteAllText(path, json, Encoding.Unicode);
                    completed = true;
                }
            }
            catch (Exception)
            {
                completed = false;
            }

            return completed;
        }

        public static void Remove(string userName)
        {
            IO.AppendDirectory();
            var path = Path.Combine(IO.GetAccountDirectoryPath(), $"{userName}.json");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static bool IsRegistered(string userName)
        {
            IO.AppendDirectory();
            var path = Path.Combine(IO.GetAccountDirectoryPath(), $"{userName}.json");
            if (File.Exists(path))
            {
                PlayerAccount account = null;
                using (StreamReader file = new StreamReader(path, Encoding.Unicode))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    account = (PlayerAccount)serializer.Deserialize(file, typeof(PlayerAccount));

                }

                if (account != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool VerifyPassword(Player target, string password)
        {
            var verify = false;

            IO.AppendDirectory();
            var path = Path.Combine(IO.GetAccountDirectoryPath(), $"{target.Username}.json");
            if (File.Exists(path))
            {
                PlayerAccount account = null;
                using (StreamReader file = new StreamReader(path, Encoding.Unicode))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    account = (PlayerAccount)serializer.Deserialize(file, typeof(PlayerAccount));
                }

                if (account != null)
                {
                    verify = VerifyDigest(password, account.Digest);
                }
            }

            return verify;
        }

        /*
        public static List<PlayerAccount> GetAuthPlayerFromDirectory()
        {
            IO.AppendDirectory();
            var files = Directory.GetFiles(IO.GetAccountDirectoryPath());

            List<PlayerAccount> accounts = new List<PlayerAccount>();
            foreach (var fileName in files)
            {
                var path = Path.Combine(IO.GetAccountDirectoryPath(), fileName);
                using (StreamReader file = new StreamReader(path, Encoding.Unicode))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    PlayerAccount account = (PlayerAccount)serializer.Deserialize(file, typeof(PlayerAccount));
                    accounts.Add(account);
                }

            }
            return accounts;
        }
        */
        private static bool TryEncryptPassword(string passwd, out string digest)
        {
            digest = null;
            try
            {
                digest = BCrypt.Net.BCrypt.HashPassword(passwd, 11);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                return false;
            }

            return true;
        }

        private static bool VerifyDigest(string passwd, string digest)
        {
            var verify = false;
            try
            {
                verify = BCrypt.Net.BCrypt.Verify(passwd, digest);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                return false;
            }
            return verify;
        }
    }
}
