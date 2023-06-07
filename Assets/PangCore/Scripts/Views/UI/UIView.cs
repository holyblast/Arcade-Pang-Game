using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// Responsible for the UI throughout the Title scene.
    /// </summary>
    public class UIView : MonoBehaviour
    {
        [Inject] IUIService _uiController;

        [SerializeField] private List<GameObject> _deactivateOnStageSelect;
        [SerializeField] private List<GameObject> _activateOnStageSelect;
        [SerializeField] private List<GameObject> _stagesActivated;


        /// <summary>
        /// checks data world map progress through the controller.
        /// </summary>
        public void OnClickStageSelectUI()
        {
            var currentStageProgress = _uiController.GetPlayerStageProgress();
            SetAccessableStages(currentStageProgress);
            SetTitleMenu(false);
        }

        public void OnClickMenu()
        {
            SetTitleMenu(true);
        }

        public void OnClickQuit()
        {
            _uiController.Quit();
        }

        /// <summary>
        /// Displays/Hides certain gameobjects if the select stage option has been pressed.
        /// </summary>
        /// <param name="state"></param>
        private void SetTitleMenu(bool state)
        {
            for (int i = 0; i < _deactivateOnStageSelect.Count; i++)
            {
                _deactivateOnStageSelect[i].SetActive(state);
            }
            for (int i = 0; i < _activateOnStageSelect.Count; i++)
            {
                _activateOnStageSelect[i].SetActive(!state);
            }
        }

        /// <summary>
        /// limits your exposure to further areas if you have not completed the areas.
        /// </summary>
        private void SetAccessableStages(int currentMaxAccessableArea)
        {
            for (int i = 0; i < currentMaxAccessableArea; i++)
            {
                _stagesActivated[i].SetActive(true);
            }
        }
    }
}
