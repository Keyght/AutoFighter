using System;
using System.Threading;
using System.Threading.Tasks;
using Characters;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Класс для создания врагов и направления их к игроку
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _boss;
    [SerializeField] private float _radius;
    [SerializeField] private float _spawnTime;
    [SerializeField] private int _bossProbability;

    private CancellationTokenSource _cancelTokenSource;

    private void Awake()
    {
        _cancelTokenSource = new CancellationTokenSource();
    }

    private async void Start()
    {
        await Spawn(_cancelTokenSource.Token);
    }

    private async Task Spawn(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var timer = 0f;
            while (timer < _spawnTime + 0.01)
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

            GameObject prefab;
            bool isBoss;
            if (random.NextDouble() * 100 < _bossProbability)
            {
                prefab = _boss;
                isBoss = true;
            }
            else
            {
                prefab = _enemy;
                isBoss = false;
            }
            
            var minion = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            minion.GetComponent<Enemy>().IsBoss = isBoss;
            minion.GetComponent<MoveToTarget>().Target = _player;
        }
    }
    
    public static void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    protected void OnDestroy()
    {
        _cancelTokenSource.Cancel();
        _cancelTokenSource.Dispose();
    }
}
