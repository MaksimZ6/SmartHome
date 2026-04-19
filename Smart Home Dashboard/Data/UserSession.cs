namespace Smart_Home_Dashboard.Data
{
    public static class UserSession
    {
        public static string? CurrentUser { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);

        public static void Logout() => CurrentUser = null;
    }
}