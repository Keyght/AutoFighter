using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        [SerializeField] private bool _isBoss;

        public bool IsBoss
        {
            get => _isBoss;
            set => _isBoss = value;
        }

        private new void Start()
        {
            base.Start();
            Utilits.AllEnemies.Add(this);
            AttackingTag = AttackingTags.Enemy;
        }

        protected override void Attack(Collider2D col)
        {
            if (Time.time < LastAttackTime + (float) 1/_attackSpeed) return;
            if (Targets.Count == 0) return;
            if (col.GetComponentInParent<Character>() != Targets[0]) return;
            LastAttackTime = Time.time;
            SetAttackAnimation(true);
            var currentChar = Targets[0];
            var directionX =  currentChar.transform.position.x - transform.position.x;
            Utilits.Flip(_body, directionX);
            currentChar.Health.ChangeHealth(_damage);
        }

        protected new void OnDestroy()
        {
            base.OnDestroy();
            Utilits.AllEnemies.Remove(this);
        }
    }
}
