using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// Responsible for the UI throughout the game scene along with the cancel input button that is acts as the menu activation.
    /// </summary>
    public class UIGameView : MonoBehaviour
    {
        [SerializeField] private InputActionReference _backButtonAction;
        [SerializeField] private GameObject _menuUI;
        [SerializeField] private GameObject _areaUI;
        [SerializeField] private GameObject _innerAreaUI;
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private GameObject _volume;

        [Inject] private ISceneLoaderService _sceneLoaderService;
        [Inject] private IEnemyService _enemyService;
        [Inject] private IGameService _gameService;
        [Inject] private IPlayerService _playerService;

        private bool ignore = false;

        private void OnEnable()
        {
            _backButtonAction.action.performed += backButtonAction;
            _backButtonAction.action.Enable();
        }

        private void OnDisable()
        {
            _backButtonAction.action.performed -= backButtonAction;
            _backButtonAction.action.Disable();
        }

        /// <summary>
        /// responisble for checking if the game has ended and displays the result accordingly.
        /// 1. win area with further stages in the same area
        /// 2. win the last part of the area
        /// 3. lose
        /// </summary>
        private void Update()
        {
            if (ignore || _gameService.stopped) return;

            GameResultType gameResult = _gameService.CheckConditions();

            if (gameResult != GameResultType.None)
            {
                ignore = true;
                _enemyService.SetStopState(true);
                _playerService.IsStopped = true;
                _gameService.stopped = true;
                _backButtonAction.action.Disable();

                _gameService.SetAreaData();
                switch(gameResult)
                {
                    case GameResultType.Lose:
                        GameOver();
                        break;
                    case GameResultType.WinAreaInner:
                        NextInnerArea();
                        break;
                    case GameResultType.WinArea:
                        NextArea();
                        break;
                }
            }
        }

        private void backButtonAction(InputAction.CallbackContext obj)
        {
            SetMenuState();
        }

        public void SetMenuState()
        {
            var isActive = !_menuUI.activeSelf;
            _menuUI.SetActive(isActive);
            _volume.SetActive(isActive);

            _enemyService.SetStopState(isActive);
            _playerService.IsStopped = isActive;
            _gameService.stopped = isActive;
        }

        public void NextArea()
        {
            _areaUI.SetActive(true);
            _volume.SetActive(true);
        }

        public void NextInnerArea()
        {
            _innerAreaUI.SetActive(true);
            _volume.SetActive(true);
        }

        public void GameOver()
        {
            _gameOverUI.SetActive(true);
            _volume.SetActive(true);
        }

        public void RestartScene()
        {
            _sceneLoaderService.GameScreen();
        }

        public void GoToTitleScreen()
        {
            _sceneLoaderService.TitleScreen();
        }

        public void Quit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
