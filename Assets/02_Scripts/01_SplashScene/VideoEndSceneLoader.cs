using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // 비디오 플레이어 컴포넌트
    public string MainMenuScene;         // 이동할 씬 이름

    private GameData gameData;

    void Start()
    {
        gameData = SaveLoadSystem.LoadGameData();

        videoPlayer.loopPointReached += OnVideoEnd;  // 영상이 끝나면 호출
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (gameData.playerData.playerName == null || gameData.playerData.playerName == "")
        {
            SceneManager.LoadScene("01_InputNickname");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            SceneManager.LoadScene(MainMenuScene);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
