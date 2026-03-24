using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class newHpManager : MonoBehaviour
{
    public Dictionary<string, int> playerHpDict = new Dictionary<string, int>();
    public int maxHp = 100;

    void Start()
    {
        // 씬에 있는 플레이어들 초기화
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            playerHpDict[p.name] = maxHp; // "Player(Clone)" , "Player_2" 등
        }
    }

    [PunRPC]
    public void ApplyDamage(int viewID, int damage, string attackerName)
    {
        GameObject targetObj = PhotonView.Find(viewID).gameObject;
        PhotonView targetPV = targetObj.GetComponent<PhotonView>();

        Debug.Log($"{attackerName}가 {targetObj.name}에게 {damage} 데미지!");

        if(!playerHpDict.ContainsKey(targetObj.name))
            playerHpDict[targetObj.name] = maxHp;

        playerHpDict[targetObj.name] -= damage;

        if(playerHpDict[targetObj.name] <= 0)
        {
            playerHpDict[targetObj.name] = 0;
            Debug.Log($"{targetObj.name} 사망!");
        }

        // ✅ 자기 자신일 때만 UI 업데이트
        if (targetPV.IsMine)
        {
            FindObjectOfType<PlayerUI>()?.UpdateHP(playerHpDict[targetObj.name], maxHp);
        }
    }
}
