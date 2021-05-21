using System.Collections;
using UnityEditor;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.SetDialogText("Choose an action.");
            yield break;
        }
        //moved from the battle system 
        public override IEnumerator Attack()
        {
            var isDead = battleSystem.EnemyUnit.TakeDamage(battleSystem.PlayerUnit.Damage);
        
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                battleSystem.SetState(new WonState(battleSystem));
            }
            else
            {
                battleSystem.SetState(new EnemyTurnState(battleSystem));
            }
        }

        public override IEnumerator Heal()
        {
            battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} is renewed in strength!");
            
            battleSystem.PlayerUnit.Heal(5);
        
            yield return new WaitForSeconds(1f);
        
            battleSystem.SetState(new EnemyTurnState(battleSystem));
        }

        public override IEnumerator Pause()
        {
            battleSystem.SetState(new PauseState(battleSystem, this));
            yield break;
        }

        public override IEnumerator Blocking()
        {
            //enemyUnit.blocking = true;
            //dialogueText.text = enemyUnit.unitName + " is blocking ";
            //yield return new WaitForSeconds(2f);

            //state = BattleState.PLAYERTURN;
            //PlayerTurn();
            battleSystem.PlayerUnit.blocking = true;
            battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} is blocking");
            yield return new WaitForSeconds(5);

            battleSystem.SetState(new EnemyTurnState(battleSystem));


        }

    }
}