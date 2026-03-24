using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class CountDown60s : MonoBehaviourPunCallbacks
{
    public Text countdownText;
    public bool isDodge;

    private void Start()
    {
        StartCoroutine(Countdown());
        isDodge = false;
    }

    private IEnumerator Countdown()
    {
        float remainingTime = 70f;

        while (remainingTime > 0)
        {
            countdownText.text = Mathf.Ceil(remainingTime).ToString(); // 프레임마다 줄어든 값 반올림 해서 보기 좋게
            remainingTime -= Time.deltaTime; // 프레임마다 시간 감소

            if (remainingTime <= 10)
            {
                countdownText.color = new Color(1f, 70f / 255f, 85f / 255f);
            }

            if (remainingTime <= 0)
            {
                countdownText.text = "0";

                // 4초 기다리기
                yield return new WaitForSeconds(6f);

                isDodge = true;
                photonView.RPC("OnDodge", RpcTarget.Others);

                // 그리고 나서 로드
                SceneManager.LoadScene("02_MainMenu");
            }
            yield return null;
        }
    }

    [PunRPC]
    private void OnDodge()
    {
        SceneManager.LoadScene("02_MainMenu");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"플레이어 {otherPlayer.NickName} 가 나감 (닷지)");
        SceneManager.LoadScene("02_MainMenu");
    }
}
