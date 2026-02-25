using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CardSearch : MonoBehaviour
{
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private Transform cardInventoryContainer;
    
    private SimpleCardGrid gridScript;
    private List<CardUI> allCardUIs = new List<CardUI>();

    private void Awake()
    {
        gridScript = cardInventoryContainer.GetComponent<SimpleCardGrid>();
        
        if (searchInput != null)
        {
            searchInput.onValueChanged.AddListener(OnSearchInputChanged);
        }
    }

    private void Start()
    {
        // Collect all card UIs from the inventory
        CardUI[] allCards = cardInventoryContainer.GetComponentsInChildren<CardUI>(includeInactive: true);
        allCardUIs.AddRange(allCards);
        
        Debug.Log($"CardSearch initialized with {allCardUIs.Count} cards");
    }

    private void OnSearchInputChanged(string searchText)
    {
        FilterCards(searchText.ToLower());
    }

    private void FilterCards(string searchText)
    {
        int visibleCount = 0;
        
        foreach (CardUI cardUI in allCardUIs)
        {
            bool matches = string.IsNullOrEmpty(searchText) || 
                          cardUI.GetCardData().cardName.ToLower().Contains(searchText);
            
            cardUI.gameObject.SetActive(matches);
            
            if (matches)
            {
                visibleCount++;
            }
        }
        
        Debug.Log($"Search: '{searchText}' - Found {visibleCount} cards");
        
        // Re-layout the grid after filtering
        if (gridScript != null)
        {
            gridScript.LayoutCards();
        }
    }

    public void ClearSearch()
    {
        if (searchInput != null)
        {
            searchInput.text = "";
        }
    }
}
