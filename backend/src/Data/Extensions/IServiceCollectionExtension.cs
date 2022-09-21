using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class IServiceCollectionExtension
{
  public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<Settings>(options =>
    {
      options.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
      options.Database = configuration.GetSection("MongoConnection:Database").Value;
    });


    services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
    //services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
    //services.AddScoped(typeof(IPermissionUserRepository), typeof(PermissionUserRepository));
    //services.AddScoped(typeof(IAccessRoutePermissionRepository), typeof(AccessRoutePermissionRepository));

    return services;
  }
}

