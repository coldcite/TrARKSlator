using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrARKSlator
{
    public static class ParsingSupport
    {

        // Checks if msg is a command
        public static bool isPSO2ChatCommand(string msg)
        {
            return (
                msg.StartsWith("/la") ||
                msg.StartsWith("/mla") ||
                msg.StartsWith("/fla") ||
                msg.StartsWith("/cla") ||
                msg.StartsWith("/sym") ||
                msg.StartsWith("/cam") ||
                msg.StartsWith("/cmf") ||
                msg.StartsWith("/pal") ||
                msg.StartsWith("/mpal")
            );

        }

        // Cleans up from crap
        // TODO: too much replace/regex, check out performance and try to find a better method if available.
        public static string chatCleanUp(string msg)
        {

            string cleanStr = "";

            // These are quite simple
            StringBuilder sb = new StringBuilder(msg);
            cleanStr = sb
                .Replace("/a ", "").Replace("/p ", "").Replace("/t ", "")           // Channel modifier
                .Replace("{red}", "").Replace("{ora}", "").Replace("{yel}", "")
                .Replace("{gre}", "").Replace("{blu}", "").Replace("{pur}", "")
                .Replace("{vio}", "").Replace("{bei}", "").Replace("{whi}", "")
                .Replace("{blk}", "").Replace("{def}", "")                          // Colors
                .Replace(" nw", "")                                                 // nw param for /ci
                .Replace("/toge ", "").Replace("/moya ", "")                        // Chat bubble type
                .ToString();

            // These are a little bit more complex, we require regex
            cleanStr = Regex.Replace(cleanStr, @"\/vo\d+\s*", "");        // /voXX command
            cleanStr = Regex.Replace(cleanStr, @"\/mn\d+\s*", "");        // /mn command
            cleanStr = Regex.Replace(cleanStr, @"\ss\d+\s*$*", " ");      // sXXX param for /ci
            cleanStr = Regex.Replace(cleanStr, @"t\d\s*", "");            // tX param for /ci
            cleanStr = Regex.Replace(cleanStr, @"\/ci\d\s\d\s*", "");     // /ciX X command
            cleanStr = Regex.Replace(cleanStr, @"\/ci\d\s*", "");         // /ciX command

            return cleanStr;

        }

    }

}
