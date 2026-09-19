using EmployeeManagement.Models;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllEmployeesAsync();

    Task<Employee?> GetEmployeeByIdAsync(int id);

    Task<Employee> CreateEmployeeAsync(Employee employee);

    Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);

    Task<Employee?> DeleteEmployeeAsync(int id);
}