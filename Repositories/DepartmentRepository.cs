using AssignmentWebApp.Data;
using AssignmentWebApp.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AssignmentWebApp.Repositories
{
    public class DepartmentRepository
    {
        private readonly DatabaseContext _context;

        public DepartmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<DepartmentRequest> GetAllDepartments()
        {
            var departments = new List<DepartmentRequest>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Departments", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new DepartmentRequest
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentCode = reader["DepartmentCode"].ToString(),
                            DepartmentName = reader["DepartmentName"].ToString()
                        });
                    }
                }
            }
            return departments;
        }

        public DepartmentRequest AddDepartment(DepartmentRequest department)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Departments (DepartmentCode, DepartmentName) OUTPUT INSERTED.DepartmentId VALUES (@Code, @Name)", connection);
                command.Parameters.AddWithValue("@Code", department.DepartmentCode);
                command.Parameters.AddWithValue("@Name", department.DepartmentName);

                // Retrieve the newly created department ID
                department.DepartmentId = (int)command.ExecuteScalar();
            }
            return department;
        }

        public DepartmentRequest GetDepartmentById(int id)
        {
            DepartmentRequest department = null;
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Departments WHERE DepartmentId = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentRequest
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentCode = reader["DepartmentCode"].ToString(),
                            DepartmentName = reader["DepartmentName"].ToString()
                        };
                    }
                }
            }
            return department;
        }

        public void UpdateDepartment(DepartmentRequest department)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Departments SET DepartmentCode = @Code, DepartmentName = @Name WHERE DepartmentId = @Id", connection);
                command.Parameters.AddWithValue("@Code", department.DepartmentCode);
                command.Parameters.AddWithValue("@Name", department.DepartmentName);
                command.Parameters.AddWithValue("@Id", department.DepartmentId);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDepartment(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Departments WHERE DepartmentId = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
