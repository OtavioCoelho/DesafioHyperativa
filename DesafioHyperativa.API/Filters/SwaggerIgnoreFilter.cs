using DesafioHyperativa.Domain.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace DesafioHyperativa.API.Filters;

public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null)
        {
            return;
        }

        var excludedProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);
        foreach (PropertyInfo excludedProperty in excludedProperties)
        {
            if (schema.Properties.ContainsKey(excludedProperty.Name))
            {
                schema.Properties.Remove(excludedProperty.Name);
            }
        }
    }
}
