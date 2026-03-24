using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RegionSelector : MonoBehaviourPunCallbacks
{
    public Image OptimalIcon;
    public Image KRIcon;
    public Image JPIcon;
    public Image HKIcon;
    public Image SGIcon;


    // private void Awake()
    // {
    //     OptimalIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
    //     // Optionally, connect to a default region
    //     // PhotonNetwork.ConnectUsingSettings();
    //     PhotonNetwork.ConnectToBestCloudServer();
    // }

    private void Awake()
    {
        OptimalIcon.color = new Color(0.0f, 188f / 255f, 188f / 255f);
        
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to best region...");
            PhotonNetwork.ConnectToBestCloudServer();
        }
    }


    public void ConnectToRegion(string region)
    {
        Debug.Log($"Trying to connect to region: {region}");
        PhotonNetwork.Disconnect();
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region;
        PhotonNetwork.ConnectUsingSettings();
    }

    public static bool IsRegionConnected = false;

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected to region: {PhotonNetwork.CloudRegion}");
        Debug.Log("Ping: " + PhotonNetwork.GetPing());
        IsRegionConnected = true; // ✅ 연결 성공 표시
    }


    public void OnClickOptimal()
    {
        Debug.Log("Connecting to best region automatically...");
        PhotonNetwork.Disconnect();

        PhotonNetwork.ConnectToBestCloudServer(); // ✅ 자동으로 최적 리전 연결

        OptimalIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        // 나머지 #808080
        KRIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        JPIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        HKIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        SGIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
    }

    public void OnClickKR()
    {
        ConnectToRegion("kr");
        KRIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        // 나머지 #808080
        OptimalIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        JPIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        HKIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        SGIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
    }

    public void OnClickJP()
    {
        ConnectToRegion("jp");
        JPIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        // 나머지 #808080
        OptimalIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        KRIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        HKIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        SGIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
    }

    public void OnClickHK()
    {
        ConnectToRegion("hk");
        HKIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        // 나머지 #808080
        OptimalIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        KRIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        JPIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        SGIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
    }

    public void OnClickSG()
    {
        ConnectToRegion("sg");
        SGIcon.color = new Color(0.0f, 188.0f / 255.0f, 188.0f / 255.0f); // 00bcbc
        // 나머지 #808080
        OptimalIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f / 255.0f); // 808080
        KRIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f); // 808080
        JPIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f); // 808080
        HKIcon.color = new Color(128.0f / 255.0f, 128.0f / 255.0f, 128.0f); // 808080
    }
}
