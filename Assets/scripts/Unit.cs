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
        [SerializeField] private int increasedAttack;
        [SerializeField] private int ptDeath = 1;
        [SerializeField] private int fatigueCounter = 1;

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

        public int IncreasedAttack
        {
            get { return increasedAttack; }
            set { increasedAttack = value; }
        }

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

 
        

        //Adrenaline inrease Check 
        public void AdrenalineCheck(int adrenalineAmount ,int unitAttackAmount)
        {
            //make sure this only activates once for each 

            if (adrenalineAmount >= 25 && ptDeath == 1)
            {
                ptDeath++;
                IncreasedAttack = unitAttackAmount += adrenalineAmount / 15 * 2;
                unitAttackAmount += increasedAttack;

            }
            else if (adrenalineAmount >= 50 && ptDeath == 2)
            {
                ptDeath++;
                IncreasedAttack = unitAttackAmount += adrenalineAmount / 15 * 2;
                unitAttackAmount += increasedAttack;
            }
            else if (adrenalineAmount >= 75 && ptDeath == 3)
            {
                //decrease health at this point 
                ptDeath++;
                IncreasedAttack = unitAttackAmount += adrenalineAmount / 15 * 2;
                unitAttackAmount += increasedAttack;
                Health -= 5;
            }
            else if (adrenalineAmount >= 100 && ptDeath == 5)
            {
                //cripple the Unit 
                ptDeath++;
                IncreasedAttack = unitAttackAmount += adrenalineAmount / 15 * 2;
                unitAttackAmount += increasedAttack;
                Health = 2;
            }

        }

        //Fatigue Decrease check 
        //Include dodging when fatigue is high up 
        public void FatigueCheck(int fatigueAmount , int unitAttackAmoount)
        {
            
            if (fatigueAmount >= 25 && fatigueCounter == 1)
            {
                //lower attack value 
                unitAttackAmoount -= fatigueAmount / 10;
                fatigueCounter++;
                
            }
            else if(fatigue >= 50 && fatigueCounter == 2)
            {
                unitAttackAmoount -= fatigueAmount / 10;
                fatigueCounter++;
            }
            else if(fatigueCounter >= 75 && fatigueCounter == 3)
            {
                unitAttackAmoount -= fatigueAmount / 10;
                fatigueCounter++;
            }
            else if(fatigueCounter >= 100 && fatigueCounter == 4)
            {
                //Max fatigue penalty 
                unitAttackAmoount -= fatigueAmount / 10;
                fatigueCounter++;
            }

        }


        

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