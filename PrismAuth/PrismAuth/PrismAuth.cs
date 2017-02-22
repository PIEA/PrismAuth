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
        public AccountManager AccountManager { get => this._accountManager; }
        private AccountManager _accountManager;

        protected override void OnEnable()
        {
            this._accountManager = new AccountManager();
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
            if (!this.AccountManager.IsLogined(e.Player))
            {
                e.Player.SendMessage("please log in first.");
                e.Cancel = true;
            }
        }

        private void Level_BlockBreak(object sender, MiNET.Worlds.BlockBreakEventArgs e)
        {
            if (!this.AccountManager.IsLogined(e.Player))
            {
                e.Player.SendMessage("please sure you log in first.");
                e.Cancel = true;
            }
        }

        private void PlayerFactory_PlayerCreated(object sender, PlayerEventArgs e)
        {
            e.Player.PlayerJoin += Player_PlayerJoinAsync;
            e.Player.PlayerLeave += Player_PlayerLeave;
        }

        private async void Player_PlayerJoinAsync(object sender, PlayerEventArgs e)
        {
            if (await this.AccountManager.IsRegisteredAsync(e.Player))
            {
                Popup popup = new Popup()
                {
                    Message = "please login first"
                };
                e.Player.AddPopup(popup);
            }
            else
            {
                Popup popup = new Popup()
                {
                    Message = "please register first"
                };
                e.Player.AddPopup(popup);
            }
        }

        private void Player_PlayerLeave(object sender, PlayerEventArgs e)
        {
            this.AccountManager.DisconnectPlayer(e.Player);
        }

        public override void OnDisable()
        {
            this.Context.Server.PlayerFactory.PlayerCreated -= PlayerFactory_PlayerCreated;
            this.Context.Server.LevelManager.LevelCreated -= LevelManager_LevelCreated;
        }


    }
}
