using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();

            AddJobViewModel jobViewModel = new AddJobViewModel(employers, skills);
            return View(jobViewModel);
        }

        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string [] selectedSkills)
        {

            if (ModelState.IsValid)
            {
                Job jobObject;
                jobObject = new Job (viewModel.Name);
                foreach(string selectedSkill in selectedSkills)
                {
                    int num = int.Parse(selectedSkill);
                    JobSkill jobSkill;
                    jobSkill = new JobSkill();
                    jobSkill.Job = jobObject;
                    jobSkill.SkillId = num;
                    context.Add(jobSkill);
                }
                jobObject.EmployerId = viewModel.EmployerId;
                              
                context.Add(jobObject);
                context.SaveChanges();

                return Redirect("/");
            }
            
            viewModel.EmployerOptions = context.Employers.Select(employer => new SelectListItem(employer.Name, employer.Id.ToString())).ToList();
            viewModel.SkillOptions = context.Skills.ToList();

            return View("AddJob", viewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
