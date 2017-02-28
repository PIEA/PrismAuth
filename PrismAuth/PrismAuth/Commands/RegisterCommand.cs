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
using PrismAuth.Util;

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
            if (AccountManager.LoginedPlayer.Keys.Contains(commander.Username))
            {
                commander.SendMessage(ChatColors.Yellow + StringResource.AlreadyLogined);
                return;
            }

            var result = AccountManager.RegisterPlayer(commander, args[0]);
            if (result.Successed)
            {
                commander.SendMessage(ChatColors.Green + StringResource.CompletedRegister);
            }
            else
            {
                commander.SendMessage(ChatColors.Red + StringResource.FaildRegister);
                commander.SendMessage(ChatColors.Red + result.Message);
            }
        }
    }
}
