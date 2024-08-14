namespace PLM.IoC;
public static class DependencyContainer
{
    public static IServiceCollection AddPLMServices(this IServiceCollection services)
    {
        services.AddBusinessLogicServices()
                .AddRepositoryServices()
                .AddPresenterServices()
                .AddControllerServices();

        return services;
    }
}