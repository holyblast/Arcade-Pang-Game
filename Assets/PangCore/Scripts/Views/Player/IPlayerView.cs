using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the view.
    /// </summary>
    public interface IPlayerView
    {
        public void SetStartLocation(Vector2 startLocation);
    }
}