namespace PLM.BusinessLogic;
public static class DependecyContainer
{
    public static IServiceCollection AddBusinessLogicServices
        (this IServiceCollection services)
    {
        //Get All
        services.AddScoped<GetAllIdeaService>();
        services.AddScoped<GetAllUserEmployeeService>();
        services.AddScoped<GetAllCategoryIdeaService>();
        services.AddScoped<GetAllRoleService>();
        services.AddScoped<GetAllProductProposalService>();
        services.AddScoped<GetAllDesignService>();
        services.AddScoped<GetAllReviewProductProposalService>();
        services.AddScoped<GetAllReviewDesignService>();
        services.AddScoped<GetAllDesignHistoryService>();
        services.AddScoped<IGetAllInputPortFactory, GetAllInputPortFactory>();

        //Get By Params
        services.AddScoped<IGetByParamsInputPort<string>, GetByParamsIdeaService>();
        services.AddScoped<IGetByParamsInputPort<int>, GetByParamsUserEmployeeService>();
        services.AddScoped<IGetByParamsInputPort<bool>, GetByParamsReviewProductProposalService>();
        services.AddScoped<IGetByParamsInputPort<uint>, GetByParamsDesignCommentService>();
        services.AddScoped<IGetByParamsInputPortFactory, GetByParamsInputPortFactory>();

        //Create
        services.AddScoped<ICreateInputPort<CreateIdeaDTO>, CreateIdeaService>();
        services.AddScoped<ICreateInputPort<CreateUserEmployeeDTO>, CreateUserEmployeeService>();
        services.AddScoped<ICreateInputPort<CreateProductProposalDTO>, CreateProductProposalService>();
        services.AddScoped<ICreateInputPort<CreateDesignDTO>, CreateDesignService>();
        services.AddScoped<ICreateInputPort<CreateReviewProductProposalDTO>, CreateReviewProductProposalService>();
        services.AddScoped<ICreateInputPort<CreateReviewDesignDTO>, CreateReviewDesignService>();
        services.AddScoped<ICreateInputPort<CreateDesignCommentDTO>, CreateDesignCommentService>();
        services.AddScoped<ICreateInputPortFactory, CreateInputPortFactory>();

        //Update
        services.AddScoped<IUpdateInputPort<UpdateIdeaDTO>, UpdateIdeaService>();
        services.AddScoped<IUpdateInputPort<UpdateUserEmployeeDTO>, UpdateUserEmployeeService>();
        services.AddScoped<IUpdateInputPort<UpdateProductProposalDTO>, UpdateProductProposalService>();
        services.AddScoped<IUpdateInputPort<UpdateDesignDTO>, UpdateDesignService>();
        services.AddScoped<IUpdateInputPortFactory, UpdateInputPortFactory>();

        //Delete
        services.AddScoped<DeleteIdeaService>();
        services.AddScoped<DeleteUserEmployeeService>();
        services.AddScoped<DeleteProductProposalService>();
        services.AddScoped<DeleteDesignService>();
        services.AddScoped<DeleteDesignHistoryService>();
        services.AddScoped<IDeleteInputPortFactory, DeleteInputPortFactory>();

        //File Converter
        services.AddScoped<PlainTextConverter>();
        services.AddScoped<WordConverter>();
        services.AddScoped<PDFConverter>();
        services.AddScoped<IFileConverterFactory, FileConverterFactory>();

        return services;
    }
}