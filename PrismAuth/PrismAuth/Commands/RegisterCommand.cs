using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth;

namespace PrismAuth.Commands
{
    public class RegisterCommand : PrismAuth
    {
        [Command]
        public void Register(Player commander)
        {

        }

        [Command]
        public void Register(Player commander, string password)
        {
            var username = commander.Username;
        }
    }
}
