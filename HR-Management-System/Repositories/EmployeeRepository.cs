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


namespace HR_Management_System.Repositories

{
    public class EmployeeRepository : IEmployeService
    {
        private ResponseDto _responseDto = new();
        private readonly PostgresContext _context;

        public EmployeeRepository(PostgresContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> GetEmployes()
        {
            var employees = await _context.Employees.OrderBy(e => e.Id).ToListAsync();
            if (employees == null || employees.Count == 0)
            {
                _responseDto.Message = "No employees info found";
                _responseDto.Success = false;
                _responseDto.Payload = null;
                return _responseDto;

            }
            _responseDto.Message = "List of employees";
            _responseDto.Success = true;
            _responseDto.Payload = employees;
            return _responseDto;
        }


        public async Task<ResponseDto> GetEmploye(int id)
        {
            if (_context.Employees == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                return _responseDto;
            }
            var employe = await _context.Employees.FindAsync(id);

            if (employe == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                return _responseDto;
            }

            _responseDto.StatusCode = StatusCodes.Status200OK;
            _responseDto.Payload = employe;
            return _responseDto;
        }

        //public async Task<ResponseDto> PostEmploye(Employee employee)
        //{
        //    if (_context.Employees == null)
        //    {
        //        _responseDto.StatusCode = StatusCodes.Status400BadRequest;
        //        _responseDto.Message = "Entity set 'PostgresContext.Employees'  is null.";
        //        return _responseDto;
        //    }
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    _responseDto.StatusCode = StatusCodes.Status201Created;
        //    return _responseDto;
        //}

        public async Task<ResponseDto> PutEmploye(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                return _responseDto;
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeExists(id))
                {
                    _responseDto.StatusCode = StatusCodes.Status404NotFound;
                    return _responseDto;
                }
                else
                {
                    throw;
                }
            }

            _responseDto.StatusCode = StatusCodes.Status204NoContent;
            return _responseDto;
        }

        public async Task<ResponseDto> DeleteEmploye(int id)
        {
            if (_context.Employees == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                return _responseDto;
            }
            var employe = await _context.Employees.FindAsync(id);
            if (employe == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                return _responseDto;
            }

            _context.Employees.Remove(employe);
            await _context.SaveChangesAsync();

            _responseDto.StatusCode = StatusCodes.Status204NoContent;
            return _responseDto;
        }

        public async Task<ResponseDto> GetSalary()
        {
            var salarys = await _context.Employees.Include(i => i.Salary).ToListAsync();
            if (salarys == null || salarys.Count == 0)
            {
                _responseDto.Message = "No salarys info found";
                _responseDto.Success = false;
                _responseDto.Payload = null;
                return _responseDto;

            }
            _responseDto.Message = "List of salarys";
            _responseDto.Success = true;
            _responseDto.Payload = salarys;
            return _responseDto;
        }

        public async Task<ResponseDto> PostEmploye(Employee employee)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                transaction.Commit();
                _responseDto.Message = "Saved Successfull employee";
                _responseDto.Success = true;
                _responseDto.StatusCode = StatusCodes.Status200OK;
                return _responseDto;
            }
            catch (Exception)
            {
                transaction.Rollback();
                _responseDto.Message = "Save Failed employee";
                _responseDto.Success = false;
                _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                return _responseDto;
            }
        }
        private bool EmployeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
