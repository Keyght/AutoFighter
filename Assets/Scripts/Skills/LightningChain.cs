using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class LightningChain : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _countTime;
        [SerializeField] private Countdown _countdown;

        private void Start()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private async void OnClickButton()
        {
            _button.interactable = false;
            _countdown.CountTime = _countTime;
            await _countdown.UntilCountdown();
            _button.interactable = true;
        }
    }
}
