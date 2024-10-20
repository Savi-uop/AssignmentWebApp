using AssignmentWebApp.Models;
using AssignmentWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }
    using AssignmentWebApp.Models;
using AssignmentWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmployeeRequest>> GetEmployees()
    {
        return Ok(_service.GetEmployees());
    }

    [HttpGet("{id}")]
    public ActionResult<EmployeeRequest> GetEmployee(int id)
    {
        var employee = _service.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public ActionResult<EmployeeRequest> CreateEmployee(EmployeeRequest employee)
    {
        var createdEmployee = _service.AddEmployee(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, EmployeeRequest employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest();
        }

        _service.UpdateEmployee(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var existingEmployee = _service.GetEmployeeById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        _service.DeleteEmployee(id);
        return NoContent();
    }
}

[HttpGet]
    public ActionResult<IEnumerable<EmployeeRequest>> GetEmployees()
    {
        return Ok(_service.GetEmployees());
    }

    // Add methods for Create, Update, and Delete operations...
}

