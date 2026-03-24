using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerName;
}

// 세팅 정보
[System.Serializable]
public class SettingsData
{
    
}

// 통합 데이터 클래스
[System.Serializable]
public class GameData
{
    public PlayerData playerData = new PlayerData();
    public SettingsData settingsData = new SettingsData();
    public string checksum = ""; // checksum 필드를 만들어서 JSON 암호화 전에 값을 계산
}