// using UnityEngine;
// using UnityEngine.EventSystems;

// public class DragDropPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     private RectTransform rectTransform;
//     private Canvas canvas;
//     private CanvasGroup canvasGroup;
//     private Vector2 startPosition;

//     public string correctSlotName; 

//     void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvasGroup = GetComponent<CanvasGroup>();
//         canvas = GetComponentInParent<Canvas>();
//         startPosition = rectTransform.anchoredPosition;
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         canvasGroup.alpha = 0.6f;
//         canvasGroup.blocksRaycasts = false;
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         canvasGroup.alpha = 1f;
//         canvasGroup.blocksRaycasts = true;

//         GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;

//         if (dropTarget != null && dropTarget.name == correctSlotName)
//         {
//             rectTransform.SetParent(dropTarget.transform);
//             rectTransform.anchoredPosition = Vector2.zero;
//             this.enabled = false;
//         }
//         else
//         {
//             StartCoroutine(ShakeAndReturn());
//         }
//     }

//     System.Collections.IEnumerator ShakeAndReturn()
//     {
//         Vector2 originalPos = rectTransform.anchoredPosition;

//         for (int i = 0; i < 8; i++)
//         {
//             rectTransform.anchoredPosition = originalPos + new Vector2(Random.Range(-6f,6f), 0);
//             yield return new WaitForSeconds(0.02f);
//         }

//         rectTransform.anchoredPosition = startPosition;
//     }
// }
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string correctSlotName;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Canvas canvas;

    [Header("Settings")]
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
            Debug.LogWarning("THER IS NO SLOT NAMED " + correctSlotName);
            return;
        }

        RectTransform slotRect = correctSlot.GetComponent<RectTransform>();
        float distance = Vector2.Distance(rectTransform.anchoredPosition, slotRect.anchoredPosition);

        if (distance <= snapDistance)
        {
            rectTransform.anchoredPosition = slotRect.anchoredPosition;
            canvasGroup.blocksRaycasts = false; 
            Destroy(this); 
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
