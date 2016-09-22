using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeBot.Models
{
    public enum CommandType
    {
        None,
        OrderFood,
        BookTable
    }

    public class Command
    {
        public CommandType CommandType;
        public string Time;
        public string Date;
        public int Guests;
    }
}