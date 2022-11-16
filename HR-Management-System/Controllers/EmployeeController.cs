using HR_Management_System.DTOs;
using HR_Management_System.Interfaces;
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
        private readonly IEmployeService _employeService;

        public EmployeeController(Models.PostgresContext context, IEmployeService employeService)
        {
            _context = context;
            _employeService = employeService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetEmployees()
        {
            var em = await _employeService.GetEmployes();
            return StatusCode(em.StatusCode, em);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmploye(int id)
        {
            var r = await _employeService.GetEmploye(id);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmploye(Employee employee)
        {
            var r = await _employeService.PostEmploye(employee);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmploye(int id, Employee employee)
        {
            var r = await _employeService.PutEmploye(id, employee);
            return StatusCode(r.StatusCode, r);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var r = await _employeService.DeleteEmploye(id);
            return StatusCode(r.StatusCode, r);
        }
    }
}
