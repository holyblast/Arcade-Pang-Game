using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// The class is responsible for moving if the required input was met (Joystick UI bound to a gamepad)
    /// It also checks if the player can currently take another shot or not.
    /// </summary>
    public class PlayerView : MonoBehaviour
    {
        [Inject] private IPlayerService _playerService;
        [Inject] private IWeaponService _weaponService;

        [SerializeField] private InputActionReference _playerOnScreenJoystick;
        private Rigidbody2D _rigidbody;

        /// <summary>
        /// get the starting point through the controller
        /// </summary>
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            transform.localPosition = _playerService.GetPlayerPosition();
        }

        private void OnEnable()
        {
            _playerOnScreenJoystick.action.Enable();
        }

        private void OnDisable()
        {
            _playerOnScreenJoystick.action.Disable();
        }

        /// <summary>
        /// Allow movement so long as a menu isn't open.
        /// </summary>
        private void Update()
        {
            if (_playerService.IsStopped) return;

            Vector3 moveTo = _playerOnScreenJoystick.action.ReadValue<Vector2>();
            moveTo.y = 0;
            moveTo *= Time.deltaTime * 10;
            moveTo += transform.position;
            _rigidbody.MovePosition(moveTo);
        }

        /// <summary>
        /// Attempt to shoot a hook if one doesn't exist or if a menu isn't currently open.
        /// </summary>
        public void ShootAttempt()
        {
            var canShoot = _playerService.IsPlayerAbleToShoot();
            if (canShoot)
            {
                var weaponType = _playerService.GetWeaponType();
                Vector2 playerPosition = transform.localPosition;
                playerPosition.y -= transform.localScale.y;
                _weaponService.SpawnHook(playerPosition, weaponType);
            }
        }
    }
}
