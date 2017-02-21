using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using System.IO;
using PrismAuth.Util;
using PrismAuth.Account;
using Newtonsoft.Json;
using PrismAuth.Commands;

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin", 
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuth : Plugin
    {
        public List<string> LoginedPlayer { get; protected set; } 

        protected override void OnEnable()
        {
            IO.AppendDirectory();
            this.LoginedPlayer = new List<string>();
            this.Context.Server.PlayerFactory.PlayerCreated += PlayerFactory_PlayerCreated;

            this.Context.Server.LevelManager.LevelCreated += LevelManager_LevelCreated;
            base.OnEnable();
        }

        private void LevelManager_LevelCreated(object sender, MiNET.Worlds.LevelEventArgs e)
        {
            e.Level.BlockBreak += Level_BlockBreak;
            e.Level.BlockPlace += Level_BlockPlace;
        }

        private void Level_BlockPlace(object sender, MiNET.Worlds.BlockPlaceEventArgs e)
        {
            if (!this.LoginedPlayer.Contains(e.Player.Username))
            {
                e.Player.SendMessage("please log in first.");
                e.Cancel = true;
            }
        }

        private void Level_BlockBreak(object sender, MiNET.Worlds.BlockBreakEventArgs e)
        {
            if (!this.LoginedPlayer.Contains(e.Player.Username))
            {
                e.Player.SendMessage("please sure you log in first.");
                e.Cancel = true;
            }
        }

        private void PlayerFactory_PlayerCreated(object sender, PlayerEventArgs e)
        {
            e.Player.PlayerJoin += Player_PlayerJoin;
            e.Player.PlayerLeave += Player_PlayerLeave;
        }

        private void Player_PlayerJoin(object sender, PlayerEventArgs e)
        {
            if (AccountManager.IsRegistered(e.Player.Username))
            {
                Popup popup = new Popup()
                {
                    DisplayDelay = 5000,
                    Message = "please login first"
                };
                e.Player.AddPopup(popup);
            }
            else
            {
                Popup popup = new Popup()
                {
                    DisplayDelay = 5000,
                    Message = "please register first"
                };
                e.Player.AddPopup(popup);
            }
        }

        private void Player_PlayerLeave(object sender, PlayerEventArgs e)
        {
            this.LoginedPlayer.Remove(e.Player.Username);
        }

        public override void OnDisable()
        {
            this.Context.Server.PlayerFactory.PlayerCreated -= PlayerFactory_PlayerCreated;
            this.Context.Server.LevelManager.LevelCreated -= LevelManager_LevelCreated;
        }


    }
}
