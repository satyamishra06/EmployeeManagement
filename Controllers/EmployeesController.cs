using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();

        return Ok(employees);
    }

    // GET: api/employees/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    // POST: api/employees
    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Employee employee)
    {
        var createdEmployee =
            await _employeeService.CreateEmployeeAsync(employee);

        return CreatedAtAction(
            nameof(GetEmployeeById),
            new { id = createdEmployee.Id },
            createdEmployee
        );
    }

    // PUT: api/employees/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(
        int id,
        Employee employee)
    {
        var updatedEmployee =
            await _employeeService.UpdateEmployeeAsync(id, employee);

        if (updatedEmployee == null)
        {
            return NotFound();
        }

        return Ok(updatedEmployee);
    }

    // DELETE: api/employees/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deletedEmployee =
            await _employeeService.DeleteEmployeeAsync(id);

        if (deletedEmployee == null)
        {
            return NotFound();
        }

        return Ok(deletedEmployee);
    }
}