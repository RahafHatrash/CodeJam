using UnityEngine;
using UnityEngine.UI;

public class TestButtonHandler : MonoBehaviour
{
    public GameObject puzzlePanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (puzzlePanel != null)
                puzzlePanel.SetActive(true);
        });
    }
}
