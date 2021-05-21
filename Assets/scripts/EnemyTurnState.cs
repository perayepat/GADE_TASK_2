using System.Collections;
using UnityEngine;
//public enum enemy
//{
//HARD ,
//EASY,
//MEDUIM
//}
//make these difficulties thier own states and depending on what difficulty the player chose in battle system 
//that state will be called 

namespace m.m.TurnBasedGame
{
    public class EnemyTurnState : State
    {
        public EnemyTurnState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            //use a switch to run the different values 
            #region This all goes in the easy class which will be ran if the easy level is chosen 
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
            #endregion
        }

        public override IEnumerator Pause()
        {
            battleSystem.SetState(new PauseState(battleSystem, this));
            yield break;
        }
    }
}
