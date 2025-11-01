using UnityEngine;

public class PosterTrigger2D : MonoBehaviour
{
    public GameObject interactText; // النص "اضغط E"
    public RectTransform puzzlePanel; // Panel الخاص بهذا البوستر
    public GameObject blurPoster;      // البوستر المشوش
    public GameObject clearPoster;     // البوستر الواضح بعد النجاح

    private bool playerInRange = false;

    void Start()
    {
        if (interactText != null)
            interactText.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PuzzleCompletionManager manager = FindObjectOfType<PuzzleCompletionManager>();
            if (manager != null)
            {
                manager.SetCurrentPuzzle(puzzlePanel, blurPoster, clearPoster);
            }

            if (puzzlePanel != null)
                puzzlePanel.gameObject.SetActive(true);

            if (interactText != null)
                interactText.SetActive(false);

            Time.timeScale = 0f; // توقف اللعبة أثناء البازل
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactText != null)
                interactText.SetActive(false);
        }
    }
}
