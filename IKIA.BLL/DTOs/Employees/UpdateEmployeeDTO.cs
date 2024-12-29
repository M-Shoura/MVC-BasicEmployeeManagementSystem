﻿using IKIA.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IKIA.BLL.DTOs.Employees
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }


        [MaxLength(50, ErrorMessage = "Max length is 50")]
        [MinLength(5, ErrorMessage = "Min length is 5")]
        public string Name { get; set; } = null!;


        [Range(22, 20)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }


        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [EmailAddress]
        public string? Email { get; set; }


        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }


        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
       
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }

    }
}