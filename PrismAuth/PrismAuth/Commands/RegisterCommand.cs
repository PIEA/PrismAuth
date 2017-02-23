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
    public class RegisterCommand : BaseCommand
    {

        [Command(Name = "reg")]
        public void Register(Player commander)
        {
            commander.SendMessage("/reg (password)");
        }

        [Command(Name = "reg")]
        public void Register(Player commander, string password)
        {
            commander.SendMessage("this is register command.");
            if (this.AccountManager.IsRegistered(commander))
            {
                commander.SendMessage("you are already registered.");
                return;
            }
            else
            {
                if (this.AccountManager.RegisterPlayer(commander, password))
                {
                    commander.SendMessage("successfully registered.");
                }
                else
                {
                    commander.SendMessage("failed to register.");
                }
            }
        }
    }
}
