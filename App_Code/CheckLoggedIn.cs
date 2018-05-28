using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;

public class CheckLoggedIn
{
    private static Byte[] KEY_64 = { 4, 1, 6, 2, 8, 1, 1, 2 };
    private static Byte[] IV_64 = { 2, 1, 1, 8, 2, 6, 1, 4 };

	public static string Check(string value)
	{
        DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        Byte[] buffer = Convert.FromBase64String(value);
        MemoryStream ms = new MemoryStream(buffer);
        CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(cs);
        return(sr.ReadToEnd());
    }
}
