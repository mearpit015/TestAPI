using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Model
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
    }
}
