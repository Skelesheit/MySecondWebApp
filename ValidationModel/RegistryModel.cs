using System.ComponentModel.DataAnnotations;
namespace WEB
{
    public class RegistryModel
    {
        [Required(ErrorMessage="Неверное имя")]
        public string Name { get; set; }
        [Required(ErrorMessage=" неверный пароль")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароли не совпадают")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
