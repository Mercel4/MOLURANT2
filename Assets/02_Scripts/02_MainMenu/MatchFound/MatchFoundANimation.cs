using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Pun;

public class MatchFoundANimation : MonoBehaviourPun
{
    public static MatchFoundANimation Instance;

    public GameObject MainPannel;
    public AudioSource matchFoundSound;

    public GameObject targetObject;
    public Text targetText;

    public GameObject CountdownObject;
    public Text CountdownText;

    private bool hasPlayed = false; // 한 번만 실행용

    private void Awake()
    {
        Instance = this;
        MainPannel.SetActive(false);
    }

    private void Update()
    {
        // space bar로 테스트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowAnimation();
        }
    }

    public void ShowAnimation()
    {
        if (hasPlayed) return;
        hasPlayed = true;

        MainPannel.SetActive(true);
        matchFoundSound.Play();
        StartCoroutine(MoveScaleandAlpha());
    }

    private IEnumerator MoveScaleandAlpha()
    {
        Vector3 originalScale = new Vector3(0.75f, 0.75f, 0.75f);
        Vector3 targetScale = new Vector3(1f, 1f, 1f);

        Vector3 CountdownOriginalScale = new Vector3(0.35f, 0.35f, 0.35f);
        Vector3 CountdownTargetScale = new Vector3(0.6f, 0.6f, 0.6f);

        Color originalColor = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 0f);
        Color targetColor = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 1f);

        Color CountdownOriginalColor = new Color(CountdownText.color.r, CountdownText.color.g, CountdownText.color.b, 0f);
        Color CountdownTargetColor = new Color(CountdownText.color.r, CountdownText.color.g, CountdownText.color.b, 1f);

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            targetObject.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            CountdownObject.transform.localScale = Vector3.Lerp(CountdownOriginalScale, CountdownTargetScale, t);
            targetText.color = Color.Lerp(originalColor, targetColor, t);
            CountdownText.color = Color.Lerp(CountdownOriginalColor, CountdownTargetColor, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        targetObject.transform.localScale = targetScale;
        targetText.color = targetColor;
        CountdownObject.transform.localScale = CountdownTargetScale;
        CountdownText.color = CountdownTargetColor;

        yield return StartCoroutine(CountDown3Seconds());
    }

    private IEnumerator CountDown3Seconds()
    {
        int count = 3;
        while (count > 0)
        {
            CountdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        CountdownText.text = "로딩중입니다";
        CountdownText.fontSize = 70;
        yield return new WaitForSeconds(1f);

        PhotonNetwork.LoadLevel("03_STSelect");
    }
}