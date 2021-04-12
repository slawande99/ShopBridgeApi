using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridgeApi.Models
{
    public class AuthRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}