using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismAuth.Cryptography
{
    public class EncryptResult
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public byte[] CipherText { get; set; }
    }
}
