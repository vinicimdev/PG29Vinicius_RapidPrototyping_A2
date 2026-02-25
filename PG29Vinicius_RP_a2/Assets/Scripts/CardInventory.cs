using UnityEngine;

public class CardInventory : MonoBehaviour
{
    public static CardInventory Instance { get; private set; }
    
    [SerializeField] private Card[] allCards;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        InitializeInventory();
    }
    
    private void InitializeInventory()
    {
        // Create 15 unique cards
        Card[] baseCards = new Card[15];
        
        baseCards[0] = new Card(0, "Fireball", "Deal 3 damage to target enemy", 3, "Attack", 2);
        baseCards[1] = new Card(1, "Ice Wall", "Gain 2 armor this turn", 2, "Defense", 1);
        baseCards[2] = new Card(2, "Holy Light", "Heal 2 HP", 2, "Support", 2);
        baseCards[3] = new Card(3, "Dark Ritual", "Draw 1 card, lose 1 HP", 1, "Spell", 3);
        baseCards[4] = new Card(4, "Berserker", "Attack +2 for 1 turn", 4, "Attack", 3);
        baseCards[5] = new Card(5, "Shield Bash", "Block 1 attack", 2, "Defense", 1);
        baseCards[6] = new Card(6, "Poison Cloud", "Deal 1 damage to all enemies", 3, "Attack", 2);
        baseCards[7] = new Card(7, "Mana Crystal", "Gain 1 mana next turn", 1, "Support", 1);
        baseCards[8] = new Card(8, "Dragon Strike", "Deal 5 damage, costs 1 more each turn", 5, "Attack", 4);
        baseCards[9] = new Card(9, "Life Steal", "Deal 2 damage and heal 2 HP", 3, "Spell", 3);
        baseCards[10] = new Card(10, "Blizzard", "Deal 2 damage to all enemies, freeze 1", 4, "Attack", 3);
        baseCards[11] = new Card(11, "Fortify", "Gain 3 armor", 3, "Defense", 2);
        baseCards[12] = new Card(12, "Shadow Clone", "Create a copy of this card", 4, "Spell", 4);
        baseCards[13] = new Card(13, "Holy Smite", "Deal 3 damage, restore 1 HP", 3, "Attack", 2);
        baseCards[14] = new Card(14, "Time Warp", "Skip opponent's next turn", 5, "Spell", 5);
        
        // Create 45 cards total (3 copies of each)
        allCards = new Card[45];
        
        for (int i = 0; i < 45; i++)
        {
            Card baseCard = baseCards[i % 15];
            allCards[i] = new Card(i, baseCard.cardName, baseCard.description, baseCard.cost, baseCard.cardType, baseCard.rarity);
        }
    }
    
    public Card GetCard(int cardId)
    {
        if (cardId >= 0 && cardId < allCards.Length)
            return allCards[cardId];
        return null;
    }
    
    public Card[] GetAllCards()
    {
        return allCards;
    }
}
