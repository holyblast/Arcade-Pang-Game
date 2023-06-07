using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace PangGame
{
    /// <summary>
    /// Responsible for generating and maintaining the MVC pattern betwen the model and the view
    /// it is responsible for creating both the view and the model through the factory and properlly
    /// stores them in a memory pool for future usage.
    /// </summary>
    public class EnemyService : IEnemyService, IStartable
    {
        [Inject] private GameModel _gameModel;
        private IEnemyViewFactory _enemyViewFactory;

        private Dictionary<string, IEnemyView> _enemyViews = new Dictionary<string, IEnemyView>();
        private Dictionary<string, EnemyModel> _enemyModels = new Dictionary<string, EnemyModel>();

        public EnemyService(IEnemyViewFactory enemyViewFactory, GameModel gameModel)
        {
            _enemyViewFactory = enemyViewFactory;
            _gameModel = gameModel;
        }

        /// <summary>
        /// returns the movement for the view to use.
        /// </summary>
        public float GetMovement(string id, float deltaTime)
        {
            if (!_enemyModels.TryGetValue(id, out EnemyModel model)) return 0f;

            var movement = model.GetMovement(deltaTime);
            return movement;
        }

        public void OnTriggerEnterEvent(string id, string tag)
        {
            if (!_enemyModels.TryGetValue(id, out EnemyModel model)) return;

            model.HandleCollision(tag);
        }

        /// <summary>
        /// A command ran by Vcontainer's DI.
        /// an entry point that mimics the Unity engine's start command.
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < _gameModel.GetEnemyData().Count; i++)
            {
                SpawnBubble(
                    _gameModel.GetEnemyData()[i].initialDirection,
                    _gameModel.GetEnemyData()[i].bubbleSize,
                    _gameModel.GetEnemyData()[i].color,
                    _gameModel.GetEnemyData()[i].startingPosition);
            }
        }

        /// <summary>
        /// Spawn the bubble by using a factory or reusing one from the memory pool and setting it with all the needed information
        /// </summary>
        private void SpawnBubble(MoveDirectionType moveDirectionType, BubbleSizeType bubbleSizeType, Color32 color, Vector2 position, float bounceHeight = 0f)
        {
            IEnemyView view = _enemyViewFactory.CreateIEnemyView();
            var id = view.GetId();
            view.SetPosition(position);
            var size = _gameModel.GetSizeFromData(bubbleSizeType);
            view.SetScale(size);
            view.SetColor(color);
            view.SetVelocity(bounceHeight);
            view.SetFreezeZorAll(false);
            _enemyViews.Add(id, view);

            var velocity = _gameModel.GetVelocityFromData(bubbleSizeType);
            EnemyModel model = new EnemyModel(this, id, moveDirectionType, bubbleSizeType);
            model.SetVelocity(velocity);
            _enemyModels.Add(id, model);
        }

        /// <summary>
        /// disposes the bublle back to the memory pool while gathering it's data and spawning 2 smaller bubbles bu calling spawn bubble
        /// </summary>
        public void SplitBubble(string id)
        {
            if (!_enemyViews.TryGetValue(id, out IEnemyView view)) return;
            if (!_enemyModels.TryGetValue(id, out EnemyModel model)) return;

            view.SetFirstAnimationFrame();
            var position = view.GetPosition();
            var color = view.GetColor();
            var bubbleSizeType = model.GetBubbleSize();
            var moveDirectionType = model.GetMovementDirection();
            var moveDirectionType2 = model.GetPopReverseDirection(moveDirectionType);

            _enemyViewFactory.ReturnIEnemyViewToPool(view);
            _enemyModels.Remove(id);
            _enemyViews.Remove(id);

            if (bubbleSizeType == BubbleSizeType.None)
            {
                if (_enemyViews.Count == 0)
                    _gameModel.CheckWinCondition();
                return;
            }

            var velocity = _gameModel.GetVelocityFromData(bubbleSizeType);
            model.SetVelocity(velocity);

            var size = _gameModel.GetSizeFromData(bubbleSizeType);
            view.SetScale(size);

            SpawnBubble(moveDirectionType, bubbleSizeType, color, position, 5f);
            SpawnBubble(moveDirectionType2, bubbleSizeType, color, position, 5f);
        }


        /// <summary>
        /// in case of collision with the ground
        /// </summary>
        public void Bounce(string id, float height)
        {
            if (!_enemyViews.TryGetValue(id, out IEnemyView view)) return;

            view.SetVelocity(height);
        }

        // in case a collsion with a weapon
        public void TriggerPopAnimation(string id)
        {
            if (!_enemyViews.TryGetValue(id, out IEnemyView view)) return;

            view.TriggerPopAnimation();
        }

        /// <summary>
        /// stops all views, in cases like menu, etc.
        /// </summary>
        public void SetStopState(bool state)
        {
            foreach (var views in _enemyViews.Values)
            {
                views.StopState(state);
            }
        }

        public int GetMonsterCount()
        {
            return _enemyViews.Count;
        }

        /// <summary>
        /// collision with player
        /// </summary>
        public void LoseCondition()
        {
            _gameModel.gameResult = GameResultType.Lose;
        }
    }
}
