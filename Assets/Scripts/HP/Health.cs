using System;

namespace HP
{
    public class Health
    {
        public event Action<int, float> onHealthChanged;

        private readonly int _maxHp;
        private int _currentHp;

        public int CurrentHp => _currentHp;

        public Health(int maxHp)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
        }

        public void ChangeHealth(int value)
        {
            _currentHp += value;
            
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

        public void InvokeChanges()
        {
            var currentHealthAsPercantage = (float) _currentHp / _maxHp;
            onHealthChanged?.Invoke(_currentHp, currentHealthAsPercantage);
        }

        private void Death()
        {
            onHealthChanged?.Invoke(0, 0);
        }
    }
}
