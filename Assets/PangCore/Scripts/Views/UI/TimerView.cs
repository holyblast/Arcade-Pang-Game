using TMPro;
using UnityEngine;
using VContainer;

/// <summary>
/// Displaus a timer during gameplay, stops during menu screen.
/// if the timer reaches zero - display the gameover screen
/// </summary>
namespace PangGame
{
    public class TimerView : MonoBehaviour
    {
        [Inject] private IGameService _gameService;

        private float _remainingTime;

        /// <summary>
        /// cache the string thus not creating future new strings to compare with.
        /// </summary>
        private const string TimeRemainingStringText = "Time: ";

        private TextMeshProUGUI _textMeshProUGUI;
        // Start is called before the first frame update
        void Start()
        {
            _remainingTime = _gameService.GetAreaInnerData().time;
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            UpdateText();
        }


        void Update()
        {
            if (_gameService.stopped) return;

            _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0f)
                _gameService.SetLoseCondition();

            UpdateText();
        }

        private void UpdateText()
        {
            _textMeshProUGUI.text = $"{TimeRemainingStringText}{_remainingTime.ToString("F2")}";
        }
    }
}