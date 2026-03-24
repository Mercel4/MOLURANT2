using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class MatchMakingManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // Define the game version
    private bool isConnecting;

    public static bool isWaiting = false;
    private float elapsedTime = 0f;

    #region public fields
    [Tooltip("최대 플레이어수 설정. 만약에 최대 플레이어 수를 넘어가면 새로운 룸이 생성됩니다.")]
    [SerializeField] private byte maxPlayers = 2;
    #endregion

    [FoldoutGroup("Icon")] public Image OptimalIcon;
    [FoldoutGroup("Icon")] public Image KRIcon;
    [FoldoutGroup("Icon")] public Image JPIcon;
    [FoldoutGroup("Icon")] public Image HKIcon;
    [FoldoutGroup("Icon")] public Text infoText;
    public Text CountUpText;
    public GameObject CountUpTextObj;
    private bool isOptimal;

    private void Awake()
    {
        infoText.text = "";
        CountUpTextObj.SetActive(false);
        PhotonNetwork.Disconnect();
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "";
        PhotonNetwork.ConnectUsingSettings();

        OptimalIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        KRIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f);
        JPIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f);
        HKIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f);

        isWaiting = false;
        isOptimal = true;

        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnclickOptimal()
    {
        ChangeServer("");
    }

    public void OnclickKR()
    {
        ChangeServer("kr");
    }

    public void OnclickJP()
    {
        ChangeServer("jp");
    }

    public void OnclickHK()
    {
        ChangeServer("hk");
    }

    private void ChangeServer(string region)
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region;
        PhotonNetwork.ConnectUsingSettings();

        OptimalIcon.color = region == "" ? new Color(0f, 188f / 255f, 188f / 255f) : new Color(128f / 255f, 128f / 255f, 128f / 255f);
        KRIcon.color = region == "kr" ? new Color(0f, 188f / 255f, 188f / 255f) : new Color(128f / 255f, 128f / 255f, 128f / 255f);
        JPIcon.color = region == "jp" ? new Color(0f, 188f / 255f, 188f / 255f) : new Color(128f / 255f, 128f / 255f, 128f / 255f);
        HKIcon.color = region == "hk" ? new Color(0f, 188f / 255f, 188f / 255f) : new Color(128f / 255f, 128f / 255f, 128f / 255f);

        infoText.text = "서버 변경중입니다. 잠시만 기다려 주십시오...";
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master. Ready to Join Random Room.");
        Debug.Log(PhotonNetwork.CloudRegion);
        infoText.text = "서버에 성공적으로 연결되었습니다.";
    }

    public void OnclickPlay()
    {
        Debug.Log("StartMatchmaking called.");
        StartMatchMaking();

        isWaiting = true;
        StartCoroutine(WaitTimerCoroutine());
    }

    private void StartMatchMaking()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRandomFailed. Creating room. Reason: " + message);

        string roomName = "Room_" + UnityEngine.Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions
        {
            MaxPlayers = maxPlayers,
            IsVisible = true,
            IsOpen = true
        });
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name + "| Player Nick: " + PhotonNetwork.LocalPlayer.NickName + " | Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        TryStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player entered: " + newPlayer.NickName + " | Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        TryStartGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left: " + otherPlayer.NickName + " | Players: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room.");
        isWaiting = false;
    }

    private void TryStartGame()
    {
        if (PhotonNetwork.CurrentRoom == null) return;

        int currentPlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int max = PhotonNetwork.CurrentRoom.MaxPlayers;

        if (currentPlayerCount == max && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("게임을 찾았습니다");
            isWaiting = false;

            // 모든 클라이언트에 애니메이션 표시 RPC
            photonView.RPC("RPC_ShowMatchFound", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_ShowMatchFound()
    {
        if (MatchFoundANimation.Instance != null)
        {
            MatchFoundANimation.Instance.ShowAnimation();
        }
    }

    public void CancelMatchmaking()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        else
            PhotonNetwork.LeaveLobby();
    }

    private IEnumerator WaitTimerCoroutine()
    {
        while (isWaiting)
        {
            CountUpTextObj.SetActive(true);
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            CountUpText.text = $"{minutes:00} : {seconds:00}";
            yield return null;
        }
    }
}