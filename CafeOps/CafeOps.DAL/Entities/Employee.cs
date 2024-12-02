using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.DAL.Entities
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Guid? CafeId { get; set; }
        public DateTime StartDate { get; set; }
        public Cafe? Cafe { get; set; }
    }
}
