// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;
// using TMPro;

// public class WordPuzzle : MonoBehaviour
// {
//     public List<Button> letterButtons;
//     private string targetPhrase = "PICK UP YOUR GLASSES";
//     private string currentInput = "";

//     public TextMeshProUGUI displayText; 
//     public GameObject rewardObject; 

//     void Start()
//     {
//         foreach (Button btn in letterButtons)
//         {
//             btn.onClick.AddListener(() => OnLetterClick(btn));
//         }

//         if (rewardObject != null)
//             rewardObject.SetActive(false);
//     }

//     void OnLetterClick(Button btn)
//     {
// string letter = btn.GetComponentInChildren<TextMeshProUGUI>().text;
//         currentInput += letter;

//         displayText.text = currentInput;

//         if (currentInput.Length == targetPhrase.Length)
//         {
//             if (currentInput.ToUpper() == targetPhrase)
//             {
//                 displayText.text = "✅ Correct!";
//                 if (rewardObject != null)
//                     rewardObject.SetActive(true); 
//             }
//             else
//             {
//                 displayText.text = "❌ Try again!";
//                 currentInput = "";
//             }
//         }
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WordPuzzle : MonoBehaviour
{
    public List<Button> letterButtons; 
    private string targetPhrase = "PICK UP YOUR GLASSES";
    private string currentInput = "";
    private int nextLetterIndex = 0;

    public TextMeshProUGUI displayText; 
    public GameObject rewardObject; 
    [Header("Wrong feedback (tweakable)")]
    public float flashDuration = 0.35f;
    public float shakeAmount = 8f;
    public int shakeVibrato = 10;

    void Start()
    {
        foreach (Button btn in letterButtons)
        {
            btn.onClick.AddListener(() => OnLetterClick(btn));
        }

        if (rewardObject != null)
            rewardObject.SetActive(false);

        UpdateDisplay();
    }

    void OnLetterClick(Button btn)
    {
        string letter = GetButtonLetter(btn).ToUpper();
        if (string.IsNullOrEmpty(letter)) return;

        while (nextLetterIndex < targetPhrase.Length && targetPhrase[nextLetterIndex] == ' ')
        {
            currentInput += " ";
            nextLetterIndex++;
        }

        if (nextLetterIndex >= targetPhrase.Length)
            return;

        char expectedLetter = char.ToUpper(targetPhrase[nextLetterIndex]);

        if (letter[0] == expectedLetter)
        {
            currentInput += targetPhrase[nextLetterIndex];
            nextLetterIndex++;
            btn.interactable = false;
            UpdateDisplay();

            while (nextLetterIndex < targetPhrase.Length && targetPhrase[nextLetterIndex] == ' ')
            {
                currentInput += " ";
                nextLetterIndex++;
            }

            if (nextLetterIndex >= targetPhrase.Length)
            {
                if (displayText != null) displayText.text = "✅ Correct!";
                if (rewardObject != null) rewardObject.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(FlashAndShakeButton(btn));
        }
    }

    string GetButtonLetter(Button btn)
    {
        // يدعم TextMeshProUGUI
        TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null) return tmp.text.Trim();

        Text legacy = btn.GetComponentInChildren<Text>();
        if (legacy != null) return legacy.text.Trim();

        Debug.LogWarning("Button has no TextMeshProUGUI or Text child: " + btn.name);
        return "";
    }

    void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = currentInput;
    }

    IEnumerator FlashAndShakeButton(Button btn)
    {
        Image img = btn.GetComponent<Image>();
        RectTransform rt = btn.GetComponent<RectTransform>();

        if (rt == null)
            yield break;

        Vector3 originalPos = rt.localPosition;
        Color originalColor = img != null ? img.color : Color.white;

        float elapsed = 0f;
        int vibrato = Mathf.Max(1, shakeVibrato);

        while (elapsed < flashDuration)
        {
            float t = elapsed / flashDuration;
            float damper = 1f - t; 
            Vector2 shake = Random.insideUnitCircle * (shakeAmount * damper / 100f * 100f);
            rt.localPosition = originalPos + new Vector3(shake.x, shake.y, 0f);

            if (img != null)
            {
                img.color = Color.Lerp(originalColor, new Color(1f, 0.5f, 0.5f), 0.9f);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.localPosition = originalPos;
        if (img != null) img.color = originalColor;

        yield break;
    }
}
