using FurryFriends.Application.Services.Authentication.Interfaces;
using FurryFriends.Application.Shared.Models.Base;
using System.Security.Cryptography;
using System.Text;

namespace FurryFriends.Application.Services.Authentication;

public class EncryptionService : IEncryptionService
{
    private readonly string _encryptionKey = "85y2ex1P0n1";
    private readonly SymmetricAlgorithm _cryptoService;

    public EncryptionService()
    {
        _cryptoService = Rijndael.Create();
    }

    public async Task<ServiceResponse> EncryptAsync(string content)
    {
        byte[] encrypted = EncryptStringToBytes(content);

        string encryptedPassword = Convert.ToBase64String(encrypted);

        return await Task.FromResult(new ServiceResponse(encryptedPassword));
    }

    #region Private Methods
    private byte[] EncryptStringToBytes(string plainText)
    {
        byte[] encrypted;

        byte[] legalKey = GetLegalKey(_encryptionKey);

        // Create an Rijndael object
        // with the specified key and IV.
        using (Rijndael rijAlg = Rijndael.Create())
        {
            rijAlg.Key = legalKey;
            rijAlg.IV = legalKey;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    private byte[] GetLegalKey(string Key)
    {
        string sTemp;
        if (_cryptoService.LegalKeySizes.Length > 0)
        {
            int moreSize = _cryptoService.LegalKeySizes[0].MinSize;
            // key sizes are in bits
            while (Key.Length * 8 > moreSize)
            {
                moreSize += _cryptoService.LegalKeySizes[0].SkipSize;
            }
            sTemp = Key.PadRight(moreSize / 8, ' ');
        }
        else
            sTemp = Key;

        // convert the secret key to byte array
        return Encoding.ASCII.GetBytes(sTemp);
    }
    #endregion
}