using UnityEngine;

public class PuzzleCompletionManager : MonoBehaviour
{
    [Header("Current Puzzle References")]
    public RectTransform currentPuzzlePanel;   // Panel للبوستر الحالي
    public GameObject currentBlurPoster;       // البوستر المشوش الحالي
    public GameObject currentClearPoster;      // البوستر الواضح بعد النجاح
    public GameObject wellDonePanel;           // لوحة "Well Done"

    // 🔹 يتم استدعاؤه لما البازل يفتح، نحدد أي Panel وبوستر حالي
    public void SetCurrentPuzzle(RectTransform panel, GameObject blur, GameObject clear)
    {
        currentPuzzlePanel = panel;
        currentBlurPoster = blur;
        currentClearPoster = clear;
    }

    public void CheckPuzzleCompletion()
    {
        if (currentPuzzlePanel == null) return;

        bool allPlaced = true;
        DragDropPiece[] pieces = currentPuzzlePanel.GetComponentsInChildren<DragDropPiece>();

        foreach (DragDropPiece piece in pieces)
        {
            if (piece != null)
            {
                allPlaced = false;
                break;
            }
        }

        if (allPlaced)
            ShowWellDone();
    }

    void ShowWellDone()
    {
        if (wellDonePanel != null)
            wellDonePanel.SetActive(true);
    }

    // 🔹 يتم استدعاؤه عند الضغط على زر "Done"
    public void OnDoneButton()
    {
        if (wellDonePanel != null)
            wellDonePanel.SetActive(false);

        if (currentBlurPoster != null)
            currentBlurPoster.SetActive(false);

        if (currentClearPoster != null)
            currentClearPoster.SetActive(true);

        // نظف المراجع بعد إغلاق البازل
        currentPuzzlePanel = null;
        currentBlurPoster = null;
        currentClearPoster = null;
    }
}
