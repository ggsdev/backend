using System.Security.Cryptography;
using System.Text;

namespace PRIO.src.Shared.Utils
{
    public static class Decrypt
    {
        private static void DeriveKeyAndIv(byte[] passphrase, byte[] salt, int iterations, out byte[] key, out byte[] iv)
        {
            var hashList = new List<byte>();

            var preHashLength = passphrase.Length + (salt?.Length ?? 0);
            var preHash = new byte[preHashLength];

            Buffer.BlockCopy(passphrase, 0, preHash, 0, passphrase.Length);
            if (salt != null)
                Buffer.BlockCopy(salt, 0, preHash, passphrase.Length, salt.Length);

            var hash = MD5.Create();
            var currentHash = hash.ComputeHash(preHash);

            for (var i = 1; i < iterations; i++)
            {
                currentHash = hash.ComputeHash(currentHash);
            }

            hashList.AddRange(currentHash);

            while (hashList.Count < 48) // for 32-byte key and 16-byte iv
            {
                preHashLength = currentHash.Length + passphrase.Length + (salt?.Length ?? 0);
                preHash = new byte[preHashLength];

                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(passphrase, 0, preHash, currentHash.Length, passphrase.Length);
                if (salt != null)
                    Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + passphrase.Length, salt.Length);

                currentHash = hash.ComputeHash(preHash);

                for (var i = 1; i < iterations; i++)
                {
                    currentHash = hash.ComputeHash(currentHash);
                }

                hashList.AddRange(currentHash);
            }

            hash.Clear();
            key = new byte[32];
            iv = new byte[16];
            hashList.CopyTo(0, key, 0, 32);
            hashList.CopyTo(32, iv, 0, 16);
        }

        public static bool TryParseBase64String(string base64String, out byte[]? bytes)
        {
            try
            {
                bytes = Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                bytes = null;
                return false;
            }
        }
        public static string DecryptAes(string encryptedString, string passphrase)
        {

            var base64Bytes = Convert.FromBase64String(encryptedString);
            var saltBytes = base64Bytes[8..16];
            var cipherTextBytes = base64Bytes[16..];

            var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);

            DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out var keyBytes, out var ivBytes);


            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            aes.KeySize = 256;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);

            using var msDecrypt = new MemoryStream(cipherTextBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }

        public static string DecryptAesEnv(string encryptedString, string passphrase)
        {
            var base64Bytes = Convert.FromBase64String(encryptedString);
            var saltBytes = base64Bytes[0..8];
            var cipherTextBytes = base64Bytes[8..];

            var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);

            DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out var keyBytes, out var ivBytes);

            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            aes.KeySize = 256;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            var decryptor = aes.CreateDecryptor(keyBytes, ivBytes);

            using var msDecrypt = new MemoryStream(cipherTextBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }

        public static string EncryptAes(string plainText, string passphrase)
        {
            byte[] saltBytes = GenerateSalt();

            byte[] passphraseBytes = Encoding.UTF8.GetBytes(passphrase);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out byte[] keyBytes, out byte[] ivBytes);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                aes.KeySize = 256;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, ivBytes);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainTextBytes, 0, plainTextBytes.Length);
                        csEncrypt.FlushFinalBlock();
                    }

                    byte[] encryptedBytes = msEncrypt.ToArray();

                    byte[] encryptedBytesWithSalt = new byte[saltBytes.Length + encryptedBytes.Length];
                    Buffer.BlockCopy(saltBytes, 0, encryptedBytesWithSalt, 0, saltBytes.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, encryptedBytesWithSalt, saltBytes.Length, encryptedBytes.Length);

                    return Convert.ToBase64String(encryptedBytesWithSalt);
                }
            }
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[8]; // 8 bytes for the salt
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
    //public static DecryptedCredentials DecryptAes(string encryptedEmail, string encryptedPassword, string passphrase)
    //{
    //    var base64EmailBytes = Convert.FromBase64String(encryptedEmail);
    //    var base64PasswordBytes = Convert.FromBase64String(encryptedPassword);

    //    var saltBytes = base64EmailBytes[8..16];
    //    var emailCipherTextBytes = base64EmailBytes[16..];
    //    var passwordCipherTextBytes = base64PasswordBytes[16..];

    //    var passphraseBytes = Encoding.UTF8.GetBytes(passphrase);

    //    DeriveKeyAndIv(passphraseBytes, saltBytes, 1, out var keyBytes, out var ivBytes);

    //    var aes = GetAesInstance();
    //    aes.Key = keyBytes;
    //    aes.IV = ivBytes;

    //    aes.KeySize = 256;
    //    aes.Padding = PaddingMode.PKCS7;
    //    aes.Mode = CipherMode.CBC;


    //    var emailDecryptor = aes.CreateDecryptor(keyBytes, ivBytes);
    //    var passwordDecryptor = aes.CreateDecryptor(keyBytes, ivBytes);

    //    using var msEmailDecrypt = new MemoryStream(emailCipherTextBytes);
    //    using var msPasswordDecrypt = new MemoryStream(passwordCipherTextBytes);

    //    using var csEmailDecrypt = new CryptoStream(msEmailDecrypt, emailDecryptor, CryptoStreamMode.Read);
    //    using var csPasswordDecrypt = new CryptoStream(msPasswordDecrypt, passwordDecryptor, CryptoStreamMode.Read);

    //    using var srEmailDecrypt = new StreamReader(csEmailDecrypt);
    //    using var srPasswordDecrypt = new StreamReader(csPasswordDecrypt);

    //    var decryptedCredentials = new DecryptedCredentials
    //    {
    //        Email = srEmailDecrypt.ReadToEnd(),
    //        Password = srPasswordDecrypt.ReadToEnd()
    //    };

    //    return decryptedCredentials;
    //}

    //private static Aes GetAesInstance()
    //{
    //    var aes = Aes.Create();
    //    return aes;
    //}

    //private static void DeriveKeyAndIv(byte[] passphrase, byte[] salt, int iterations, out byte[] key, out byte[] iv)
    //{
    //    var hashList = new List<byte>();

    //    var preHashLength = passphrase.Length + (salt?.Length ?? 0);
    //    var preHash = new byte[preHashLength];

    //    Buffer.BlockCopy(passphrase, 0, preHash, 0, passphrase.Length);
    //    if (salt != null)
    //        Buffer.BlockCopy(salt, 0, preHash, passphrase.Length, salt.Length);

    //    using (var hash = MD5.Create())
    //    {
    //        var currentHash = MD5.HashData(preHash);

    //        for (var i = 1; i < iterations; i++)
    //        {
    //            currentHash = MD5.HashData(currentHash);
    //        }

    //        hashList.AddRange(currentHash);

    //        while (hashList.Count < 48)
    //        {
    //            preHashLength = currentHash.Length + passphrase.Length + (salt?.Length ?? 0);
    //            preHash = new byte[preHashLength];

    //            Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
    //            Buffer.BlockCopy(passphrase, 0, preHash, currentHash.Length, passphrase.Length);
    //            if (salt != null)
    //                Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + passphrase.Length, salt.Length);

    //            currentHash = MD5.HashData(preHash);

    //            Parallel.For(1, iterations, i =>
    //            {
    //                currentHash = MD5.HashData(currentHash);
    //            });

    //            hashList.AddRange(currentHash);
    //        }
    //    }

    //    key = new byte[32];
    //    iv = new byte[16];
    //    hashList.CopyTo(0, key, 0, 32);
    //    hashList.CopyTo(32, iv, 0, 16);
    //}

    //public class DecryptedCredentials
    //{
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //}
}
