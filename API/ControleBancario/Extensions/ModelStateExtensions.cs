namespace ExtensionMethods
{
    using System.Collections.Generic;    
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensions
    {
        public static string GenerateValidation(this ModelStateDictionary dictionary)
        {
            var errors = new List<string>();

            foreach (var value in dictionary.Values)
            {
                foreach (var error in value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return string.Join("\n", errors);
        }
    }
}