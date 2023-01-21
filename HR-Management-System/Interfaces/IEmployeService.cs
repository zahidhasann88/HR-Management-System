﻿using HR_Management_System.DTOs;
using HR_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Interfaces
{
    public interface IEmployeService
    {
        public List<Employee> GetEmployes();
        public Task<ResponseDto> GetEmploye(int id);
        public Task<ResponseDto> PutEmploye(int id, Employee employee);
        public Task<ResponseDto> PostEmploye(Employee employee);
        public Task<ResponseDto> DeleteEmploye(int id);
        public Task<ResponseDto> GetSalary();

    }
}
