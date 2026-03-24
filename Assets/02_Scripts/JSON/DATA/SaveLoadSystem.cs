using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public static class SaveLoadSystem
{
    // path는 static 생성자에서 안전하게 초기화
    private static string path;
    private static readonly string key = "G7r!9Xk#2LpQw1Za"; // AES 16글자 키
    private static readonly string iv  = "F3v@8Nn%4YmRk2Tb"; // AES 16글자 IV

    // static 생성자: 클래스가 처음 사용될 때 실행됨
    static SaveLoadSystem()
    {
        path = Application.persistentDataPath + "/data1.json";
        Debug.Log("SaveLoadSystem initialized. Data path: " + path);
    }

    // GameData 저장
    public static void SaveGameData(GameData data)
    {
        // 체크섬 계산 전에 초기화
        data.checksum = "";
        string jsonForChecksum = JsonUtility.ToJson(data);
        data.checksum = ComputeChecksum(jsonForChecksum);

        // JSON 직렬화
        string json = JsonUtility.ToJson(data);

        // AES 암호화 후 파일 저장
        byte[] bytes = Encoding.UTF8.GetBytes(json);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            File.WriteAllBytes(path, encrypted);
        }
    }

    // GameData 불러오기
    public static GameData LoadGameData()
    {
        if (!File.Exists(path)) return new GameData(); // 파일 없으면 새 데이터 생성

        try
        {
            byte[] encrypted = File.ReadAllBytes(path);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decrypted = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);

                string json = Encoding.UTF8.GetString(decrypted);
                GameData data = JsonUtility.FromJson<GameData>(json);

                // 체크섬 검증
                string originalChecksum = data.checksum;
                data.checksum = ""; 
                string recalculated = ComputeChecksum(JsonUtility.ToJson(data));

                if (originalChecksum != recalculated)
                {
                    Debug.LogWarning("데이터 변조 감지됨! (체크섬 불일치)");
                    infoUI.isUseCheat = true;

                    // 초기화 후 즉시 정상값으로 덮어쓰기
                    GameData resetData = new GameData();
                    SaveGameData(resetData);
                    return resetData;
                }

                data.checksum = originalChecksum;
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("데이터 변조 감지됨! (복호화 실패)\n" + e.Message);
            infoUI.isUseCheat = true;

            // 초기화 후 정상값 덮어쓰기
            GameData resetData = new GameData();
            SaveGameData(resetData);
            return resetData;
        }
    }

    // 체크섬 계산
    public static string ComputeChecksum(string json)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            byte[] hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
