using UnityEngine;

namespace PangGame
{
    public interface IWeaponViewFactory
    {
        public IWeaponView CreateIWeaponView();
        public void ReturnIWeaponViewToPool(IWeaponView weapon);
    }
}
