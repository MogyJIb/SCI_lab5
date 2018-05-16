using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Utils
{
    public class Constants
    {
        public const string Client = "client",
            Tour = "tour",
            TourKind = "tour kind";

        public const string DbTablesCache = "cache ten table's elements";

        public const int Variant = 21;
        public const int Seconds = 2 * Variant + 240;

        public const string CachedProfile = "Caching";


        public const string ClientSort = "Client sort",
            TourSort = "Tour sort",
            TourKindSort = "TourKind sort";

        public const string ClientFilter = "Client filter",
            TourFilter = "Tour filter",
            TourKindFilter = "TourKind filter";

    }
}
