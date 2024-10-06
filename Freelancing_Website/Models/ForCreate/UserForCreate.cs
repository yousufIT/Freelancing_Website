namespace Freelancing_Website.Models.ForCreate
{
    public class UserForCreate
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public double Rating { get; set; }
    }
}
