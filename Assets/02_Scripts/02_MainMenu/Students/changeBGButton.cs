using UnityEngine;

public class changeBGButton : MonoBehaviour
{
    public GameObject AbidosBG;
    public GameObject MillenniumBG;
    public GameObject NPOHDBG;

    private void Awake()
    {
        AbidosBG.SetActive(true);
        MillenniumBG.SetActive(false);
        NPOHDBG.SetActive(false);
    }

    public void OnClickAbidosBGButton()
    {
        AbidosBG.SetActive(true);
        MillenniumBG.SetActive(false);
        NPOHDBG.SetActive(false);
    }

    public void OnClickMillenniumBGButton()
    {
        AbidosBG.SetActive(false);
        MillenniumBG.SetActive(true);
        NPOHDBG.SetActive(false);
    }

    public void OnClickNPOHDBGButton()
    {
        AbidosBG.SetActive(false);
        MillenniumBG.SetActive(false);
        NPOHDBG.SetActive(true);
    }
}
