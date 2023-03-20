using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HP
{
    public class HealthCanvas : MonoBehaviour, IHealthChangable
    {
        [SerializeField] private Image _healthBarFilling;
        [SerializeField] private TextMeshProUGUI _healthCount;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Character _character;
        private Health _health;

        private void Start()
        {
            _health = _character.Health;
            _health.onHealthChanged += OnHealthChanged;
            _health.InvokeChanges();
        }

        public void OnHealthChanged(DamageData damageData)
        {
            _healthCount.text = damageData.CurrentHp.ToString();
            _healthCount.color = _gradient.Evaluate(damageData.CurrentHpAsPercantage);

            _healthBarFilling.fillAmount = damageData.CurrentHpAsPercantage;
            _healthBarFilling.color = _gradient.Evaluate(damageData.CurrentHpAsPercantage);
        }

        private void OnDestroy()
        {
            _health.onHealthChanged -= OnHealthChanged;
        }
    }
}
