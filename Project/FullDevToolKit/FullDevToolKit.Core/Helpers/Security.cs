﻿using System.Text;
using System.Security.Cryptography;

namespace FullDevToolKit.Helpers
{
    public enum CryptProvider
    {
        Rijndael = 0,
        RC2 = 1,
        DES = 2,
        TripleDES = 3,

    }

    public class Encryptor
    {
        #region Private members
        private string _key = string.Empty;
        private CryptProvider _cryptProvider;
        private SymmetricAlgorithm _algorithm;
        private void SetIV()
        {
            switch (_cryptProvider)
            {
                case CryptProvider.Rijndael:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;
                default:
                    _algorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }
        #endregion

        #region Properties

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        #endregion

        #region Constructors

        public Encryptor()
        {
            _algorithm = new RijndaelManaged();
            _algorithm.Mode = CipherMode.CBC;
            _cryptProvider = CryptProvider.Rijndael;
            _key = "dkproject";
        }

        public Encryptor(CryptProvider cryptProvider)
        {
            // Seleciona algoritmo simétrico
            switch (cryptProvider)
            {
                case CryptProvider.Rijndael:
                    _algorithm = new RijndaelManaged();
                    _cryptProvider = CryptProvider.Rijndael;
                    break;
                case CryptProvider.RC2:
                    _algorithm = new RC2CryptoServiceProvider();
                    _cryptProvider = CryptProvider.RC2;
                    break;
                case CryptProvider.DES:
                    _algorithm = new DESCryptoServiceProvider();
                    _cryptProvider = CryptProvider.DES;
                    break;
                case CryptProvider.TripleDES:
                    _algorithm = new TripleDESCryptoServiceProvider();
                    _cryptProvider = CryptProvider.TripleDES;
                    break;
            }
            _algorithm.Mode = CipherMode.CBC;
            _key = "dkproject";
        }
        #endregion

        #region Public methods

        public virtual byte[] GetKey()
        {
            string salt = string.Empty;

            // Ajuta o tamanho da chave se necessário e retorna uma chave válida
            if (_algorithm.LegalKeySizes.Length > 0)
            {
                // Tamanho das chaves em bits
                int keySize = _key.Length * 8;
                int minSize = _algorithm.LegalKeySizes[0].MinSize;
                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Busca o valor máximo da chave
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Seta um tamanho válido
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Preenche a chave com arterisco para corrigir o tamanho
                        _key = _key.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(_key.Length);
        }

        public virtual string Encrypt(string plainText)
        {
            try
            {

                byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(plainText);
                byte[] keyByte = GetKey();


                // Seta a chave privada
                _algorithm.Key = keyByte;
                SetIV();

                // Interface de criptografia / Cria objeto de criptografia
                ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();

                MemoryStream _memoryStream = new MemoryStream();

                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);

                // Grava os dados criptografados no MemoryStream
                _cryptoStream.Write(plainByte, 0, plainByte.Length);
                _cryptoStream.FlushFinalBlock();

                // Busca o tamanho dos bytes encriptados
                byte[] cryptoByte = _memoryStream.ToArray();
                // Converte para a base 64 string para uso posterior em um xml
                return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public virtual string Decrypt(string cryptoText)
        {

            try
            {
                // Converte a base 64 string em num array de bytes
                byte[] cryptoByte = Convert.FromBase64String(cryptoText);
                byte[] keyByte = GetKey();


                // Seta a chave privada
                _algorithm.Key = keyByte;
                SetIV();

                // Interface de criptografia / Cria objeto de descriptografia
                ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();

                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);

                // Busca resultado do CryptoStream
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }

    public static class MD5
    {
        public static string BuildMD5(string text)
        {
            string sString = "";
            using (MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider())
            {

                byte[] msg = Encoding.Default.GetBytes(text);

                byte[] hash = csp.ComputeHash(msg);

                for (int i = 0; i <= hash.Length - 1; i++)
                {
                    sString = sString + (hash[i].ToString("x2"));

                }

            }
            return sString;
        }
    }
}
