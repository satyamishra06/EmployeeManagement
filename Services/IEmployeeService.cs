using EmployeeManagement.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeesAsync();

    Task<Employee?> GetEmployeeByIdAsync(int id);

    Task<Employee> CreateEmployeeAsync(EmployeeDto employeeDto);

    Task<Employee?> UpdateEmployeeAsync(int id, EmployeeDto employeeDto);

    Task<Employee?> DeleteEmployeeAsync(int id);
}