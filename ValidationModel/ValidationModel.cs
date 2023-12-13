using System.ComponentModel.DataAnnotations;
namespace WEB
{
    public class ValidationModel
    {
        [Required(ErrorMessage="Заполните email")] 
        public string Email {  get; set; }

        [Required(ErrorMessage="Заполните пароль")]
        public string Password { get; set; }
    }
}
