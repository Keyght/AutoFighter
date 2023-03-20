using System;
using UnityEngine;

namespace Characters
{
    public class Player : Character
    {
        [SerializeField] private GameObject _endCanvas;
        
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

        private new void Start()
        {
            base.Start();
            AttackingTag = AttackingTags.Player;
        }

        protected override void Attack()
        {
            if (Time.time < LastAttackTime + (float) 1/_attackSpeed) return;
            if (Targets.Count == 0) return;
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
            _endCanvas.SetActive(true);
        }
    }
}
