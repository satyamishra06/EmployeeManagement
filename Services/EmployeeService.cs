using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = new Employee
        {
            Name = employeeDto.Name,
            Email = employeeDto.Email,
            Department = employeeDto.Department,
            Salary = employeeDto.Salary
        };

        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee?> UpdateEmployeeAsync(
        int id,
        EmployeeDto employeeDto)
    {
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

        return existingEmployee;
    }

    public async Task<Employee?> DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
        {
            return null;
        }

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync();

        return employee;
    }
}