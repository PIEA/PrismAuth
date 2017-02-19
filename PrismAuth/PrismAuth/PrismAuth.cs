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
using PrismAuth.Auth;

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin", 
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuth : Plugin
    {
        public AuthPlayers AuthList => this._authList;
        private AuthPlayers _authList;

        protected override void OnEnable()
        {

            this.Context.Server.PlayerFactory.PlayerCreated += (sender, e) =>
            {
                e.Player.PlayerJoin += Player_PlayerJoin;
            };
            base.OnEnable();
        }

        private void Player_PlayerJoin(object sender, PlayerEventArgs e)
        {
            
            while (true)
            {
                AuthPlayer playerData = new AuthPlayer(e.Player.Username);

            }
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}
