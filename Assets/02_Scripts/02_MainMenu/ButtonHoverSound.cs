using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource hoverSound; // 호버 사운드를 재생할 AudioSource

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            hoverSound.Play();
        }
    }
}
