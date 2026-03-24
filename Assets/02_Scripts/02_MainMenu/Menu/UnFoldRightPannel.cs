using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnFoldRightPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gameObj;           // 이동시킬 패널
    public Vector3 beforePos;            // 원래 위치
    public Vector3 afterPos;             // 호버 시 위치
    public float duration = 0.5f;        // 이동 시간

    private Coroutine currentCoroutine;

    // 마우스 들어왔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        MovePanel(afterPos);
    }

    // 마우스 나갔을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        MovePanel(beforePos);
    }

    // 코루틴 실행/중복 방지
    private void MovePanel(Vector3 targetPos)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(SmoothAnimateMove(gameObj.transform.localPosition, targetPos, duration));
    }

    // 실제 애니메이션 코루틴
    private IEnumerator SmoothAnimateMove(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            gameObj.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        gameObj.transform.localPosition = endPos;
        currentCoroutine = null;
    }
}
