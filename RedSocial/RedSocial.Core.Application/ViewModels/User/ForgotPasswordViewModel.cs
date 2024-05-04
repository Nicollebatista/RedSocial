using System.ComponentModel.DataAnnotations;

namespace RedSocial.Core.Application.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar el UserName")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }   
        
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
