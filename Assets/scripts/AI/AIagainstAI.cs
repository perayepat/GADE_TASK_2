using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m.m.TurnBasedGame
{

    public class AIagainstAI : State
    {
        public AIagainstAI(BattleSystem system) : base(system)
        {
        }
        //implement an ai that calls the enemy instead of the player here 
        //this would use the same features as the ai just chainging who it calls after each attack 

        public override IEnumerator Start()
        {
            Debug.Log(battleSystem.EnemyAIUnit.Name);
            battleSystem.HUD.SetDialogText($"{battleSystem.EnemyAIUnit.Name} attacks!");
            //handelling blocking 
            var isDead = battleSystem.EnemyUnit.TakeDamage(battleSystem.EnemyAIUnit.Damage);
            if (battleSystem.EnemyUnit.blocking == true)
            {
                isDead = battleSystem.EnemyAIUnit.isBlocking(battleSystem.EnemyUnit.Damage, battleSystem.EnemyAIUnit.Block);
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name}Blocked");
                battleSystem.EnemyAIUnit.blocking = false;
            }
            else
            {
                isDead = battleSystem.EnemyAIUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
            }
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                battleSystem.SetState(new LostState(battleSystem));
            }
            else
            {
                battleSystem.SetState(new EnemyTurnState(battleSystem));
            }

        }
    }
}


