// Models/DepartmentFormViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace StudentMgmtApp.Models
{
    public class DepartmentFormViewModel
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}