using System;

namespace WebApp.Controllers
{
    public static class Utils
    {
        public static DateTime ServerNow
        {
            get
            {
                return DateTime.Now.AddDays(0);
            }
        }
        public static string API_PATH = "http://localhost:50000/Remote";
        public static string ImageURL = "http://localhost:50000/Remote/UploadImages/";
        // public static string ImageURL = "http://nawafizapp.gear.host/UploadImages/";
        public static string ImageDefaultName = "index.png";
    }
    public enum LanguageHelper { ARABIC = 1, ENGLISH = 2 };
    public enum ClassifyStateHelper { ACTIVE = 1, STOPPED = 2, DISPOSE = 3 };



    public static class Roles
    {
        public static string Admin = "AdminRole";
        public static string Nurse = "NurseRole";
        public static string Doctor = "DoctorRole";
    }
    public enum Sorts : long
    {
        DateAddedUp = 1, DateAddedDown = 2,
        NameUpDown = 3, NameDownUp = 4,
        PriceUpDown = 5, PriceDownUp = 6,
        ArabicNameUp = 7, ArabicNameDown = 8,
        EnglishNameUp = 9, EnglishNameDown = 10,
        StatusUp = 11, StatusDown = 12,
        IdUp = 13, IdDown = 14,
        OrderIdUp = 15, OrderIdDown = 16,
        CustomerNameUp = 17, CustomerNameDown = 18,
        OrederStatusUp = 19, OrederStatusDown = 20,
        TotalUp = 21, TotalDown = 22,
        DateModifiedUp = 23, DateModifiedDown = 24,
        QuantityUp = 25, QuantityDown = 26,
        AttributeGroupIdUp = 27, AttributeGroupIdDown = 28,
        ValueUp = 29, ValueDown = 30,
        InvoiceUp = 31, InvoiceDown = 32,
        EmailUp = 33, EmailDown = 34,
        lockedUp = 35, lockedDown = 36

    };
    public enum RoleTypes : int
    {
        Doctor = 1, Nurse = 2, Admin = 0
    }


}