using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="New Item", menuName ="Game Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Sword,
        HealthPotion,
        FatiguePotion,
        Coin,
    }; 
    public ItemType itemType;

    public new string name;
    public string discription;
    public Sprite image;
    public int value;
    public int amount;

    public int fatigue;
    public int adrenaline;
    public int health;
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.FatiguePotion: return ItemAssets.Instance.fatiguePotionSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;   
        }
    }

}

