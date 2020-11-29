namespace ShopWebApp.Constants
{
    public static class Constant
    {
        /*URL API*/
        public const string BASE_ADDRESS = "https://localhost:44366/api/";
        public const string API_PRODUCT = "products";
        public const string API_USER = "users";
        public const string API_SUPPLIER = "suppliers";
        public const string API_CATEGORY = "categories";
        /*Method*/
        public const string API_ADD_USER = API_USER + "/AddUser";
        public const string API_GET_PRODUCTS_RELATED = API_PRODUCT + "/GetProductsRelated";
        public const string API_TOTAL_PRODUCT = API_PRODUCT + "/GetTotalProduct";

        /*Status code*/
        public const int ERROR_CODE_NOT_FOUND = 404;
        public const string NOT_FOUND_MESSAGE = "The email address or password is incorrect.";
        public const int CODE_OK = 200;
        public const int ERROR_CODE_DUPLICATE_DATA = 452;
        public const string DUPLICATE_DATA_MESSAGE = "Email address is already used.";
        public const int ERROR_CODE_INTERNAL = 500;
        public const string INTERNAL_MESSAGE = "something went wrong please try again or contact to administrator to get more information.";
        public const int ERROR_CODE_SQL_CONNECTION = 503;
        public const string SQL_CONNECTION_MESSAGE = "Can not connect to database.";

        public const string PREFIX_PHONE = "+84"; 
        public const int PAGE_SIZE = 10;

        /*Url resource image*/
        public const string url = "https://onlineshop.blob.core.windows.net/";
        public const string urlImage = url + "sanpham";
        public const string urlLogo = url + "logo/logo.png";
        public const string urlFavicon = url + "logo/favicon.jpg";

        public const string SESSION_USER = "_user";

        public const int DETAILED_PRODUCT_QUANTITY = 4;

    }
}
