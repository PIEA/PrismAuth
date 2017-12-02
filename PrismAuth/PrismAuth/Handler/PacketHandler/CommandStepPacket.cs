using MiNET;
using MiNET.Net;
using MiNET.Plugins.Attributes;
using MiNET.Utils;
using PrismAuth.Account;
using PrismAuth.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.PacketHandler
{
    public class CommandStepPacket
    {
        [PacketHandler]
        public Package HandleCommandStep(McpeCommandRequest packet, Player target)
        {
            if (!AccountManager.LoginedPlayer.Keys.Contains(target.Username))
            {
                var command = packet.command;
                command = command.Remove(0, 1);
                command = command.Split(' ')[0];
                if (command == "reg" || command == "login")
                {
                    return packet;
                }
                target.SendMessage(ChatColors.Yellow + StringResource.DoNotLogined);
                return null;
            }

            return packet;
        }
    }
}
