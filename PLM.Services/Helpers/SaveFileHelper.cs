namespace PLM.BusinessLogic.Helpers;
/// <summary>
/// Provides functionality for saving files to a specified directory.
/// </summary>
internal static class SaveFileHelper
{
    private const string FILE_PATH = @"C:\Documents\PLM\";

    /// <summary>
    /// Saves the provided document content to a file with the specified name.
    /// </summary>
    /// <param name="documentContent">The content of the document to be saved.</param>
    /// <param name="documentName">The name of the file to be created (without extension).</param>
    /// <returns>The full path of the saved file.</returns>
    public static void Save(byte[] documentContent, string documentName)
    {
        try
        {
            // If the folders doesn´t exists, then they will be created
            if (!Directory.Exists(FILE_PATH))
                Directory.CreateDirectory(FILE_PATH);

            //Merge the file name with the directory path to get the full path
            string fullFilePath = Path.Combine(FILE_PATH, documentName + ".pdf");

            //Write the content of the PDF to the specified file
            File.WriteAllBytes(fullFilePath, documentContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}