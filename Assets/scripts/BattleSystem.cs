using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace m.m.TurnBasedGame
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private HUD hud;
        [SerializeField] private UnitHUD playerHUD, enemyHUD;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform playerPosition;
        [SerializeField] private Transform enemyPosition;
        public GameObject itemsPanel;
        private int onAndOffTing = 1;
        private Unit _playerUnit;
        private Unit _enemyUnit;
        private State _currentState;
        private Setdifficulty setdifficulty;

        public UnitHUD PlayerHUD
        {
            get { return playerHUD; }
            set { playerHUD = value; }
        }

        public UnitHUD EnemyHUD
        {
            get { return enemyHUD; }
            set { enemyHUD = value; }
        }



        public Unit PlayerUnit => _playerUnit;
        public Unit EnemyUnit => _enemyUnit;
        public HUD HUD => hud;

        public Setdifficulty Setdifficulty => setdifficulty;

        
        public void SetState(State state)
        {
            //making running each states start easier from other classes 
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

        public void OnSapButton()
        {
            StartCoroutine(_currentState.Sapping());

        }

        public void OnStackButton()
        {
            StartCoroutine(_currentState.Stacking());
        }

        public void OnRestButton()
        {
            StartCoroutine(_currentState.Resting());
        }

        private void Start()
        {
            SetupBattle();
        }

        public void OnEscapeButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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

        public void OnItemsPanelEnable()
        {
            
            onAndOffTing ++;
            itemsPanel.active = false;
            if (onAndOffTing % 2 == 0)
            {
                itemsPanel.active = true;
            }
        }
    }


}