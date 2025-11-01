using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceHandler : MonoBehaviour
{
    [Header("Scene Settings")]
    
    public string nextSceneName = "NextScene";
    public string BlueEnding = "BlueEnding";
    public string RedEnding = "RedEnding";




    [Header("Tags for Collision Choices")]
    
    public string BlueEndingTag = "BlueEnding";
    public string RedEndingTag = "RedEnding";







    // ---  BUTTON FUNCTIONS ---

    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name not set in ChoiceHandler!");
        }
    }

    
    public void QuitGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }







    // --- COLLISION HANDLING ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        //  Blue Ending collider
        if (other.CompareTag(BlueEndingTag))
        {
            Debug.Log("Collided with Blue Ending, loading scene");
            if (!string.IsNullOrEmpty(BlueEnding))
            {
                SceneManager.LoadScene(BlueEnding);
            }
        }

        //  Red Ending collider
        else if (other.CompareTag(RedEndingTag))
        {
            Debug.Log("Collided with Red Ending, loading scene");
            if (!string.IsNullOrEmpty(RedEnding))
            {
                SceneManager.LoadScene(RedEnding);
            }
        }
    }
}
