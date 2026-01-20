using System.ComponentModel.DataAnnotations;

namespace KlantenDienstWeb.Models;
public class LoginViewModel
{

    [Display(Name = "Gebruikersnaam")]
    [Required(ErrorMessage = "Verplicht")]
    public string Emailadres { get; set; } = string.Empty;
    [Required(ErrorMessage = "Verplicht")]
    [DataType(DataType.Password)]
    public string Paswoord { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; } = string.Empty;
    public string? ReturnUrl { get; set; } = string.Empty;
}