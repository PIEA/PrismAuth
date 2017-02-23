using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using System.IO;
using PrismAuth.Util;
using PrismAuth.Account;
using Newtonsoft.Json;
using PrismAuth.Commands;
using PrismAuth.Handler;
using PrismAuth.Handler.EventHandler;
using PrismAuth.Handler.PacketHandler;

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin",
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuth : Plugin
    {
        public Accounts AccountManager { get; set; } = new Accounts();
    }
}
