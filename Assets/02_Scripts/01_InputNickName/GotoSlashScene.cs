using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoSlashScene : MonoBehaviour
{
    public void GotoScene()
    {
        SceneManager.LoadScene("!01_SplashScene");
    }
}