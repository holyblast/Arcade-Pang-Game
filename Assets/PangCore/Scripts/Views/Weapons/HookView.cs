using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// inherits from weaponView
    /// </summary>
    public class HookView : WeaponView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            _weaponService.OnTriggerEnterEvent(_id, other.tag);
        }

        private void Update()
        {
            var destination = _weaponService.GetMovement(_id, Time.deltaTime);

            var vec = transform.localPosition;
            vec.y += destination;
            transform.localPosition = vec;
        }
    }
}
