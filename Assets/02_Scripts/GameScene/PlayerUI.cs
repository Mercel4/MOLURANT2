using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text hpText;  // 인스펙터에서 연결

    public void UpdateHP(int current, int max)
    {
        hpText.text = $"{current}";
    }
}
