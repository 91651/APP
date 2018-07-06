using System.ComponentModel.DataAnnotations;

namespace APP.UI.MVC.Admin.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}