using HR_Management_System.DTOs;
using HR_Management_System.Interfaces;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeService _employeService;

        public EmployeeController(IEmployeService employeService)
        {
            _employeService = employeService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _employeService.GetEmployeesAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> GetEmploye(int id)
        {
            var r = await _employeService.GetEmployeeByIdAsync(id);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPost("create-employee")]
        [AllowAnonymous]
        public async Task<ActionResult<Employee>> PostEmploye(Employee employee)
        {
            var r = await _employeService.PostEmployeeAsync(employee);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutEmploye(int id, Employee employee)
        {
            var r = await _employeService.PutEmployeeAsync(id, employee);
            return StatusCode(r.StatusCode, r);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var r = await _employeService.DeleteEmployeeAsync(id);
            return StatusCode(r.StatusCode, r);
        }
        [HttpGet("Salary")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto>> GetSalary([FromBody] LolDto lolDto)
        {
            var sa = await _employeService.GetSalaryAsync(lolDto.Skip, lolDto.Take);
            return StatusCode(sa.StatusCode, sa);
        }
    }
}
