using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Payment.Common.Models;
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

        public ActionResult ToPay()
        {
            try
            {
                // 2. 发起API调用（以支付能力下的统一收单交易创建接口为例）
                AlipayTradeCreateResponse response = Factory.Payment.Common().Create("Apple iPhone11 128G", "2234567890", "5799.00", "2088002656718920");
                // 3. 处理响应或异常
                if ("10000".Equals(response.Code))
                {
                    Debug.WriteLine("调用成功");
                }
                else
                {
                    Debug.WriteLine("调用失败，原因：" + response.Msg + "，" + response.SubMsg);
                }
                return View("Home");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("调用遭遇异常，原因：" + ex.Message);
                return View("Home");
            }
        }
    }
}
