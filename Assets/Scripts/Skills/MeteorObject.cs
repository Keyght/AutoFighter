using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Класс для описания поведения метеорита
    /// </summary>
    public class MeteorObject : MonoBehaviour
    {
        [SerializeField] private GameObject _bang;
    
        private float _size;

        public float Size
        {
            get => _size;
            set => _size = value;
        }

        private void OnDestroy()
        {
            var bang = Instantiate(_bang, transform.position, Quaternion.identity);
            var mainModule = bang.GetComponent<ParticleSystem>().main;
            mainModule.startSize = _size * 2;
        }
    }
}
