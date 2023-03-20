using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    
    private void Start()
    {
        StartCoroutine(Utilits.LifeRoutine(_lifeTime, gameObject));
    }
}
