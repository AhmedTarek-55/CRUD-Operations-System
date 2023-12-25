using System.ComponentModel.DataAnnotations;

namespace Data_Access_Tier.Entities
{
    public class Employee : BaseEntity
    {
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
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
