using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Player Settings")]
    public string playerPrefabName = "Player"; // Resources/Player.prefab 이름
    public Transform spawnParent;              // 스폰된 플레이어 부모 오브젝트 (선택)

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // 씬 자동 동기화
        Debug.Log("🌐 PhotonNetwork 자동 씬 동기화 설정 완료");
    }

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("이미 룸에 입장해 있음. 바로 플레이어 스폰");
            SpawnPlayer();
        }
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("✅ OnJoinedRoom - 방 입장 완료! 플레이어 스폰 시작");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogError("❌ PhotonNetwork 준비 안 됨. 스폰 불가");
            return;
        }

        // 랜덤 스폰 위치
        Vector3 spawnPos = new Vector3(Random.Range(-2f, 2f), 1f, Random.Range(-2f, 2f));

        // 네트워크 상에서 내 플레이어 생성
        GameObject player = PhotonNetwork.Instantiate(playerPrefabName, spawnPos, Quaternion.identity);

        // 내 플레이어 이름 변경 (예: Player_1, Player_2 ...)
        int playerIndex = PhotonNetwork.CurrentRoom.PlayerCount; // 방에 몇 명 있는지로 번호 결정
        player.name = $"{playerPrefabName}_{playerIndex}";

        if (spawnParent != null)
            player.transform.SetParent(spawnParent);

        Debug.Log($"🎯 내 플레이어 스폰 완료: {player.name} / IsMine: {player.GetComponent<PhotonView>().IsMine}");
    }
}
