using System.Collections;
using UnityEngine;

namespace m.m.TurnBasedGame
{
    enum playerState
    {
        Player,
        AI
    }
    public class EasyEnemy : State
    {
        playerState _playerState;
        public EasyEnemy(BattleSystem system) : base(system)
        {
        }

        public override IEnumerator Start()
        {
            _playerState = playerState.Player;
            switch (_playerState)
            {
                case playerState.Player:
                    battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
                    //handelling blocking 
                    var isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                    if (battleSystem.PlayerUnit.blocking == true)
                    {
                        isDead = battleSystem.PlayerUnit.isBlocking(battleSystem.EnemyUnit.Damage, battleSystem.PlayerUnit.Block);
                        battleSystem.HUD.SetDialogText($"{battleSystem.PlayerUnit.Name}Blocked");
                        battleSystem.PlayerUnit.blocking = false;
                        battleSystem.EnemyUnit.AddAdrenaline(10);
                    }
                    else

                    {
                        battleSystem.EnemyUnit.AddAdrenaline(10);
                        isDead = battleSystem.PlayerUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
                    }
                    yield return new WaitForSeconds(1f);

                    if (isDead)
                    {
                        battleSystem.SetState(new LostState(battleSystem));
                    }
                    else
                    {
                        battleSystem.SetState(new PlayerTurnState(battleSystem));
                    }
                    break;
                case playerState.AI:
                    battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
                    //handelling blocking 
                    isDead = battleSystem.EnemyAIUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                    if (battleSystem.EnemyAIUnit.blocking == true)
                    {
                        battleSystem.EnemyUnit.AddAdrenaline(10);
                        isDead = battleSystem.EnemyAIUnit.isBlocking(battleSystem.EnemyUnit.Damage, battleSystem.EnemyAIUnit.Block);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyAIUnit.Name}Blocked");
                        battleSystem.EnemyAIUnit.blocking = false;
                    }
                    else
                    {
                        battleSystem.EnemyUnit.AddAdrenaline(10);
                        isDead = battleSystem.EnemyAIUnit.TakeDamage(battleSystem.EnemyUnit.Damage);
                        battleSystem.HUD.SetDialogText($"{battleSystem.EnemyUnit.Name} attacks!");
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
                default:
                    break;
            }

        }
    }
}