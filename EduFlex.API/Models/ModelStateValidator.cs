using Application.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EduFlex.API.Models;

public static class ModelStateValidator
{
    public static void ValidateModelState(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            var errors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            throw new ValidationException(errors);
        }
    }
}
