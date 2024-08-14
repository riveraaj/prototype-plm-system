namespace PLM.BusinessLogic.Factories;
internal class DeleteInputPortFactory(IServiceProvider serviceProvider) : IDeleteInputPortFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IDeleteInputPort GetInputPort(InputPortType type)
    {
        try
        {
            return type switch
            {
                InputPortType.Idea => _serviceProvider.GetRequiredService<DeleteIdeaService>(),
                InputPortType.UserEmployee => _serviceProvider.GetRequiredService<DeleteUserEmployeeService>(),
                InputPortType.ProductProposal => _serviceProvider.GetRequiredService<DeleteProductProposalService>(),
                InputPortType.Design => _serviceProvider.GetRequiredService<DeleteDesignService>(),
                InputPortType.DesignHistory => _serviceProvider.GetRequiredService<DeleteDesignHistoryService>(),
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
