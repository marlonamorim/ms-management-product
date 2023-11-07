using MGM.MS.Management.Product.Infrastructure.Config;
using MGM.MS.Management.Product.Infrastructure.DependencyInjection;
using MGM.MS.Management.Product.Notification.DependencyInjection;
using MGM.MS.Management.Product.Services.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x => x.SuppressAsyncSuffixInActionNames = false)
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api-Gerenciamento-Produto",
        Description = "Flow de cadastro de produto",
        Contact = new OpenApiContact
        {
            Name = "MGM Eleva Tecnologia da Informação"
        }
    });

    s.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return new[] { api.GroupName };
        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            return new[] { controllerActionDescriptor.ControllerName };

        throw new InvalidOperationException("Habilite uma determinada tag para os endpoints.");
    });

    s.DocInclusionPredicate((name, api) => true);
    s.EnableAnnotations();
});

builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddNotification();
builder.Services.AddServices();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Customers API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
