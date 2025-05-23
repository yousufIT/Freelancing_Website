﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSphere.Domain.Models
{
    public class Review : IBase
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; } 
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }
        public bool IsDeleted { get; set; }

    }
}
