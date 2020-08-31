using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCrudWithOracle.Interface;
using CoreCrudWithOracle.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreCrudWithOracle.Controllers
{
    public class StudentController : Controller
    {
        IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> students = studentService.GetAllStudents();
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            studentService.AddStudent(student);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Student student = studentService.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            studentService.EditStudent(student);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Student student = studentService.GetStudentById(id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            studentService.DeleteStudent(student);
            return RedirectToAction(nameof(Index));
        }
    }
}
