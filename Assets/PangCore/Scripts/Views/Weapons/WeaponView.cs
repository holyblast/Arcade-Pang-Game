using System;
using UnityEngine;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// resposible for assigning an ID for use via the controller,
    /// created through a factory with memory pooling.
    /// </summary>
    public class WeaponView : MonoBehaviour, IWeaponView
    {
        [Inject] protected IWeaponService _weaponService;

        public string _id;

        public string GetId()
        {
            if (string.IsNullOrEmpty(_id))
                _id = Guid.NewGuid().ToString();

            return _id;
        }

        public void SetStartPosition(Vector2 playerPosition)
        {
            transform.localPosition = playerPosition;
        }
    }
}
