using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth;
using PrismAuth.Account;

namespace PrismAuth.Commands
{
    public class RegisterCommand : PrismAuth
    {
        [Command]
        public void Register(Player commander)
        {
            commander.SendMessage("/register (password)");
        }

        [Command]
        public void Register(Player commander, string password)
        {
            commander.SendMessage("this is register command.");
            if (AccountManager.IsRegistered(commander.Username))
            {
                commander.SendMessage("you are already registered.");
                return;
            }

            if (AccountManager.Add(commander.Username, password))
            {
                commander.SendMessage("successfully registered.");
                this.LoginedPlayer.Add(commander.Username);
            }
            else
            {
                commander.SendMessage("failed to register.");
            }
        }
    }
}
