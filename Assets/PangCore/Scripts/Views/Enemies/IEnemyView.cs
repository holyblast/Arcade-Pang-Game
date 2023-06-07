using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the view.
    /// </summary>
    public interface IEnemyView
    {
        public string GetId();
        public Vector2 GetPosition();
        public void SetPosition(Vector2 position);
        public void SetVelocity(float height);
        public void SetScale(float bounds);
        public void SetColor(Color32 color);
        public Color32 GetColor();
        public void TriggerPopAnimation();
        public void SetFirstAnimationFrame();
        public void StopState(bool state);
        public void SetFreezeZorAll(bool state);
    }
}