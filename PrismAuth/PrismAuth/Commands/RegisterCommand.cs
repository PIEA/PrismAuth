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
using MiNET.Utils;
using PrismAuth.Resources;

namespace PrismAuth.Commands
{
    public class RegisterCommand : BaseCommand
    {

        [Command(Name = "reg")]
        public void Register(Player commander)
        {
            commander.SendMessage(ChatColors.Gray + StringResource.RegisterHint);
        }

        [Command(Name = "reg")]
        public void Register(Player commander, params string[] args)
        {
            if (this.AccountManager.IsLogined(commander))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
            }
            else
            {
                if (!this.AccountManager.IsRegistered(commander))
                {
                    if (this.AccountManager.RegisterPlayer(commander, args[0]))
                    {
                        commander.SendMessage(ChatColors.Green + StringResource.CompletedRegister);
                    }
                    else
                    {
                        commander.SendMessage(ChatColors.Red + StringResource.FaildRegister);
                    }
                }
                else
                {
                    commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyRegistered);
                    return;
                }
            }
        }
    }
}
