using UnityEngine;
using VContainer;

namespace PangGame
{
    public class AreaSelect : MonoBehaviour
    {
        [Inject] private IGameService _gameService;
        [Inject] private ISceneLoaderService _sceneLoaderService;

        [SerializeField] private int area;

        /// <summary>
        /// World map buttons that start the stage.
        /// we the needed data to the controller and load the game scene.
        /// </summary>
        public void AreaSelected()
        {
            _gameService.SetArea(area);
            _gameService.SetInnerArea(0);
            _sceneLoaderService.GameScreen();
        }
    }
}