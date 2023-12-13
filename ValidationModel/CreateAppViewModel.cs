using System.ComponentModel.DataAnnotations;
namespace WEB
{
    public class CreateAppViewModel
    {
        [Required(ErrorMessage="Ввести имя приложения")]
        public string Name { get; set; }


        [Required(ErrorMessage="Введите каталог")]

        public string Tag { get; set; }

        [Required(ErrorMessage = "Иконка приложения")]
        public string IconSource { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        [Range(0, 1000, ErrorMessage = "Недопустимая цена")]
        public decimal Price { get; set; }

        [Required(ErrorMessage="Введите оценку")]
        [Range(0, 5, ErrorMessage = "Недопустимая цена")]
        public decimal Mark { get; set; }

        [Required(ErrorMessage="Введите компанию")]
        public string Company { get; set; }
        

        [Required(ErrorMessage = "Введите описание приложения")]
        public string DescriptionSource { get; set; }


        [Required(ErrorMessage = "Введите картинки для приложения")]
        public string ImageSource { get; set; }
    }
}
