using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services;

public interface IEmployeeService
{
    Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

    Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id);

    Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeDto employeeDto);

    Task<EmployeeResponseDto?> UpdateEmployeeAsync(
        int id,
        EmployeeDto employeeDto);

    Task<EmployeeResponseDto?> DeleteEmployeeAsync(int id);
}