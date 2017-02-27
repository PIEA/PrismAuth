﻿using MiNET;
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
    public class DropItemPacket
    {
        [PacketHandler]
        public Package HandleDropItem(McpeDropItem packet, Player target)
        {
            if (!AccountManager.IsLogined(target))
            {
                target.SendMessage(ChatColors.Yellow + StringResource.DoNotLogined);
                return null;
            }

            return packet;
        }
    }
}
