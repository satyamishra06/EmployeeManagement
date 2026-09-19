using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        AppDbContext context,
        ILogger<EmployeeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
    {
        var employees = await _context.Employees.ToListAsync();

        return employees.Select(employee => new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary
        }).ToList();
    }

    public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id)
    {
        _logger.LogInformation(
            "Fetching employee with ID {EmployeeId}",
            id);

        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return null;
        }

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary
        };
    }

    public async Task<EmployeeResponseDto> CreateEmployeeAsync(
        EmployeeDto employeeDto)
    {
        _logger.LogInformation(
            "Creating employee with email {Email}",
            employeeDto.Email);

        var employee = new Employee
        {
            Name = employeeDto.Name,
            Email = employeeDto.Email,
            Department = employeeDto.Department,
            Salary = employeeDto.Salary
        };

        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary
        };
    }

    public async Task<EmployeeResponseDto?> UpdateEmployeeAsync(
        int id,
        EmployeeDto employeeDto)
    {
        _logger.LogInformation(
            "Updating employee with ID {EmployeeId}",
            id);

        var existingEmployee = await _context.Employees.FindAsync(id);

        if (existingEmployee == null)
        {
            return null;
        }

        existingEmployee.Name = employeeDto.Name;
        existingEmployee.Email = employeeDto.Email;
        existingEmployee.Department = employeeDto.Department;
        existingEmployee.Salary = employeeDto.Salary;

        await _context.SaveChangesAsync();

        return new EmployeeResponseDto
        {
            Id = existingEmployee.Id,
            Name = existingEmployee.Name,
            Email = existingEmployee.Email,
            Department = existingEmployee.Department,
            Salary = existingEmployee.Salary
        };
    }

    public async Task<EmployeeResponseDto?> DeleteEmployeeAsync(int id)
    {
        _logger.LogInformation(
            "Deleting employee with ID {EmployeeId}",
            id);
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return null;
        }

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync();

        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary
        };
    }
}