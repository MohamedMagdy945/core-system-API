namespace AppCoreSystem.Domain.Constant
{
    public static class Permissions
    {
        public static class Users
        {
            public const string Create = "user.create";
            public const string Delete = "user.delete";
            public const string View = "user.view";
            public const string Update = "user.update";
        }

        public static class Roles
        {
            public const string Assign = "role.assign";
            public const string View = "role.view";
        }
    }
}
