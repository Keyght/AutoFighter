using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timer;
        [SerializeField] private Image _countFill;
        private float _countTime;

        public float CountTime
        {
            set => _countTime = value;
        }
        
        public async Task UntilCountdown()
        {
            _countFill.fillAmount = 1;
            var timer = _countTime;
            while (timer > 0)
            {
                _timer.text = ((int)timer + 1).ToString();
                timer -= Time.deltaTime;
                _countFill.fillAmount = timer / _countTime;
                await Task.Yield();
            }
            _timer.ClearMesh();
        }
    }
}
