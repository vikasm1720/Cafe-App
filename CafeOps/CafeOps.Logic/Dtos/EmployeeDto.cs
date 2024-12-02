using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.Logic.Dtos
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int DaysWorked { get; set; }
        public Guid? CafeId { get; set; }
        public string Cafe { get; set; }
        public string StartDate { get; set; }
    }

}
