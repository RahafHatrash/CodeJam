using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip CameraSound;
    public AudioClip SwirlSound;
    public string nextSceneName;

    // Called from an animation event
    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // Example animation event functions
    public void PlayCameraSound()
    {
        if (audioSource && CameraSound)
            audioSource.PlayOneShot(CameraSound);
    }

    public void PlaySwirlSound()
    {
        if (audioSource && SwirlSound)
            audioSource.PlayOneShot(SwirlSound);
    }
}
