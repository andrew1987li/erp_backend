using System.Collections.Generic;
using System.Linq;

namespace Tw.Com.Kooco.Admin.Misc.Definition
{
    public class Platform
    {
        public static readonly Dictionary<int, string> List;
        private static readonly Dictionary<int, string> AllDefinedPlatform;

        static Platform()
        {
            AllDefinedPlatform = new Dictionary<int, string>(3)
            {
                { -1, "All" },
                { 0, "Free Bonus" },
                { 1, "Google Play" },
                { 2, "Apple" }
            };

            List = AllDefinedPlatform;
        }

        public static string GetName(int key)
        {
            return AllDefinedPlatform.Keys.Contains(key) ? AllDefinedPlatform[key] : "Undefined";
        }
    }
}