using System.Collections;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class BeginState : State
    {
        public BeginState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.SetDialogText($"A wild {battleSystem.EnemyUnit.Name} approaches...");
            
            yield return new WaitForSeconds(2f);
            
            //this state 
            battleSystem.SetState(new PlayerTurnState(battleSystem));
        }
    }
}