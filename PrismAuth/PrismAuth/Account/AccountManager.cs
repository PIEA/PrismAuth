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
using System.Threading;
using PrismAuth.Resources;

namespace PrismAuth.Account
{
    public static class AccountManager
    {
        /// <summary>
        /// Key = 플레이어 이름, Value = 플레이어 데이터
        /// </summary>
        public static ConcurrentDictionary<string, Player> LoginedPlayer = new ConcurrentDictionary<string, Player>();

        public static Result RegisterPlayer(Player player, string password)
        {
            var result = new Result();
            var path = PrismAuthIO.GetFilePath(player.Username + ".json");

            if (File.Exists(path))
            {
                var read = PrismAuthIO.ReadJsonFromFile(path);

                if (read.Data != null)
                {
                    result.Message = StringResource.AlreadyRegistered;
                    result.Successed = false;

                    return result;
                }
            }

            if (TryEncryptPassword(password, out string digest))
            {
                var account = new PlayerAccount() { Password = digest };
                var json = JsonConvert.SerializeObject(account, Formatting.Indented);
                result = PrismAuthIO.WriteJsonToFile(path, json);
            }

            if (result.Successed)
            {
                LoginedPlayer.TryAdd(player.Username, player);
            }
            return result;
        }

        public static Result LoginPlayer(Player player, string password)
        {
            var result = new Result();

            var path = PrismAuthIO.GetFilePath(player.Username + ".json");
            if (File.Exists(path))
            {
                var read = PrismAuthIO.ReadJsonFromFile(path);

                if (read.Data != null)
                {
                    try
                    {
                        result.Successed = BCrypt.Net.BCrypt.Verify(password, read.Data.Password);

                        if (!result.Successed)
                        {
                            result.Message = StringResource.NotIncorrectPasswd;
                        }
                    }
                    catch (BCrypt.Net.SaltParseException ex)
                    {
                        Console.WriteLine(ex.Message);
                        result.Message = ex.Message;
                        result.Successed = false;
                    }
                }
            }
            else
            {
                result.Successed = false;
                result.Message = StringResource.DoNotRegistered;
            }

            if (result.Successed)
            {
                LoginedPlayer.TryAdd(player.Username, player);
            }

            return result;
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
    }
}
