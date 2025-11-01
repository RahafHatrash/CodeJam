using UnityEngine;

public class AnimationAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip CameraSound;
    public AudioClip SwirlSound;

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
