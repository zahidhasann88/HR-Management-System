using HR_Management_System.DTOs;
using HR_Management_System.Interfaces;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            //var em = await _employeService.GetEmployes();
            //return StatusCode(em.StatusCode, em);
            return await Task.FromResult(_employeService.GetEmployes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmploye(int id)
        {
            var r = await _employeService.GetEmploye(id);
            return StatusCode(r.StatusCode, r);
        }

        [HttpPost("create-employee")]
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
        [HttpGet("Salary")]
        public async Task<ActionResult<ResponseDto>> GetSalary()
        {
            var sa = await _employeService.GetSalary();
            return StatusCode(sa.StatusCode, sa);
        }
        //[HttpPost("lol2")]
        //public async Task<IActionResult> Lol2([FromBody] FuckDto input)
        //{
        //    // method - 1
        //    //using var transaction = _context.Database.BeginTransaction();
        //    //try
        //    //{
        //    //    _context.Employees.Add(input.Fuck1);
        //    //    await _context.SaveChangesAsync();
        //    //    _context.Salaries.Add(input.Fuck2);
        //    //    await _context.SaveChangesAsync();
        //    //    transaction.Commit();
        //    //    return Ok();
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    transaction.Rollback();
        //    //    return BadRequest();
        //    //}
        //}
    }
}
