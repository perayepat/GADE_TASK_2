using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemDisplay : MonoBehaviour
{
    public Item item;

    public Text nameTxt;
    public Text descripTxt;
    public Image artWrk;
    public Text healthTxt;
    public Text fatigueTxt;
    public Text adrenlineTxt;

    void Start()
    {
        nameTxt.text = item.name;
        descripTxt.text = item.discription;
        artWrk.sprite = item.image;
        healthTxt.text = item.health.ToString();
        fatigueTxt.text = item.fatigue.ToString();
        adrenlineTxt.text = item.adrenaline.ToString();
    }


}
