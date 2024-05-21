using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.Validations
{
    public class RegisterValidation
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório!")]
        [StringLength(50, ErrorMessage = "O campo Password deve ter no mínimo {2} caracteres.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[-!@#$%^&*()_+{}\[\]:;<>,.?~]).*$", ErrorMessage = "A senha deve conter pelo menos um caractere especial.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo ConfirmPassword é obrigatório!")]
        [Compare(nameof(Password), ErrorMessage = "Senhas diferentes!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
