using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    /// <summary>
    /// Класс для реализации всех скилов с перезарядкой
    /// </summary>
    public abstract class CountableSkill : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _countdownTime;
        [SerializeField] private Countdown _countdown;

        private CancellationTokenSource _cancellationTokenSource;
        protected abstract Task PerformSpell();

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _countdown.CountTime = _countdownTime;
            _button.onClick.AddListener(OnClickButton);
        }

        private async void OnClickButton()
        {
            _button.interactable = false;
            PerformSpell();
            await _countdown.UntilCountdown(_cancellationTokenSource.Token);
            _button.interactable = true;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }
}
