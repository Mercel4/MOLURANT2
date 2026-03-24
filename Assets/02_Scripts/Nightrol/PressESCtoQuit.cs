using UnityEngine;

public class PressESCtoQuit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            // 빌드에서는 애플리케이션 종료
            Application.Quit();
            #endif
        }
    }
}
