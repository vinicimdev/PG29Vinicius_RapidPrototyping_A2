using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }
    
    [SerializeField] private Transform cardSlotsContainer;
    [SerializeField] private Transform cardInventoryContainer;
    [SerializeField] private CardSlot[] cardSlots = new CardSlot[5];
    [SerializeField] private CardUI cardInventoryPrefab;
    [SerializeField] private TextMeshProUGUI deckInfoText;
    
    private List<CardUI> inventoryCardUIs = new List<CardUI>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }
    
    private void Start()
    {
        InitializeCardSlots();
        InitializeCardInventory();
        UpdateDeckInfo();
    }
    
    private void InitializeCardSlots()
    {
        // Find all card slots in the scene
        CardSlot[] foundSlots = cardSlotsContainer.GetComponentsInChildren<CardSlot>();
        
        // Resize the cardSlots array to match found slots
        cardSlots = new CardSlot[foundSlots.Length];
        
        for (int i = 0; i < foundSlots.Length; i++)
        {
            cardSlots[i] = foundSlots[i];
            cardSlots[i].Initialize(i);
        }
        
        Debug.Log($"Initialized {foundSlots.Length} card slots");
    }
    
    private void InitializeCardInventory()
    {
        // Get all 45 cards from inventory
        Card[] allCards = CardInventory.Instance.GetAllCards();
        
        foreach (Card card in allCards)
        {
            // Create a UI element for each card
            CardUI cardUI = Instantiate(cardInventoryPrefab, cardInventoryContainer);
            cardUI.Initialize(card, card.id);
            inventoryCardUIs.Add(cardUI);
            
            // Add the dragging script
            if (cardUI.GetComponent<CardDrag>() == null)
            {
                cardUI.gameObject.AddComponent<CardDrag>();
            }
        }
        
        Debug.Log($"Initialized inventory with {inventoryCardUIs.Count} cards");
        
        // Call the grid layout after all cards are instantiated
        SimpleCardGrid gridScript = cardInventoryContainer.GetComponent<SimpleCardGrid>();
        if (gridScript != null)
        {
            gridScript.LayoutCards();
            Debug.Log("Card grid layout applied");
        }
    }
    
    public void OnCardPlaced(int cardId, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < cardSlots.Length)
        {
            UpdateDeckInfo();
            Debug.Log($"Card {cardId} placed in slot {slotIndex}");
            
            // Re-layout the grid after a card is placed
            SimpleCardGrid gridScript = cardInventoryContainer.GetComponent<SimpleCardGrid>();
            if (gridScript != null)
            {
                gridScript.LayoutCards();
                Debug.Log("Card grid re-layout applied after card placement");
            }
        }
    }
    
    public void UpdateDeckInfo()
    {
        int filledSlots = 0;
        int totalCost = 0;
        
        foreach (CardSlot slot in cardSlots)
        {
            if (slot != null && slot.IsOccupied())
            {
                filledSlots++;
                if (slot.GetCard() != null)
                {
                    totalCost += slot.GetCard().cost;
                }
            }
        }
        
        if (deckInfoText != null)
        {
            deckInfoText.text = $"Deck: {filledSlots}/{cardSlots.Length} | Total Cost: {totalCost}";
        }
    }
    
    public CardSlot GetSlot(int index)
    {
        if (index >= 0 && index < cardSlots.Length)
            return cardSlots[index];
        return null;
    }
    
    public int GetFilledSlots()
    {
        int count = 0;
        foreach (CardSlot slot in cardSlots)
        {
            if (slot.IsOccupied())
                count++;
        }
        return count;
    }
}
