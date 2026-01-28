using KlantenDienstData.Models;
using System.ComponentModel.DataAnnotations;

namespace KlantenDienstWeb.Models
{
    public class PaswoordWijzigenVM
    {
        [DataType(DataType.EmailAddress)]
        public string Emailadres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Huidig paswoord is verplicht.")]
        [DataType(DataType.Password)]
        public string HuidigPaswoord { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nieuw paswoord is verplicht.")]
        [DataType(DataType.Password)]
        public string NieuwPaswoord { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bevestig nieuw paswoord is verplicht.")]
        [Compare("NieuwPaswoord", ErrorMessage = "De Wachtwoorden komen niet overeen.")]
        [DataType(DataType.Password)]
        public string BevestigNieuwPaswoord { get; set; } = string.Empty;
    }
}
