using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the view to the controller.
    /// </summary>
    public interface IEnemyService
    {
        public int GetMonsterCount();
        public float GetMovement(string id, float deltaTime);
        public void Bounce(string id, float y);
        public void SplitBubble(string id);
        public void TriggerPopAnimation(string id);
        public void OnTriggerEnterEvent(string id, string tag);
        public void SetStopState(bool state);
        public void LoseCondition();
    }
}
