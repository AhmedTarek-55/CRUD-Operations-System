using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [MaxLength(20)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}