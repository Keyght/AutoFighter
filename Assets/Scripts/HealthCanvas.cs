using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private Image _poisonIcon;
    [SerializeField] private TextMeshProUGUI _healthCount;
    [SerializeField] private TextMeshProUGUI _additionalHealthCount;
    [SerializeField] private Gradient _gradient;

    private Character _character;
    private Health _health;

    private void Start()
    {
        _health = _character.Health;
        _health.HealthChanged += OnHealthChanged;
    }

    public void OnHealthChanged(int currentHealth, float currentHealthAsPercantage)
    {
        _healthCount.text = currentHealth.ToString();
        _healthCount.color = _gradient.Evaluate(currentHealthAsPercantage);

        _healthBarFilling.fillAmount = currentHealthAsPercantage;
        _healthBarFilling.color = _gradient.Evaluate(currentHealthAsPercantage);
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}
