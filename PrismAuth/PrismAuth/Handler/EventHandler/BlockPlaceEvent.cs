using PrismAuth.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class BlockPlaceEvent
    {
        public void BlockPlace(object sender, MiNET.Worlds.BlockPlaceEventArgs e)
        {
            if (!Account.Account.IsLogined(e.Player))
            {
                e.Player.SendMessage("please log in first.");
                e.Cancel = true;
            }
        }
    }
}
