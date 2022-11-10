using HR_Management_System.DTOs;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Management_System.Controllers

{
    public class MyClass1
    {
        public int? Employeeid { get; set; }
        //public int? Id { get; set; }
    }

    public class MyClass2
    {
        public int? Employeeid { get; set; }
        public string EmployeName { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly Models.PostgresContext _context;

        public EmployeeController(Models.PostgresContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetEmployees()
        {
            List<Employee> employees = await _context.Employees.ToListAsync();


            if (employees.Count > 0)
            {
                return StatusCode(StatusCodes.Status200OK, new ResponseDto
                {
                    Message = "Employee List",
                    Success = true,
                    Payload = employees
                });
            }

            return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
            {
                Message = "No Employee",
                Success = false,
                Payload = null
            });

        }

        [HttpPost("GetEmployeeById")]
        public async Task<ActionResult<ResponseDto>> GetEmployees([FromBody] MyClass1 input)
        {
            if (input.Employeeid == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "id error",
                    Success = false,
                    Payload = null
                });
            }

            var employee = await _context.Employees.Where(VALK => VALK.Employeeid == input.Employeeid).FirstOrDefaultAsync();

            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "No Employee",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = " employee info",
                Success = true,
                Payload = employee
            });
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult<ResponseDto>> PutEmployee([FromBody] Employee input)
        {
            if (input.Employeeid == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id is null",
                    Success = false,
                    Payload = null
                });
            }
            if (input.EmployeName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " Name is null",
                    Success = false,
                    Payload = null
                });
            }

            //old 
            Employee employee = await _context.Employees.Where(i => i.Employeeid == input.Employeeid).FirstOrDefaultAsync();
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "this employee not listed your db",
                    Success = false,
                    Payload = null
                });
            }

            //new
            employee.EmployeName = input.EmployeName;
            employee.DependentName = input.DependentName;
            employee.Age = input.Age;
            employee.Dependentid = input.Dependentid;
            employee.DependentName = input.DependentName;
            employee.Position = input.Position;
            employee.Joindate = input.Joindate;
            employee.Salary = input.Salary;
            _context.Employees.Update(employee);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "updating Unsuccesfull",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "updating complete",
                Success = true,
                Payload = null
            });
        }
        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<ResponseDto>> PostEmployee([FromBody] Employee input)
        {
            if (input.EmployeName == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " name is null",
                    Success = false,
                    Payload = null
                });
            }

            //old
            Employee country = await _context.Employees.Where(i => i.Employeeid == input.Employeeid).FirstOrDefaultAsync();
            if (country != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "already exist",
                    Success = false,
                    Payload = null
                });
            }


            _context.Employees.Add(input);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "creating error",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "creating done",
                Success = true,
                Payload = new { input.Employeeid } // optional, can be null too like update
            });
        }
        // DELETE: api/Countries/5
        [HttpDelete("DeleteEmployee")]
        public async Task<ActionResult<ResponseDto>> DeleteEmployee([FromBody] MyClass1 input)
        {
            if (input.Employeeid == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = " id is null",
                    Success = false,
                    Payload = null
                });
            }

            Employee employee = await _context.Employees.Where(i => i.Employeeid == input.Employeeid).FirstOrDefaultAsync();
            if (employee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "not exist your db",
                    Success = false,
                    Payload = null
                });
            }

            _context.Employees.Remove(employee);
            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "deleting error",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "deleted",
                Success = true,
                Payload = new { input.Employeeid } // optional, can be null too like update
            });
        }
        private bool EmployeeExists(int? id)
        {
            return _context.Employees.Any(e => e.Employeeid == id);
        }
    }
}
