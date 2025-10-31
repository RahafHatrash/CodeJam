using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragDropWord : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    public string correctSlotName; // اسم المكان الصحيح

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

        // لو ترك الكلمة على مكان فاضي أو غلط
        if (dropTarget == null || dropTarget.name != correctSlotName)
        {
            StartCoroutine(ShakeAndReturn()); // تهتز وترجع
            return;
        }

        // ✅ صح
        RectTransform slotRect = dropTarget.GetComponent<RectTransform>();
        rectTransform.SetParent(slotRect); // تخلي الكلمة داخل المربع
        rectTransform.anchoredPosition = Vector2.zero; // تتوسط المربع تمامًا
        this.enabled = false; // ما تقدر تتحرك بعدين

        // ما نغير لون المربع (نقدر نضيف مؤثر ثاني لو حبيتي بعدين)
    }

    System.Collections.IEnumerator ShakeAndReturn()
    {
        Vector2 originalPos = rectTransform.anchoredPosition;

        // تهتز بسيط
        for (int i = 0; i < 8; i++)
        {
            rectTransform.anchoredPosition = originalPos + new Vector2(Random.Range(-6f, 6f), 0);
            yield return new WaitForSeconds(0.02f);
        }

        // ترجع لمكانها الأصلي
        rectTransform.anchoredPosition = startPosition;
    }
}
