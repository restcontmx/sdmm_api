using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Warrior.Data
{
    public static class Validations
    {
        public static string defaultString(string data) {
            return data == null ? "" : data;
        }

        public static int setBooleanValue(bool status)
        {
            return status ? 1 : 0;
        }
    }
}