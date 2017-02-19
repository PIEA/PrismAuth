using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Net;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;

namespace PrismAuth
{
    internal class PacketHandler : PrismAuth
    {
        [PacketHandler]
        public Package HandlePlayerMove(McpeMovePlayer packet, Player target)
        {
            return packet;
        }
    }
}
