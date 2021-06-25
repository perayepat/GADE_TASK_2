using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="New Item", menuName ="Game Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Strength,
        HealthPotion,
        FatiguePotion,
        Speed,
        StealAdrenalline,
        BloodShot,
        Bane,
        Revenge
    }
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
            case ItemType.Strength: return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.FatiguePotion: return ItemAssets.Instance.fatiguePotionSprite;
            case ItemType.Speed: return ItemAssets.Instance.coinSprite;   
        }
    }

}

