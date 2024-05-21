using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Domain.Validations
{
    public class LoginValidation
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório!")]
        public string Username {  get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
