using Newtonsoft.Json;
using PrismAuth.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PrismAuth.Auth
{
    public class AuthPlayers
    {
        public static AuthPlayers Context => _context;
        private static AuthPlayers _context = new AuthPlayers();

        public List<AuthPlayer> Players => this.players;
        private List<AuthPlayer> players;
        
        private AuthPlayers()
        {
            this.players = GetAuthPlayerFromDirectory();
        }

        public int Count => this.players.Count;

        public void Add(AuthPlayer item)
        {
            if (!Directory.Exists(ContextConstants.AuthPlayerDirectory))
            {
                Directory.CreateDirectory(ContextConstants.AuthPlayerDirectory);
            }

            using (StreamWriter writer = new StreamWriter(Path.Combine(ContextConstants.AuthPlayerDirectory, $"{item.Name}.json"), 
                true, System.Text.Encoding.Unicode))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, item);
            }

            this.players.Add(item);
        }

        public void Clear()
        {
            if (!Directory.Exists(ContextConstants.AuthPlayerDirectory))
            {
                Directory.CreateDirectory(ContextConstants.AuthPlayerDirectory);
            }

            var files = Directory.GetFiles(ContextConstants.AuthPlayerDirectory);
            foreach (var fileName in files)
            {
                File.Delete(Path.Combine(ContextConstants.AuthPlayerDirectory, fileName));
            }

            this.players.Clear();
        }

        public bool Remove(AuthPlayer item)
        {
            if (!Directory.Exists(ContextConstants.AuthPlayerDirectory))
            {
                Directory.CreateDirectory(ContextConstants.AuthPlayerDirectory);
            }

            var files = Directory.GetFiles(ContextConstants.AuthPlayerDirectory);
            var fileList = new List<string>(files);
            var fileName = $"{item.Name}.json";
            if (fileList.Exists(x => x == fileName))
            {
                File.Delete(Path.Combine(ContextConstants.AuthPlayerDirectory, fileName));
                this.players.Remove(this.players.Find(x => x.Name == item.Name));

                return true;
            }
            return false;
        }

        private List<AuthPlayer> GetAuthPlayerFromDirectory()
        {
            if (!Directory.Exists(ContextConstants.AuthPlayerDirectory))
            {
                Directory.CreateDirectory(ContextConstants.AuthPlayerDirectory);
            }
            var files = Directory.GetFiles(ContextConstants.AuthPlayerDirectory);

            List<AuthPlayer> players = new List<AuthPlayer>();
            foreach (var fileName in files)
            {
                using (StreamReader reader = new StreamReader(Path.Combine(ContextConstants.AuthPlayerDirectory, fileName), 
                    System.Text.Encoding.Unicode))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var playerData = (AuthPlayer)serializer.Deserialize(reader, typeof(AuthPlayer));
                    if (playerData != null)
                    {
                        Add(playerData);
                    }
                }
            }
            return players;
        }
    }
}
