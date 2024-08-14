namespace PLM.BusinessLogic.Factories;
public class GetAllInputPortFactory(IServiceProvider serviceProvider) : IGetAllInputPortFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IGetAllInputPort GetInputPort(InputPortType type)
    {
        try
        {
            return type switch
            {
                InputPortType.Idea => _serviceProvider.GetRequiredService<GetAllIdeaService>(),
                InputPortType.UserEmployee => _serviceProvider.GetRequiredService<GetAllUserEmployeeService>(),
                InputPortType.CategoryIdea => _serviceProvider.GetRequiredService<GetAllCategoryIdeaService>(),
                InputPortType.Role => _serviceProvider.GetRequiredService<GetAllRoleService>(),
                InputPortType.ProductProposal => _serviceProvider.GetRequiredService<GetAllProductProposalService>(),
                InputPortType.Design => _serviceProvider.GetRequiredService<GetAllDesignService>(),
                InputPortType.ReviewProductProposal => _serviceProvider.GetRequiredService<GetAllReviewProductProposalService>(),
                InputPortType.ReviewDesign => _serviceProvider.GetRequiredService<GetAllReviewDesignService>(),
                InputPortType.DesignHistory => _serviceProvider.GetRequiredService<GetAllDesignHistoryService>(),
                _ => throw new ArgumentException("Invalid type", nameof(type)),
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}