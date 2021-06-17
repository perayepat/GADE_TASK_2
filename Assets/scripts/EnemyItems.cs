using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    public class EnemyItems : State
    {
        Button PlayerAttackButton;
        Button PlayerItemsButton;
        Button PlayerHealButton;
        Button PlayerBlockButton;
        public bool areButtonsDisabled;
        bool isDeathActive;
        List<Button> buttonsList = new List<Button>();
        public enum EnemyPossessions
        {
        Resetrain,
        Blind,
        RoidRage,
        BigRed,
        KoolAid,
        YearsOfTraining
        }

        public EnemyItems(BattleSystem system) : base(system)
        {
        }


        public override IEnumerator Start()
        {
            #region Misc
            PlayerAttackButton = GameObject.Find("Button - Attack").GetComponent<Button>();
            PlayerHealButton = GameObject.Find("Button - Heal").GetComponent<Button>();
            PlayerBlockButton = GameObject.Find("Button - Block").GetComponent<Button>();
            PlayerItemsButton = GameObject.Find("Button - ItemUse").GetComponent<Button>();
            AddtheButton();
            #endregion
        
            yield return new WaitForSeconds(3f);
            UseRandomItems();
        }

        public void UseRandomItems()
        {
            battleSystem.EnemyUnit.TurnCounter++;
            int buttons = Random.Range(0, 5);
            if (isDeathActive)
            {
                battleSystem.EnemyUnit.Health -= 4;
            }
            EnemyPossessions possessions;
            if (battleSystem.EnemyUnit.AttackCounter % 6 == 0)
            {
                possessions = (EnemyPossessions)Random.Range(0, 6);
                switch (possessions)
                {
                    case EnemyPossessions.Resetrain:
                        // stop the player from doing a random action
                        
                        buttonsList[buttons].image.enabled = false;
                        buttonsList[buttons].enabled = false;
                        areButtonsDisabled = true;
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Has restrained you");
                        battleSystem.SetState(new PlayerTurnState(battleSystem));
                        break;
                    case EnemyPossessions.Blind:
                        //the player misses the attack 
                        buttonsList[3].enabled = false;
                        buttonsList[3].image.enabled = false;
                        break;
                    case EnemyPossessions.RoidRage:
                        //Enemy looses half thier health for double the damage 
                        battleSystem.EnemyUnit.Damage += 10;
                        battleSystem.EnemyUnit.Health -= 40;
                        battleSystem.EnemyUnit.AddAdrenaline(60);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Has sacrificed a lot for mass power ");
                        break;
                    case EnemyPossessions.BigRed:
                        //Adrenaline to the max but fatigue increases faster = better attacks 
                        battleSystem.EnemyUnit.AddAdrenaline(70);
                        battleSystem.EnemyUnit.Damage += 20;
                        isDeathActive = true;
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Has gone red to the bone but at what cost");
                        battleSystem.SetState(new PlayerTurnState(battleSystem));
                        break;
                    case EnemyPossessions.KoolAid:
                        //Drops the fatigue and feeels refreshed 
                        battleSystem.EnemyUnit.AddFatigue(-battleSystem.EnemyUnit.Fatigue);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} feels refreshed");
                        break;
                    case EnemyPossessions.YearsOfTraining:
                        // stats rise but only once 
                        battleSystem.EnemyUnit.Level += 10;
                        battleSystem.EnemyUnit.Health += 5;
                        battleSystem.EnemyUnit.Damage += 5;
                        battleSystem.EnemyUnit.Block -= 5;
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} remebered they have Years of training ");
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} Prepare for death");
                        battleSystem.SetState(new PlayerTurnState(battleSystem));

                        break;
                    default:
                        break;
                }

            }

            //Turn the attck button back on for the player after a turn or two if uts off 
            if (battleSystem.EnemyUnit.TurnCounter % 2 == 0 && areButtonsDisabled == true)
            {
                foreach (Button b in buttonsList)
                {
                    b.enabled = true;
                    b.image.enabled = true;
                }
                areButtonsDisabled = false;
            }

        }
        public void AddtheButton()
        {

            buttonsList.Add(PlayerAttackButton);
            buttonsList.Add(PlayerBlockButton);
            buttonsList.Add(PlayerHealButton);
            buttonsList.Add(PlayerItemsButton);
        }
    }

}


