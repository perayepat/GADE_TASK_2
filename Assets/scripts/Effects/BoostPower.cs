using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m.m.TurnBasedGame
{

    public class BoostPower :State

    {
        public BoostPower(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            AttackCounterCheck();
            BoostDamage();
            yield break;

        }
        //work on power boosters here

        //block once every two attacks but when health falls below 50% block twice and use a attack spell 
        public void  AttackCounterCheck()
        {
                 battleSystem.EnemyUnit.AtkCounter(1);
             
                if (battleSystem.EnemyUnit.AttackCounter % 2 == 0)
                {
                    battleSystem.EnemyUnit.blocking = true;
                }
   
            Debug.Log(battleSystem.EnemyUnit.AttackCounter.ToString());
            
        }

        public void BoostDamage()
        {
            if (battleSystem.EnemyUnit.Adrenaline == 60 && battleSystem.EnemyUnit.BoostCounter == 1)
            {
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Has a burst of strength");
                battleSystem.EnemyUnit.Damage += 10;
            }
               
        }
    }
}


