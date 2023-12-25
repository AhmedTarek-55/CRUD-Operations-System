using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public string? ImageURL { get; set; }
        public IFormFile? Image { get; set; }
        public int DepartmentId { get; set; }
    }
}
