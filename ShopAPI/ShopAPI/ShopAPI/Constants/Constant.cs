
namespace ShopAPI.Constants
{
    public static class Constant
    {
        public const int DUPLICATE_DATA = 452;
        public const string DUPLICATE_DATA_MESSAGE = "Email address is already used.";
        public const int NOT_FOUND = 404;
        public const string NOT_FOUND_MESSAGE = "Resource not found.";
        public const int OK = 200;
        public const string OK_MESSAGE = "OK";
        public const int INTERNAL_ERROR = 500;
        public const string INTERNAL_MESSAGE = "A Server Error Occurred Please Contact the Administrator.";
        public const int SQL_EXECUTION_ERROR = 512;
        public const string SQL_EXECUTION_MESSAGE = "SQL execution error.";
        // SMTP port
        public const int PORT = 587;
        public const string GMAIL_HOST = "smtp.gmail.com";
        public const string YAHOO_HOST = "smtp.mail.yahoo.com";
        public const string OUTLOOK_HOST = "smtp.office365.com";
        // Mail's subject
        public const string SUBJECT_REGISTER_ACCOUNT = "Register New-store's account";
        public const string MAIL_SECTION = "MailAccount";

        public static string DOMAIN_IMAGE = "https://onlineshop.blob.core.windows.net";
        public static string DOMAIN_IMAGE_LOGO = "https://onlineshop.blob.core.windows.net/logo/logo.png";

        public static int IS_ACTIVE = 0;
        public static string CONTENT(string userName, string baseUrl, int id, string email)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                  <meta charset='utf-8'/>
                  < link rel='shortcut icon' type = 'image/x-icon' href = 'https://onlineshop.blob.core.windows.net/logo/favicon.jpg')' />
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
    }
}