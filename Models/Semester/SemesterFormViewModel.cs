// Models/SemesterFormViewModel.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentMgmtApp.Models
{
    public class SemesterFormViewModel
    {
        public int SemesterId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}