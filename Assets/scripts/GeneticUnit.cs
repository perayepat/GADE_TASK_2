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
        [SerializeField] int dnaSize;
        [SerializeField] float min;
        [SerializeField] float max;
        [Header("Changing values")]
        [SerializeField] float minAtkIndex;
        [SerializeField] float maxAtkIndex;


        //genetic testing 
        [SerializeField] float unitActions;
        [SerializeField] int index;
        GeneticAlgorithm<float> ga; // type of what im testing 
        System.Random Random;
        float actionSuccessChance;

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
            ga = new GeneticAlgorithm<float>(
                populationSize,
                dnaSize,
                Random,
                GetrandomActions,
                FitnessFunction,
                elitism,
                mutationRate);
            yield return new WaitForSeconds(1f);
            //unitActions = GetrandomActions();// cast this to an int 
            FitnessFunction(index);
            actionSuccessChance = GetrandomActions();
            //from -1.0 to 1.0
            if (actionSuccessChance >= -1f && actionSuccessChance <= -0.5f) //attack 
            {
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
            }
            else if (actionSuccessChance >= -0.5 && actionSuccessChance <=0f)//block
            {
                #region
                battleSystem.GeneticUnit.blocking = true;
                yield return new WaitForSeconds(1f);
                battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} + blocked your attack");
                battleSystem.SetState(new AIagainstAI(battleSystem));
                #endregion
            }
            else if (actionSuccessChance >= 0.3f && actionSuccessChance <= 0.6f)//heal
            {
                HealthItemEnemy();
            }
            else if (actionSuccessChance >= 0.6f && actionSuccessChance <= 0.7f)//boost power
            {
                #region
                //use Boost attack power potion after adrenaline increases after 60
                battleSystem.SetState(new BoostPower(battleSystem));
                yield return new WaitForSeconds(2f);
                battleSystem.HUD.SetDialogText($"{battleSystem.GeneticUnit.Name} + Gains a attak boost");
                battleSystem.SetState(new AIagainstAI(battleSystem));
                #endregion

            }
            else if (actionSuccessChance >= 0.7f && actionSuccessChance <= 1f)//defence item
            {
                DefenceItem();
                yield return new WaitForSeconds(1f);
                battleSystem.SetState(new AIagainstAI(battleSystem));
            }
        }

        //when a action is successful 
        //according to parameters add a point 

        private void Update()
        {
            ga.NewGeneration();
            if (ga.BestFitness != null)
            {
                this.enabled = false;
            }
            
        }

        private float FitnessFunction(int index)
        {

            //increase the score on successful actions according to parameters 
            float score = 0; // for each gene
            Genes<float> dna = ga.Population[index];
            //set up if statements that refrence the random action in correlation with the conidotion to make sure points are
            //allocated properly and states are adjusted properly
            for (int i = 0; i < dna._Genes.Length; i++)
            {
                if ((dna._Genes[i] >= -1f && dna._Genes[i] <= -0.5f)&&(battleSystem.EnemyAIUnit.Health <= 75))
                {
                    score += 1;
                }
                else if ((dna._Genes[i] >= -1f && dna._Genes[i] <= -0.5f) && (battleSystem.EnemyAIUnit.Health <= 50))
                {
                    score += 1;
                }
            }
            return score;

        }

        //Get random actions which are enums but refrence the enums using thier int values 
        //In order to return the right value
        public float GetrandomActions()
        {
            double actions;
            actions = (Random.NextDouble() * (max - min) + min);

            return (float)actions;

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

