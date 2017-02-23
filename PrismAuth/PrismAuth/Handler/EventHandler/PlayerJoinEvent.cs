using MiNET;
using PrismAuth.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class PlayerJoinEvent : BaseEventHandler
    {
        public void PlayerJoin(object sender, PlayerEventArgs e)
        {
            if (this.AccountManager.IsRegistered(e.Player))
            {
                e.Player.SendMessage("plz login first.");
            }
            else
            {
                e.Player.SendMessage("plz register first.");
            }
        }
    }
}
