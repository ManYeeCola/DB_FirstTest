using BusinessObjects.Dao;
using BusinessObjects.Entity;
using BusinessObjects.Services;
using DB_FirstTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace DB_FirstTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentServices _studentService;
        
        private readonly ICourseDao _courseDao;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IStudentServices studentService,ICourseDao courseDao)
        {
            _logger = logger;
            _studentService = studentService;
            _courseDao = courseDao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetStudent()
        {
            try
            {
                return Json(_studentService.GetStudent());
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        public JsonResult SaveStudent(Student student,Course course)
        {
            try
            {
                return Json(_studentService.SaveStudent(student, course));
            }catch(Exception e)
            {
                return Json(e.Message);
            }
        }
    }
}
