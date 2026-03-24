using UnityEngine;
using UnityEngine.Video;
using System.Collections;

[RequireComponent(typeof(VideoPlayer))]
public class VideoLoader : MonoBehaviour
{
    public VideoPlayer video;
    public string fileName;
    private AudioSource audioSource;

    void Start()
    {
        video = GetComponent<VideoPlayer>();

        if (!TryGetComponent(out audioSource))
            audioSource = gameObject.AddComponent<AudioSource>();

        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        video.SetTargetAudioSource(0, audioSource);

        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        video.Prepare();
        video.prepareCompleted += (vp) => vp.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(FadeOutAudio(2f));
        }
    }

    

    public IEnumerator FadeOutAudio(float duration)
    {
        if (audioSource != null)
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = 0f;
        }
    }
}
