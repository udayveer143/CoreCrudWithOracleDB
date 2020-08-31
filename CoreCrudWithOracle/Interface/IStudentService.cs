using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrudWithOracle.Models;

namespace CoreCrudWithOracle.Interface
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        void AddStudent(Student student);
        void EditStudent(Student student);
        void DeleteStudent(Student student);

    }
}
