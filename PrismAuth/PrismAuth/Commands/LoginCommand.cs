using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth.Account;

namespace PrismAuth.Commands
{
    public class LoginCommand : BaseCommand
    {
        [Command(Name = "login")]
        public void Login(Player commander)
        {
            commander.SendMessage("/login (password)");
        }

        [Command(Name = "login")]
        public void Login(Player commander, string password)
        {
            if (this.AccountManager.IsLogined(commander))
            {
                commander.SendMessage("you are already logined.");
            }
            else
            {
                if (!this.AccountManager.IsRegistered(commander))
                {
                    commander.SendMessage("please register first.");
                }
                else
                {
                    if (this.AccountManager.LoginPlayer(commander, password))
                    {
                        commander.SendMessage("login completed");
                    }
                    else
                    {
                        commander.SendMessage("password not incorrect!");
                    }
                }
            }
        }
    }
}
