using AssignmentWebApp.Models;
using AssignmentWebApp.Repositories;
using System.Collections.Generic;

namespace AssignmentWebApp.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<EmployeeRequest> GetEmployees()
        {
            return _repository.GetAllEmployees();
        }

        public EmployeeRequest GetEmployeeById(int id)
        {
            return _repository.GetEmployeeById(id);
        }

        public EmployeeRequest AddEmployee(EmployeeRequest employee)
        {
            return _repository.CreateEmployee(employee);
        }

        public void UpdateEmployee(EmployeeRequest employee)
        {
            _repository.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            _repository.DeleteEmployee(id);
        }
    }
}
