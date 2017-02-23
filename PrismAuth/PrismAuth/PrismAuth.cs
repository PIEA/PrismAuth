using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth.Account;

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin",
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuth : Plugin
    {
        public Accounts AccountManager { get; set; } = new Accounts();
    }
}
