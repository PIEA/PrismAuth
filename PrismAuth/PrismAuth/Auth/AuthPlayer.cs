using MiNET;
using PrismAuth.Cryptography;
using PrismAuth.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Auth
{

    public class AuthPlayer
    {
        public string Name { get; set; }
        public EncryptResult Password { get; set; }
        public bool IsLogined { get; set; }

        public AuthPlayer(string name)
        {
            this.Name = name;
            if (!Directory.Exists(ContextConstants.AuthPlayerDirectory))
            {
                Directory.CreateDirectory(ContextConstants.AuthPlayerDirectory);
            }


        }

        public void SetPassword(string password)
        {
            this.Password = AES.EncryptString(password);
        }
    }
}
