using UnityEngine;

[System.Serializable]
public class Card
{
    public int id;
    public string cardName;
    public string description;
    public int cost;
    public string cardType; // "Attack", "Defense", "Spell", "Support"
    public int rarity; // 1-5, where 5 is legendary
    public Sprite cardImage;
    
    public Card(int id, string cardName, string description, int cost, string cardType, int rarity)
    {
        this.id = id;
        this.cardName = cardName;
        this.description = description;
        this.cost = cost;
        this.cardType = cardType;
        this.rarity = rarity;
    }
}
