namespace CA.CrossCuttingConcerns.Constants
{
    public static class ConfigurationConstants
    {
        public const int DefaultCacheSize = 1024;

        public const string CorsPolicy = nameof(CorsPolicy);

        public const string AllowedOrigins = "Cors:AllowedOrigins";

        public const string DefaultContentType = "application/json";
    }
}
