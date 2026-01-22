using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace KlantenDienstWeb
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var modelType = context.Metadata.ModelType;
            var underlying = Nullable.GetUnderlyingType(modelType) ?? modelType;

            if (underlying == typeof(decimal))
                return new DecimalModelBinder();

            return null;
        }
    }
}
