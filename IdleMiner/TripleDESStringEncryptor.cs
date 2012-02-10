using System;
using System.Security.Cryptography;
using System.Text;

namespace IdleMiner
{
    public class TripleDESStringEncryptor
    {
        private byte[] _key;
        private readonly TripleDESCryptoServiceProvider _provider;
        private readonly UTF8Encoding UTF8 = new UTF8Encoding();

        public TripleDESStringEncryptor(string key)
        {
            var hashProvider = new MD5CryptoServiceProvider();
            _key = hashProvider.ComputeHash(UTF8.GetBytes(key));
            _provider = new TripleDESCryptoServiceProvider
                            {Key = _key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};
            hashProvider.Clear();
        }

        #region IStringEncryptor Members

        public string EncryptString(string plainText)
        {
            byte[] dataToEncrypt = UTF8.GetBytes(plainText);
            ICryptoTransform encryptor = _provider.CreateEncryptor();
            byte[] results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            _provider.Clear();
            return Convert.ToBase64String(results);
        }

        public string DecryptString(string encryptedText)
        {
             byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);
             ICryptoTransform decryptor = _provider.CreateDecryptor();
             byte[] results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            _provider.Clear();
            return UTF8.GetString(results);
        }

        #endregion
    }
}