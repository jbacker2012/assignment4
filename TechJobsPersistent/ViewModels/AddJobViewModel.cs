﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Employer is required.")]
        public int EmployerId { get; set; }
        public IList<SelectListItem> EmployerOptions { get; set; }
   
        public IList<Skill> SkillOptions { get; set;}
        
        public AddJobViewModel(List<Employer> employers, List<Skill> skills )
        {
            EmployerOptions = employers.Select(employer => new SelectListItem(employer.Name, employer.Id.ToString())).ToList();
            SkillOptions = skills;
        }

        public AddJobViewModel()
        {

        }
    }
}
