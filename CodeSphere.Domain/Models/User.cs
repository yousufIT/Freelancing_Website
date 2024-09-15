using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeSphere.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public double Rating { get; set; }
        public Profile Profile { get; set; }
        public List<Project> Projects { get; set; }
        public List<Bid> Bids { get; set; }
        public List<Review> ReviewsReceived { get; set; } 
        public List<Review> ReviewsGiven { get; set; } 
    }
}
