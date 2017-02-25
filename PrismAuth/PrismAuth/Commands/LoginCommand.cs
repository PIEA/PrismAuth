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
    public class LoginCommand : BaseCommand
    {
        [Command(Name = "login")]
        public void Login(Player commander)
        {
            commander.SendMessage(ChatColors.Gray + StringResource.LoginHint);
        }

        [Command(Name = "login")]
        public void Login(Player commander, string password)
        {
            if (this.AccountManager.IsLogined(commander))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
            }
            else
            {
                if (!this.AccountManager.IsRegistered(commander))
                {
                    commander.SendMessage(ChatColors.Yellow + StringResource.DoNotRegistered);
                }
                else
                {
                    if (this.AccountManager.LoginPlayer(commander, password))
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
            foreach (var name in Accounts.LoginedPlayer)
            {
                commander.SendMessage(name);
            }

            commander.SendMessage("end.");
        }
#endif
    }
}
