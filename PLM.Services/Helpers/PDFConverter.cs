namespace PLM.BusinessLogic.Helpers;
internal class PDFConverter : IFileConverter
{
    public byte[] Convert(FileUploadDTO oFileUploadDTO)
    {
        try
        {
            using MemoryStream inputStream = new(oFileUploadDTO.Content);

            Document pdfDocument = new(inputStream);

            using MemoryStream ms = new();

            pdfDocument.Save(ms);
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}