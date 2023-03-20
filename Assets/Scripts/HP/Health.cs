using System;

namespace HP
{
    public class Health
    {
        public event Action<DamageData> onHealthChanged;

        private readonly int _maxHp;
        private float _currentHp;

        public int MaxHp => _maxHp;
        public float CurrentHp => _currentHp;

        public Health(int maxHp)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
        }

        public void ChangeHealth(float value)
        {
            _currentHp += value;
            
            if (_currentHp <= 0)
            {
                _currentHp = 0;
            }
            else if (_currentHp > _maxHp)
            {
                _currentHp = _maxHp;
            }
            
            InvokeChanges();
        }

        public void InvokeChanges()
        {
            var currentHealthAsPercantage =  _currentHp / _maxHp;
            onHealthChanged?.Invoke(new DamageData(_currentHp, currentHealthAsPercantage));
        }
    }
}
