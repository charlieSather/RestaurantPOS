using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantPosApp.Contracts;
using RestaurantPosApp.Data;
using RestaurantPosApp.Models;

namespace RestaurantPosApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public EmployeeController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var employee = _repo.Employee.GetEmployee(userId);

            if (employee != null)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                employee.UserId = userId;
                _repo.Employee.CreateEmployee(employee);
                _repo.Save();

                return RedirectToAction("Index", "Restaurant");
            }
            else
            {
                return View(employee);
            }
        }
    }
}
