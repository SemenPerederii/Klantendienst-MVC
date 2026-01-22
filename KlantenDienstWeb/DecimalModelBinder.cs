using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
namespace KlantenDienstWeb
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var raw = valueResult.FirstValue;

            // Leeg veld: geldig (zeker bij decimal?)
            if (string.IsNullOrWhiteSpace(raw))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            raw = raw.Trim();

            // 1) Normaliseer: maak decimaalteken altijd "."
            // - verwijder spaties (soms "1 234,56")
            // - vervang "," door "."
            var normalized = raw.Replace(" ", "").Replace(",", ".");

            // 2) Probeer invariant parse (accepteert "2.5" én "2,5" door normalisatie)
            if (decimal.TryParse(
                    normalized,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var value))
            {
                bindingContext.Result = ModelBindingResult.Success(value);
                return Task.CompletedTask;
            }

            // 3) Fallback: probeer nog current culture (voor edge cases)
            if (decimal.TryParse(raw, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
            {
                bindingContext.Result = ModelBindingResult.Success(value);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Ongeldig getal.");
            return Task.CompletedTask;
        }

    }
}
