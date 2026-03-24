using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviourPunCallbacks
{
    // 플레이어별 선택 캐릭터 저장
    public Dictionary<int, string> playerCharacters = new Dictionary<int, string>();
    public static string enemyCharacter;
    public static string allyCharacter;

    private void Start()
    {
        GetAllPlayerCharacters();
    }

    void GetAllPlayerCharacters()
    {
        playerCharacters.Clear();

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object charObj;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Player" + player.ActorNumber + "_Character", out charObj))
            {
                string character = (string)charObj;
                playerCharacters.Add(player.ActorNumber, character);
                Debug.Log("Player " + player.ActorNumber + " 선택한 캐릭터: " + character);

                // ui에 자신 캐릭터 정보 표시
                if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    allyCharacter = character;
                }
                else
                {
                    enemyCharacter = character;
                }
            }
            else
            {
                Debug.Log("Player " + player.ActorNumber + " 캐릭터 정보 없음");
            }
        }
    }
}
