using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrudWithOracle.Interface;
using CoreCrudWithOracle.Models;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace CoreCrudWithOracle.Services
{
    public class StudentService : IStudentService
    {
        private readonly string ConnectionString;
        public StudentService(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("OracleDBConnection");
        }
        public void AddStudent(Student student)
        {
            try
            {
                using (OracleConnection con = new OracleConnection(ConnectionString))
                {
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = "insert into student(Id, Name, Age) values('" + student.Id + "','" + student.Name + "','" + student.Age + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "delete from student where id='" + student.Id + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void EditStudent(Student student)
        {
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "update student set name='" + student.Name + "', age='" + student.Age + "' where id=" + student.Id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "select id, name, age from student";
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(rdr["id"]),
                            Name = rdr["name"].ToString(),
                            Age = rdr["age"].ToString()
                        };
                        students.Add(student);
                    }
                    con.Close();
                }
            }
            return students;
        }

        public Student GetStudentById(int id)
        {
            Student student = new Student();
            using (OracleConnection con = new OracleConnection(ConnectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.BindByName = true;
                    cmd.CommandText = "select * from student where id=" + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        student.Id = Convert.ToInt32(rdr["id"]);
                        student.Name = rdr["name"].ToString();
                        student.Age = rdr["age"].ToString();
                    }
                    con.Close();
                }
            }
            return student;
        }
    }
}
