using System.Collections.Generic;
using HP;
using UnityEngine;

namespace Characters
{
    public abstract class Character : MonoBehaviour, IHealthChangable
    {
        [SerializeField] private Transform _body;
        [SerializeField] private int _maxHp;
        [SerializeField] private int _damage;
        [SerializeField] int _attackSpeed;

        private float _lastAttackTime;
        private Health _health;
        private List<Character> _targets;
        protected string AttackingTag;
        private static readonly int _attacking = Animator.StringToHash("Attacking");

        public List<Character> Targets => _targets;
        public Health Health => _health;

        private void Awake()
        {
            _health = new Health(_maxHp);
        }

        protected void Start()
        {
            _targets = new List<Character>();
            _health.onHealthChanged += OnHealthChanged;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<Character>();
            if (enemy.AttackingTag.Equals(AttackingTag)) return;
            if (!_targets.Contains(enemy)) _targets.Add(enemy);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Attack();
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<Character>();
            if (enemy.AttackingTag.Equals(AttackingTag)) return;
            if (_targets.Contains(enemy)) _targets.Remove(enemy);
        }

        private void Attack()
        {
            if (Time.time < _lastAttackTime + (float) 1/_attackSpeed) return;
            if (_targets.Count == 0) return;
            _lastAttackTime = Time.time;
            SetAttackAnimation(true);
            var currentChar = _targets[0];
            var directionX =  currentChar.transform.position.x - transform.position.x;
            Utilits.Flip(_body, directionX);
            currentChar.Health.ChangeHealth(_damage);
        }

        public void OnHealthChanged(int currentHealth, float currentHealthAsPercantage)
        {
            if (currentHealth == 0)
            {
                Destroy(gameObject);
            }
        }
        
        private void SetAttackAnimation(bool flag)
        {
            if (_body.TryGetComponent<Animator>(out var anim))
            {
                anim.SetBool(_attacking, flag);
            }
        }

        private void OnDestroy()
        {
            SetAttackAnimation(false);
            _health.onHealthChanged -= OnHealthChanged;
        }
        
       
    }
}
