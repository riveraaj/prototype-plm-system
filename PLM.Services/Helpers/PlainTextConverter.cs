namespace PLM.BusinessLogic.Helpers;
/// <summary>
/// Converts plain text files to PDF format.
/// Implements the IFileConverter interface for file conversion.
/// </summary>
internal class PlainTextConverter : IFileConverter
{
    /// <summary>
    /// Converts the provided plain text file to a PDF document.
    /// </summary>
    /// <param name="oFileUploadDTO">The file upload data transfer object containing file content and metadata.</param>
    /// <returns>A byte array representing the PDF document.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the content type is not "text/plain".</exception>
    public byte[] Convert(FileUploadDTO oFileUploadDTO)
    {
        try
        {
            // Check if the content type is "text/plain"
            if (oFileUploadDTO.ContentType != "text/plain")
                throw new InvalidOperationException("Unsupported content type");

            // Create a new PDF document
            Aspose.Pdf.Document pdfDocument = new();

            // Add a page to the document
            Page oPage = pdfDocument.Pages.Add();

            // Create a text fragment from the file content
            TextFragment oTextFragment = new(System.Text.Encoding.UTF8.GetString(oFileUploadDTO.Content));
            oPage.Paragraphs.Add(oTextFragment);

            // Save the document to a memory stream
            using MemoryStream ms = new();
            pdfDocument.Save(ms);

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