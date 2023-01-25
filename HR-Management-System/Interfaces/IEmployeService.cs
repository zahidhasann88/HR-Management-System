using HR_Management_System.DTOs;
using HR_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Management_System.Interfaces
{
    public interface IEmployeService
    {
        public Task<List<Employee>> GetEmployeesAsync();
        public Task<ResponseDto> GetEmployeeByIdAsync(int id);
        public Task<ResponseDto> PutEmployeeAsync(int id, Employee employee);
        public Task<ResponseDto> PostEmployeeAsync(Employee employee);
        public Task<ResponseDto> DeleteEmployeeAsync(int id);
        public Task<ResponseDto> GetSalaryAsync(int skip, int take);

    }
}
