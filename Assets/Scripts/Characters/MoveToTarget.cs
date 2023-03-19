using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Characters
{
    public class MoveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private int _stopRadius;
        [SerializeField] private float _speed;

        private Transform _target;
        private CancellationTokenSource _cancelTokenSource;
        private static readonly int _walking = Animator.StringToHash("Walking");

        public Transform Target
        {
            set => _target = value;
        }

        private async void Start()
        {
            _cancelTokenSource = new CancellationTokenSource(); 
            await Move(_cancelTokenSource.Token);
        }

        private async Task Move(CancellationToken token)
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
