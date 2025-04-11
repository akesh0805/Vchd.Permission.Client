using System.ComponentModel.DataAnnotations;

namespace Vchd.Permission.Client.Models;

public class LoginModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }


