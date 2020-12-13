namespace ShopWebApp.Constants
{
    public static class Constant
    {
        /*URL API*/
        //public const string BASE_ADDRESS = @"https://projectonlineshopapi.azurewebsites.net/api/";
        public const string BASE_ADDRESS = @"https://localhost:44366/api/";
        public const string API_PRODUCT = "products";
        public const string API_USER = "users";
        public const string API_CART = "carts";
        public const string API_SUPPLIER = "suppliers";
        public const string API_CATEGORY = "categories";
        /*Method*/
        public const string API_ADD_USER = API_USER + "/AddUser";
        public const string API_RESET_PASSWORD = API_USER + "/RestoreAccount";
        public const string API_CHANGE_PASSWORD = API_USER + "/ChangePassword";
        public const string API_UPDATE_PROFILE = API_USER + "/UpdateProfile";
        public const string API_GET_PRODUCTS_RELATED = API_PRODUCT + "/GetProductsRelated";
        public const string API_SEARCH_PRODUCTS = API_PRODUCT + "/SearchProduct";
        public const string API_TOTAL_PRODUCT = API_PRODUCT + "/GetTotalProduct";
        public const string API_GET_NUM_OF_CART_ITEMS = API_CART + "/GetNumOfCartItems";
        public const string API_CART_CHECK_OUT = API_CART + "/CheckOut";
        /* Status code*/
        public const int USER_NOT_EXIST = 513;
        public const int EMPTY_LIST = 600;
        public const int ERROR_CODE_CART_ITEM_INVALID = 606;
        public const string CART_ITEM_INVALID_MESSAGE = "Your purchase was canceled.";
        public const int ERROR_CODE_BAD_REQUEST = 400;
        public const string BAD_REQUEST_MESSAGE = "Data request is invalid";
        public const int ERROR_CODE_AUTHENTICATION = 401;
        public const string AUTHENTICATION_MESSAGE = "User have not loged in yet. Please log in user";
        public const int ERROR_CODE_NOT_FOUND = 404;
        public const string NOT_FOUND_MESSAGE = "The email address or password is incorrect.";
        public const string ADD_ERROR_MESSAGE = "Item was added failed. This item is unavailable.";
        public const int CODE_OK = 200;
        public const string OK_MESSAGE = "Successfully";
        public const int ERROR_CODE_DUPLICATE_DATA_EMAIL = 452;
        public const string DUPLICATE_DATA_EMAIL_MESSAGE = "Email address is already used.";
        public const int ERROR_CODE_DUPLICATE_DATA_USER_NAME = 453;
        public const string DUPLICATE_DATA_USER_NAME_MESSAGE = "User name is already used.";
        public const int ERROR_CODE_INTERNAL = 500;
        public const string INTERNAL_MESSAGE = "something went wrong please try again or contact to administrator to get more information.";
        public const int ERROR_CODE_SQL_CONNECTION = 503;
        public const string SQL_CONNECTION_MESSAGE = "Can not connect to database.";
        public const string REQUEST_RELOAD_MESSAGE = "Can not connect to database.";

        public const string PREFIX_PHONE = "+84"; 
        public const int PAGE_SIZE = 10;
        public const int NOT_FOUND_INT = 0;
        /* Url resource image*/
        public const string URL = "https://onlineshop.blob.core.windows.net/";
        public const string URL_IMAGE = URL + "products";
        public const string URL_LOGO = URL + "logo/logo.png";
        public const string URL_FAVICON = URL + "logo/favicon.jpg";

        public const string SESSION_USER = "_user";

        public const int DETAILED_PRODUCT_QUANTITY = 4;

        /* Layout name */
        public const string FOOTER_PARTIAL_VIEW_NAME= "_FooterPartialView";
        public const string HEADER_PARTIAL_VIEW_NAME = "_HeaderPartialView";
        public const string NAVIGATION_MENU_VIEW_NAME = "_NavigationMenu";
        public const string SUPPLIER_PARTIAL_VIEW_NAME = "_SupplierPartialView";
        public const string TAG_PARTIAL_VIEW_NAME = "_TagsPartialView";
        /* View name */
        public const string INDEX = "Index";
        public const string ERROR = "Error";
        // User page
        public const string SIGN_IN = "SignIn";
        public const string SIGN_UP = "SignUp";
        public const string UPDATE_PROFILE = "UpdateProfile";
        public const string RESET = "Reset";
        public const string CHANGE_PASSWORD = "ChangePassword";
        // Product page
        public const string DETAIL = "Detail";
        public const string DETAIL_WITH_CONDITION = "DetailWithCondition";
        public const string SEARCH_PARTIAL = "SearchPartial";
        // Cart page
        public const string PAYMENT = "Payment";
        // Paypal
        public const string CLIENT_ID_PAYPAL = "AZyfjEW5oOePcqf4VBrIzZ_rE95Jhx7Xffbw6-pSx-sUjUmPXuQuRnigTyM8za-wrcACzvGHQilIMiS1";



    }
}
