using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI slotIndexText;
    [SerializeField] private CanvasGroup slotCanvasGroup;
    
    private Card assignedCard;
    private int slotIndex;
    private bool isOccupied = false;
    
    // Visual feedback
    private Color originalColor;
    private Color hoverColor = new Color(1f, 1f, 1f, 1f);
    private Color emptyColor = new Color(0.7f, 0.7f, 0.7f, 1f);
    
    private void Awake()
    {
        if (slotCanvasGroup == null)
            slotCanvasGroup = GetComponent<CanvasGroup>();
        
        if (slotImage != null)
            originalColor = slotImage.color;
    }
    
    public void Initialize(int index)
    {
        slotIndex = index;
        if (slotIndexText != null)
            slotIndexText.text = (index + 1).ToString();
        
        SetEmpty();
    }
    
    public void SetCard(Card card)
    {
        assignedCard = card;
        isOccupied = true;
        
        if (slotImage != null && card.cardImage != null)
        {
            slotImage.sprite = card.cardImage;
            slotImage.color = Color.white;
        }
        
        if (slotIndexText != null)
            slotIndexText.text = card.cardName;
    }
    
    public void SetEmpty()
    {
        assignedCard = null;
        isOccupied = false;
        
        if (slotImage != null)
        {
            slotImage.sprite = null;
            slotImage.color = emptyColor;
        }
        
        if (slotIndexText != null)
            slotIndexText.text = (slotIndex + 1).ToString();
    }
    
    public Card GetCard()
    {
        return assignedCard;
    }
    
    public int GetSlotIndex()
    {
        return slotIndex;
    }
    
    public bool IsOccupied()
    {
        return isOccupied;
    }
    
    public void OnHoverEnter()
    {
        if (slotImage != null)
            slotImage.color = hoverColor;
    }
    
    public void OnHoverExit()
    {
        if (slotImage != null)
            slotImage.color = isOccupied ? Color.white : emptyColor;
    }
}
