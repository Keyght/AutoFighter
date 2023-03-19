using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int, float> HealthChanged;

    private int _maxHp;
    private int _currentHp;

    public int CurrentHp => _currentHp;

    public Health(int maxHp)
    {
        _maxHp = maxHp;
        _currentHp = maxHp;
    }

    public void ChangeHealth(int value, bool isDamage)
    {
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            Death();
        }
        else if (_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
            InvokeChanges();
        }
        else
        {
            InvokeChanges();
        }
    }

    private void InvokeChanges()
    {
        var currentHealthAsPercantage = (float)_currentHp / _maxHp;
        HealthChanged?.Invoke(_currentHp, currentHealthAsPercantage);
    }

    private void Death()
    {
        HealthChanged?.Invoke(0, 0);
    }
}
