using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PressButtonLockIn : MonoBehaviourPunCallbacks
{
    public GameObject Button;
    public GameObject LockInPannel;
    public RectTransform UpperLine;
    public RectTransform LowerLine;
    public RectTransform LeftLine;
    public RectTransform RightLine;

    public Image BottomImage;
    public Color TargetColor;
    public Image EnemySelectImage;

    private bool isMoveUPandDown = false;
    public bool isLockIn = false;

    private void Start()
    {
        LockInPannel.SetActive(false);
        isLockIn = false;
        EnemySelectImage.color = Color.gray;
    }

    public void OnclickLockIn()
    {
        Button.SetActive(false);
        StartCoroutine(LineMoveSimultaneous());

        isLockIn = true;

        // 룸 프로퍼티에 내 잠금 상태 저장
        Hashtable props = new Hashtable() {
            { "Player" + PhotonNetwork.LocalPlayer.ActorNumber + "_LockIn", true },
            { "Player" + PhotonNetwork.LocalPlayer.ActorNumber + "_Character", STSelectManager.currentCharacter }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);


        // 다른 플레이어에게도 잠금 상태 알림
        photonView.RPC("OnOtherPlayerLockedIn", RpcTarget.Others);

    }

    [PunRPC]
    public void OnOtherPlayerLockedIn()
    {
        Debug.Log("Other player has locked in.");
        EnemySelectImage.color = Color.white;
    }

    private void Update()
    {
        if (isMoveUPandDown)
        {
            StartCoroutine(LineMoveAll());
            isMoveUPandDown = false;
        }

        if (STSelectManager.currentCharacter != "")
        {
            LockInPannel.SetActive(true);
        }
        else
        {
            LockInPannel.SetActive(false);
        }
    }

    private IEnumerator LineMoveSimultaneous()
    {
        float duration = 0.15f;

        Vector2 upOriginal = UpperLine.anchoredPosition;
        Vector2 upTarget = new Vector2(upOriginal.x, -220f);

        Vector2 lowOriginal = LowerLine.anchoredPosition;
        Vector2 lowTarget = new Vector2(lowOriginal.x, -220f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            UpperLine.anchoredPosition = Vector2.Lerp(upOriginal, upTarget, t);
            LowerLine.anchoredPosition = Vector2.Lerp(lowOriginal, lowTarget, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        UpperLine.anchoredPosition = upTarget;
        LowerLine.anchoredPosition = lowTarget;
        isMoveUPandDown = true;
    }

    private IEnumerator LineMoveAll()
    {
        float duration = 0.15f;

        Vector2 leftOriginal = LeftLine.anchoredPosition;
        Vector2 leftTarget = new Vector2(leftOriginal.x, -340f);

        Vector2 UpperOriginal = UpperLine.anchoredPosition;
        Vector2 UpperTarget = new Vector2(UpperOriginal.x, -340f);

        Vector2 lowerOriginal = LowerLine.anchoredPosition;
        Vector2 lowerTarget = new Vector2(lowerOriginal.x, -340f);

        Vector2 rightOriginal = RightLine.anchoredPosition;
        Vector2 rightTarget = new Vector2(rightOriginal.x, -340f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            LeftLine.anchoredPosition = Vector2.Lerp(leftOriginal, leftTarget, t);
            RightLine.anchoredPosition = Vector2.Lerp(rightOriginal, rightTarget, t);
            UpperLine.anchoredPosition = Vector2.Lerp(UpperOriginal, UpperTarget, t);
            LowerLine.anchoredPosition = Vector2.Lerp(lowerOriginal, lowerTarget, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        LeftLine.anchoredPosition = leftTarget;
        RightLine.anchoredPosition = rightTarget;
        UpperLine.anchoredPosition = UpperTarget;
        LowerLine.anchoredPosition = lowerTarget;

        yield return StartCoroutine(FadeColor());
    }

    private IEnumerator FadeColor()
    {
        float duration = 0.25f;
        float elapsed = 0f;

        Color originalColor = BottomImage.color;
        Color targetColor = TargetColor;

        float alpha = originalColor.a;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Color newColor = Color.Lerp(originalColor, targetColor, t);
            newColor.a = alpha;
            BottomImage.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Color finalColor = targetColor;
        finalColor.a = alpha;
        BottomImage.color = finalColor;

        yield return new WaitForSeconds(0.25f);

        elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Color newColor = Color.Lerp(targetColor, originalColor, t);
            newColor.a = alpha;
            BottomImage.color = newColor;

            elapsed += Time.deltaTime;
            yield return null;
        }

        Color endColor = originalColor;
        endColor.a = alpha;
        BottomImage.color = endColor;
    }

    // 룸 프로퍼티 업데이트 감지
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);

        bool allLocked = true;

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object isLocked;
            if (!PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("Player" + player.ActorNumber + "_LockIn", out isLocked))
            {
                allLocked = false;
                break;
            }

            if (!(bool)isLocked)
            {
                allLocked = false;
                break;
            }
        }

        if (allLocked)
        {
            Debug.Log("모든 플레이어가 선택을 확정했습니다!");
            StartGame();
        }
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
