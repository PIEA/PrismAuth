using MiNET;
using Newtonsoft.Json;
using PrismAuth.Cryptography;
using PrismAuth.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrismAuth.Account
{
    public class AccountManager
    {
        public static bool Add(string userName, string password)
        {
            IO.AppendDirectory();
            var completed = false;
            try
            {
                var path = Path.Combine(IO.GetAccountDirectoryPath(), $"{userName}.json");
                var authPlayer = new PlayerAccount() { Name = userName, EncryptedPassword = AES.EncryptString(password) };
                using (StreamWriter file = new StreamWriter(path, false, Encoding.Unicode))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, authPlayer);
                }
                completed = true;
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
    }
}
