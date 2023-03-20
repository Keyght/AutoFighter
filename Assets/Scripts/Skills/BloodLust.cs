using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    /// <summary>
    /// Класс для реализации жажды крови
    /// </summary>
    public class BloodLust : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _frame;
        [SerializeField] private Player _player;
        [SerializeField] private int _bloodLustPercent;
        
        private bool _isBloodLusteded;

        private void Start()
        {
            _isBloodLusteded = false;
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            _isBloodLusteded = !_isBloodLusteded;
            _frame.SetActive(_isBloodLusteded);

            _player.BloodLustPercent = (float) _bloodLustPercent / 100;
            _player.IsBloodLusted = _isBloodLusteded;
        }
    }
}
