using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class EventLoader : PrismAuth
    {
        protected override void OnEnable()
        {
            this.Context.Server.PlayerFactory.PlayerCreated += (sender, e) =>
            {
                e.Player.PlayerJoin += new PlayerJoinEvent().PlayerJoin;
                e.Player.PlayerLeave += new PlayerLeaveEvent().PlayerLeave;
            };

            this.Context.Server.LevelManager.LevelCreated += (sender, e) =>
            {
                e.Level.BlockBreak += new BlockBreakEvent().BlockBreak;
                e.Level.BlockPlace += new BlockPlaceEvent().BlockPlace;
            };
        }
    }
}
