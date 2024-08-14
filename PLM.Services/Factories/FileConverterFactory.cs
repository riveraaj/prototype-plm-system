namespace PLM.BusinessLogic.Factories;
internal class FileConverterFactory(IServiceProvider serviceProvider) : IFileConverterFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IFileConverter GetFileConverter(ContentType type)
    {
        try
        {
            return type switch
            {
                ContentType.PlainText => _serviceProvider.GetRequiredService<PlainTextConverter>(),
                ContentType.Word => _serviceProvider.GetRequiredService<WordConverter>(),
                ContentType.PDF => _serviceProvider.GetRequiredService<PDFConverter>(),
                _ => throw new ArgumentException("Invalid type", nameof(type)),
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}