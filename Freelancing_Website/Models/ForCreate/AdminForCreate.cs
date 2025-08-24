namespace Freelancing_Website.Models.ForCreate
{
    public class AdminForCreate
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // plain password input
        public string Role { get; set; } // ignore or set to Admin
        public double Rating { get; set; } = 0;
    }
}
