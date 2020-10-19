using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;
namespace WebMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeServices _empServics = new EmployeeServices();
        //private EmployeeServices _empServics;
        // GET: Employee
        public ActionResult List()
        {
            _empServics = new EmployeeServices();
            var model = _empServics.GetEmployeesList();
            return View(model);
        }
        
        public ActionResult AddEmployee()
        {
            return View();
        }
        public ActionResult EditEmployee(int Id)
        {
            var model = _empServics.GetEditbyId(Id);
            return View(model);
        }
        public ActionResult DeleteEmployee(int Id)
        {
            _empServics.DeleteEmp(Id);
            return RedirectToAction("List"); 
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel model)
        {
            _empServics.InsertEmployee(model);
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeModel model)
        {
            _empServics.UpdateEmp(model);
            return RedirectToAction("List");
        }
    }
}