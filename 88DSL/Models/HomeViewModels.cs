using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _88DSL.Models
{
  
    public class LoginViewModel
    {
        [Required]      
       
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
     
        public string Password { get; set; }

    
        public bool RememberMe { get; set; }

        public string UserRole { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Balance { get; set; }

    }

}
