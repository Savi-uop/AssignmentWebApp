using AssignmentWebApp.Data;
using AssignmentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AssignmentWebApp.Repositories
{
    public class EmployeeRepository
    {
        private readonly DatabaseContext _context;

        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeRequest> GetAllEmployees()
        {
            var employees = new List<EmployeeRequest>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeRequest
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Salary = (decimal)reader["Salary"],
                            DepartmentId = (int)reader["DepartmentId"]
                        });
                    }
                }
            }
            return employees;
        }

        public EmployeeRequest GetEmployeeById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.AddWithValue("@EmployeeId", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new EmployeeRequest
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Salary = (decimal)reader["Salary"],
                            DepartmentId = (int)reader["DepartmentId"]
                        };
                    }
                }
            }
            return null; // Return null if the employee is not found
        }

        public EmployeeRequest CreateEmployee(EmployeeRequest employee)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary, DepartmentId) OUTPUT INSERTED.EmployeeId VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @Salary, @DepartmentId)", connection);

                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);

                employee.EmployeeId = (int)command.ExecuteScalar(); // Retrieve the newly created EmployeeId
            }
            return employee;
        }

        public void UpdateEmployee(EmployeeRequest employee)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, DateOfBirth = @DateOfBirth, Salary = @Salary, DepartmentId = @DepartmentId WHERE EmployeeId = @EmployeeId", connection);

                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                command.ExecuteNonQuery(); // Execute the update command
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Employees WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.AddWithValue("@EmployeeId", id);

                command.ExecuteNonQuery(); // Execute the delete command
            }
        }
    }
}
