using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace m.m.TurnBasedGame
{
    
    public class GeneticUnit : State
    {
        #region state misc
        public GeneticUnit(BattleSystem system) : base(system)
        {
        }

        #endregion
        [SerializeField] int populationSize = 200;
        [SerializeField] float mutationRate = 0.01f;
        [SerializeField] int elitism = 5;
        //genetic testing 
        [SerializeField] UnitActions unitActions;
        [SerializeField] int index;
        GeneticAlgorithm<Unit> ga; // type of what im testing 
        System.Random Random;
   

        //actions need to be recorded using UnitActions As Attack,Defence,Healing eg.(0.5,0.5,0.1)
        //Action depending on 0 and between trainedValue

        //enum Class for the chosen actions

        /// <summary>
        /// 
        //private float FitnessFunction(int index)
        //{

        //    float score = 0;
        //    DNA<char> dna = ga.Population[index];
        //    //The genes are the whole string array but can be changed to a unit 
        //    for (int i = 0; i < dna.Genes.Length; i++)
        //    {
        //        if (dna.Genes[i] == targetString[i])
        //        {
        //            score += 1;
        //        }
        //    }

        //    score /= targetString.Length;

        //    score = (Mathf.Pow(2, score) - 1) / (2 - 1);

        //    return score;
        //}
        /// </summary>
        /// <returns></returns>

        public override IEnumerator Start()
        {
            Random = new System.Random();
            
            yield return null;
            unitActions = GetrandomActions();
            FitnessFunction(unitActions,index);
            switch (unitActions)
            {
                case UnitActions.Attacking:
                    #region
                    var isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.GeneticUnit.Damage);
                    battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} attacks!");
                    //handelling blocking 
                    isDead = battleSystem.EnemyAIUnit.TakeDamage(battleSystem.GeneticUnit.Damage);
                    if (battleSystem.EnemyAIUnit.blocking == true)
                    {
                        battleSystem.GeneticUnit.AddAdrenaline(10);
                        isDead = battleSystem.EnemyAIUnit.isBlocking(battleSystem.GeneticUnit.Damage, battleSystem.EnemyAIUnit.Block);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyAIUnit.Name}Blocked");
                        battleSystem.EnemyAIUnit.blocking = false;
                    }
                    else
                    {
                        battleSystem.GeneticUnit.AddAdrenaline(10);
                        isDead = battleSystem.EnemyAIUnit.TakeDamage(battleSystem.GeneticUnit.Damage);
                        battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} attacks!");
                    }
                    yield return new WaitForSeconds(1f);

                    if (isDead)
                    {
                        battleSystem.SetState(new LostState(battleSystem));
                    }
                    else
                    {
                        battleSystem.SetState(new AIagainstAI(battleSystem));
                    }
                    break;
                #endregion
                case UnitActions.Blocking:
                    #region
                    battleSystem.GeneticUnit.blocking = true;
                    yield return new WaitForSeconds(1f);
                    battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} + blocked your attack");
                    battleSystem.SetState(new AIagainstAI(battleSystem));
                    #endregion
                    break;
                case UnitActions.ItemHeal:
                    HealthItemEnemy();
                    break;
                case UnitActions.ItemAttackBoost:
                    #region
                    //use Boost attack power potion after adrenaline increases after 60
                    battleSystem.SetState(new BoostPower(battleSystem));
                    yield return new WaitForSeconds(2f);
                    battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} + Gains a attak boost");
                    battleSystem.SetState(new AIagainstAI(battleSystem));
                    #endregion
                    break;
                case UnitActions.ItemDefBoost:
                    DefenceItem();
                    yield return new WaitForSeconds(1f);
                    battleSystem.SetState(new AIagainstAI(battleSystem));
                    break;
            }

        }

        //when a action is successful 
        //according to parameters add a point 
        private void FitnessFunction(UnitActions actions, int index)
        {
            //increase the score on successful actions according to parameters 
            float score = 0; // for each gene
            Genes<Unit> dna = ga.Population[index];
            for (int i = 0; i < dna._Genes.Length; i++)
            {

            }
        }




        //Get random action 
        public UnitActions GetrandomActions()
        {
            int randAction = Random.Next(0, 5);
            unitActions = (UnitActions)randAction;
            return unitActions;

        }
        public void HealthItemEnemy()
        {

                battleSystem.GeneticUnit.HealCounter(-1);
                battleSystem.GeneticUnit.Health += 20;
                battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} Has healed himself");

                battleSystem.SetState(new AIagainstAI(battleSystem));

                //last health boost last legs 
                if (battleSystem.GeneticUnit.HealingCounter == 1)
                {
                    battleSystem.GeneticUnit.Health += 30;
                    battleSystem.GeneticUnit.HealCounter(-1);

                    battleSystem.SetState(new AIagainstAI(battleSystem));
                }
        }    

        public void DefenceItem()
        {
            battleSystem.GeneticUnit.Block += 10;
            battleSystem.GeneticUnit.AddFatigue(25);
            battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} Has is wearing prepared");
            battleSystem.GeneticUnit.BstCounter(-1);
           
        }
    }

}

