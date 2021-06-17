using System.Collections;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class EasyEnemy : State
    {
        public EasyEnemy(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
            //handelling blocking 
            var isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
            if (battleSystem.PlayerUnit.blocking == true)
            {
                isDead = battleSystem.PlayerUnit.isBlocking(battleSystem.EnemyUnit.Damage, battleSystem.PlayerUnit.Block);
                battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name}Blocked");
                battleSystem.PlayerUnit.blocking = false;
            }
            else
            {
                isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
            }
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                battleSystem.SetState(new LostState(battleSystem));
            }
            else
            {
                battleSystem.SetState(new PlayerTurnState(battleSystem));
            }
        }
    }
}