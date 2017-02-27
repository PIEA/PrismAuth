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
    public class BlockBreakEvent
    {
        public void BlockBreak(object sender, MiNET.Worlds.BlockBreakEventArgs e)
        {
            if (!AccountManager.IsLogined(e.Player))
            {
                e.Player.SendMessage(ChatColors.Yellow + StringResource.DoNotLogined);
                e.Cancel = true;
            }
        }
    }
}
