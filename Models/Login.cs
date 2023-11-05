#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace the_wall.Models;
public class Login
{
    [Required(ErrorMessage = "Debes ingresar tu correo.")]
    public string EmailLog { get; set; }
    [Required(ErrorMessage = "Debes ingresar tu contraseña.")]
    public string PasswordLog { get; set; }
}
