using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Класс для непрерывного двеижения объекта к цели
    /// </summary>
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private int _stopRadius;
        [SerializeField] private float _speed;

        private Transform _target;
        private CancellationTokenSource _cancelTokenSource;
        private static readonly int _walking = Animator.StringToHash("Walking");

        public int StopRadius => _stopRadius;
        public CancellationTokenSource CancellationTokenSource
        {
            get => _cancelTokenSource;
            set => _cancelTokenSource = value;
        }

        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        private async void Start()
        {
            GetComponent<CircleCollider2D>().radius = _stopRadius;
            _cancelTokenSource = new CancellationTokenSource(); 
            Move(_cancelTokenSource.Token);
        }

        public async Task Move(CancellationToken token) 
        {
            try
            {
                var direction = _target.transform.position - transform.position;
                Utilits.Flip(_body, direction.x);
                while (direction.magnitude > _stopRadius && !token.IsCancellationRequested)
                {
                    direction = _target.transform.position - transform.position;
                    SetWalkAnimation(true);
                    transform.Translate(direction.normalized * _body.transform.localScale.y * _speed * Time.deltaTime);
                    await Task.Yield();
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }

                SetWalkAnimation(false);
                if (Target.TryGetComponent<MoveToTarget>(out var moveComponent))
                {
                    moveComponent.Target = transform;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                SetWalkAnimation(false);
            }
        }

        public async Task ReMove()
        {
            CancellationTokenSource.Cancel();
            await Task.Delay(100);
            CancellationTokenSource.Dispose();
            CancellationTokenSource = new CancellationTokenSource();
            await Move(CancellationTokenSource.Token);
        }

        private void SetWalkAnimation(bool flag)
        {
            if (_body.TryGetComponent<Animator>(out var anim))
            {
                anim.SetBool(_walking, flag);
            }
        }

        private void OnDestroy()
        {
            SetWalkAnimation(false);
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }
    }
}
