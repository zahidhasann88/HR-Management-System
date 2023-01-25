using HR_Management_System.DTOs;
using HR_Management_System.Interfaces;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Http;
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

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                return await _context.Employees.Include(i => i.Salary).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<ResponseDto> GetEmployeeByIdAsync(int id)
        {
            if (_context.Employees == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                return _responseDto;
            }
            var employe = await _context.Employees.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (employe == null)
            {
                _responseDto.StatusCode = StatusCodes.Status404NotFound;
                _responseDto.Success = false;
                return _responseDto;
            }

            _responseDto.StatusCode = StatusCodes.Status200OK;
            _responseDto.Success = true;
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

        public async Task<ResponseDto> PutEmployeeAsync(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                    _responseDto.Success = false;
                    return _responseDto;
                }

                Employee e = await _context.Employees.Where(i => i.Id == id).FirstOrDefaultAsync();
                if (e == null)
                {
                    _responseDto.Message = "No Employee found with id: " + id;
                    _responseDto.Success = false;
                    _responseDto.StatusCode = StatusCodes.Status404NotFound;
                }
                // if we want to update one/some property use this syntax
                //e.Name = employee.Name;
                //_context.Entry(e).Property(o => o.Name).IsModified = true;
                //int isSaved = await _context.SaveChangesAsync();

                // if we want to update all property
                e.Name = employee.Name;
                e.Designation = e.Designation;
                // put all column like that
                _context.Employees.Update(e);
                int isSaved = await _context.SaveChangesAsync();
                if (isSaved > 0)
                {
                    _responseDto.StatusCode = StatusCodes.Status200OK;
                    _responseDto.Message = "Update Successful.";
                    _responseDto.Success = true;
                    return _responseDto;
                }
                else
                {
                    _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                    _responseDto.Message = "Update failed.";
                    _responseDto.Success = false;
                    return _responseDto;
                }
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                _responseDto.Message = "Update failed. Something went wrong.";
                _responseDto.Success = false;
                _responseDto.Payload = ex;
                return _responseDto;
            }
        }

        public async Task<ResponseDto> DeleteEmployeeAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                    _responseDto.Success = false;
                    return _responseDto;
                }

                Employee e = await _context.Employees.Where(i => i.Id == id).FirstOrDefaultAsync();
                if (e == null)
                {
                    _responseDto.Message = "No Employee found with id: " + id;
                    _responseDto.Success = false;
                    _responseDto.StatusCode = StatusCodes.Status404NotFound;
                }

                _context.Employees.Remove(e);
                int isSaved = await _context.SaveChangesAsync();
                if (isSaved > 0)
                {
                    _responseDto.StatusCode = StatusCodes.Status200OK;
                    _responseDto.Message = "Delete Successful.";
                    _responseDto.Success = true;
                    return _responseDto;
                }
                else
                {
                    _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                    _responseDto.Message = "Delete failed.";
                    _responseDto.Success = false;
                    return _responseDto;
                }
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                _responseDto.Message = "Delete failed. Something went wrong.";
                _responseDto.Success = false;
                _responseDto.Payload = ex;
                return _responseDto;
            }
        }

        // pagination (skip - take) // explanation
        // 0 - 5 // means: 1 2 3 4 5
        // 5 - 5 // means: 6 7 8 9 10
        // 10 - 5 // means: 11 12 13 14 15
        public async Task<ResponseDto> GetSalaryAsync(int skip, int take)
        {
            var salarys = await _context.Salaries.Skip(skip).Take(take).ToListAsync();
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

        public async Task<ResponseDto> PostEmployeeAsync(Employee employee)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                {
                    Employee e = await _context.Employees.Where(i => i.Id == employee.Id).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        _responseDto.Message = "Employee already exists with id: " + employee.Id;
                        _responseDto.Success = false;
                        _responseDto.StatusCode = StatusCodes.Status409Conflict;
                        transaction.Rollback();
                        return _responseDto;
                    }

                    _context.Employees.Add(e);
                    int isSaved = await _context.SaveChangesAsync();
                    if (isSaved > 0)
                    {
                        _responseDto.StatusCode = StatusCodes.Status200OK;
                        _responseDto.Message = "Create Successful.";
                        _responseDto.Success = true;
                        transaction.Commit();
                        return _responseDto;
                    }
                    else
                    {
                        _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                        _responseDto.Message = "Create failed.";
                        _responseDto.Success = false;
                        transaction.Rollback();
                        return _responseDto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = StatusCodes.Status400BadRequest;
                _responseDto.Message = "Create failed. Something went wrong.";
                _responseDto.Success = false;
                _responseDto.Payload = ex;
                return _responseDto;
            }
        }
    }
}

// do not use findasync
// only use tolistasync or firstordefaultasync
