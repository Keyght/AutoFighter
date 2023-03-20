using System.Collections.Generic;
using HP;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Базовый класс для всех персонажей, которые могут атаковать и имеют здоровье
    /// </summary>
    public abstract class Character : MonoBehaviour, IHealthChangable
    {
        [SerializeField] protected Transform _body;
        [SerializeField] private int _maxHp;
        [SerializeField] protected float _damage;
        [SerializeField] protected int _attackSpeed;

        protected float LastAttackTime;
        private Health _health;
        private List<Character> _targets;
        private AttackingTags _attackingTag;
        private static readonly int _attacking = Animator.StringToHash("Attacking");

        public List<Character> Targets => _targets;
        public Health Health => _health;
        public AttackingTags AttackingTag
        {
            get => _attackingTag;
            set => _attackingTag = value;
        }

        protected abstract void Attack(Collider2D col);

        protected void Awake()
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

        protected void OnTriggerStay2D(Collider2D col)
        {
            Attack(col);
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            var enemy = col.GetComponentInParent<Character>();
            if (enemy.AttackingTag.Equals(AttackingTag)) return;
            if (_targets.Contains(enemy)) _targets.Remove(enemy);
        }

        public void OnHealthChanged(DamageData damageData)
        {
            if (damageData.CurrentHp == 0)
            {
                Destroy(gameObject);
            }
        }

        protected void SetAttackAnimation(bool flag)
        {
            if (_body.TryGetComponent<Animator>(out var anim))
            {
                anim.SetBool(_attacking, flag);
            }
        }

        protected void OnDestroy()
        {
            
            SetAttackAnimation(false);
            _health.onHealthChanged -= OnHealthChanged;
        }
    }
}
