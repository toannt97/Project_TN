using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Contants
{
    public static class Contant
    {
        /*URL API*/
        public static string BASE_ADDRESS = "https://localhost:44366/api/";
        public static string API_PRODUCT = "products";
        public static string API_USER = "users";
        public static string API_SUPPLIER = "suppliers";
        public static string API_CATEGORY = "categories";
        /*Method*/
        public static string API_ADD_USER = API_USER + "/AddUser";

        /*Status code*/
        public static int ERROR_CODE_NOT_FOUND = 404;
        public static string NOT_FOUND_MESSAGE = "The email address or password is incorrect.";
        public static int CODE_OK = 200;
        public static int ERROR_CODE_DUPLICATE_DATA = 452;
        public static string DUPLICATE_DATA_MESSAGE = "Email address is already used.";
        public static int ERROR_CODE_INTERNAL = 500;
        public static string INTERNAL_MESSAGE = "something went wrong please try again or contact to administrator to get more information.";
        public static int ERROR_CODE_SQL_CONNECTION = 503;
        public static string SQL_CONNECTION_MESSAGE = "Can not connect to database.";

        public static string PREFIX_PHONE = "+84";
        public static int PAGE_SIZE = 9;

        public static string urlImage = "https://onlineshop.blob.core.windows.net/sanpham";
        public static string urlLogo = "https://onlineshop.blob.core.windows.net/logo";


    }
}
