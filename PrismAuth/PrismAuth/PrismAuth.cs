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

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin", 
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuth : Plugin
    {
        public event EventHandler<PlayerEventArgs> PlayerLogined;

        public List<Player> LoginedPlayer { get; private set; }

        protected virtual void OnPlayerlogined(PlayerEventArgs e)
        {
            PlayerLogined?.Invoke(this, e);
        }

        protected override void OnEnable()
        {
            IO.AppendDirectory();
            this.LoginedPlayer = new List<Player>();
            PlayerLogined += PrismAuth_PlayerLogined;

            AccountManager.Add("tester", "1234qwer");
        }

        private void PrismAuth_PlayerLogined(object sender, PlayerEventArgs e)
        {
            this.LoginedPlayer.Add(e.Player);
        }

        public override void OnDisable()
        {
        }


    }
}
