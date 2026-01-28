using KlantenDienstData.Models;

namespace KlantenDienstWeb.Models
{
    public class PaswoordWijzigenVM
    {
        public Personeelslid Account { get; set; }
        public string Emailadres { get; set; } = string.Empty;
        public string HuidigPaswoord { get; set; } = string.Empty;
        public string NieuwPaswoord { get; set; } = string.Empty;
        public string BevestigNieuwPaswoord { get; set; } = string.Empty;
    }
}
