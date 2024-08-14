namespace PLM.BusinessLogic.Factories;
internal class CreateInputPortFactory(IServiceProvider serviceProvider) : ICreateInputPortFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public ICreateInputPort<T> GetInputPort<T>()
         => _serviceProvider.GetRequiredService<ICreateInputPort<T>>();
}