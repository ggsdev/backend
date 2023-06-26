using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PRIO.src.Shared.Utils.Binders
{
    public class GuidBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (!Guid.TryParse(value, out var guidValue))
            {
                bindingContext.ModelState.AddModelError(modelName, "Invalid GUID format in route param");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(guidValue);
            return Task.CompletedTask;
        }

    }

    public class GuidBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Guid))
            {
                return new GuidBinder();
            }

            return null;
        }
    }
}

