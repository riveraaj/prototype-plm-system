namespace PLM.BusinessObjects.Interfaces.Factories;
public interface IFileConverterFactory
{
    public IFileConverter GetFileConverter(ContentType type);
}