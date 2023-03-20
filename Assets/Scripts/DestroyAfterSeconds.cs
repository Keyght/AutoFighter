using UnityEngine;

/// <summary>
/// Класс для всех объектов, которые уничтожаются спустя время
/// </summary>
public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    
    private void Start()
    {
        StartCoroutine(Utilits.LifeRoutine(_lifeTime, gameObject));
    }
}
