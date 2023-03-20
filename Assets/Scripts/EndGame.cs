using TMPro;
using UnityEngine;

/// <summary>
/// Класс для завершения игры
/// </summary>
public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _endCanvas;
    [SerializeField] private TextMeshProUGUI _text;
    
    private float _startTime;
    private static EndGame _instacne;

    public static EndGame Instance => _instacne;

    private void Start()
    {
        _instacne = this;
        _startTime = Time.time;
    }
    
    public static void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void GameOver()
    {
        _endCanvas.SetActive(true);
        var timer = (Time.time - _startTime);
        var min = Mathf.Floor(timer / 60);
        var sec = timer%60;
        _text.SetText(_text.text + " " + min + " min "  + sec + " sec");
    }
}