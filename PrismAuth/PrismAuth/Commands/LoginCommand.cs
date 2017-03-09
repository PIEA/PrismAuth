using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth.Account;
using MiNET.Utils;
using PrismAuth.Resources;

namespace PrismAuth.Commands
{
    public class LoginCommand
    {
        private PluginContext context;

        public LoginCommand(PluginContext context)
        {
            this.context = context;
        }

        [Command(Description = "Shows login command hint.")]
        public void Login(Player commander)
        {
            commander.SendMessage(ChatColors.Gray + StringResource.LoginHint);
        }

        [Command(Description = "Login to server.")]
        public void Login(Player commander, params string[] args)
        {
            if (AccountManager.LoginedPlayer.Keys.Contains(commander.Username))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
                return;
            }

            var result = AccountManager.LoginPlayer(commander, args[0]);
            if (result.Successed)
            {
                commander.SendMessage(ChatColors.Green + StringResource.CompletedLogin);
            }
            else
            {
                commander.SendMessage(ChatColors.Red + StringResource.FaildLogin);
                commander.SendMessage(ChatColors.Red + result.Message);
            }
        }
#if DEBUG
        [Command(Name = "logined")]
        public void Logined(Player commander)
        {
            foreach (var name in AccountManager.LoginedPlayer)
            {
                commander.SendMessage(name.Key);
            }

            commander.SendMessage("end.");
        }
#endif
    }
}
