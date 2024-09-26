using CodeSphere.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodeSphere.Domain.Models
{
    public class User :IdentityUser, IBase
    {
        public string Name { get; set; }
        public double Rating { get; set; } // Rating (out of 5 stars)
        public string Role { get; set; }   // Freelancer, Client, or Admin

        public User()
        {
            Rating = 0;
        }
        public bool IsDeleted { get; set ; }
        int IBase.Id { get ; set ; }
    }
}
