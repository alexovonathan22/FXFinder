﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FXFinder.Core.DBModels
{
    public static class UserRoles
    {
        // Active
        public const string Admin = "Administrator";
        public const string User = "User";
        //In active
        public const string Noob = "Noob";
        public const string Elite = "Elite";
    }

    public static class AuthorizedUserTypes
    {
        public const string Admin = "AuthorizedAdmin";
        public const string Users = "AuthorizedUsers";
        public const string UserAndAdmin = "AuthorizedUserAdmin";

    }
}
