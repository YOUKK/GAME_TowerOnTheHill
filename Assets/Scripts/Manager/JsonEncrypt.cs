using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class JsonEncrypt
{
    // json AES 암호화를 위한 변수
    public static byte[] aesKey; // 암호화/복호화에 필요한 키 값
    public static byte[] iv; // 초기화 벡터
    private static string aesKeyPath;
    private static string ivPath;

    // AES 암호화
    public static string AESEncrypt(string data)
    {
        CheckAESKey();

        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = iv;

            // 암호화 변환기 생성
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            // 텍스트를 암호화
            byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(data), 0, data.Length);

            // 암호화된 바이트 배열을 Base64 문자열로 변환 후 반환
            return System.Convert.ToBase64String(encrypted);
        }
    }

    // AES 복호화
    public static string AESDecrypt(string encryptedText)
    {
        CheckAESKey();

        byte[] buffer = System.Convert.FromBase64String(encryptedText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = aesKey;
            aes.IV = iv;

            // 복호화 변환기 생성
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            // 암호화된 바이트 배열을 복호화
            byte[] decrypted = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);

            // 복호화된 바이트 배열을 UTF-8 문자열로 변환 후 반환
            return Encoding.UTF8.GetString(decrypted);
        }
    }

    private static byte[] GenerateRandomByte(int length)
    {
        byte[] randomBytes = new byte[length];

        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }

        return randomBytes;
    }

    // AES키랑 IV가 있는지 확인, 없으면 새로 생성
    private static void CheckAESKey()
    {
        aesKeyPath = Path.Combine(Application.persistentDataPath, "aesKey.dat");
        ivPath = Path.Combine(Application.persistentDataPath, "aesIV.dat");

        if (File.Exists(aesKeyPath) && File.Exists(ivPath)) // 키와 iv가 존대한다면
        {
            aesKey = File.ReadAllBytes(aesKeyPath);
            iv = File.ReadAllBytes(ivPath);
        }
        else // 없다면 새로 생성
        {
            aesKey = GenerateRandomByte(16);
            iv = GenerateRandomByte(16);

            File.WriteAllBytes(aesKeyPath, aesKey);
            File.WriteAllBytes(ivPath, iv);
        }
    }
}
