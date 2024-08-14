namespace PLM.BusinessLogic.Helpers;
internal static class EncryptorHelper
{
    public static string Encrypt(string text)
    {

        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));

        // Convert bytes to a hexadecimal string
        StringBuilder builder = new();

        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("x2"));

        return builder.ToString();
    }

    public static bool ValidateEncryption(string text, string encryptedText)
    {
        // Encrypt the entered text
        string newEncryptedText = Encrypt(text);

        // Compare the new encrypted text with the encrypted text
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        return comparer.Compare(newEncryptedText, encryptedText) == 0;
    }
}