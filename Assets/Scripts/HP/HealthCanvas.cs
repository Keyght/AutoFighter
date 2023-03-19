using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HP
{
    public class HealthCanvas : MonoBehaviour
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

        private void OnHealthChanged(int currentHealth, float currentHealthAsPercantage)
        {
            _healthCount.text = currentHealth.ToString();
            _healthCount.color = _gradient.Evaluate(currentHealthAsPercantage);

            _healthBarFilling.fillAmount = currentHealthAsPercantage;
            _healthBarFilling.color = _gradient.Evaluate(currentHealthAsPercantage);
        }

        private void OnDestroy()
        {
            _health.onHealthChanged -= OnHealthChanged;
        }
    }
}
