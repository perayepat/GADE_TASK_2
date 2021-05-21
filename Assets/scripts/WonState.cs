using System.Collections;

namespace m.m.TurnBasedGame
{
    public class WonState : State
    {
        public WonState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.SetDialogText("You won the battle!");
            yield break;
        }
    }
}