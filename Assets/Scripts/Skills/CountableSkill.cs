using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public abstract class CountableSkill : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _countdownTime;
        [SerializeField] private Countdown _countdown;

        protected abstract Task PerformSpell();
        
        private void Start()
        {
            _countdown.CountTime = _countdownTime;
            _button.onClick.AddListener(OnClickButton);
        }

        private async void OnClickButton()
        {
            _button.interactable = false;
            PerformSpell();
            await _countdown.UntilCountdown();
            _button.interactable = true;
        }
    }
}
