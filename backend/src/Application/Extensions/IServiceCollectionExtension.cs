using Application.Interfaces;
using Application.Helpers;
using Application.Helpers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
  public static class IServiceCollectionExtension
  {
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddMemoryCache();
      services.AddTransient<IUserService, UserService>();
      services.AddTransient<IToken, Token>();

       //services.AddScoped<IToken, Token>();      
      //services.AddScoped<IPermissionUserService, PermissionUserService>();
      //services.AddScoped<IPermissionService, PermissionService>();
      //services.AddScoped<IPhoneService, PhoneService>();
      //services.AddScoped<IEmailService, EmailService>();
      //services.AddScoped<INotificationService, NotificationService>();
      //services.AddScoped<IApplicationService, ApplicationService>();
      //services.AddScoped<IPerfilService, PerfilService>();
      //services.AddScoped<IAccessService, AccessService>();
      //services.AddScoped<ISystemService, SystemService>();
      //services.AddScoped<IAccessRouteService, AccessRouteService>();
      //services.AddScoped<IAccessRoutePermissionService, AccessRoutePermissionService>();

      return services;
    }
  }
}
