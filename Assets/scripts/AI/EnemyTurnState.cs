using System.Collections;
using UnityEngine;
public enum enemy
{
    HARD,
    EASY,
    MEDUIM,
    NONE
}
//make these difficulties thier own states and depending on what difficulty the player chose in battle system 
//that state will be called 

namespace m.m.TurnBasedGame
{
    
    public class EnemyTurnState : State
    {
        public static EnemyTurnState instance = null;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            
        }
        bool easyOn;

        
        public enemy _enemy;
        public EnemyTurnState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {

            _enemy = enemy.EASY;
            switch (_enemy)
            {
                case enemy.HARD:
                    battleSystem.SetState(new HardEnemy(battleSystem));
                    break;
                case enemy.EASY:
                    battleSystem.SetState(new EasyEnemy(battleSystem));
                    break;
                case enemy.MEDUIM:
                    battleSystem.SetState(new MeduimEnemy(battleSystem));
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(1f);

           

        }
        public void OnSetEnemyLevelEasy()
        {
            _enemy = enemy.EASY;
        }

        public void ONSetEnemyLevelMeduim()
        {
            _enemy = enemy.MEDUIM;
        }
        public void ONSetEnemyLevelHard()
        {
            _enemy = enemy.HARD;
        }



        public override IEnumerator Pause()
        {
            battleSystem.SetState(new PauseState(battleSystem, this));
            yield break;
        }

        #region Buttons 
        public void OnEasyButton()
        {
            _enemy = enemy.EASY; 
        }
        public void OnMeduimButton()
        {
            _enemy = enemy.MEDUIM;
        }
        public void OnHardButton()
        {
            _enemy = enemy.HARD;
        }
        #endregion
    }

    

}

