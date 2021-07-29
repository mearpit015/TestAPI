namespace TestAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TestAPI.Model;
    using System;
    using System.IO;
    using System.Web;

    [Route("api/[controller]")]
    [ApiController]
    public class Employee : Controller
    {

        private readonly ApiContext _context;
        public Employee(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(Employees), 200)]
        public async Task<ActionResult<bool>> Get()
        {
            var response = await GetEmployeesAsync().ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet("checkUser")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> CheckUser(string UserName, string Password)
        {
            if (_context.Users.ToList().Count == 0)
                AddTestData(_context);

            if (_context.Users.Any(x => x.UserName == UserName && x.Password == Password))
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpDelete("remove")]
        public ActionResult<bool> RemoveEmployee(int Id)
        {
            if (Id > 0 && _context.Employees.Any(x => x.Id == x.Id))
            {
                var selectEmp = _context.Employees.FirstOrDefault(x => x.Id == Id);
                selectEmp.IsActive = false;
                _context.SaveChanges();
                //_context.Employees.Remove(selectEmp);
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("updateEmp")]
        public ActionResult<bool> UpdateEmployee(Employees employees)
        {
            if (ModelState.IsValid && _context.Employees.Any(x => x.Id == employees.Id))
            {
                var selectEmp = _context.Employees.FirstOrDefault(x => x.Id == employees.Id);
                selectEmp.FirstName = employees.FirstName;
                selectEmp.LastName = employees.LastName;
                selectEmp.IsActive = true;
                selectEmp.Department = employees.Department;
                selectEmp.Email = employees.Email;
                _context.SaveChanges();


                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("addEmp")]
        public ActionResult<Employees> AddEmployee(Employees employees)
        {
            if (ModelState.IsValid)
            {
                var emp1 = new Employees()
                {

                    FirstName = employees.FirstName,
                    LastName = employees.LastName,
                    Department = employees.Department,
                    Email = employees.Email,
                    IsActive = true
                };
                _context.Employees.Add(emp1);
                var result = _context.SaveChanges();
                //employees.Id = result;
                return Ok(emp1);
            }
            return Ok("[]");
        }

        [HttpGet("getEmployeeById")]
        public ActionResult<Employees> GetEmployeeById(int Id)
        {
            if (Id > 0)
            {
                return _context.Employees.FirstOrDefault(x => x.Id == Id);
            }
            return null;
        }
        private void AddTestData(ApiContext context)
        {

            // context.Database.EnsureDeleted();
            var testUser = new RegisterdUser
            {
                UserName = "mearpit",
                Password = "123"
            };

            context.Users.Add(testUser);

            var testadm = new RegisterdUser
            {
                UserName = "adm-emp",
                Password = "123"
            };

            context.Users.Add(testadm);

            var emp1 = new Employees()
            {

                FirstName = "Arpit",
                LastName = "Srivastava",
                Department = "Dotnet",
                Email = "abc@gmail.com",
                IsActive = true
            };
            context.Employees.Add(emp1);
            var emp2 = new Employees()
            {

                FirstName = "Raj",
                LastName = "Singh",
                Department = "Dotnet",
                Email = "abc2@gmail.com",
                IsActive = true
            };
            context.Employees.Add(emp2);
            var emp3 = new Employees()
            {

                FirstName = "Raj",
                LastName = "Sharma",
                Department = "Java",
                Email = "abc3@gmail.com",
                IsActive = true
            };
            context.Employees.Add(emp3);
            context.SaveChanges();
        }

        private async Task<List<Employees>> GetEmployeesAsync()
        {
            return _context.Employees.Where(x => x.IsActive == true).ToList();
        }

        

    }
}
