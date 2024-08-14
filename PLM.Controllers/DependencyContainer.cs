namespace PLM.Controllers;
public static class DependencyContainer
{
    public static IServiceCollection AddControllerServices
        (this IServiceCollection services)
    {
        //Idea
        services.AddScoped<GetAllIdeaController>();
        services.AddScoped<GetForProductProposalController>();
        services.AddScoped<CreateIdeaController>();
        services.AddScoped<UpdateIdeaController>();
        services.AddScoped<DeleteIdeaController>();

        //User Employee & Person
        services.AddScoped<GetAllUserEmployeeController>();
        services.AddScoped<GetByIdUserEmployeeController>();
        services.AddScoped<CreateUserEmployeeController>();
        services.AddScoped<UpdateUserEmployeeController>();
        services.AddScoped<DeleteUserEmployeeController>();

        //Category Idea
        services.AddScoped<GetAllCategoryIdeaController>();

        //Role
        services.AddScoped<GetAllRoleController>();

        //Product Proposal
        services.AddScoped<GetAllProductProposalController>();
        services.AddScoped<CreateProductProposalController>();
        services.AddScoped<UpdateProductProposalController>();
        services.AddScoped<DeleteProductProposalController>();

        //Design
        services.AddScoped<GetAllDesignController>();
        services.AddScoped<CreateDesignController>();
        services.AddScoped<UpdateDesignController>();
        services.AddScoped<DeleteDesignController>();

        //Review Product Proposal
        services.AddScoped<GetAllReviewProductProposalController>();
        services.AddScoped<GetForDesignController>();
        services.AddScoped<CreateReviewProductProposalController>();

        //Review Design
        services.AddScoped<GetAllReviewDesignController>();
        services.AddScoped<CreateReviewDesignController>();

        //Design History
        services.AddScoped<GetAllDesignHistoryController>();
        services.AddScoped<DeleteDesignHistoryController>();

        //Design Comment
        services.AddScoped<CreateDesignCommentController>();
        services.AddScoped<GetByDesignIdDesingCommentController>();

        return services;
    }
}