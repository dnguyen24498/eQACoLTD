using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Constants
{
    public static class ConstantProperties
    {
        public static string BackendAPIEndPoint = "https://localhost:5001";
        public static string GetUserProfilesPagingEndPoint(int page) {
            return $"/api/users?pageIndex={page}";
        }

        public static string GetUsersRolesEndPoint(string userName)
        {
            return $"/api/users/{userName}/roles";
        }
        public static string PutUserRolesEndPoint(string userName)
        {
            return $"/api/users/{userName}/roles";
        }

        public static string GetAccountProfileEndPoint = "/api/accounts/profile";
        public static string PutAccountProfileEndPoint = "/api/accounts/profile";

        public static string GetRolesEndPoint(int page)
        {
            return $"/api/roles?pageIndex={page}";
        }
    }
}
