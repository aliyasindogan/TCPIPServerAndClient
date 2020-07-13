using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseRequest
    {
        public string IpAddress { get; set; }
        public int TcpPort { get; set; }
        public string SendData { get; set; }
    }
}