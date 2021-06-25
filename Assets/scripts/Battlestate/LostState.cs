using System.Collections;

namespace m.m.TurnBasedGame
{
    public class LostState : State
    {
        public LostState(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.SetDialogText("You were defeated.");
            yield break;
        }
    }
}