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

        [Command(Name = "login")]
        public void Login(Player commander)
        {
            commander.SendMessage(ChatColors.Gray + StringResource.LoginHint);
        }

        [Command(Name = "login")]
        public void Login(Player commander, params string[] args)
        {
            if (AccountManager.IsLogined(commander))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
            }
            else
            {
                if (!AccountManager.IsRegistered(commander))
                {
                    commander.SendMessage(ChatColors.Yellow + StringResource.DoNotRegistered);
                }
                else
                {
                    if (AccountManager.LoginPlayer(commander, args[0]))
                    {
                        commander.SendMessage(ChatColors.Green + StringResource.CompletedLogin);
                    }
                    else
                    {
                        commander.SendMessage(ChatColors.Red + StringResource.NotIncorrectPasswd);
                    }
                }
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
