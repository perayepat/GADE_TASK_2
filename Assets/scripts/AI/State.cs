using System.Collections;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public abstract class State : MonoBehaviour
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

        private void Update()
        {

        }
        public virtual IEnumerator AIBased()
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

        public virtual IEnumerator Resting()
        {
            yield break;
        }

        public virtual IEnumerator Stacking()
        {
            yield break;
        }

        public virtual IEnumerator Sapping()
        {
            yield break;
        }
        public virtual IEnumerator UseItem()
        {
            yield break;
        }
    }
}