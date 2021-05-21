using System;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private string unitName;
        [SerializeField] private int level;
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private int damage;
        [SerializeField] private int block;
        internal bool blocking;
        

        public string Name => unitName;
        public int Level => level;

        internal bool IsBlocking(int damage, object block)
        {
            throw new NotImplementedException();
        }

        public int Health => health;
        public int MaxHealth => maxHealth;
      
        public int Damage => damage;
        public int Block => block;
        


        public bool TakeDamage(int amount)
        {
            health = Math.Max(0, health - amount);
            return health == 0;
        }

        public void Heal(int amount)
        {
            health += amount;
            if (health > maxHealth)
                health = maxHealth;
        }
   

        public bool isBlocking(int dmg, int block)
        {

            health -= dmg - block;
            if (health <= 0)
                return true;
            else
                return false;
        }

    }
}