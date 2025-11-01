using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctSlotName;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Canvas canvas;
    public float snapDistance = 80f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        GameObject correctSlot = GameObject.Find(correctSlotName);
        if (correctSlot == null)
        {
            rectTransform.anchoredPosition = originalPosition;
            return;
        }

        RectTransform slotRect = correctSlot.GetComponent<RectTransform>();
        float distance = Vector2.Distance(rectTransform.anchoredPosition, slotRect.anchoredPosition);

        if (distance <= snapDistance)
        {
            rectTransform.anchoredPosition = slotRect.anchoredPosition;
            canvasGroup.blocksRaycasts = false;
            Destroy(this);

            FindObjectOfType<PuzzleCompletionManager>()?.CheckPuzzleCompletion();
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
