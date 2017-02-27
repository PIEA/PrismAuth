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
    public class RegisterCommand
    {
        private PluginContext context;

        public RegisterCommand(PluginContext context)
        {
            this.context = context;
        }

        [Command(Aliases = new string[] { "reg" }, Description = "Shows register command hint")]
        public void Register(Player commander)
        {
            commander.SendMessage(ChatColors.Gray + StringResource.RegisterHint);
        }

        [Command(Aliases = new string[] { "reg" }, Description = "Register to server.")]
        public void Register(Player commander, params string[] args)
        {
            if (AccountManager.IsLogined(commander))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
            }
            else
            {
                if (!AccountManager.IsRegistered(commander))
                {
                    if (AccountManager.RegisterPlayer(commander, args[0]))
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
