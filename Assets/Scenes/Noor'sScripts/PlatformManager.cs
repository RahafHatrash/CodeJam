using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Tags")]
    public string BlueTag = "Blue";
    public string RedTag = "Red";

public AudioClip switchSound;
private AudioSource audioSource;
    
    private GameObject[] BluePlatforms;
    private GameObject[] RedPlatforms;
    private bool isBlueActive = true;

    void Start()
    {
        // Find all platforms with the assigned tags
        BluePlatforms = GameObject.FindGameObjectsWithTag(BlueTag);
        RedPlatforms = GameObject.FindGameObjectsWithTag(RedTag);
        audioSource = gameObject.AddComponent<AudioSource>();
        UpdatePlatforms();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isBlueActive = !isBlueActive;
            UpdatePlatforms();
            audioSource.PlayOneShot(switchSound);
        }
    }

    void UpdatePlatforms()
    {
        foreach (GameObject platform in BluePlatforms)
            platform.SetActive(isBlueActive);

        foreach (GameObject platform in RedPlatforms)
            platform.SetActive(!isBlueActive);
    }
}