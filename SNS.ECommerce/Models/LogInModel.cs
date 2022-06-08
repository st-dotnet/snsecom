using System.ComponentModel.DataAnnotations;

namespace SNS.ECommerce.Models
{
    public class LogInModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
