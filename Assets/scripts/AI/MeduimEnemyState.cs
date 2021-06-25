using m.m.TurnBasedGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class MeduimEnemy : State
    {

        public MeduimEnemy(BattleSystem system) : base(system)
        {
        }
        /// <summary> What it has to be able to do 
        /// The meduim enemy should be able to :
        /// block - check 
        /// attack -check
        /// use a healing item when injured  - check
        /// and boost attack power - Check
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Start()
        {

            
            //Ecapsulating each of the ifs would make too many scripts 
            //if you block more than you attack you heal 

            #region seperate these into coroutines 
            // use health potion and switch back to the player         
            battleSystem.SetState(new HealthBoost(battleSystem));
              

            if(battleSystem.EnemyUnit.HealingCounter == 1)
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} regained thier final strength");

                yield return new WaitForSeconds(2f);
            
            //use Boost attack power potion after adrenaline increases after 60
                battleSystem.SetState(new BoostPower(battleSystem));
                yield return new WaitForSeconds(2f);
            

        
            #endregion

            
            #region When Enemy is attacking 
            var isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
            if (battleSystem.PlayerUnit.blocking == true )
            {
                isDead = battleSystem.PlayerUnit.isBlocking(battleSystem.EnemyUnit.Damage, battleSystem.PlayerUnit.Block);
                battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name}Blocked");
                battleSystem.PlayerUnit.blocking = false;

                battleSystem.PlayerUnit.AddAdrenaline(10);
                battleSystem.SetState(new BoostPower(battleSystem));
            }
            else
            {
                isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");

                battleSystem.PlayerUnit.AddAdrenaline(10);
                battleSystem.SetState(new BoostPower(battleSystem));
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


    }
}

