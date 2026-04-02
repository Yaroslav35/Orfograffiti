using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orfograffiti
{
    internal class ThemeApp
    {
        public static Dictionary<string, List<string>> ThemeDict = new Dictionary<string, List<string>>()
        {
            {"white", new List<string>() {"whitefon.jpg","e6e6e6","9c9c9c"}},
            {"blue", new List<string>() {"bluefon.jfif", "c9baff","8973fa"}},
            {"red", new List<string>() {"redfon.jfif", "ffbaba","d94a4a"}},
            {"yellow", new List<string>() {"yellowfon.jfif", "fff0ba","c29c2d"}},
            {"green", new List<string>() {"greenfon.jfif", "baffbb","36a12f"}}
        };
    }
}
