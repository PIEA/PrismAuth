using MiNET;
using Newtonsoft.Json;
using PrismAuth.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace PrismAuth.Account
{
    public static class AccountManager
    {
        public static ConcurrentDictionary<string, Player> LoginedPlayer = new ConcurrentDictionary<string, Player>();

        public static bool RegisterPlayer(Player player, string password)
        {
            var completed = AddPlayer(player.Username, password);

            if (completed)
            {
                LoginedPlayer.TryAdd(player.Username, player);
            }
            return completed;
        }

        public static bool LoginPlayer(Player player, string password)
        {
            var completed = VerifyPassword(player.Username, password);

            if (completed)
            {
                LoginedPlayer.TryAdd(player.Username, player);
            }
            return completed;
        }

        public static bool IsLogined(Player player)
        {
            return LoginedPlayer.Keys.Contains(player.Username);
        }

        public static bool IsRegistered(Player player)
        {
            var path = IO.GetFilePath(player.Username + ".json");
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

        public static void LogoutPlayer(Player player)
        {
            LoginedPlayer.TryRemove(player.Username, out player);
        }

        public static void RemovePlayerAccount(string userName)
        {
            var path = IO.GetFilePath(userName + ".json");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static bool AddPlayer(string userName, string password)
        {
            var completed = false;
            try
            {
                var path = IO.GetFilePath(userName + ".json");
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                if (TryEncryptPassword(password, out string digest))
                {
                    var account = new PlayerAccount() { Password = digest };
                    var json = JsonConvert.SerializeObject(account, Formatting.Indented);
                    // error
                    File.WriteAllText(path, json, Encoding.Unicode);
                    completed = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                completed = false;
            }

            return completed;
        }

        private static bool VerifyPassword(string targetName, string password)
        {
            var verify = false;

            var path = IO.GetFilePath(targetName + ".json");
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
                    verify = VerifyDigest(password, account.Password);
                }
            }

            return verify;
        }

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
