using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the view.
    /// </summary>
    public interface IWeaponView
    {
        public string GetId();
        public void SetStartPosition(Vector2 playerPosition);
    }
}
