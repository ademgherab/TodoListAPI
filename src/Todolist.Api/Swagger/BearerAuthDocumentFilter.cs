using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Todolist.Api.Swagger;

// Ensures Swagger UI treats the Bearer scheme as globally required so the Authorization header is sent.
public sealed class BearerAuthDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Security ??= new List<OpenApiSecurityRequirement>();

        swaggerDoc.Security.Add(
            new OpenApiSecurityRequirement
            {
                // Reference the scheme added via AddSecurityDefinition("Bearer", ...)
                [
                    new OpenApiSecuritySchemeReference("Bearer", swaggerDoc, externalResource: null)
                ] = new List<string>(),
            }
        );
    }
}
