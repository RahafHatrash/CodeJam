using UnityEngine;
using TMPro;
using System.Collections;

public class AutoTextSwitcher : MonoBehaviour
{
    public TextMeshProUGUI[] texts; // Assign your TMP text objects
    public float switchDelay = 3f;  // Time between text switches
    public CanvasGroup canvasGroup; // Assign your Canvas or Panel here

    private int currentIndex = 0;

    void Start()
    {
        // Hide all texts except the first
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].gameObject.SetActive(i == 0);
        }

        // Start switching
        StartCoroutine(SwitchText());
    }

    IEnumerator SwitchText()
    {
        // Go through each text
        while (currentIndex < texts.Length - 1)
        {
            yield return new WaitForSeconds(switchDelay);

            texts[currentIndex].gameObject.SetActive(false);
            currentIndex++;
            texts[currentIndex].gameObject.SetActive(true);
        }

        // Wait one more delay after the last text
        yield return new WaitForSeconds(switchDelay);

        // Hide the entire canvas (fade out optional)
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;      // Instantly hide
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            gameObject.SetActive(false); // Disable the object this script is on
        }
    }
}

