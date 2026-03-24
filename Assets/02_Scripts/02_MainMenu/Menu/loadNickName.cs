using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class loadNickName : MonoBehaviour
{
    public Text nickNameText;
    private GameData gameData;

    private void Awake()
    {
        gameData = SaveLoadSystem.LoadGameData();
        
        PhotonNetwork.NickName = gameData.playerData.playerName;

        nickNameText.text = PhotonNetwork.NickName;
    }
}
