using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ForCheck
{
    /// <summary>
    /// Класс для роботоспостобности движения к цели в тестовых сценах
    /// </summary>
    public class MoveToTargetCheck : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private int _stopRadius;
        [SerializeField] private float _speed;

        [SerializeField] private Transform _target;
        private CancellationTokenSource _cancelTokenSource;
        private static readonly int _walking = Animator.StringToHash("Walking");

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

        private void Start()
        {
            _cancelTokenSource = new CancellationTokenSource(); 
            //await Move(_cancelTokenSource.Token);
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
                await Task.Yield();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                SetWalkAnimation(false);
            }
        }

        private void SetWalkAnimation(bool flag)
        {
            if (_body.TryGetComponent<Animator>(out var anim))
            {
                anim.SetBool(_walking, flag);
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

        private void OnDestroy()
        {
            SetWalkAnimation(false);
            _cancelTokenSource.Cancel();
            _cancelTokenSource.Dispose();
        }
    }
}
