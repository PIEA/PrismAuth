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
        [PacketHandler, Receive]
        public Package HandlePlayerChat(McpeText packet, Player target)
        {
            if (!this.AccountManager.IsLogined(target))
            {
                target.SendMessage("please login first.");

                return null;
            }

            return packet;
        }
    }
}
