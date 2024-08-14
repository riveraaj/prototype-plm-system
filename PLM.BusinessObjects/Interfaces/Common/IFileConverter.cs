namespace PLM.BusinessObjects.Interfaces.Common;
public interface IFileConverter
{
    public byte[] Convert(FileUploadDTO oFileUploadDTO);
}