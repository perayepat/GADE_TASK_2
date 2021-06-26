using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private UnitHUD playerHUD;
        [SerializeField] private UnitHUD enemyHUD;
        [SerializeField] private UnitHUD enemyAiHUD;
        [SerializeField] private Text dialogText;
        [SerializeField] private GameObject PauseScreen;

        public void Initialize(Unit playerUnit, Unit enemyUnit, Unit enemyAI)
        {
            playerHUD.Initialize(playerUnit);
            enemyHUD.Initialize(enemyUnit);
            enemyAiHUD.Initialize(enemyAI);
        }

        public void SetDialogText(string text)
        {
            dialogText.text = text;
        }

        public void ShowPauseMenu()
        {
            PauseScreen.SetActive(true);
        }
        
        public void HidePauseMenu()
        {
            PauseScreen.SetActive(false);
        }
    }
}
