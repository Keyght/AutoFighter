using System;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

namespace Characters
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _target;
        [SerializeField] private int _stopRadius;
        [SerializeField] private float _speed;
        
        private static readonly int _walking = Animator.StringToHash("Walking");

        public Transform Target
        {
            set => _target = value;
        }

        private async void Start()
        {
            Move();
        }

        private async Task Move()
        {
            var direction = _target.transform.position - transform.position;
            if (direction.x < 0)
            {
                var scale = _body.localScale;
                _body.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
            }
            while (direction.magnitude > _stopRadius)
            {
                SetWalkAnimation(true);
                transform.Translate(direction.normalized * 100 * _speed * Time.deltaTime);
                await Task.Yield();
            }
            SetWalkAnimation(false);
        }

        private void SetWalkAnimation(bool flag)
        {
            if (_body.TryGetComponent<Animator>(out var anim))
            {
                anim.SetBool(_walking, flag);
            }
        }
    }
}
