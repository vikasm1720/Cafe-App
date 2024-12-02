﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.DAL.Entities
{
    public class Cafe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Logo { get; set; }
        public string Location { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}