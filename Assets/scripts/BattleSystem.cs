
using UnityEngine;

namespace m.m.TurnBasedGame
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private HUD hud;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform enemyPosition;

        private Unit _playerUnit;
        private Unit _enemyUnit;
        private State _currentState;
        private Setdifficulty setdifficulty;

        public Unit PlayerUnit => _playerUnit;
        public Unit EnemyUnit => _enemyUnit;
        public HUD HUD => hud;

        public Setdifficulty Setdifficulty => setdifficulty;

        public void SetState(State state)
        {
            
            _currentState = state;
            StartCoroutine(_currentState.Start());
        }

        [ContextMenu("Pause")]
        public void OnPauseButton()
        {
    
            StartCoroutine(_currentState.Pause());
        }

        public void OnResumeButton()
        {
            StartCoroutine(_currentState.Resume());
        }

        public void OnAttackButton()
        {
            StartCoroutine(_currentState.Attack());
        }

        public void OnHealButton()
        {
            StartCoroutine(_currentState.Heal());
        }
        public void onBlockButton()
        {
            StartCoroutine(_currentState.Blocking());
        }

        public void onUseItemButton()
        {
            Debug.Log("Items");
            //open the menu and handle the buttons in the menu
        }

        private void Start()
        {
            
            SetupBattle();
        }

        private void SetupBattle()
        {
            var playerGameObject = Instantiate(playerPrefab, playerPosition);
            _playerUnit = playerGameObject.GetComponent<Unit>();
        
            var enemyGameObject = Instantiate(enemyPrefab, enemyPosition);
            _enemyUnit = enemyGameObject.GetComponent<Unit>();

            hud.Initialize(_playerUnit, _enemyUnit);
            
            SetState(new BeginState(this));
        }
    }


}