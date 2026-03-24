using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class PlayerNameInputField : MonoBehaviour
{
    private GameData gameData;
    public GameObject ErrorText;
    public Text AgreeButton;

    private void Start()
    {
        ErrorText.SetActive(false);
        AgreeButton.color = Color.gray;

        // 게임 데이터 로드
        gameData = SaveLoadSystem.LoadGameData();

        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            string defaultName = "";

            // null 체크 후 닉네임 가져오기
            if (gameData != null)
            {
                if (gameData.playerData != null)
                {
                    if (!string.IsNullOrEmpty(gameData.playerData.playerName))
                    {
                        defaultName = gameData.playerData.playerName;
                    }
                }
            }

            // InputField와 Photon 닉네임에 설정
            _inputField.text = defaultName;
            PhotonNetwork.NickName = defaultName;
        }
    }

    // InputField Submit 시 호출
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            StartCoroutine(DelayedHideError());
            return;
        }

        // Photon 닉네임 업데이트
        PhotonNetwork.NickName = value;

        // Save in game data
        LegitGameDataManager.ApplyChange(gameData, (data) =>
        {
            if (data != null && data.playerData != null)
            {
                data.playerData.playerName = value;
                // ErrorText.SetActive(false);
                AgreeButton.color = Color.white;
            }
        });

        Debug.Log("Player name set to " + value);
    }

    public IEnumerator DelayedHideError()
    {
        ErrorText.SetActive(true);
        yield return new WaitForSeconds(2f);
        ErrorText.SetActive(false);
    }
}
