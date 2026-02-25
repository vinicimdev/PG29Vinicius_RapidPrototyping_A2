using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI copyCountText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    
    private Card cardData;
    private int cardId;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    
    public void Initialize(Card card, int id)
    {
        cardData = card;
        cardId = id;
        
        UpdateDisplay();
    }
    
    public void UpdateDisplay()
    {
        if (cardData == null) return;
        
        if (cardNameText != null)
            cardNameText.text = cardData.cardName;
        
        if (costText != null)
            costText.text = cardData.cost.ToString();
        
        if (descriptionText != null)
            descriptionText.text = cardData.description;
        
        if (cardImage != null && cardData.cardImage != null)
            cardImage.sprite = cardData.cardImage;
        
        UpdateCopyCount();
    }
    
    public void UpdateCopyCount()
    {
        // Not needed anymore - just hide the copy count text
        if (copyCountText != null)
        {
            copyCountText.gameObject.SetActive(false);
        }
    }
    
    public Card GetCardData()
    {
        return cardData;
    }
    
    public int GetCardId()
    {
        return cardId;
    }
    
    public void SetDragging(bool isDragging)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isDragging ? 0.6f : 1f;
        }
    }
    
    public void ResetPosition()
    {
        if (GetComponent<RectTransform>() != null)
        {
            GetComponent<RectTransform>().anchoredPosition = (Vector2)originalPosition;
        }
    }
    
    public void StoreOriginalPosition()
    {
        originalPosition = transform.position;
    }
}
