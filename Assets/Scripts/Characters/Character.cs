using System;
using HP;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private int _damage;
        [SerializeField] int _attackSpeed;

        private float _lastAttackTime;
        private Health _health;
        private Character _target;

        public Character Target
        {
            get => _target;
            set => _target = value;
        }

        public Health Health => _health;

        private void Awake()
        {
            _health = new Health(_maxHp);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log(col.gameObject);
            if (_target is null) return;
            if (Time.time < _lastAttackTime + (float) 1/_attackSpeed) return;
            _lastAttackTime = Time.time;
            Attack(_target.Health);
        }

        private void Attack(Health health)
        {
            health.ChangeHealth(_damage);
        }
    }
}
