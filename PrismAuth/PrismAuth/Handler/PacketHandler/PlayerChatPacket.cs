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
                var msg = packet.message;
                if (msg.StartsWith("/"))
                {
                    msg = msg.Remove(0, 1);
                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        return null;
                    }
                    var msgs = msg.Split(' ').ToList();

                    var first = msgs.First();
                    if (first == "reg" || first == "login")
                    {
                        return packet;
                    }
                }

                target.SendMessage("please login first.");
                return null;
            }

            return packet;
        }
    }
}
