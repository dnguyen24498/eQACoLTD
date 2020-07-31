using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Constants
{
    public static class ConstantProperties
    {
        public static int PageSize = 20;
        public static string BackendAPIEndPoint = "https://localhost:5001";
        public static string GetAccountPagingEndPoint(int page) {
            return $"/api/accounts?pageIndex={page}";
        }

        public static string GetAccountRolesEndPoint(string userName)
        {
            return $"/api/accounts/{userName}/roles";
        }
        public static string PutAccountRolesEndPoint(string userName)
        {
            return $"/api/accounts/{userName}/roles";
        }

        public static string GetUserProfileEndPoint = "/api/users/profile";
        public static string PutUserProfileEndPoint = "/api/users/profile";
    }
}
