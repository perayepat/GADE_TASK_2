using System;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class Unit : MonoBehaviour
    {
        [Header("Character Base")]
        [SerializeField] private string unitName;
        [SerializeField] private int level;
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private int damage;
        [SerializeField] private int block;

        [Header("Character Attributes")]
        [SerializeField] private int fatigue;
        [SerializeField] private int maxFatigue;
        [SerializeField] private int adrenaline;
        [SerializeField] private int maxAdrenaline;
        [SerializeField] private int attackCounter;
        [SerializeField] private int boostCounter;
        [SerializeField] private int healingCounter;
        [SerializeField] private int turnCounter;

        private bool poisin;
        internal bool blocking;
        

        public string Name => unitName;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }


        public int AttackCounter => attackCounter;
        public int BoostCounter => boostCounter;
        public int HealingCounter => healingCounter;
        public int MaxHealth => maxHealth;
        public int MaxFatigue => fatigue;
        public int MaxAdrenaline => adrenaline;

        public int TurnCounter
        {
            get { return turnCounter; }
            set { turnCounter = value; }
        }

        public bool Poisin
        {
            get { return poisin; }
            set { poisin = value; }
        }
    
        public int Fatigue
        {
            get { return fatigue; }
            set { fatigue = value; }
        }
        public int Adrenaline
        {
            get { return adrenaline; }
            set { adrenaline = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }
      
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public int Block
        {
            get
            {
                return block;
            }
            set
            {
                block = value;
            }
        }

        //Put values like attackcounter healing and what not in here so that thier not effected when you call a new class
        //the new class always keeps these values such as health the same 
        
        /// <summary>
        /// Mediuim enemy methods needed :
        /// counters 
        /// </summary>
        /// <returns></returns>
        /// 

        public void AddAdrenaline(int amount)
        {
            Adrenaline += amount;
            if (Adrenaline > maxAdrenaline)
            {
                Adrenaline = MaxAdrenaline;
            }
        }

        public void AddFatigue(int amount)
        {
            Fatigue += amount;
            if (Fatigue > MaxFatigue)
                Fatigue = maxFatigue;
        }
       
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

        #region Counters and Behaviour Tricks 
        public void AtkCounter(int amount)
        {
            attackCounter += amount;
        }

        public void BstCounter(int amount)
        {
            boostCounter += amount;
        }
        public void HealCounter(int amount)
        {
            healingCounter += amount;
        }
        #endregion

    }
}