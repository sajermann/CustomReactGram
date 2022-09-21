using Api.Helpers.Interfaces;
using Api.Helpers;
using Api.Routes;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
builder.Services.AddTransient<ICustomException, CustomException>();
builder.Services.AddAllDi(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "..", "Data", "Uploads")),
  RequestPath = "/Files"
});

app.UseHttpsRedirection();
app.UseCors(
  cors => cors.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    );


app.AddUsersRoutes();

app.Run();

