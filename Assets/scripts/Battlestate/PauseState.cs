using System.Collections;

namespace m.m.TurnBasedGame
{
    public class PauseState : State
    {
        private readonly State _resumeState;

        public PauseState(BattleSystem system, State resumeState) : base(system)
        {
            _resumeState = resumeState;
        }

        public override IEnumerator Start()
        {
            battleSystem.HUD.ShowPauseMenu();
            yield break;
        }

        public override IEnumerator Resume()
        {
            battleSystem.HUD.HidePauseMenu();
            battleSystem.SetState(_resumeState);
            yield break;
        }
    }
}