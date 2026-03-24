using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GetPing : MonoBehaviourPunCallbacks
{
    public Text pingText;

    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            int ping = PhotonNetwork.GetPing();
            pingText.text = "현재 핑: " + ping + " ms";
        }
        else
        {
            pingText.text = "서버가 연결되지 않았습니다.";
        }
    }
}
