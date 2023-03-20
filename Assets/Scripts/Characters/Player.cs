using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Класс для описания игрока
    /// </summary>
    public class Player : Character
    {
        private bool _isBloodLusted;
        private float _bloodLustValue;
        private float _bloodLustPercent;

        public bool IsBloodLusted
        {
            set => _isBloodLusted = value;
        }
        public float BloodLustPercent
        {
            set => _bloodLustPercent = value;
        }

        private new void Awake()
        {
            base.Awake();
            Utilits.AllEnemies = new List<Enemy>();
        }
        
        private new void Start()
        {
            base.Start();
            AttackingTag = AttackingTags.Player;
        }

        protected override void Attack(Collider2D col)
        {
            if (Time.time < LastAttackTime + (float) 1/_attackSpeed) return;
            if (Targets.Count == 0) return;
            if (col.gameObject.GetComponentInParent<Character>() != Targets[0]) return;
            LastAttackTime = Time.time;
            SetAttackAnimation(true);
            var currentChar = Targets[0];
            var directionX =  currentChar.transform.position.x - transform.position.x;
            Utilits.Flip(_body, directionX);
            if (_isBloodLusted)
            {
                _bloodLustValue = currentChar.Health.MaxHp * _bloodLustPercent;
                currentChar.Health.ChangeHealth(-1 * _bloodLustValue);
                Health.ChangeHealth(_bloodLustValue);
            }
            currentChar.Health.ChangeHealth(_damage);
        }

        protected new void OnDestroy()
        {
            base.OnDestroy();
            EndGame.Instance.GameOver();
        }
    }
}
