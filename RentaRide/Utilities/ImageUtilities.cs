using System.Security.Cryptography;

namespace RentaRide.Utilities
{
    public static class ImageUtilities
    {
        public static string ProcessImageUpload(IFormFile file, string folderLocation ,string key, string iv, out string imageFilePath)
        {
            try
            {

                var bKey = Convert.FromBase64String(key);
                var ivKey = Convert.FromBase64String(iv);

                var byteArray = ImageToByteArray(file);
                var encryptedBytes = EncryptImageByteArray(byteArray, bKey, ivKey);
                var filename = Guid.NewGuid().ToString();
                var filePath = Path.Combine(folderLocation, filename);
                SaveEncryptedFile(encryptedBytes, filePath);
                imageFilePath = filePath;
                return filename;
            }
            catch (Exception e)
            {
                imageFilePath = "";
                return null;
            }

        }

        public static byte[] ProcessDecodeImage(string imgName, string imgpath, string key, string iv)
        {
            string FilePath = Path.Combine(imgpath, imgName);
            var bKey = Convert.FromBase64String(key);
            var ivKey = Convert.FromBase64String(iv);

            var encryptedBytes = ReadEncryptedFile(FilePath);
            if (imgName == "Default.png")
            {
                return encryptedBytes;
            }
            var decyptBytes = DecryptByteArray(encryptedBytes, bKey, ivKey);
            return decyptBytes;
        }

        private static byte[] ImageToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        private static byte[] EncryptImageByteArray(byte[] byteArray, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(byteArray, 0, byteArray.Length);
                        csEncrypt.FlushFinalBlock();
                        return msEncrypt.ToArray();
                    }
                }

            }
        }
        private static void SaveEncryptedFile(byte[] encryptedBytes, string filePath)
        {
            File.WriteAllBytes(filePath, encryptedBytes);
        }

        private static byte[] ReadEncryptedFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        private static byte[] DecryptByteArray(byte[] encryptedBytes, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var msDecrypt = new MemoryStream(encryptedBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var msOutput = new MemoryStream())
                {
                    csDecrypt.CopyTo(msOutput);
                    return msOutput.ToArray();
                }
            }
        }
    }
}
