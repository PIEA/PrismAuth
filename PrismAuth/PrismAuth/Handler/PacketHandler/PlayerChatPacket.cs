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
    public class PlayerChatPacket : BasePacketHandler
    {
        [PacketHandler, Send]
        public Package HandlePlayerChat(McpeText packet, Player target)
        {
            target.SendMessage("hello.");
            return packet;
        }
    }
}
