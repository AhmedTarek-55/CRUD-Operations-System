using System.ComponentModel.DataAnnotations;

namespace Data_Access_Tier.Entities
{
    public class Department : BaseEntity
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [MaxLength(20)]
        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
