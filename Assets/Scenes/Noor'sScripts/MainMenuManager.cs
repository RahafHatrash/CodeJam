using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Hala'sScene";     
    public string creditsSceneName = "Credits";    

    
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    
    public void OpenCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();      
    }
}
