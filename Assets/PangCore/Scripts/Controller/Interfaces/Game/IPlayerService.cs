using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the view to the controller.
    /// </summary>
    public interface IPlayerService
    {
        public bool IsStopped { get; set; }
        public bool IsPlayerAbleToShoot();
        public Vector2 GetPlayerPosition();
        public WeaponType GetWeaponType();
    }
}