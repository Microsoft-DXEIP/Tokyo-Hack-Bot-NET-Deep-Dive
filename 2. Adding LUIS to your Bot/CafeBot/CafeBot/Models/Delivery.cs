using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeBot.Models
{
    [Serializable]
    public class Delivery
    {
        public Order Order;
        public string Address;
    }
}