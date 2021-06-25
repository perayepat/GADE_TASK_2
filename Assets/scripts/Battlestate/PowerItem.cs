using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    
    public class PlayerTurnButtons : State
    {
        public PlayerTurnButtons(BattleSystem system) : base(system){}

        Button btn;
        GameObject discOne;
        public override IEnumerator Start()
        {


            yield break;
        }
    
    }

}


