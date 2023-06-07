using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    public class ScreenToWorldService : IScreenToWorldService
    {
        private Camera _main;
        public ScreenToWorldService() 
        {
            _main = Camera.main;
        }

        public bool IsColliderHit(Vector2 touchPosition, out GameObject gameObject)
        {
            gameObject = null;

            Vector2 worldPosition = _main.ScreenToWorldPoint(touchPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                gameObject = hit.collider.gameObject;
                return true;
            }

            return false;
        }
    }
}
