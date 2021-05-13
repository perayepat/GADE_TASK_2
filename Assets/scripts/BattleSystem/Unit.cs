using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int block;

    public int maxHP;
    public int currentHP;

    public bool blocking;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public bool isBlocking(int dmg, int block)
    {

        currentHP -= dmg - block;
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
