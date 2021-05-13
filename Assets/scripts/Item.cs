using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Game Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string discription;
    public Sprite image;

    public int fatigue;
    public int adrenaline;
    public int health;

}
