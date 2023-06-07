
namespace PangGame
{
    /// <summary>
    /// The represenative of all the weapon data types,
    /// is responsible for checking the collision and address the collisions.
    /// </summary>
    public class WeaponModel
    {
        private const string Top = "Top";
        private const string Enemy = "Enemy";

        private string _id;
        private const float weaponSpeed = 20f;
        
        private IWeaponService _weaponService;
        private WeaponType _weaponType;

        public WeaponModel(IWeaponService weaponService, string id, WeaponType weaponType)
        {
            _weaponService = weaponService;
            _id = id;
            _weaponType = weaponType;
        }

        public float GetMovement(float deltaTime)
        {
            var movement = weaponSpeed * deltaTime;
            return movement;
        }

        public void HandleCollision(string tag)
        {
            switch (tag)
            {
                case Enemy:
                case Top: _weaponService.ReturnWeaponToMemoryPool(_id, _weaponType); break;
            }
        }
    }
}
