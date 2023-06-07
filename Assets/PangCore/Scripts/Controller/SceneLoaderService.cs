using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// responsible for changing scenes
    /// </summary>
    public class SceneLoaderService : ISceneLoaderService
    {
        private const short Title = 0;
        private const short Game = 1;

        public void TitleScreen()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Title);
        }

        public void GameScreen()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Game);
        }
    }
}
