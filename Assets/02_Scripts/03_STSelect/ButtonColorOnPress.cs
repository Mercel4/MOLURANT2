using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColorOnPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Text targetText;
    public GameObject targetOBJ;
    public Color normalColor = Color.white;
    public Color pressedColor = Color.gray;

    public void OnPointerDown(PointerEventData eventData)
    {
        targetText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetText.color = normalColor;
    }

    public void OnclickButton()
    {
        targetOBJ.SetActive(false);
        targetText.color = pressedColor;
    }
}
