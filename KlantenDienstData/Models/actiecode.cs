using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KlantenDienstData.Models;

public partial class Actiecode
{
    public int ActiecodeId { get; set; }

    [Required(ErrorMessage ="Naam is een Verplicht veld")]
    public string Naam { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Geldig van")]
    [Required(ErrorMessage = "Geldig Van is een Verplicht veld")]
    [CustomValidation(typeof(Actiecode), nameof(GeldigVanDatumValidatie))]
    public DateOnly GeldigVanDatum { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Geldig tot")]
    [Required(ErrorMessage = "Geldig tot is een Verplicht veld")]
    [CustomValidation(typeof(Actiecode), nameof(GeldigTotDatumValidatie))]
    public DateOnly GeldigTotDatum { get; set; }

    public bool IsEenmalig { get; set; }

    public static ValidationResult? GeldigVanDatumValidatie(DateOnly geldigVanDatum, ValidationContext context)
    {
        var vandaag = DateOnly.FromDateTime(DateTime.Today);
        if (geldigVanDatum < vandaag)
        {
            return new ValidationResult(" moet vandaag of later zijn");
        }
        return ValidationResult.Success;
    }

    public static ValidationResult? GeldigTotDatumValidatie(DateOnly geldigTotDatum, ValidationContext context)
    {
        Actiecode code = (Actiecode)context.ObjectInstance;
        if (geldigTotDatum < code.GeldigVanDatum)
        {
            return new ValidationResult(" moet groter of gelijk zijn aan Geldig van");
        }
        return ValidationResult.Success;
    }
}
