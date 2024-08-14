namespace PLM.DataBase;
public static class DependencyContainer
{
    public static IServiceCollection AddRepositoryServices
        (this IServiceCollection services)
    {
        services.AddTransient<IConnection, PLMContext>();
        services.AddScoped<IUserEmployeeRepository, UserEmployeeRespository>();
        services.AddScoped<IIdeaRepository, IdeaRepository>();
        services.AddScoped<ICategoryIdeaRepository, CategoryIdeaRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IProductProposalRepository, ProductProposalRepository>();
        services.AddScoped<IDesignRepository, DesignRepository>();
        services.AddScoped<IReviewProductProposalRepository, ReviewProductProposalRepository>();
        services.AddScoped<IReviewDesignRepository, ReviewDesignRepository>();
        services.AddScoped<IDesignHistoryRepository, DesignHistoryRepository>();
        services.AddScoped<IDesignCommentRepository, DesignCommentRepository>();

        return services;
    }
}