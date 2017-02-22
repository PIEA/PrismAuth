using MiNET;
using PrismAuth.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Handler.EventHandler
{
    public class PlayerJoinEvent
    {
        private Account.Account accountManager;

        public PlayerJoinEvent(Account.Account manager)
        {
            this.accountManager = manager;
        }

        public async void PlayerJoinAsync(object sender, PlayerEventArgs e)
        {
            if (await this.accountManager.IsRegisteredAsync(e.Player))
            {
                Popup popup = new Popup()
                {
                    Message = "please login first"
                };
                e.Player.AddPopup(popup);
            }
            else
            {
                Popup popup = new Popup()
                {
                    Message = "please register first"
                };
                e.Player.AddPopup(popup);
            }
        }
    }
}
