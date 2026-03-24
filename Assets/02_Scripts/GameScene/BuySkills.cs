using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class BuySkills : MonoBehaviour
{
    public Image skillImageC;
    public Image skillImageQ;

    public static bool isBuydC = false;
    public static bool isBuydQ = false;

    private void Start()
    {
        skillImageC.color = new Color(178f/255f, 178f/255f, 178f/255f, 1f);
        skillImageQ.color = new Color(178f/255f, 178f/255f, 178f/255f, 1f);
    }

    public void OnClickBuySkillC()
    {
        if (isBuydC) return;
        skillImageC.color = Color.white;
        isBuydC = true;
    }

    public void OnClickBuySkillQ()
    {
        if (isBuydQ) return;
        skillImageQ.color = Color.white;
        isBuydQ = true;
    }
}
