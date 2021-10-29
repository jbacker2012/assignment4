using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    
    public class EmployerController : Controller
    {
        private JobDbContext context;

    public EmployerController(JobDbContext dbContext)
        {
           context = dbContext;
        }
        
        public IActionResult Index()
        {
            List<Employer> events = context.Employers.ToList();

            return View(events);
        }

        public IActionResult Add()
        {         
            return View(new AddEmployerViewModel());
        }

        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Employer employer = new Employer(viewModel.Name, viewModel.Location);
                

                context.Add(employer);
                context.SaveChanges();

                return Redirect("/Employer/");
            }

            return View("Add", viewModel);
        }

        public IActionResult About(int id)
        {
            Employer employer = context.Employers.First(e => e.Id == id); 
            ViewBag.Employer = employer;
            return View();
        }
    }
}
