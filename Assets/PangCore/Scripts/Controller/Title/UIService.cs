using UnityEngine;

namespace PangGame
{
    public class UIService : IUIService
    {
        private GameModel _gameModel;

        public UIService(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public int GetPlayerStageProgress()
        {
            return _gameModel.HighestStageReached;
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
