using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PSH.Strings {
    public class PearlStrings {
        public static String RemoveBetween(String str, String start, String end) {
            String regex = $"{start}.*?{end}";
            return Regex.Replace(str, regex, "");
        }
    }
}