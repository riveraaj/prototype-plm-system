namespace PLM.BusinessLogic.Helpers;
internal static class HandlerFileHelper
{

    private static readonly string FILE_PATH = @"http:\\127.0.0.1:8080\PLM\";
    /// <summary>
    /// Handles the file associated with the product proposal.
    /// Converts and saves the file based on its content type.
    /// </summary>
    /// <param name="id">ID of the product proposal</param>
    /// <param name="userEmployeeId">ID of the user employee</param>
    /// <param name="oFileUploadDTO">DTO with file upload data</param>
    /// <returns>Path of the saved file</returns>
    public static string HandleFile(int id, int userEmployeeId,
                                    FileUploadDTO oFileUploadDTO,
                                    IFileConverterFactory _fileConverterFactory)
    {
        try
        {
            // Generate a title for the file based on the proposal ID, user employee ID, and date
            string fileTittle = $"{oFileUploadDTO.Name}-{id}" +
                                $"-{userEmployeeId}-" +
                                $"{DateTime.Now.ToString("dd-MM-yy").Replace('/', '-')}";

            // Determine the file converter based on the content type
            var fileConverter = oFileUploadDTO.ContentType.ToLower() switch
            {
                "text/plain" => _fileConverterFactory.GetFileConverter(ContentType.PlainText),
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => _fileConverterFactory.GetFileConverter(ContentType.Word),
                "application/msword" => _fileConverterFactory.GetFileConverter(ContentType.Word),
                "application/pdf" => _fileConverterFactory.GetFileConverter(ContentType.PDF),
                _ => throw new ArgumentException("Invalid type",
                                                 nameof(oFileUploadDTO))
            };

            // Convert the file content and save it
            byte[] file = fileConverter.Convert(oFileUploadDTO);
            SaveFileHelper.Save(oFileUploadDTO.Content, fileTittle);
            string pathFile = Path.Combine(FILE_PATH, fileTittle + ".pdf");

            return pathFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}