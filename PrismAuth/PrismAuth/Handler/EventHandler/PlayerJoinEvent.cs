﻿using MiNET;
using MiNET.Utils;
using PrismAuth.Account;
using PrismAuth.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class PlayerJoinEvent
    {
        public void PlayerJoin(object sender, PlayerEventArgs e)
        {
            if (!AccountManager.LoginedPlayer.Keys.Contains(e.Player.Username))
            {
                e.Player.SendMessage(ChatColors.Yellow + StringResource.DoNotLogined);
            }
        }
    }
}
