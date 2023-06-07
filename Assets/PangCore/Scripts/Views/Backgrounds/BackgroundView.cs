using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// The class swaps between a list of sprites set up in the inspector
    /// </summary>
    public class BackgroundView : MonoBehaviour
    {
        /// <summary>
        /// DI injection
        /// </summary>
        [Inject] private IGameService _gameService;

        [SerializeField] private List<SpriteRenderer> _backgrounds;

        void Awake()
        {
            // request the data through a controller
            var indexToActivate = (int)_gameService.GetAreaData().backgroundType - 1;

            // with data set the view accordingly.
            _backgrounds[indexToActivate].gameObject.SetActive(true);
            _backgrounds[indexToActivate].color = _gameService.GetAreaInnerData().backgroundColor;
        }
    }
}