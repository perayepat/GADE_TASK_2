using UnityEngine;
using UnityEngine.UI;

namespace m.m.TurnBasedGame
{
    public class UnitHUD : MonoBehaviour
    {
        [SerializeField] private Text unitName;
        [SerializeField] private Text level;
        [SerializeField] private Image fillImage;
        [SerializeField] private Slider adrenalineBar;
        [SerializeField] private Slider fatigueBar;

        private int _maxHealth;
        private Unit _unit;
        //set unit values here
        public void Initialize(Unit unit)
        {
            _unit = unit;
            
            unitName.text = _unit.Name;
            level.text = _unit.Level.ToString();
        }

   
        public void Update()
        {
            SetHealth();
            
            SetFatigue();
            SliderHealth();
        }
        
        public void SetHealth()
        {
            if (_unit.Health == 0)
            {
                fillImage.fillAmount = 0;
            }
            else
            {
                fillImage.fillAmount = (float) _unit.Health / _unit.MaxHealth;
            }
        }
        public void SliderHealth()
        {
            
            if (_unit.Adrenaline== 0)
            {
                adrenalineBar.value = 0;
            }
            else
            {
                adrenalineBar.value = _unit.Adrenaline;
            }
        }



        public void SetFatigue()
        {
            if (_unit.Fatigue == 0)
            {
                fatigueBar.value = 0;
            }
            else
            {
                fatigueBar.value = _unit.Fatigue;
            }
        }

    }
}