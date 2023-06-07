using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    // <summary>
    /// Responsible for generating and maintaining the MVC pattern betwen the model and the view
    /// it is responsible for creating both the view and the model through the factory and properlly
    /// stores them in a memory pool for future usage.
    /// </summary>
    public class WeaponService : IWeaponService
    {
        private IWeaponViewFactory _weaponViewFactory;
        private Dictionary<string, IWeaponView> _weaponViews = new Dictionary<string, IWeaponView>();
        private Dictionary<string, WeaponModel> _weaponModels = new Dictionary<string, WeaponModel>();
        private Dictionary<int, int> _weaponsInEffectCount = new Dictionary<int, int>();

        public WeaponService(IWeaponViewFactory weaponViewFactory)
        {
            _weaponViewFactory = weaponViewFactory;

            _weaponsInEffectCount.Add((int)WeaponType.Hook, 0);
        }

        /// <summary>
        /// returns the movement for the view to use.
        /// </summary>
        public float GetMovement(string id, float deltaTime)
        {
            if (!_weaponModels.TryGetValue(id, out WeaponModel model)) return 0f;

            var movement = model.GetMovement(deltaTime);
            return movement;
        }

        public void OnTriggerEnterEvent(string id, string tag)
        {
            if (!_weaponModels.TryGetValue(id, out WeaponModel model)) return;

            model.HandleCollision(tag);
        }

        public int GetWeaponsCountInEffect(WeaponType weaponType)
        {
            var weaponCount = 0;

            _weaponsInEffectCount.TryGetValue((int)weaponType, out weaponCount);

            return weaponCount;
        }

        
        /// <summary>
        /// Spawns the hook under the user's feet.
        /// </summary>
        public void SpawnHook(Vector2 startPosition, WeaponType weaponType)
        {
            IWeaponView view = _weaponViewFactory.CreateIWeaponView();
            var id = view.GetId();
            view.SetStartPosition(startPosition);
            _weaponViews.Add(id, view);

            _weaponsInEffectCount[((int)weaponType)]++;

            WeaponModel model = new WeaponModel(this, id, weaponType);
            _weaponModels.Add(id, model);
        }

        /// <summary>
        /// return the class to the memory pool for future usage
        /// </summary>
        public void ReturnWeaponToMemoryPool(string id, WeaponType weaponType)
        {
            if (!_weaponViews.TryGetValue(id, out IWeaponView view)) return;
            if (!_weaponModels.TryGetValue(id, out WeaponModel model)) return;

            _weaponsInEffectCount[((int)weaponType)]--;

            _weaponViewFactory.ReturnIWeaponViewToPool(view);
            _weaponModels.Remove(id);
            _weaponViews.Remove(id);
        }
    }
}
