using AssignmentWebApp.Models;
using AssignmentWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    // GET: api/departments
    [HttpGet]
    public ActionResult<IEnumerable<DepartmentRequest>> GetDepartments()
    {
        return Ok(_service.GetDepartments());
    }

    // POST: api/departments
    [HttpPost]
    public ActionResult<DepartmentRequest> CreateDepartment([FromBody] DepartmentRequest department)
    {
        if (department == null)
        {
            return BadRequest("Department data is invalid.");
        }

        var createdDepartment = _service.CreateDepartment(department);
        return CreatedAtAction(nameof(GetDepartments), new { id = createdDepartment.Id }, createdDepartment);
    }

    // PUT: api/departments/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateDepartment(int id, [FromBody] DepartmentRequest department)
    {
        if (department == null || id != department.Id)
        {
            return BadRequest("Department data is invalid.");
        }

        var existingDepartment = _service.GetDepartmentById(id);
        if (existingDepartment == null)
        {
            return NotFound($"Department with ID {id} not found.");
        }

        _service.UpdateDepartment(department);
        return NoContent();
    }

    // DELETE: api/departments/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteDepartment(int id)
    {
        var existingDepartment = _service.GetDepartmentById(id);
        if (existingDepartment == null)
        {
            return NotFound($"Department with ID {id} not found.");
        }

        _service.DeleteDepartment(id);
        return NoContent();
    }
}
