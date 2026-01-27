using KlantenDienstData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenDienstServices.DTO_s
{
    public class LogInInfoDTO
    {
        [Display(Name = "Emailadres")]
        [Required(ErrorMessage = "Verplicht")]
        [DataType(DataType.EmailAddress)]
        public string Emailadres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Verplicht")]
        [DataType(DataType.Password)]
        public string Paswoord { get; set; } = string.Empty;
        public PersoneelslidAccount? GevondenAccount { get; set; }

        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
