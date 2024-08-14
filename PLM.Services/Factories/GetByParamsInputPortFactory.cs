namespace PLM.BusinessLogic.Factories;
internal class GetByParamsInputPortFactory(IServiceProvider serviceProvider)
    : IGetByParamsInputPortFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IGetByParamsInputPort<T> GetInputPort<T>()
         => _serviceProvider.GetRequiredService<IGetByParamsInputPort<T>>();
}