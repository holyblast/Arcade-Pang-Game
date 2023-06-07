using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// the represenative of the enemy's data, it deals with all the calculations and collision detections the enemy has.
    /// </summary>
    public class EnemyModel
    {
        public Vector2 Velocity { get; private set; }

        private string _id;
        private BubbleSizeType _bubbleSizeType;
        private MoveDirectionType _moveDirectionType;

        private IEnemyService _enemyService;

        private const string Walls = "Walls";
        private const string Ground = "Ground";
        
        private const string Hook = "Hook";
        private const string Player = "Player";

        public EnemyModel(IEnemyService enemyService, string id, MoveDirectionType moveDirectionType, BubbleSizeType bubbleSizeType)
        {
            _enemyService = enemyService;
            _id = id;
            _moveDirectionType = moveDirectionType;
            _bubbleSizeType = bubbleSizeType;
        }

        public void ChangeDirection()
        {
            var direction = (int)_moveDirectionType;
            direction = -direction;
            _moveDirectionType = (MoveDirectionType)direction;
        }

        public MoveDirectionType GetPopReverseDirection(MoveDirectionType popdirection)
        {
            var direction = (int)popdirection;
            direction = -direction;
            return (MoveDirectionType)direction;
        }

        public float GetMovement(float deltaTime)
        {
            var movement = Velocity.x * deltaTime * (int)_moveDirectionType;
            return movement;
        }

        public BubbleSizeType GetBubbleSize()
        {
            return _bubbleSizeType;
        }

        public MoveDirectionType GetMovementDirection()
        {
            return _moveDirectionType;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void HandleCollision(string tag)
        {
            switch (tag)
            {
                case Ground: HandleGroundCollision(); break;
                case Walls: HandleWallCollision(); break;
                case Player: _enemyService.LoseCondition(); break;
                case Hook: HandleHookCollision(); break;
            }
        }

        private void HandleGroundCollision()
        {
            _enemyService.Bounce(_id, Velocity.y);
        }

        private void HandleWallCollision()
        {
            ChangeDirection();
        }

        private void HandleHookCollision()
        {
            if (_bubbleSizeType == BubbleSizeType.None) return; 
            _bubbleSizeType -= 1;
            _enemyService.TriggerPopAnimation(_id);
        }
    }
}
