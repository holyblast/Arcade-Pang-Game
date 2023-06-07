using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// a controller that helps the flow between the view and the model.
    /// checks if the player can shoot and requests the player starting position from the model.
    /// </summary>
    public class PlayerService : IPlayerService
    {
        private readonly PlayerModel _playerModel;
        private readonly GameModel _gameModel;
        public PlayerService(PlayerModel playerModel, GameModel gameModel)
        {
            _playerModel = playerModel;
            _gameModel = gameModel;
        }

        public bool IsStopped { get; set; }

        public WeaponType GetWeaponType()
        {
            return _playerModel.GetWeaponType();
        }

        public bool IsPlayerAbleToShoot()
        {
            return _playerModel.IsPlayerAbleToShoot() && !IsStopped;
        }

        public Vector2 GetPlayerPosition()
        {
            return _gameModel.GetPlayerPosition();
        }
    }
}
