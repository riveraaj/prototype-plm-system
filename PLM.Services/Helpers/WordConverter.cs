namespace PLM.BusinessLogic.Helpers;
/// <summary>
/// Converts Word documents to PDF format.
/// Implements the IFileConverter interface for file conversion.
/// </summary>
internal class WordConverter : IFileConverter
{
    /// <summary>
    /// Converts the provided Word document file to a PDF document.
    /// </summary>
    /// <param name="oFileUploadDTO">The file upload data transfer object containing file content and metadata.</param>
    /// <returns>A byte array representing the PDF document.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the content type is neither "application/msword" nor "application/vnd.openxmlformats-officedocument.wordprocessingml.document".</exception>
    public byte[] Convert(FileUploadDTO oFileUploadDTO)
    {
        try
        {
            // Check if the content type is word"
            if (oFileUploadDTO.ContentType != "application/msword" ||
                oFileUploadDTO.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                throw new InvalidOperationException("Unsupported content type");

            // Create a memory stream for the input file content
            using MemoryStream inputStream = new(oFileUploadDTO.Content);

            // Load the Word document from the input stream
            Aspose.Words.Document wordDocument = new(inputStream);

            // Create a memory stream for the output PDF document
            using MemoryStream ms = new();

            // Save the Word document as a PDF in the output stream
            wordDocument.Save(ms, Aspose.Words.SaveFormat.Pdf);

            // Return the PDF document as a byte array
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}