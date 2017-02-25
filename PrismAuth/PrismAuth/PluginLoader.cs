using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiNET;
using MiNET.Utils;
using PrismAuth.Resources;
using System.Globalization;

namespace PrismAuth
{
    public class PluginLoader : PrismAuth
    {
        private const string defaultLang = "en-US";

        protected override void OnEnable()
        {
            SetLanguage();
        }

        public void SetLanguage()
        {
            var userLang = Config.GetProperty("lang",  defaultLang);

            try
            {
                StringResource.Culture = new CultureInfo(userLang);
            }
            catch (CultureNotFoundException)
            {
                StringResource.Culture = new CultureInfo(defaultLang);
            }
        }
    }
}
