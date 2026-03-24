using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class askGoBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text targetText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MatchMakingManager.isWaiting)
            targetText.text = "BACK : 뒤로 돌아갈 경우 매치메이킹 대기가 취소됩니다.";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetText.text = "BACK";
    }
}
