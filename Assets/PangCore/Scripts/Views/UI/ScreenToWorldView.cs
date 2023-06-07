using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// Converts the screen touch point to a world point.
    /// </summary>
    public class ScreenToWorldView : MonoBehaviour
    {
        [Inject] private IScreenToWorldService _screenToWorldService;

        [SerializeField] private InputActionReference _primaryTouchPressed;
        [SerializeField] private InputActionReference _primaryTouchPosition;
        private GameObject _collider;

        /// <summary>
        /// cache the string thus not creating future new strings to compare with.
        /// </summary>
        private const string AreaSelect = "AreaSelect"; 

        private void OnEnable()
        {
            _primaryTouchPressed.action.performed += OnTouchPressed;
            _primaryTouchPressed.action.Enable();
            _primaryTouchPosition.action.Enable();
        }

        private void OnDisable()
        {
            _primaryTouchPressed.action.performed -= OnTouchPressed;
            _primaryTouchPressed.action.Disable();
            _primaryTouchPosition.action.Disable();
        }

        /// <summary>
        /// In the case that a collider was hit
        /// check if it meets one of the tags.
        /// </summary>
        private void OnTouchPressed(InputAction.CallbackContext obj)
        {
            var touchPosition = _primaryTouchPosition.action.ReadValue<Vector2>();
            var isColliderHit = _screenToWorldService.IsColliderHit(touchPosition, out _collider);

            if (!isColliderHit) return;

            if (_collider.tag.Equals(AreaSelect, System.StringComparison.OrdinalIgnoreCase))
                _collider.GetComponent<AreaSelect>().AreaSelected();
        }
    }
}
