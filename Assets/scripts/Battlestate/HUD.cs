using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private UnitHUD playerHUD;
        [SerializeField] private UnitHUD enemyHUD;
        [SerializeField] private UnitHUD enemyAiHUD;
        [SerializeField] private UnitHUD geneticHUD;
        [SerializeField] private Text dialogText;
        [SerializeField] private GameObject PauseScreen;

        public void Initialize(Unit playerUnit, Unit enemyUnit, Unit enemyAI, Unit geneticUnit)
        {
            playerHUD.Initialize(playerUnit);
            enemyHUD.Initialize(enemyUnit);
            enemyAiHUD.Initialize(enemyAI);
            geneticHUD.Initialize(geneticUnit);
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
