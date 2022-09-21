using Application.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

  public static class IServiceCollectionExtension
  {
    public static IServiceCollection AddAllDi(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddRepositories(configuration);
      services.AddServices();

      return services;
    }
  }

