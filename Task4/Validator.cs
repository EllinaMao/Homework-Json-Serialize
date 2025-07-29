using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task4
{
    internal static class RestaurantValidator
    {
        public static bool IsValid(string Txt, Regex Pattern)
        {
            if (string.IsNullOrWhiteSpace(Txt))
            { return false; }
            return Pattern.IsMatch(Txt);
        }
    }
}
