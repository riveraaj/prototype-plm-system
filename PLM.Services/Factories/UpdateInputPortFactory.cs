namespace PLM.BusinessLogic.Factories;
internal class UpdateInputPortFactory(IServiceProvider serviceProvider) : IUpdateInputPortFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IUpdateInputPort<T> GetInputPort<T>()
         => _serviceProvider.GetRequiredService<IUpdateInputPort<T>>();
}