using System.ComponentModel.DataAnnotations;

namespace DatingAppapi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string userName { get; set; }
        
        [Required]
        [StringLength(8,ErrorMessage="Password should be of length less then 8")]
        public string password { get; set; }
    }
}