using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CardUI cardUI;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GraphicRaycaster graphicRaycaster;

    private Vector2 originalAnchoredPosition;
    private bool isDragging = false;
    private CardSlot hoveredSlot;
    private int originalSortingOrder;

    private void Awake()
    {
        cardUI = GetComponent<CardUI>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"BEGIN DRAG: {cardUI.GetCardData().cardName}");

        isDragging = true;
        originalAnchoredPosition = rectTransform.anchoredPosition;

        // Store original sorting order and set to high value to appear on top
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            originalSortingOrder = canvas.sortingOrder;
            canvas.sortingOrder = 100; // High value to appear on top
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.7f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        rectTransform.anchoredPosition += eventData.delta;
        CheckNearestSlot();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("END DRAG");
        isDragging = false;

        // Reset sorting order
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = originalSortingOrder;
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }

        if (hoveredSlot != null)
        {
            PlaceCardInSlot();
        }
        else
        {
            ResetPosition();
        }

        hoveredSlot = null;
    }

    private void CheckNearestSlot()
    {
        // Get all card slots from the scene
        CardSlot[] allSlots = FindObjectsByType<CardSlot>(FindObjectsSortMode.None);

        CardSlot closestSlot = null;
        float closestDistance = float.MaxValue;
        float detectionRange = 150f;

        foreach (CardSlot slot in allSlots)
        {
            float distance = Vector3.Distance(rectTransform.position, slot.GetComponent<RectTransform>().position);

            if (distance < detectionRange && distance < closestDistance)
            {
                closestDistance = distance;
                closestSlot = slot;
            }
        }

        // Update hover state
        if (closestSlot != hoveredSlot)
        {
            if (hoveredSlot != null)
            {
                hoveredSlot.OnHoverExit();
            }

            hoveredSlot = closestSlot;

            if (hoveredSlot != null)
            {
                hoveredSlot.OnHoverEnter();
            }
        }
    }

    private void PlaceCardInSlot()
    {
        if (hoveredSlot == null) return;

        Debug.Log($"PLACING: {cardUI.GetCardData().cardName} in slot {hoveredSlot.GetSlotIndex()}");

        hoveredSlot.SetCard(cardUI.GetCardData());

        // Disable the card UI in the inventory so it's no longer visible
        gameObject.SetActive(false);

        DeckManager.Instance.OnCardPlaced(cardUI.GetCardId(), hoveredSlot.GetSlotIndex());
    }

    private void ResetPosition()
    {
        rectTransform.anchoredPosition = originalAnchoredPosition;
    }
}