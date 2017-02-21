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
    public class LoginCommand : PrismAuth
    {
        [Command(Name = "login")]
        public void Login(Player commander)
        {
            commander.SendMessage("/login (password)");
        }

        [Command(Name = "login")]
        public void Login(Player commander, string password)
        {
            if (this.LoginedPlayer.Exists(x => x == commander.Username))
            {
                commander.SendMessage("you are already logined.");
            }
            else
            {
                if (!AccountManager.IsRegistered(commander.Username))
                {
                    commander.SendMessage("please register first.");
                }
                else
                {
                    if (AccountManager.VerifyPassword(commander, password))
                    {
                        commander.SendMessage("login completed");
                        this.LoginedPlayer.Add(commander.Username);
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
