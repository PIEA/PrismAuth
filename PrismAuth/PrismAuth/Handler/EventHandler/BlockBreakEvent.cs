using PrismAuth.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class BlockBreakEvent : BaseEventHandler
    {
        public void BlockBreak(object sender, MiNET.Worlds.BlockBreakEventArgs e)
        {
            if (!this.AccountManager.IsLogined(e.Player))
            {
                e.Player.SendMessage("please log in first.");
                e.Cancel = true;
            }
        }
    }
}
