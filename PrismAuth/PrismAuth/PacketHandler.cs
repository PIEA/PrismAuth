using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Net;
using MiNET.Plugins;
using MiNET.Plugins.Attributes;
using PrismAuth.Account;

namespace PrismAuth
{
    public class PacketHandler : PrismAuth
    {
        [PacketHandler]
        public Package HandlePlayerChat(McpeText packet, Player target)
        {
            packet.Reset();
            return packet;
        }
    }
}
