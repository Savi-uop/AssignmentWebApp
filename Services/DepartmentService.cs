using AssignmentWebApp.Models;
using AssignmentWebApp.Repositories;
using System.Collections.Generic;

namespace AssignmentWebApp.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _repository;

        public DepartmentService(DepartmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DepartmentRequest> GetDepartments()
        {
            return _repository.GetAllDepartments();
        }

        public DepartmentRequest CreateDepartment(DepartmentRequest department)
        {
            // Validate the input as necessary
            return _repository.AddDepartment(department);
        }

        public DepartmentRequest GetDepartmentById(int id)
        {
            // Retrieve a specific department by ID
            return _repository.GetDepartmentById(id);
        }

        public void UpdateDepartment(DepartmentRequest department)
        {
            // Validate the input as necessary
            _repository.UpdateDepartment(department);
        }

        public void DeleteDepartment(int id)
        {
            _repository.DeleteDepartment(id);
        }
    }
}
