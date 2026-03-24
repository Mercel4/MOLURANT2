using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenuButtonManager : MonoBehaviour
{
    public GameObject settingMenuPanel;

    private void Awake()
    {
        settingMenuPanel.SetActive(false);
    }

    public void OpenSettingMenu()
    {
        settingMenuPanel.SetActive(true);
    }

    public void OnClickBackButton()
    {
        settingMenuPanel.SetActive(false);
    }

    public void OnClickInformationButton()
    {
        Application.OpenURL("https://docs.google.com/document/d/1c1Yk2b1Y7b0YJ3j1F6gG8H9K2L3M4N5O6P7Q8R9S0T1U/edit?usp=sharing");
    }

    public void OnClickSettingButton()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnClickQuitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
