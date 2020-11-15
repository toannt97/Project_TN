namespace ShopAPI.Constants
{
    public class Constant
    {
        public static int DUPLICATE_DATA = 452;
        public static string DUPLICATE_DATA_MESSAGE = "Email address is already used";


        // SMTP port
        public static int PORT = 587;
        public static string GMAIL_HOST = "smtp.gmail.com";
        public static string YAHOO_HOST = "smtp.mail.yahoo.com";
        public static string OUTLOOK_HOST = "smtp.office365.com";
        // Mail's subject
        public static string SUBJECT = "Register New-store's account";
        public static string CONTENT =
            "<div>" +
                ""+
            "</div>";
    }
}
