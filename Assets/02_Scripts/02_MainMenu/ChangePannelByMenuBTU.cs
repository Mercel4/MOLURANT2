using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangePannelByMenuBTU : MonoBehaviour
{
    public GameObject MenuPannel;
    public GameObject LobbyPannel;
    public GameObject StudentsPannel;
    [SerializeField] public VideoLoader videoLoader;

    public AudioSource StudentsPannelAudio;


    private void Awake()
    {
        MenuPannel.SetActive(true);
        LobbyPannel.SetActive(false);
        StudentsPannel.SetActive(false);
    }

    public void OnClickPlayerButon()
    {
        LobbyPannel.SetActive(true);
        StudentsPannel.SetActive(false);
    }

    public void OnClickMenuButton()
    {
        MenuPannel.SetActive(true);
        StudentsPannel.SetActive(false);
    }

    public void OnClickStudentsButton()
    {
        MenuPannel.SetActive(false);
        StudentsPannel.SetActive(true);

        videoLoader.StartCoroutine(videoLoader.FadeOutAudio(6f));
        StartCoroutine(FadeInAudio(6f, "StudentsPannel"));
    }

    public void OnclickBackButton()
    {
        MenuPannel.SetActive(true);
        StudentsPannel.SetActive(false);

        SceneManager.LoadScene("02_MainMenu");
    }

    public IEnumerator FadeInAudio(float duration, string pannelName)
    {   
        if (pannelName == "StudentsPannel" && StudentsPannelAudio != null)
        {
            StudentsPannelAudio.Play();

            float targetVolume = 0.3f;
            float elapsed = 0f;
            StudentsPannelAudio.volume = 0f;

            while (elapsed < duration)
            {
                StudentsPannelAudio.volume = Mathf.Lerp(0f, targetVolume, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            StudentsPannelAudio.volume = targetVolume;
        }
    }
}
