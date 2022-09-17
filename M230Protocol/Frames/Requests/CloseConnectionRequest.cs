using M230Protocol.Frames.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M230Protocol.Frames.Requests
{
    class CloseConnectionRequest : Request
    { 
        public CloseConnectionRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.CloseConnection;
        }
    }
}