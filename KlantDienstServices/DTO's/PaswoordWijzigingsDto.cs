using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices.DTO_s
{
    public class PaswoordWijzigingsDto
    {
        public string Emailadres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Huidig paswoord is verplicht.")]
        public string HuidigPaswoord { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nieuw paswoord is verplicht.")]
        public string NieuwPaswoord { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bevestig nieuw paswoord is verplicht.")]
        public string BevestigNieuwPaswoord { get; set; } = string.Empty;
    }
}
