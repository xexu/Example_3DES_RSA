using OpenSSL.Crypto;
using System;
using System.Security.Cryptography;
using System.Text;

public static class Cryptography
{
    /*
     * 
     * Add openssl.net 0.5 reference (Might work with other version)
     * Copy libeay32.dll and ssleay32.dll to current folder/path
     * (Might have to) Set Project/Properties/Build Target Platform to x86
     * 
     */
    #region MD5

    public static string HashMD5(string phrase)
    {
        if (phrase == null)
            return null;
        var encoder = new UTF8Encoding();
        var md5Hasher = new MD5CryptoServiceProvider();
        var hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(phrase));
        return ByteArrayToHexString(hashedDataBytes);
    }

    #endregion

    #region RSA

    public static byte[] EncryptRSA(string publicKeyAsPem, string payload, string passphrase = null)
    {
        var encoder = new UTF8Encoding();
        byte[] byte_payload = encoder.GetBytes(payload);
        CryptoKey d = CryptoKey.FromPublicKey(publicKeyAsPem, passphrase);
        OpenSSL.Crypto.RSA rsa = d.GetRSA();
        byte[] result = rsa.PublicEncrypt(byte_payload, OpenSSL.Crypto.RSA.Padding.PKCS1);
        rsa.Dispose();
        return result;
    }
    public static string DecryptRSA(string privateKeyAsPem, byte[] payload, string passphrase = null)
    {
        var encoder = new UTF8Encoding();
        byte[] byte_payload = payload;
        CryptoKey d = CryptoKey.FromPrivateKey(privateKeyAsPem, passphrase);
        OpenSSL.Crypto.RSA rsa = d.GetRSA();
        byte[] result = rsa.PrivateDecrypt(byte_payload, OpenSSL.Crypto.RSA.Padding.PKCS1);
        rsa.Dispose();
        return encoder.GetString(result);
    }

    #endregion

    #region 3DES

    public static string EncryptTripleDES(string phrase, string key, bool hashKey = true)
    {
        var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
        var toEncryptArray = Encoding.UTF8.GetBytes(phrase);

        var tdes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        var cTransform = tdes.CreateEncryptor();
        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return ByteArrayToHexString(resultArray);
    }

    public static string DecryptTripleDES(string hash, string key, bool hashKey = true)
    {
        var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
        var toEncryptArray = HexStringToByteArray(hash);

        var tdes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };

        var cTransform = tdes.CreateDecryptor();
        var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }

    #endregion

    #region Helpers

    internal static string ByteArrayToHexString(byte[] inputArray)
    {
        if (inputArray == null)
            return null;
        var o = new StringBuilder("");
        for (var i = 0; i < inputArray.Length; i++)
            o.Append(inputArray[i].ToString("X2"));
        return o.ToString();
    }

    internal static byte[] HexStringToByteArray(string inputString)
    {
        if (inputString == null)
            return null;

        if (inputString.Length == 0)
            return new byte[0];

        if (inputString.Length % 2 != 0)
            throw new Exception("Hex strings have an even number of characters and you have got an odd number of characters!");

        var num = inputString.Length / 2;
        var bytes = new byte[num];
        for (var i = 0; i < num; i++)
        {
            var x = inputString.Substring(i * 2, 2);
            try
            {
                bytes[i] = Convert.ToByte(x, 16);
            }
            catch (Exception ex)
            {
                throw new Exception("Part of your \"hex\" string contains a non-hex value.", ex);
            }
        }
        return bytes;
    }

    #endregion
}