using Api.Helpers.Interfaces;
using Api.Helpers;
using Api.Routes;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Application.Helpers;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
builder.Services.AddTransient<ICustomException, CustomException>();
builder.Services.AddAllDi(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "CustomReactGram",
    Version = "v1",
    Contact = new OpenApiContact
    {
      Name = "Bruno Sajermann",
      Email = "sajermannbruno@gmail.com",
      Url = new Uri("https://www.linkedin.com/in/devbrunosajermann/")
    },
  });
});
builder.Services.AddSwaggerGen(c =>
{
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[] {}
    }
  });
});

#region JWT
var key = Encoding.ASCII.GetBytes(configuration.GetSection("Security").Value);
builder.Services.AddAuthentication(x =>
{
  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
  x.RequireHttpsMetadata = false;
  x.SaveToken = true;
  x.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false,
    ValidateAudience = false
  };
});
builder.Services.AddAuthorization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c => c.DefaultModelsExpandDepth(-1));

  app.UseSwaggerUI(c =>
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "SajermannDashboard.Api v1"));
}

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "..", "Data", "Uploads")),
  RequestPath = "/Files"
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(
  cors => cors.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    );


app.AddUsersRoutes();
app.AddPhotosRoutes();

app.Run();

