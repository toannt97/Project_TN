namespace ShopAPI.Constants
{
    public static class Constant
    {
        public const int OK = 200;
        public const int BAD_REQUEST = 400;
        public const int NOT_FOUND = 404;
        public const int DUPLICATE_DATA_EMAIL = 452;
        public const int DUPLICATE_DATA_USER_NAME = 453;
        public const int INTERNAL_ERROR = 500;
        public const int SQL_EXECUTION_ERROR = 512;
        public const int USER_NOT_EXIST = 513;
        public const int EMPTY_LIST = 600;
        // SMTP port
        public const int PORT = 587;
        public const string GMAIL_HOST = "smtp.gmail.com";
        public const string YAHOO_HOST = "smtp.mail.yahoo.com";
        public const string OUTLOOK_HOST = "smtp.office365.com";
        // Mail's subject
        public const string SUBJECT_REGISTER_ACCOUNT = "Register New-store's account";
        public const string MAIL_CONFIGURATION = "MailAccount";

        public const string DOMAIN_IMAGE = "https://onlineshop.blob.core.windows.net";
        public const string DOMAIN_IMAGE_LOGO = "https://onlineshop.blob.core.windows.net/logo/logo.png";
        public const string DOMAIN_IMAGE_FAVICON = "https://onlineshop.blob.core.windows.net/logo/favicon.jpg";
        public static int INIT_QUANTITY = 1;
        public static int IS_ACTIVE = 0;
        public const int NOT_FOUND_INT = 0;
        public static int IS_DELETED = -1;
        public static int IS_DELETED_ORDER = -1;
        public const int SIZE_STRING_LETTERS_PASSWORD = 4;
        public const int MIN_VALUE_PASSWORD = 1000;
        public const int MAX_VALUE_PASSWORD = 9999;

        public static string CONTENT_VERIFY_EMAIL(string userName, string baseUrl, int id, string email)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                  <meta charset='utf-8'/>
                  <link rel='shortcut icon' type = 'image/x-icon' href='')'/>
            </head>
            <body>
            <div style='font-family: Arial; background:#F8F8F8;'>
                <figure 
                    style='width: 200px;
                        padding: 0 10px 10px 0;
                        border-bottom: 1px solid #ccc;
                        border-right: 1px solid #ccc;'>
                    <img src='" + DOMAIN_IMAGE_LOGO + @"' alt='Logo New-store' style='width:100%'>
                </figure>
                <div class='container' style='background:#fff; border:1px solid #ddd'>
                    <p style='font-size: 14px; margin: 35px 0 35px 0; '>
                        Hi " + userName +
                        @",<br>
                        <br>
                        Thanks for creating your New-store account. To coninue, please verify your email address by clicking the button below.
                        <a href='" + baseUrl + $"/api/users/CreateUser?id={id}&email={email}'" + @"
                            style='cursor: default;
                                display: block;
                                margin: 20px auto;
                                color: white;
                                background: #4B985E;
                                text-decoration: none;
                                padding:10px;
                                width: 120px;
                                font-weight: 600;
                                text-align:center;'>
                                <span>Verify email</span>
                        </a>
                        If you don't create this account, please ignore this mail.
                    </p>
                </div>
            </div>
            </body>
            </html>";
        }

        public static string CONTENT_RECOVERY_ACCOUNT(string userName, string newPassword)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                  <meta charset='utf-8'/>
                  <link rel='shortcut icon' type = 'image/x-icon' href='')'/>
            </head>
            <body>
            <div style='font-family: Arial; background:#F8F8F8;'>
                <figure 
                    style='width: 200px;
                        padding: 0 10px 10px 0;
                        border-bottom: 1px solid #ccc;
                        border-right: 1px solid #ccc;'>
                    <img src='" + DOMAIN_IMAGE_LOGO + @"' alt='Logo New-store' style='width:100%'>
                </figure>
                <div class='container' style='background:#fff; border:1px solid #ddd'>
                    <p style='font-size: 14px; margin: 35px 0 35px 0; '>
                        Hi " + userName + @",<br>
                        We're sending this email because you requested a password reset. Here is your new password and please don't share this password to anyone.
                        <br>
                        Password: " + newPassword 
                        + @"<br>
                        <br>
                        <br>--------------------------------------<br>
                        Remember, if there's anything we can do to help your business be more successfully online, just let us know.<br>
                        Thanks and Best regards,
                        New Store
                    </p>
                </div>
            </div>
            </body>
            </html>";
        }
    }
}