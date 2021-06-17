using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class HealthBoost : State
    {
        

        public HealthBoost(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            HealthItemEnemy();
            yield break;
        }

        public void HealthItemEnemy()
        {

            if (battleSystem.EnemyUnit.Health == battleSystem.EnemyUnit.MaxHealth * 3f)
            {
                battleSystem.EnemyUnit.HealCounter(-1);
                battleSystem.EnemyUnit.Health += 20;
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Has healed himself");

                battleSystem.SetState(new PlayerTurnState(battleSystem));

                //last health boost last legs 
                if (battleSystem.EnemyUnit.HealingCounter == 1)
                {
                    battleSystem.EnemyUnit.Health += 30;
                    battleSystem.EnemyUnit.HealCounter(-1);
                
                    battleSystem.SetState(new PlayerTurnState(battleSystem));
                }
            }
        }
    }

}


