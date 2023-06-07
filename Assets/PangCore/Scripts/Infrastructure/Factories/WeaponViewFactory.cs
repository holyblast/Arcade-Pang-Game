using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PangGame
{
    /// <summary>
    /// A factory to generate weapons and store them in a memory pool for future usage
    /// </summary>
    public class WeaponViewFactory : IWeaponViewFactory
    {
        private readonly GameObject _weaponPrefab;
        private readonly Transform _weaponsParent;
        private Stack<IWeaponView> _weaponViewMemoryPool;

        private readonly IObjectResolver _container;

        public WeaponViewFactory(IObjectResolver container, GameObject weaponPrefab, Transform weaponsParent)
        {
            _container = container;
            _weaponPrefab = weaponPrefab;
            _weaponsParent = weaponsParent;
            _weaponViewMemoryPool = new Stack<IWeaponView>();
        }

        public IWeaponView CreateIWeaponView()
        {
            if (_weaponViewMemoryPool.Count > 0)
            {
                HookView hook = (HookView)_weaponViewMemoryPool.Pop();
                hook.transform.SetParent(_weaponsParent);
                hook.gameObject.SetActive(true);
                return hook;
            }

            GameObject newHook = _container.Instantiate(_weaponPrefab, _weaponsParent);

            HookView hookView = newHook.GetComponent<HookView>();

            return hookView;
        }

        public void ReturnIWeaponViewToPool(IWeaponView weapon)
        {
            ((WeaponView)weapon).gameObject.SetActive(false);
            _weaponViewMemoryPool.Push(weapon);
        }
    }
}