namespace Valhalla.Shared.Constants
{
    public static class ApiRoutes
    {
        public static class Vikings
        {
            public const string Base = "api/vikings";
            public const string GetAll = Base;
            public const string GetById = Base + "/{0}";
            public const string Create = Base;
            public const string Update = Base + "/{0}";
            public const string Delete = Base + "/{0}";
        }
    }
}
