using System;
using System.Threading;
using System.Threading.Tasks;
using Characters;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _boss;
    [SerializeField] private float _radius;
    [SerializeField] private float _spawnTime;
    
    private CancellationTokenSource _cancelTokenSource;

    private async void Start()
    {
        _cancelTokenSource = new CancellationTokenSource();
        await Spawn(_cancelTokenSource.Token);
    }

    private async Task Spawn(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var timer = 0f;
            while (timer < _spawnTime)
            {
                timer += Time.deltaTime;
                await Task.Yield();
            }
            if (token.IsCancellationRequested)
            {
                break;
            }
            var random = new Random();
            var x  = (float) (random.NextDouble() - 0.5) * 2 * _radius;
            var y = (float) (Math.Abs(x)/x * Math.Sqrt(_radius * _radius - x * x));

            var prefab = random.NextDouble() * 100 < 10 ? _boss : _enemy;
            var minion = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            minion.GetComponent<MoveToTarget>().Target = _player;
        }
    }
    
    private void OnDestroy()
    {
        _cancelTokenSource.Cancel();
        _cancelTokenSource.Dispose();
    }
}
