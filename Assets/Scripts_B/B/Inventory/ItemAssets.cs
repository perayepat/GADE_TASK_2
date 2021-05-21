using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    //Set sprites
public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    /// <summary> Items
    ///     Sword,
    ///  HealthPotion,
    ///FatiguePotion,
    ///  Coin,
    /// </summary>
    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite fatiguePotionSprite;
    public Sprite coinSprite;

}
