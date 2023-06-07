using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the view to the controller.
    /// </summary>
    public interface IWeaponService
    {
        public float GetMovement(string id, float deltaTime);
        public int GetWeaponsCountInEffect(WeaponType weaponType);
        public void OnTriggerEnterEvent(string id, string tag);
        public void ReturnWeaponToMemoryPool(string id, WeaponType weaponType);
        public void SpawnHook(Vector2 startPosition, WeaponType weaponType);
    }
}