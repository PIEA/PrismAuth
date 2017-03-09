using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismAuth.Resources;

namespace PrismAuth.Util
{
    public class Result<T> : Result
    {
        public T Data { get; set; }

        public Result() : base()
        {
        }
    }

    public class Result
    {
        public bool Successed { get; set; }
        public string Message { get; set; }

        public Result()
        {
            Successed = false;
            Message = StringResource.MessageNotExist;
        }
    }
}
