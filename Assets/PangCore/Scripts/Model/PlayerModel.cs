using VContainer;

namespace PangGame
{
    /// <summary>
    /// the model representative of the player
    /// </summary>
    public class PlayerModel
    {
        [Inject] private IWeaponService _weaponService;
        [Inject] private GameModel _gameModel;
        private WeaponType _weaponType = WeaponType.Hook;

        public PlayerModel(IWeaponService weaponService, GameModel gameModel)
        {
            _weaponService = weaponService;
        }
        
        /// <summary>
        /// is the player able to shoot? did he reach his maximum in field effect?
        /// </summary>
        /// <returns></returns>
        public bool IsPlayerAbleToShoot()
        {
            var weaponCount = _weaponService.GetWeaponsCountInEffect(_weaponType);

            var weaponUsageMeetsConditions = weaponCount <_gameModel.GetMaxWeaponUsageOfType(_weaponType);

            return weaponUsageMeetsConditions;
        }

        public WeaponType GetWeaponType()
        {
            return _weaponType;
        }
    }
}
