using System.Collections;

namespace m.m.TurnBasedGame
{
    public abstract class State
    {
        /// <summary>
        /// passing in the battle system using a constructor to be able to access it in other classes 
        /// </summary>
        protected BattleSystem battleSystem;

        public State(BattleSystem system)
        {
            battleSystem = system;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }
        
        public virtual IEnumerator Attack()
        {
            yield break;
        }

        public virtual IEnumerator Heal()
        {
            yield break;
        }

        public virtual IEnumerator Pause()
        {
            yield break;
        }

        public virtual IEnumerator Resume()
        {
            yield break;
        }
        public virtual IEnumerator Blocking()
        {
            yield break;
        }
    }
}