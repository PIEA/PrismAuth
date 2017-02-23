using MiNET;
using MiNET.Net;
using MiNET.Plugins.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.PacketHandler
{
    public class McpeMovePlayerPacket : BasePacketHandler
    {
        [PacketHandler]
        public void HandleMcpeMovePlayer(McpeMovePlayer packet, Player target)
        {
            if (!this.AccountManager.IsLogined(target))
            {
                target.SetPosition(new MiNET.Utils.PlayerLocation()
                {
                    X = target.SpawnPosition.X,
                    Y = target.SpawnPosition.Y,
                    Z = target.SpawnPosition.Z
                }, false);
            }
        }
    }
}
