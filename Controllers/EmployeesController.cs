using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmployeesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _context.Employees.ToListAsync();

        return Ok(employees);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
    {
        var existingEmployee = await _context.Employees.FindAsync(id);

        if (existingEmployee == null)
        {
            return NotFound();
        }

        existingEmployee.Name = employee.Name;
        existingEmployee.Email = employee.Email;
        existingEmployee.Department = employee.Department;
        existingEmployee.Salary = employee.Salary;

        await _context.SaveChangesAsync();

        return Ok(existingEmployee);
    }
}