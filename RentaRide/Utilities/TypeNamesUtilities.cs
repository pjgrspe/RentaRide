namespace RentaRide.Utilities
{
    public class TypeNamesUtilities
    {
        //public const int logTypeID1 = 1;
        //public const string logTypeName1 = "Manual";
        //public const int logTypeID2 = 2;
        //public const string logTypeName2 = "Maintenance";
        //public const int logTypeID3 = 3;
        //public const string logTypeName3 = "Change Oil";
        //public const int logTypeID4 = 4;
        //public const string logTypeName4 = "Repair";
        //public const int logTypeID5 = 5;
        //public const string logTypeName5 = "Rented";
        //public const int logTypeID6 = 6;
        //public const string logTypeName6 = "Returned";
        //public const int logTypeID7 = 7;
        //public const string logTypeName7 = "Edited";
        //public const int logTypeID8 = 8;
        //public const string logTypeName8 = "Deleted";
        public static readonly string[] logTypeNames = 
            { 
                "Initial",      //0
                "Manual",       //1
                "Maintenance",  //2
                "Oil Change",   //3
                "Repair",       //4
                "Rented",       //5
                "Returned",     //6
                "Edited",       //7
                "Deleted",      //8
                "Listed"        //9
            };
        
        public static readonly string[] logTypeclassNames = 
            { 
                "initial",      //0
                "manual",       //1
                "maintenance",  //2
                "oilchange-logs",//3
                "repair",       //4
                "rented",       //5
                "returned",     //6
                "edited",       //7
                "deleted",      //8
                "listed"        //9
            };

        public static readonly string[] fuelTypeNames = 
            { 
                "",             //0
                "Gasoline",     //1
                "Diesel",       //2
                "Electric"      //3
            };
        public static readonly string[] carStatusNames = 
            { 
                "",             //0
                "Available",    //1
                "Rented",       //2
                "Maintenance",  //3 
                "Repair"        //4
            };
        public static readonly string[] carStatusClassNames = 
            { 
                "",             //0
                "available",    //1
                "rented",       //2
                "maintenance",  //3 
                "repair"        //4
            };
        public static readonly string[] ListingStatusNames = 
            { 
                "",             //0
                "Listed",    //1
                "Hidden",       //2
                "Unlisted"   //3 
            };
        public static readonly string[] ListingStatusClassNames = 
            { 
                "",             //0
                "listed",    //1
                "hidden",       //2
                "unlisted"   //3 
            };
        public static readonly string[] PayTypeNames = 
            { 
                "",             //0
                "Cash",         //1
                "Credit Card",  //2
                "Debit Card",   //3
                "GCash",        //4
                "Bank Transfer" //5
            };

        public static readonly string[] CarTypeNames = 
            { 
                "",             //0
                "Compact",      //1
                "Sedan",        //2
                "SUV",          //3
                "VAN",          //4
                "Minivan",      //5
                "Electric",     //6
                "Hybrid",       //7
                "Luxury",       //8
                "Sports"        //9
            };

        public static readonly string[] OrderStatusNames = 
            { 
                "",             //0
                "Cancelled",    //1
                "Confirmed",    //2
                "Pending",      //3
                "OnGoing",      //4
                "Denied"        //5
            };

        public static readonly string[] OrderStatusClassNames = 
            { 
                "",             //0
                "cancelled",    //1
                "confirmed",    //2
                "pending",      //3
                "ongoing",      //4
                "denied"        //5
            };

        public static readonly string[] PayStatusNames = 
            { 
                "",             //0
                "Active",       //1
                "Inactive",     //2
                "Banned"        //3
            };
    }
}
