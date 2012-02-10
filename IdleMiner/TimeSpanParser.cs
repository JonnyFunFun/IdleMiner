using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IdleMiner
{
    class TimeSpanParser
    {
        private const string TIME_PARSER_REGEX =
            @"^((?<days>\d+)\s(?:days?|d)\b)?\s?((?<hours>\d+)\s(?:hours?|hrs?|h)\b)?\s?((?<minutes>\d+)\s(?:minutes?|mins?|m)\b)?\s?((?<seconds>\d+)\s(?:seconds?|secs?|s)\b)?$";

        public static string ToString(TimeSpan span)
        {
            var ret = "";
            if (span.Days > 0)
                ret += String.Format(@"{0} day{1} ", span.Days, span.Days > 1 ? "s" : "");
            if (span.Hours > 0)
                ret += String.Format(@"{0} hour{1} ", span.Hours, span.Hours > 1 ? "s" : "");
            if (span.Minutes > 0)
                ret += String.Format(@"{0} minute{1} ", span.Minutes, span.Minutes > 1 ? "s" : "");
            if (span.Seconds > 0)
                ret += String.Format(@"{0} second{1} ", span.Seconds, span.Seconds > 1 ? "s" : "");
            return ret.Trim();
        }

        public static bool FromString(string dsl, out TimeSpan span)
        {
            var r = new Regex(TIME_PARSER_REGEX);
            var m = r.Match(dsl.Trim().ToLower());
            if (!m.Success)
            {
                span = new TimeSpan();
                return false;
            }
            span = new TimeSpan(Convert.ToInt32(("0" + m.Groups["days"].Value)),
                                Convert.ToInt32(("0" + m.Groups["hours"].Value)),
                                Convert.ToInt32(("0" + m.Groups["minutes"].Value)),
                                Convert.ToInt32(("0" + m.Groups["seconds"].Value)));
            return true;
        }
    }
}
