using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    public class PlayerTurnState : State
    {
        private int blockCount;
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
            if (battleSystem.EnemyUnit.blocking == true)
            {
                isDead = battleSystem.EnemyUnit.isBlocking(battleSystem.PlayerUnit.Damage, battleSystem.EnemyUnit.Block);
                battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Blocked your attack ");
                battleSystem.EnemyUnit.blocking = false;
                battleSystem.PlayerUnit.AddAdrenaline(10);
            }
            else
            {
                battleSystem.PlayerUnit.AddAdrenaline(10);
                isDead = battleSystem.EnemyUnit.TakeDamage(battleSystem.PlayerUnit.Damage);
                battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} Attacks ");
                  
            }

            //preventing blocking and attacking as a way 
            if (blockCount == 1)
            {
                battleSystem.PlayerUnit.blocking = false;
                blockCount = 0;
            }
        
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
           
            battleSystem.PlayerUnit.blocking = true;
            battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} is blocking");
            yield return new WaitForSeconds(3f);

            battleSystem.SetState(new EnemyTurnState(battleSystem));
            blockCount++;

        }

        public override IEnumerator Sapping()
        {
            

          
            Button btn;
            btn = GameObject.Find("Button - ItemFour").GetComponent<Button>();

            battleSystem.PlayerUnit.Damage += 10;
            battleSystem.EnemyUnit.Damage -= 10;
            battleSystem.PlayerUnit.Adrenaline += 20;

            battleSystem.itemsPanel.active = false;
            battleSystem.HUD.SetDialogText($"5 points of damage have been taken from {battleSystem.EnemyUnit.Name}");
            battleSystem.PlayerUnit.BstCounter(-1);
            yield return new WaitForSeconds(1f);


            
            btn.gameObject.active = false;
            battleSystem.SetState(new EnemyTurnState(battleSystem));

        }
        public override IEnumerator Resting()
        {
       
            battleSystem.PlayerUnit.AddAdrenaline(-10);
            battleSystem.PlayerUnit.AddFatigue(-15);
            battleSystem.itemsPanel.active = false;
            battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} Feels rested");
            yield return new WaitForSeconds(1f);
         
            battleSystem.SetState(new EnemyTurnState(battleSystem));
        }

        public override IEnumerator Stacking()
        {
            Button btn;
            btn = GameObject.Find("Button - ItemTwo").GetComponent<Button>();
            battleSystem.PlayerUnit.Block += 10;
            battleSystem.PlayerUnit.AddFatigue(25);
            battleSystem.itemsPanel.active = false;
            battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name} Has is wearing prepared");
            yield return new WaitForSeconds(1f);
            battleSystem.PlayerUnit.BstCounter(-1);
            btn.gameObject.active = false;
            battleSystem.SetState(new EnemyTurnState(battleSystem));
        }
    }
}