using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StudyLauncher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // Define the game version
    private bool isConnecting;

    #region public fields
    [Tooltip("최대 플레이어수 설정. 만약에 최대 플레이어 수를 넘어가면 새로운 룸이 생성됩니다.")]
    [SerializeField] private byte maxPlayers = 4;

    [Tooltip("사용자가 이름을 입력하고 연결하고 플레이할 수 있는 Ui 패널")]
    [SerializeField] public GameObject controlPanel;
    [Tooltip("연결이 진행 중임을 사용자에게 알리는 UI 레이블")]
    [SerializeField] public GameObject progressLabel;
    #endregion

    private void Awake()
    {
        // true로 할 경우, 모든 플레이어가 동시에 씬 로딩
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        // 포톤 서버에 접속
        // Connect();

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        isConnecting = true;

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // 연결되여 있으면 참여, 아니면 서버 재접속 시도
        if (PhotonNetwork.IsConnected)
        {
            // 랜덤 룸에 참여를 시도해야 함. 실패하면 OnJoinRandomFailed 콜백후 랜덤 룸 생성
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            // 프로젝트 설정에 맞춰 포톤 서버에 접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
            PhotonNetwork.JoinRandomRoom();

        Debug.Log("OnConnectedToMaster() called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnected() called by PUN. Cause: {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() called by Pun. Random방이 없으므로 방을 생성합니다.");

        // 빈 방이 없으므로 새로운 방을 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            MaxPlayers = maxPlayers
        });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() call by Pun. 방 참여 성공");
        // 모든 플레이어가 로딩할 씬을 동기화    

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("첫 번째 플레이어가 방에 참여했습니다. 씬 로딩을 시작합니다.");
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
    #endregion
}