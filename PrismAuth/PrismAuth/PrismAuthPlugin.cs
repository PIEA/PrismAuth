using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using MiNET.Utils;
using PrismAuth.Account;
using PrismAuth.Commands;
using PrismAuth.Handler.EventHandler;
using PrismAuth.Handler.PacketHandler;
using PrismAuth.Resources;
using System.Collections.Concurrent;
using System.Globalization;

namespace PrismAuth
{
    [Plugin(Author = "Sepi", Description = "MiNET Auth plugin",
        PluginName = "PrismAuth", PluginVersion = "v0.1")]
    public class PrismAuthPlugin : Plugin
    {
        private const string defaultLang = "en-US";

        protected override void OnEnable()
        {
            SetLanguage();
            LoadEventHandlers();
            LoadpacketHandlers();
            LoadCommands();
        }

        private void SetLanguage()
        {
            var userLang = Config.GetProperty("lang", defaultLang);

            try
            {
                StringResource.Culture = new CultureInfo(userLang);
            }
            catch (CultureNotFoundException)
            {
                StringResource.Culture = new CultureInfo(defaultLang);
            }
        }

        private void LoadEventHandlers()
        {
            Context.Server.PlayerFactory.PlayerCreated += (sender, e) =>
            {
                e.Player.PlayerJoin += new PlayerJoinEvent().PlayerJoin;
                e.Player.PlayerLeave += new PlayerLeaveEvent().PlayerLeave;
            };

            Context.Server.LevelManager.LevelCreated += (sender, e) =>
            {
                e.Level.BlockBreak += new BlockBreakEvent().BlockBreak;
                e.Level.BlockPlace += new BlockPlaceEvent().BlockPlace;
            };
        }

        private void LoadpacketHandlers()
        {
            Context.PluginManager.LoadPacketHandlers(new CommandStepPacket());
            Context.PluginManager.LoadPacketHandlers(new DropItemPacket());
            Context.PluginManager.LoadPacketHandlers(new InteractPacket());
            Context.PluginManager.LoadPacketHandlers(new PlayerChatPacket());
            Context.PluginManager.LoadPacketHandlers(new UseItemPacket());
        }

        private void LoadCommands()
        {
            Context.PluginManager.LoadCommands(new LoginCommand(Context));
            Context.PluginManager.LoadCommands(new RegisterCommand(Context));
        }
    }
}
