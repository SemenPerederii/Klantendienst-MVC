using KlantenDienstData.Models;
using System.ComponentModel.DataAnnotations;

namespace KlantenDienstWeb.Models
{
    public class PaswoordWijzigenVM
    {
        public Personeelslid Account { get; set; }
        public string Emailadres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Huidig paswoord is verplicht.")]
        public string HuidigPaswoord { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nieuw paswoord is verplicht.")]
        public string NieuwPaswoord { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bevestig nieuw paswoord is verplicht.")]
        public string BevestigNieuwPaswoord { get; set; } = string.Empty;
    }
}
