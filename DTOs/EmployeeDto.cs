using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs;

public class EmployeeDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Department { get; set; } = string.Empty;

    [Range(0, 10000000)]
    public decimal Salary { get; set; }
}