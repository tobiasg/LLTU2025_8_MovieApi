using Movies.Api.Extensions;
using Movies.Data;
using Movies.Presentation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.AddControllers().AddApplicationPart(typeof(PresentationAssemblyReference).Assembly);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper(c => c.AddProfile<MapperProfile>());
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    await app.SeedDataAsync();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
